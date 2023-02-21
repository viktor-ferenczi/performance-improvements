using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Sandbox.Common.ObjectBuilders;
using Sandbox.Game.Entities;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using TorchPlugin.Shared.Tools;
using VRage.Game.Entity;
using VRage.Game.ModAPI.Ingame;
using VRage.Game.ObjectBuilders.Components;
using VRageMath;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MySafeZone))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public static class MySafeZonePatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixSafeZone;
            Config.PropertyChanged += OnConfigChanged;
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            enabled = Config.Enabled && Config.FixSafeZone;
            if (!enabled)
            {
                IsSafeCache.Clear();
                IsActionAllowedCache.Clear();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Update()
        {
            IsSafeCache.Cleanup();
            IsActionAllowedCache.Cleanup();
        }

        #region "IsSafe fix, see: https://support.keenswh.com/spaceengineers/pc/topic/24146-performance-mysafezone-issafe-is-called-frequently-but-not-cached"

        private static readonly UintCache<long> IsSafeCache = new UintCache<long>(139 * 60, 256);

#if DEBUG
        public static string IsSafeCacheReport => IsSafeCache.Report;
#endif

        [HarmonyPrefix]
        [HarmonyPatch("IsSafe")]
        [EnsureCode("98164fe2")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsSafePrefix(MyEntity entity, ref bool __result, ref bool __state)
        {
            if (!enabled)
                return true;

            if (IsSafeCache.TryGetValue(entity.EntityId, out var value))
            {
                __result = value != 0;
                return false;
            }

            __state = true;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch("IsSafe")]
        [EnsureCode("98164fe2")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void IsSafePostfix(MyEntity entity, bool __result, bool __state)
        {
            if (!__state)
                return;

            var entityId = entity.EntityId;
            IsSafeCache.Store(entityId, __result ? 1u : 0u, 120u + (uint)(entityId & 15));
        }

        #endregion

        #region "RemoveEntityPhantom fix, see: https://support.keenswh.com/spaceengineers/pc/topic/24149-safezone-m_removeentityphantomtasklist-hashset-corruption-due-to-race-condition"

        [HarmonyTranspiler]
        [HarmonyPatch("RemoveEntityPhantom")]
        [EnsureCode("55db36e5")]
        private static IEnumerable<CodeInstruction> RemoveEntityPhantomTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled && !Common.BetaVersion)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var i = il.FindIndex(ci => ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi.Name.Contains("Contains"));
            il[i] = new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MySafeZonePatch), nameof(PhantomTaskListContains)));

            // Lock around the Add call
            var j = il.FindIndex(ci => ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi.Name.Contains("Add"));
            il[j] = new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MySafeZonePatch), nameof(PhantomTaskListAdd)));

            il.RecordPatchedCode();
            return il;
        }

        // Class and method name copied from RemoveEntityPhantom IL code, the class name may change on game updates:
        // IL_0106: ldftn      System.Void Sandbox.Game.Entities.<>c__DisplayClass105_0::<RemoveEntityPhantom>b__0()
        [HarmonyTranspiler]
        [HarmonyPatch("Sandbox.Game.Entities.MySafeZone+<>c__DisplayClass105_0", "<RemoveEntityPhantom>b__0")] 
        [EnsureCode("b39ccae8")]
        private static IEnumerable<CodeInstruction> RemoveEntityPhantomLambdaTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled && !Common.BetaVersion)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            // Lock around the Remove call
            var i = il.FindIndex(ci => ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi.Name.Contains("Remove"));
            il[i] = new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MySafeZonePatch), nameof(PhantomTaskListRemove)));

            il.RecordPatchedCode();
            return il;
        }

        private static bool PhantomTaskListContains(HashSet<IMyEntity> map, IMyEntity item)
        {
            lock(map)
                return map.Contains(item);
        }

        private static bool PhantomTaskListAdd(HashSet<IMyEntity> map, IMyEntity item)
        {
            lock (map)
                return map.Add(item);
        }

        private static bool PhantomTaskListRemove(HashSet<IMyEntity> map, IMyEntity item)
        {
            lock (map)
                return map.Remove(item);
        }

        #endregion

        #region "IsOutside fix"

        [HarmonyPrefix]
        [HarmonyPatch("IsOutside", typeof(BoundingBoxD))]
        [EnsureCode("0d1a5386")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsOutsidePrefix(MySafeZone __instance, BoundingBoxD aabb, ref bool __result)
        {
            var d2 = (aabb.Center - __instance.PositionComp.GetPosition()).LengthSquared();
            if (__instance.Shape == MySafeZoneShape.Sphere)
            {
                var s = __instance.Radius + aabb.HalfExtents.Length();
                if (d2 > s * s)
                {
                    __result = true;
                    return false;
                }
            }
            else
            {
                var s = __instance.PositionComp.LocalAABB.HalfExtents.Length() + aabb.HalfExtents.Length();
                if (d2 > s * s)
                {
                    __result = true;
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region "IsActionAllowed fix"

        private static readonly UintCache<long> IsActionAllowedCache = new UintCache<long>(27 * 60);

#if DEBUG
        public static string IsActionAllowedCacheReport => IsActionAllowedCache.Report;
#endif

        [HarmonyPrefix]
        [HarmonyPatch("IsActionAllowed", typeof(MyEntity), typeof(MySafeZoneAction), typeof(long))]
        [EnsureCode("3d352c69")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsActionAllowedPrefix(MySafeZone __instance, MyEntity entity, MySafeZoneAction action, long sourceEntityId, ref bool __result, ref long __state)
        {
            if (!enabled)
                return true;

            if (entity == null)
                return false;

            if (!__instance.Enabled)
            {
                __result = true;
                return false;
            }

            var entityId = entity.EntityId;
            var key = __instance.EntityId ^ entityId ^ sourceEntityId ^ (long)action;
            if (IsActionAllowedCache.TryGetValue(key, out var value))
            {
                // In case of very rare key collision just run the original
                value ^= (uint)entityId;
                if (value > 1)
                    return true;

                __result = value != 0;
                return false;
            }

            __state = key;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch("IsActionAllowed", typeof(MyEntity), typeof(MySafeZoneAction), typeof(long))]
        [EnsureCode("3d352c69")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void IsActionAllowedPostfix(MyEntity entity, bool __result, long __state)
        {
            if (__state == 0)
                return;

            var entityIdLow32Bits = (uint)entity.EntityId;
            IsActionAllowedCache.Store(__state, (__result ? 1u : 0u) ^ entityIdLow32Bits, 120u + (entityIdLow32Bits & 15));
        }

        #endregion
    }
}