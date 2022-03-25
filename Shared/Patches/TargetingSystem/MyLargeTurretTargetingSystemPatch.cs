using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Sandbox.Game.Entities.Interfaces;
using Sandbox.Game.Weapons;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using TorchPlugin.Shared.Tools;
using VRage.Game.Entity;
using VRage.Library.Utils;
using VRageMath;

namespace Shared.Patches
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [HarmonyPatch(typeof(MyLargeTurretTargetingSystem))]
    public static class MyLargeTurretTargetingSystemPatch
    {
        private static IPluginConfig Config => Common.Config;

        // These patches need restart to be enabled/disabled
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixTargeting;
        }

        #region Reusing arrays in SortTargetRoots

        // Array cache for each turret targeting system instance keyed by the m_targetReceiver.Entity.EntityId
        private static readonly Cache<MyEntity[]> ArrayCache = new Cache<MyEntity[]>(4 * 3600 * 60, 64);

        public static void Clean()
        {
            if (!enabled)
                return;

            VisibilityCache.Clean();
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch("SortTargetRoots")]
        [EnsureCode("edfb7619")]
        private static IEnumerable<CodeInstruction> SortTargetRootsTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var targetReceiver = il.GetField(fi => fi.Name.Contains("m_targetReceiver"));
            var distanceEntityKeys = il.GetField(fi => fi.Name.Contains("m_distanceEntityKeys"));

            // Instead of creating a new array to return every time try to reuse the previously used one, also resize m_distanceEntityKeys
            var j = il.FindIndex(ci => ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi.Name.Contains("ToArray"));
            il.RemoveAt(j);
            il.Insert(j++, new CodeInstruction(OpCodes.Ldarg_0));
            il.Insert(j++, new CodeInstruction(OpCodes.Ldflda, distanceEntityKeys));
            il.Insert(j++, new CodeInstruction(OpCodes.Ldarg_0));
            il.Insert(j++, new CodeInstruction(OpCodes.Ldfld, targetReceiver));
            il.Insert(j, new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MyLargeTurretTargetingSystemPatch), nameof(CopyEntitiesIntoArray))));

            // Delete the redundant call to EnsureCapacity, since the fix resizes m_distanceEntityKeys as well
            var k = il.FindIndex(ci => ci.opcode == OpCodes.Call && ci.operand is MethodInfo mi && mi.Name.Contains("EnsureCapacity"));
            il.RemoveRange(k - 6, 9);

            il.RecordPatchedCode();
            return il;
        }

        private static MyEntity[] CopyEntitiesIntoArray(List<MyEntity> targetRoots, ref float[] distanceEntityKeys, IMyTargetingReceiver targetReceiver)
        {
            if (targetReceiver == null)
                return Array.Empty<MyEntity>();

            var count = targetRoots.Count;
            if (count == 0)
                return Array.Empty<MyEntity>();

            if (distanceEntityKeys == null || distanceEntityKeys.Length < count)
                distanceEntityKeys = new float[count];

            var targetReceiverEntity = targetReceiver.Entity;
            var entityId = targetReceiverEntity.EntityId;
            if (targetReceiverEntity.Closed)
            {
                ArrayCache.Forget(entityId);
                VisibilityCache.Forget(entityId);
                return Array.Empty<MyEntity>();
            }

            // Do not use ArrayExtensions.EnsureCapacity, because that's copying the existing items on resizing.
            // It would be wasted work, since we overwrite those items anyway.
            // The array is extended if needed or replaced entirely if at least 4 times longer than needed.

            if (!ArrayCache.TryGetValue(entityId, out var array) || array.Length < count || array.Length > count << 2)
            {
                array = new MyEntity[count];
                ArrayCache.Store(entityId, array, 4 * 3600 * 60);
            }

            for (var i = 0; i < count; i++)
                array[i] = targetRoots[i];

            var arrayLength = array.Length;
            for (var i = count; i < arrayLength && array[i] != null; i++)
                array[i] = null;

            return array;
        }

        #endregion

        #region Visibility cache replacement

        // Visibility cache for each turret targeting system instance keyed by the m_targetReceiver.Entity.EntityId
        private static readonly Cache<UintCache> VisibilityCache = new Cache<UintCache>(4 * 3600 * 60, 64);

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, typeof(IMyTargetingReceiver), typeof(MyLargeTurretTargetingSystem.MyTargetingOption))]
        [EnsureCode("8dfc8ab5")]
        private static IEnumerable<CodeInstruction> ConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            // Do not create the ConcurrentDictionary instances, they won't be used
            il.RemoveFieldInitialization("m_notVisibleTargets");
            il.RemoveFieldInitialization("m_lastNotVisibleTargets");
            il.RemoveFieldInitialization("m_visibleTargets");
            il.RemoveFieldInitialization("m_lastVisibleTargets");

            il.RecordPatchedCode();
            return il;
        }

        // NOTE: Issue #18: Do NOT patch UpdateVisibilityCache instead. That patch would sometimes
        // be circumvented and UpdateVisibilityCacheCounters is still called. It is not clear why.
        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("UpdateVisibilityCacheCounters")]
        [EnsureCode("e5cf202d")]
        private static bool UpdateVisibilityCacheCountersPrefix(IMyTargetingReceiver ___m_targetReceiver)
        {
            if (!enabled)
                return true;

            if (___m_targetReceiver == null)
                return false;

            if (!VisibilityCache.TryGetValue(___m_targetReceiver.Entity.EntityId, out var cache))
                return false;

            cache.Clean();
            return false;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch("SetTargetVisible")]
        [EnsureCode("983dff7e")]
        private static bool SetTargetVisiblePrefix(IMyTargetingReceiver ___m_targetReceiver, MyEntity target, bool visible, int? timeout = null)
        {
            if (!enabled)
                return true;

            var entityId = ___m_targetReceiver.Entity.EntityId;
            if (!VisibilityCache.TryGetValue(entityId, out var cache))
            {
                cache = new UintCache(3, 32);
                VisibilityCache.Store(entityId, cache, 4 * 3600 * 60);
            }

            cache.Store(target.EntityId, visible ? 1u : 0u, (uint)(timeout ?? 10 + MyRandom.Instance.Next(5)));
            return false;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch("IsTargetVisible", typeof(MyEntity), typeof(Vector3D?), typeof(bool))]
        [EnsureCode("af0e9b1d")]
        private static IEnumerable<CodeInstruction> IsTargetVisibleTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var continueLabel = il.GetLabel(oc => oc == OpCodes.Ble_S);
            var targetReceiver = AccessTools.DeclaredField(typeof(MyLargeTurretTargetingSystem), "m_targetReceiver");

            var j = il.FindIndex(ci => ci.opcode == OpCodes.Ldfld && ci.operand is FieldInfo fi && fi.Name.Contains("m_notVisibleTargets")) - 1;
            il.RemoveRange(j, 10);
            il.Insert(j++, new CodeInstruction(OpCodes.Ldarg_0));
            il.Insert(j++, new CodeInstruction(OpCodes.Ldfld, targetReceiver));
            il.Insert(j++, new CodeInstruction(OpCodes.Ldarg_1));
            il.Insert(j++, new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MyLargeTurretTargetingSystemPatch), nameof(IsTargetCachedAsNotVisible))));
            il.Insert(j, new CodeInstruction(OpCodes.Brfalse, continueLabel));

            var k = il.FindIndex(ci => ci.opcode == OpCodes.Ldfld && ci.operand is FieldInfo fi && fi.Name.Contains("m_visibleTargets")) - 1;
            il.RemoveRange(k, 5);
            il.Insert(k++, new CodeInstruction(OpCodes.Ldarg_0));
            il.Insert(k++, new CodeInstruction(OpCodes.Ldfld, targetReceiver));
            il.Insert(k++, new CodeInstruction(OpCodes.Ldarg_1));
            il.Insert(k, new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MyLargeTurretTargetingSystemPatch), nameof(IsTargetCachedAsVisible))));

            il.RecordPatchedCode();
            return il;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch("TestPotentialTarget")]
        [EnsureCode("ab1ec6a6")]
        private static IEnumerable<CodeInstruction> TestPotentialTargetTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var continueLabel = il.GetLabel(oc => oc == OpCodes.Ble_S);
            var targetReceiver = AccessTools.DeclaredField(typeof(MyLargeTurretTargetingSystem), "m_targetReceiver");

            var j = il.FindIndex(ci => ci.opcode == OpCodes.Ldfld && ci.operand is FieldInfo fi && fi.Name.Contains("m_notVisibleTargets"));
            Debug.Assert(il[j + 7].opcode == OpCodes.Ble_S && (Label)il[j + 7].operand == continueLabel);
            il.RemoveRange(j, 8);
            il.Insert(j++, new CodeInstruction(OpCodes.Ldfld, targetReceiver));
            il.Insert(j++, new CodeInstruction(OpCodes.Ldarg_1));
            il.Insert(j++, new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MyLargeTurretTargetingSystemPatch), nameof(IsTargetCachedAsNotVisible))));
            il.Insert(j, new CodeInstruction(OpCodes.Brfalse, continueLabel));

            il.RecordPatchedCode();
            return il;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch("TestTarget")]
        [EnsureCode("5061cc1b")]
        private static IEnumerable<CodeInstruction> TestTargetTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var continueLabel = il.GetLabel(oc => oc == OpCodes.Ble_S);
            var targetReceiver = AccessTools.DeclaredField(typeof(MyLargeTurretTargetingSystem), "m_targetReceiver");

            var j = il.FindIndex(ci => ci.opcode == OpCodes.Ldfld && ci.operand is FieldInfo fi && fi.Name.Contains("m_notVisibleTargets"));
            Debug.Assert(il[j + 7].opcode == OpCodes.Ble_S && (Label)il[j + 7].operand == continueLabel);
            il.RemoveRange(j, 8);
            il.Insert(j++, new CodeInstruction(OpCodes.Ldfld, targetReceiver));
            il.Insert(j++, new CodeInstruction(OpCodes.Ldarg_1));
            il.Insert(j++, new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MyLargeTurretTargetingSystemPatch), nameof(IsTargetCachedAsNotVisible))));
            il.Insert(j, new CodeInstruction(OpCodes.Brfalse, continueLabel));

            il.RecordPatchedCode();
            return il;
        }

        private static bool IsTargetCachedAsVisible(IMyTargetingReceiver targetReceiver, MyEntity target)
        {
            if (!VisibilityCache.TryGetValue(targetReceiver.Entity.EntityId, out var cache))
                return false;

            return cache.TryGetValue(target.EntityId, out var value) && value != 0;
        }

        private static bool IsTargetCachedAsNotVisible(IMyTargetingReceiver targetReceiver, MyEntity target)
        {
            if (!VisibilityCache.TryGetValue(targetReceiver.Entity.EntityId, out var cache))
                return false;

            return cache.TryGetValue(target.EntityId, out var value) && value == 0;
        }

        #endregion
    }
}