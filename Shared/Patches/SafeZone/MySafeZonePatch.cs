using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Sandbox.Game.Entities;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using VRage.Game.Entity;
using VRage.Game.ModAPI.Ingame;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MySafeZone))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class MySafeZonePatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        static MySafeZonePatch()
        {
            Configure();
            Config.PropertyChanged += OnConfigChanged;
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            Configure();
        }

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixSafeZone;

            if (!enabled)
                cache.Clear();
        }

        #region "IsSafe fix, see: https://support.keenswh.com/spaceengineers/pc/topic/24146-performance-mysafezone-issafe-is-called-frequently-but-not-cached"

        private class CachedResult
        {
            public long Expires;
            public bool Result;
        }

        private static readonly ConcurrentDictionary<long, CachedResult> cache = new ConcurrentDictionary<long, CachedResult>();
        private const long AverageExpiration = 120; // ticks
        private const long CleanupPeriod = 300 * 60; // ticks

        private const int MaxDeleteCount = 128;
        private static readonly long[] KeysToDelete = new long[MaxDeleteCount];

        private static long tick;

        public static void Clean()
        {
            if (!enabled)
                return;

            tick = Common.Plugin.Tick;
            if (tick % CleanupPeriod != 0)
                return;

            var count = 0;
            foreach (var (entityId, cachedResult) in cache)
            {
                if (cachedResult.Expires >= tick)
                    continue;

                KeysToDelete[count++] = entityId;
                if (count == MaxDeleteCount)
                    break;
            }

            for (var i = 0; i < count; i++)
                cache.Remove(KeysToDelete[i]);
        }

        [HarmonyPrefix]
        [HarmonyPatch("IsSafe")]
        [EnsureCode("98164fe2")]
        // ReSharper disable once UnusedMember.Local
        private static bool IsSafePrefix(MyEntity entity, ref bool __result)
        {
            if (!enabled)
                return true;

            if (cache.TryGetValue(entity.EntityId, out var cachedResult) && cachedResult.Expires >= tick)
            {
                __result = cachedResult.Result;
                return false;
            }

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch("IsSafe")]
        [EnsureCode("98164fe2")]
        // ReSharper disable once UnusedMember.Local
        private static void IsSafePostfix(MyEntity entity, bool __result)
        {
            if (!enabled)
                return;

            var entityId = entity.EntityId;
            var expires = tick + AverageExpiration + (entityId & 7) - 4;

            if (cache.TryGetValue(entityId, out var cachedResult))
            {
                cachedResult.Expires = expires;
                cachedResult.Result = __result;
                return;
            }

            cache[entityId] = new CachedResult { Expires = expires, Result = __result };
        }

        #endregion

        #region "RemoveEntityPhantom fix, see: https://support.keenswh.com/spaceengineers/pc/topic/24149-safezone-m_removeentityphantomtasklist-hashset-corruption-due-to-race-condition"

        [HarmonyTranspiler]
        [HarmonyPatch("RemoveEntityPhantom")]
        [EnsureCode("55db36e5")]
        // ReSharper disable once UnusedMember.Local
        private static IEnumerable<CodeInstruction> RemoveEntityPhantomTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();

            var i = il.FindIndex(ci => ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi.Name.Contains("Contains"));
            il[i] = new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MySafeZonePatch), nameof(PhantomTaskListContains)));

            // Lock around the Add call
            var j = il.FindIndex(ci => ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi.Name.Contains("Add"));
            il[j] = new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MySafeZonePatch), nameof(PhantomTaskListAdd)));

            return il.AsEnumerable();
        }

        [HarmonyTranspiler]
        [HarmonyPatch("Sandbox.Game.Entities.MySafeZone+<>c__DisplayClass103_0", "<RemoveEntityPhantom>b__0")]
        [EnsureCode("b39ccae8")]
        // ReSharper disable once UnusedMember.Local
        private static IEnumerable<CodeInstruction> RemoveEntityPhantomClosureTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();

            // Lock around the Remove call
            var i = il.FindIndex(ci => ci.opcode == OpCodes.Callvirt && ci.operand is MethodInfo mi && mi.Name.Contains("Remove"));
            il[i] = new CodeInstruction(OpCodes.Call, AccessTools.DeclaredMethod(typeof(MySafeZonePatch), nameof(PhantomTaskListRemove)));

            return il.AsEnumerable();
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
    }
}