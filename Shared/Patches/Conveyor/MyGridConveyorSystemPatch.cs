using HarmonyLib;
using Sandbox.Game.GameSystems;
using Sandbox.Game.GameSystems.Conveyors;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyGridConveyorSystem))]
    public static class MyGridConveyorSystemPatch
    {
        private const uint VerificationMask = 0xfffffffeU;
        private const int CachedItemLifetime = 3 * 60; // Ticks

        private static IPluginConfig Config => Common.Config;

        // TODO: Maybe using an array with a collision strategy and accepting some overlap would be faster. Benchmark this idea.
        // These caches can contain millions of items
        public static readonly UintCache<ulong> ReachableCache = new UintCache<ulong>(179 * 60, 65536, 1048576);

        public static void Update()
        {
            ReachableCache.Cleanup();
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch("Reachable", typeof(IMyConveyorEndpoint), typeof(IMyConveyorEndpoint))]
        [EnsureCode("73bf029a")]
        private static bool ReachableSimplePrefix(
            IMyConveyorEndpoint from,
            IMyConveyorEndpoint to,
            ref bool __result,
            ref (ulong, uint) __state)
        {
            if (!Config.FixConveyor)
                return true;

            var fromKey = (ulong)(from?.GetHashCode() ?? 0);
            var key = fromKey | ((ulong)(to?.GetHashCode() ?? 0) << 32);

            if (ReachableCache.TryGetValue(key, out var value) && (value ^ (uint)fromKey) >> 1 == 0u)
            {
                __result = (value & 1ul) != 0;
                return false;
            }

            __state = (key, (uint)fromKey);
            return true;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPostfix]
        [HarmonyPatch("Reachable", typeof(IMyConveyorEndpoint), typeof(IMyConveyorEndpoint))]
        [EnsureCode("73bf029a")]
        private static void ReachableSimplePostfix(
            bool __result,
            ref (ulong, uint) __state)
        {
            if (!Config.FixConveyor)
                return;

            ReachableCache.Store(__state.Item1, (__state.Item2 & VerificationMask) | (__result ? 1u : 0u), CachedItemLifetime);
        }
    }
}