using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using HarmonyLib;
using Sandbox.Game;
using Sandbox.Game.Entities;
using Sandbox.Game.GameSystems;
using Sandbox.Game.GameSystems.Conveyors;
using Shared.Config;
using Shared.Logging;
using Shared.Plugin;
using Shared.Tools;
using VRage;
using VRage.Game;
using TLogicalGroup = VRage.Groups.MyGroups<Sandbox.Game.Entities.MyCubeGrid, Sandbox.Game.Entities.MyGridLogicalGroupData>.Group;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyGridConveyorSystem))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public static class MyGridConveyorSystemPatch
    {
        private static IPluginLogger Log => Common.Logger;
        private static IPluginConfig Config => Common.Config;

        private static readonly RwLockDictionary<TLogicalGroup, UintCache<ulong>> ReachableCaches = new RwLockDictionary<TLogicalGroup, UintCache<ulong>>();

#if DEBUG
        public static IEnumerable<string> CacheReports => ReachableCaches.Values.Select(c => c.Report);
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UintCache<ulong> CreateCache()
        {
            return new UintCache<ulong>(999999999, 16);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Update()
        {
            if (!Config.FixConveyor && ReachableCaches.Count != 0)
            {
                // Drop all caches on turning OFF this fix
                ReachableCaches.BeginWriting();
                ReachableCaches.Clear();
                ReachableCaches.FinishWriting();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvalidateCache(MyCubeGrid grid)
        {
            if (grid == null)
            {
                return;
            }

            var group = MyCubeGridGroups.Static.Logical.GetGroup(grid);
            if (group == null)
            {
                return;
            }

            ReachableCaches.BeginReading();
            var cache = ReachableCaches.GetValueOrDefault(group);
            ReachableCaches.FinishReading();

            cache?.Clear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DropCache(MyCubeGrid grid)
        {
            if (grid == null)
            {
                return;
            }

            var group = MyCubeGridGroups.Static.Logical.GetGroup(grid);
            if (group == null)
            {
                return;
            }

            ReachableCaches.BeginWriting();
            ReachableCaches.Remove(group);
            ReachableCaches.FinishWriting();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static UintCache<ulong> GetCache(IMyConveyorEndpoint from, IMyConveyorEndpoint to)
        {
            var fromGrid = from?.CubeBlock?.CubeGrid;
            var toGrid = to?.CubeBlock?.CubeGrid;

            if (fromGrid == null || toGrid == null)
            {
                return null;
            }

            var group = MyCubeGridGroups.Static.Logical.GetGroup(fromGrid);
            var toGroup = MyCubeGridGroups.Static.Logical.GetGroup(toGrid);
            if (group != toGroup)
            {
                return null;
            }

            ReachableCaches.BeginReading();
            var cache = ReachableCaches.GetValueOrDefault(group);
            ReachableCaches.FinishReading();

            if (cache == null)
            {
                ReachableCaches.BeginWriting();
                cache = ReachableCaches.GetValueOrDefault(group);
                if (cache == null)
                {
                    cache = CreateCache();
                    ReachableCaches[group] = cache;
                }
                ReachableCaches.FinishWriting();
            }

            return cache;
        }

        [HarmonyPrefix]
        [HarmonyPatch("Reachable", typeof(IMyConveyorEndpoint), typeof(IMyConveyorEndpoint))]
        [EnsureCode("73bf029a")]
        private static bool ReachablePrefix(
            IMyConveyorEndpoint from,
            IMyConveyorEndpoint to,
            ref bool __result,
            ref (UintCache<ulong>, ulong) __state)
        {
            if (!Config.FixConveyor)
            {
                __state = (null, 0);
                return true;
            }

            try
            {
                var cache = GetCache(from, to);
                if (cache == null)
                {
                    __result = false;
                    return false;
                }

                var key = (ulong)(from?.CubeBlock.EntityId ?? 0) ^ (ulong)(to?.CubeBlock.EntityId ?? 0);

                if (cache.TryGetValue(key, out var value))
                {
                    __result = value != 0;
                    return false;
                }

                __state = (cache, key);
            } catch (ArgumentNullException)
            {
                Log.Warning("Safely suppressed a crash in MyGridConveyorSystemPatch.ReachablePrefix (from={0}, to={1})", from?.CubeBlock.EntityId ?? 0, to?.CubeBlock.EntityId ?? 0);
                __state = (null, 0);
            }

            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch("Reachable", typeof(IMyConveyorEndpoint), typeof(IMyConveyorEndpoint))]
        [EnsureCode("73bf029a")]
        private static void ReachablePostfix(
            bool __result,
            ref (UintCache<ulong>, ulong) __state)
        {
            __state.Item1?.Store(__state.Item2, __result ? 1u : 0u, 999999999);
        }

        [HarmonyPrefix]
        [HarmonyPatch("Reachable", typeof(IMyConveyorEndpoint), typeof(IMyConveyorEndpoint), typeof(long), typeof(MyDefinitionId), typeof(Predicate<IMyConveyorEndpoint>))]
        [EnsureCode("b3bebe51")]
        private static bool ReachableByPlayerPrefix(
            IMyConveyorEndpoint source,
            IMyConveyorEndpoint endPoint,
            ref bool __result)
        {
            if (!Config.FixConveyor)
                return true;

            try
            {
                var cache = GetCache(source, endPoint);
                if (cache == null)
                {
                    __result = false;
                    return false;
                }

                var key = (ulong)(source?.CubeBlock.EntityId ?? 0) ^ (ulong)(endPoint?.CubeBlock.EntityId ?? 0);

                if (cache.TryGetValue(key, out var value) && value == 0)
                {
                    // We already know that this pair of endpoints are not reachable,
                    // therefore we can skip the rest of the check regardless
                    // of the value of the rest of the parameters
                    __result = false;
                    return false;
                }
            } catch (ArgumentNullException)
            {
                Log.Warning("Safely suppressed a crash in MyGridConveyorSystemPatch.ReachableByPlayerPrefix (source={0}, endPoint={1})", source?.CubeBlock.EntityId ?? 0, endPoint?.CubeBlock.EntityId ?? 0);
            }

            return true;
        }

#if DEBUG
        // PullItem and PullItems have been optimized very well by the calcImmediately flag,
        // therefore they don't need to be optimized further for regular cases. If you see
        // huge counts reported here with some test worlds, then we may still go into this.

        private static readonly PullItemStats Stats = new PullItemStats();

        public static string PullItemReports => Stats.Report();

        // Destination inventory volume fill factor stored before invoking PullItems for change detection
        private static readonly ThreadLocal<float> VolumeFillFactor = new ThreadLocal<float>(() => -1f);

        [HarmonyPatch("PullItem", typeof(MyDefinitionId), typeof(MyFixedPoint?), typeof(IMyConveyorEndpointBlock), typeof(MyInventory), typeof(bool), typeof(bool))]
        [HarmonyPrefix]
        [EnsureCode("fc85b648")]
        private static bool PullItemPrefix(
            MyGridConveyorSystem __instance,
            MyDefinitionId itemId,
            IMyConveyorEndpointBlock start,
            ref MyFixedPoint __result)
        {
            if (!Config.Enabled)
                return true;

            Stats.PullItemCount++;

            // __result = MyFixedPoint.Zero;
            // Stats.PullItemMuted++;

            return true;
        }

        [HarmonyPatch("PullItem", typeof(MyDefinitionId), typeof(MyFixedPoint?), typeof(IMyConveyorEndpointBlock), typeof(MyInventory), typeof(bool), typeof(bool))]
        [HarmonyPostfix]
        [EnsureCode("fc85b648")]
        private static void PullItemPostfix(
            ref MyFixedPoint __result
        )
        {
            if (!Config.Enabled)
                return;
        }

        [HarmonyPatch("PullItems")]
        [HarmonyPrefix]
        [EnsureCode("877cc74b")]
        private static bool PullItemsPrefix(
            MyGridConveyorSystem __instance,
            MyInventoryConstraint inventoryConstraint,
            IMyConveyorEndpointBlock start,
            MyInventory destinationInventory,
            ref MyFixedPoint __result)
        {
            if (!Config.Enabled)
                return true;

            Stats.PullItemsCount++;

            // __result = MyFixedPoint.Zero;
            // Stats.PullItemsMuted++;

            return true;
        }

        [HarmonyPatch("PullItems")]
        [HarmonyPostfix]
        [EnsureCode("877cc74b")]
        private static void PullItemsPostfix(
            ref MyFixedPoint __result)
        {
            if (!Config.Enabled)
                return;
        }

        /* Old key calculations for caching the above

        private static void GetPullItemTracker(IMyConveyorEndpointBlock start, MyDefinitionId itemId)
        {
            var entityId = (start as IMyFunctionalBlock)?.EntityId ?? 0;
            var itemHash = itemId.GetHashCode();
            var key = entityId ^ itemHash;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void GetPullItemsTracker(IMyConveyorEndpointBlock start, MyInventoryConstraint inventoryConstraint)
        {
            var entityId = (start as IMyFunctionalBlock)?.EntityId ?? 0;
            var constraintHash = inventoryConstraint.Description.GetHashCode();
            var key = entityId ^ constraintHash;
        }
        */

#endif
    }
}