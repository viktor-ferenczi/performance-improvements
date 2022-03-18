using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using HarmonyLib;
using Sandbox.Game.Entities;
using Shared.Config;
using Shared.Plugin;
using VRage.Game.Entity;

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
            UpdateEnabled();
            Config.PropertyChanged += OnConfigChanged;
        }

        private static void UpdateEnabled()
        {
            enabled = Config.Enabled && Config.FixSafeZone;

            if (!enabled)
                cache.Clear();
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateEnabled();
        }

        private struct CachedResult
        {
            public long Expires;
            public bool Result;
        }

        private static readonly ConcurrentDictionary<long, CachedResult> cache = new ConcurrentDictionary<long, CachedResult>();
        private const long AverageExpiration = 120;  // ticks
        private const long CleanupPeriod = 300 * 60;  // ticks

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
        // ReSharper disable once UnusedMember.Local
        private static void IsSafePostfix(MyEntity entity, bool __result)
        {
            if (!enabled)
                return;

            var entityId = entity.EntityId;
            cache[entityId] = new CachedResult { Expires = tick + AverageExpiration + (entityId & 7) - 4, Result = __result };
        }
    }
}