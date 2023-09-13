using System;
using HarmonyLib;
using Sandbox.Game.GameSystems;
using Sandbox.Game.GameSystems.Conveyors;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using VRage.Game;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyGridConveyorSystem))]
    public static class MyGridConveyorSystemPatch
    {
        private const uint VerificationMask = 0xfffffffeU;
        private const int CachedItemLifetime = 5 * 60; // Ticks

        private static IPluginConfig Config => Common.Config;

        // These caches can contain millions of items
        public static readonly UintCache<ulong> ReachablePlayerItemCache = new UintCache<ulong>(217 * 60, 16384, 1048576);
        public static readonly UintCache<ulong> ReachableSimpleCache = new UintCache<ulong>(179 * 60, 16384, 1048576);

        public static void Update()
        {
            ReachablePlayerItemCache.Cleanup();
            ReachableSimpleCache.Cleanup();
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch("Reachable", typeof(IMyConveyorEndpoint), typeof(IMyConveyorEndpoint), typeof(long), typeof(MyDefinitionId), typeof(Predicate<IMyConveyorEndpoint>))]
        [EnsureCode("b3bebe51")]
        private static bool ReachablePlayerItemPrefix(
            IMyConveyorEndpoint source,
            IMyConveyorEndpoint endPoint,
            long playerId,
            MyDefinitionId itemId,
            Predicate<IMyConveyorEndpoint> endpointFilter,
            ref bool __result,
            ref (ulong, uint) __state)
        {
            if (!Config.FixConveyor)
                return true;

            var sourceKey = (ulong)(source?.GetHashCode() ?? 0);
            var key = sourceKey | ((ulong)(endPoint?.GetHashCode() ?? 0) << 32) ^ (ulong)playerId ^ (ulong)itemId.GetHashCode() ^ ((ulong)(endpointFilter?.GetHashCode() ?? 0) << 32);

            if (ReachablePlayerItemCache.TryGetValue(key, out var value) && (value ^ (uint)sourceKey) >> 1 == 0u)
            {
                __result = (value & 1ul) != 0;
                return false;
            }

            __state = (key, (uint)sourceKey);
            return true;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPostfix]
        [HarmonyPatch("Reachable", typeof(IMyConveyorEndpoint), typeof(IMyConveyorEndpoint), typeof(long), typeof(MyDefinitionId), typeof(Predicate<IMyConveyorEndpoint>))]
        [EnsureCode("b3bebe51")]
        private static void ReachablePlayerItemPostfix(
            bool __result,
            ref (ulong, uint) __state)
        {
            if (!Config.FixConveyor)
                return;

            ReachablePlayerItemCache.Store(__state.Item1, (__state.Item2 & VerificationMask) | (__result ? 1u : 0u), CachedItemLifetime);
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

            if (ReachableSimpleCache.TryGetValue(key, out var value) && (value ^ (uint)fromKey) >> 1 == 0u)
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

            ReachableSimpleCache.Store(__state.Item1, (__state.Item2 & VerificationMask) | (__result ? 1u : 0u), CachedItemLifetime);
        }
    }
}