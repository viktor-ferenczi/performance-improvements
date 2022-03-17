using System.Diagnostics.CodeAnalysis;
using System.Threading;
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
        // Must be power of two
#if TORCH || DEDICATED
        private const int Count = 0x2000;
#else
        private const int Count = 0x200;
#endif

        // Mask to get cache index by key
        private const int IndexMask = Count - 1;

        // Clean the whole cache every 128 simulation ticks (~2 seconds)
        private const int CleanCount = Count < 128 ? 1 : Count >> 7;

        // Next cache index to clean
        private static int cleanIndex;

        // Cache to store the results of IsSafe by the lower bits of the random EntityId as hash key.
        // Cache collisions are possible, but they only reduce the cache hit rate.
        private static readonly long[] CacheKeys = new long[Count];
        private static readonly bool[] CacheValues = new bool[Count];

        private static IPluginConfig Config => Common.Config;

        public static void Clean()
        {
            for (var i = 0; i < CleanCount; i++)
                CacheKeys[cleanIndex++] = 0;

            // Enough to mask only after the loop, because
            // both Count and CleanCount are a power of 2
            // and CleanCount <= Count
            cleanIndex &= IndexMask;
        }

        [HarmonyPrefix]
        [HarmonyPatch("IsSafe")]
        // ReSharper disable once UnusedMember.Local
        private static bool IsSafePrefix(MyEntity entity, ref bool __result)
        {
            if (!Config.Enabled || !Config.FixSafeZone)
                return true;

            var key = entity.EntityId;
            var index = (int)(key & IndexMask);
            if (CacheKeys[index] == key)
            {
                // Attempt to return the cached result
                __result = CacheValues[index];

                // Check again for safe lock-less concurrency (assumes no compiler optimization)
                if (CacheKeys[index] == key)
                    return false;
            }
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch("IsSafe")]
        // ReSharper disable once UnusedMember.Local
        private static void IsSafePostfix(MyEntity entity, bool __result)
        {
            if (!Config.Enabled || !Config.FixSafeZone)
                return;

            var key = entity.EntityId;
            var index = (int)(key & IndexMask);

            // Atomically register the key, store the result only if succeeds
            if (Interlocked.CompareExchange(ref CacheKeys[index], key, 0) == 0)
                CacheValues[index] = __result;
        }
    }
}