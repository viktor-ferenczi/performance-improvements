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
            {
                lock(Cache)
                    Cache.Clear();
            }
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateEnabled();
        }

        private class CachedResult
        {
            public long Expires;
            public bool Result;
        }

        private static readonly Dictionary<long, CachedResult> Cache = new Dictionary<long, CachedResult>();
        private const long AverageExpiration = 2 * 60; // ticks
        private const long CleanupPeriod = 119 * 60; // ticks

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

            lock (Cache)
            {
                var count = 0;
                foreach (var (entityId, cache) in Cache)
                {
                    if (cache.Expires >= tick)
                        continue;

                    KeysToDelete[count++] = entityId;
                    if (count == MaxDeleteCount)
                        break;
                }

                for (var i = 0; i < count; i++)
                    Cache.Remove(KeysToDelete[i]);
            }
        }

        [HarmonyPrefix]
        [HarmonyPatch("IsSafe")]
        // ReSharper disable once UnusedMember.Local
        private static bool IsSafePrefix(MyEntity entity, ref bool __result)
        {
            if (!enabled)
                return true;

            lock (Cache)
            {
                if (Cache.TryGetValue(entity.EntityId, out var cache) && cache.Expires >= tick)
                {
                    __result = cache.Result;
                    return false;
                }
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
            var expires = tick + AverageExpiration + (entityId & 7) - 4;

            lock (Cache)
            {
                if (Cache.TryGetValue(entityId, out var cache))
                {
                    cache.Expires = expires;
                    cache.Result = __result;
                    return;
                }

                Cache[entityId] = new CachedResult { Expires = expires, Result = __result };
            }
        }
    }
}