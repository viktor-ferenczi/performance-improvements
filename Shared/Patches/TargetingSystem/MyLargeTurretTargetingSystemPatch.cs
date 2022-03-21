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
using Sandbox.Game.World;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using VRage.Game.Entity;
using VRage.Library.Utils;
using VRageMath;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
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

        private class CachedArray
        {
            public long Expires;
            public MyEntity[] Array;
        }

        private const long ArrayCacheAverageExpiration = 10 * 60; // ticks

        // Array cache for each turret targeting system instance keyed by the m_targetReceiver.Entity.EntityId
        private static readonly Dictionary<long, CachedArray> ArrayCache = new Dictionary<long, CachedArray>();

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch("SortTargetRoots")]
        [EnsureCode("edfb7619")]
        private static IEnumerable<CodeInstruction> SortTargetRootsTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();

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

            return il.AsEnumerable();
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
                lock (ArrayCache)
                    ArrayCache.Remove(entityId);
                lock (VisibilityCache)
                    VisibilityCache.Remove(entityId);
                return Array.Empty<MyEntity>();
            }

            lock (ArrayCache)
            {
                if (ArrayCache.TryGetValue(entityId, out var cache))
                {
                    cache.Expires = tick + ArrayCacheAverageExpiration + (entityId & 7) - 4;

                    // Do not use ArrayExtensions.EnsureCapacity, because that's copying the existing items on resizing.
                    // It would be wasted work, since we overwrite those items anyway.
                    // The array is extended if needed or replaced entirely if at least 4 times longer than needed.
                    var arrayLength = cache.Array.Length;
                    if (arrayLength < count || arrayLength >= count * 4)
                    {
                        cache.Array = new MyEntity[count];
                        arrayLength = count;
                    }

                    var array = cache.Array;
                    for (var i = 0; i < count; i++)
                        array[i] = targetRoots[i];

                    // Set all non-null trailing items to null to terminate the loop in the caller and to release former references
                    for (var i = count; i < arrayLength && array[i] != null; i++)
                        array[i] = null;

                    return array;
                }

                var newArray = targetRoots.ToArray();
                ArrayCache[entityId] = new CachedArray
                {
                    Expires = tick + ArrayCacheAverageExpiration + (entityId & 31) - 16,
                    Array = newArray
                };

                return newArray;
            }
        }

        #endregion

        #region Visibility cache replacement

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch(MethodType.Constructor, typeof(IMyTargetingReceiver), typeof(MyLargeTurretTargetingSystem.MyTargetingOption))]
        [EnsureCode("8dfc8ab5")]
        private static IEnumerable<CodeInstruction> ConstructorTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();

            // Do not create the ConcurrentDictionary instances, they won't be used
            il.RemoveFieldInitialization("m_notVisibleTargets");
            il.RemoveFieldInitialization("m_lastNotVisibleTargets");
            il.RemoveFieldInitialization("m_visibleTargets");
            il.RemoveFieldInitialization("m_lastVisibleTargets");

            return il.AsEnumerable();
        }

        // Visibility map for each turret targeting system instance keyed by the m_targetReceiver.Entity.EntityId
        // Sign of value is the visibility, negative is not visible, positive is visible.
        // Absolute value is the simulation tick when the cache entry expires.
        private static readonly Dictionary<long, Dictionary<long, long>> VisibilityCache = new Dictionary<long, Dictionary<long, long>>();
        private const int MaxTargetsToRemove = 128;
        private static readonly long[] TargetsToRemove = new long[MaxTargetsToRemove];

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyLargeTurretTargetingSystem.UpdateVisibilityCache))]
        [EnsureCode("1f353abf")]
        private static bool UpdateVisibilityCachePrefix(IMyTargetingReceiver ___m_targetReceiver, bool ___m_parallelTargetSelectionInProcess)
        {
            if (!enabled)
                return true;

            if (___m_parallelTargetSelectionInProcess)
                return false;

            if (___m_targetReceiver == null)
                return false;

            Dictionary<long, long> cache;
            lock (VisibilityCache)
            {
                if (!VisibilityCache.TryGetValue(___m_targetReceiver.Entity.EntityId, out cache))
                    return false;
            }

            var now = tick;

            lock (cache)
            {
                lock (TargetsToRemove)
                {
                    var count = 0;
                    foreach (var (entityId, expires) in cache)
                    {
                        if (Math.Abs(expires) > now)
                            continue;

                        TargetsToRemove[count++] = entityId;

                        if (count == MaxTargetsToRemove)
                            break;
                    }

                    for (var i = 0; i < count; i++)
                        cache.Remove(TargetsToRemove[i]);
                }
            }

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

            Dictionary<long, long> cache;
            lock (VisibilityCache)
            {
                var receiverEntityId = ___m_targetReceiver.Entity.EntityId;
                if (!VisibilityCache.TryGetValue(receiverEntityId, out cache))
                {
                    cache = new Dictionary<long, long>();
                    VisibilityCache[receiverEntityId] = cache;
                }
            }

            var expires = tick + (timeout ?? 10 + MyRandom.Instance.Next(5));

            if (!visible)
                expires = -expires;

            lock (cache)
            {
                cache[target.EntityId] = expires;
            }

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

            return il.AsEnumerable();
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

            var continueLabel = il.GetLabel(oc => oc == OpCodes.Ble_S);
            var targetReceiver = AccessTools.DeclaredField(typeof(MyLargeTurretTargetingSystem), "m_targetReceiver");

            var j = il.FindIndex(ci => ci.opcode == OpCodes.Ldfld && ci.operand is FieldInfo fi && fi.Name.Contains("m_notVisibleTargets"));
            Debug.Assert(il[j + 7].opcode == OpCodes.Ble_S && (Label)il[j + 7].operand == continueLabel);
            il.RemoveRange(j, 8);
            il.Insert(j++, new CodeInstruction(OpCodes.Ldfld, targetReceiver));
            il.Insert(j++, new CodeInstruction(OpCodes.Ldarg_1));
            il.Insert(j++, new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MyLargeTurretTargetingSystemPatch), nameof(IsTargetCachedAsNotVisible))));
            il.Insert(j, new CodeInstruction(OpCodes.Brfalse, continueLabel));

            return il.AsEnumerable();
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

            var continueLabel = il.GetLabel(oc => oc == OpCodes.Ble_S);
            var targetReceiver = AccessTools.DeclaredField(typeof(MyLargeTurretTargetingSystem), "m_targetReceiver");

            var j = il.FindIndex(ci => ci.opcode == OpCodes.Ldfld && ci.operand is FieldInfo fi && fi.Name.Contains("m_notVisibleTargets"));
            Debug.Assert(il[j + 7].opcode == OpCodes.Ble_S && (Label)il[j + 7].operand == continueLabel);
            il.RemoveRange(j, 8);
            il.Insert(j++, new CodeInstruction(OpCodes.Ldfld, targetReceiver));
            il.Insert(j++, new CodeInstruction(OpCodes.Ldarg_1));
            il.Insert(j++, new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MyLargeTurretTargetingSystemPatch), nameof(IsTargetCachedAsNotVisible))));
            il.Insert(j, new CodeInstruction(OpCodes.Brfalse, continueLabel));

            return il.AsEnumerable();
        }

        private static bool IsTargetCachedAsVisible(IMyTargetingReceiver targetReceiver, MyEntity target)
        {
            Dictionary<long, long> cache;
            lock (VisibilityCache)
            {
                if (!VisibilityCache.TryGetValue(targetReceiver.Entity.EntityId, out cache))
                    return false;
            }

            lock (cache)
            {
                return cache.TryGetValue(target.EntityId, out var expires) && expires > 0;
            }
        }

        private static bool IsTargetCachedAsNotVisible(IMyTargetingReceiver targetReceiver, MyEntity target)
        {
            Dictionary<long, long> cache;
            lock (VisibilityCache)
            {
                if (!VisibilityCache.TryGetValue(targetReceiver.Entity.EntityId, out cache))
                    return false;
            }

            lock (cache)
            {
                return cache.TryGetValue(target.EntityId, out var expires) && expires < 0;
            }
        }

        #endregion

        #region Periodic cleanup

        private const long CleanupPeriod = 77 * 60; // Simulation frames (ticks)
        private const int MaxCacheEntriesToDelete = 128;

        private static readonly long[] CacheEntriesToDelete = new long[MaxCacheEntriesToDelete];

        private static int tick;

        public static void Clean()
        {
            if (!enabled)
                return;

            tick = MySession.Static.GameplayFrameCounter;

            if (tick % CleanupPeriod != 0)
                return;

            var now = tick;

            lock (ArrayCache)
            {
                var count = 0;
                foreach (var (entityId, cachedResult) in ArrayCache)
                {
                    if (cachedResult.Expires >= now)
                        continue;

                    CacheEntriesToDelete[count++] = entityId;
                    if (count == MaxCacheEntriesToDelete)
                        break;
                }

                for (var i = 0; i < count; i++)
                    ArrayCache.Remove(CacheEntriesToDelete[i]);

                lock (VisibilityCache)
                {
                    for (var i = 0; i < count; i++)
                        VisibilityCache.Remove(CacheEntriesToDelete[i]);
                }
            }
        }

        #endregion
    }
}