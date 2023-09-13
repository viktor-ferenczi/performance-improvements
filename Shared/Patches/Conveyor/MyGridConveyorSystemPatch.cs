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
        private const int CachedItemLifetime = 300; // Frames

        private static IPluginConfig Config => Common.Config;

        public static readonly UintCache<ulong> ReachablePlayerItemCache = new UintCache<ulong>(179 * 60, 2048);
        public static readonly UintCache<ulong> ReachableSimpleCache = new UintCache<ulong>(217 * 60, 2048);

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch("Reachable", typeof(IMyConveyorEndpoint), typeof(IMyConveyorEndpoint), typeof(long), typeof(MyDefinitionId), typeof(Predicate<IMyConveyorEndpoint>))]
        [EnsureCode("xxx")]
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

            var sourceKey = (ulong)source.GetHashCode();
            var key = sourceKey | ((ulong)endPoint.GetHashCode() << 32) ^ (ulong)playerId ^ (ulong)itemId.GetHashCode() ^ ((ulong)endpointFilter.GetHashCode() << 32);

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
        [EnsureCode("xxx")]
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
        [EnsureCode("xxx")]
        private static bool ReachableSimplePrefix(
            IMyConveyorEndpoint from,
            IMyConveyorEndpoint to,
            ref bool __result,
            ref (ulong, uint) __state)
        {
            if (!Config.FixConveyor)
                return true;

            var fromKey = (ulong)from.GetHashCode();
            var key = fromKey | ((ulong)to.GetHashCode() << 32);

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
        [EnsureCode("xxx")]
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