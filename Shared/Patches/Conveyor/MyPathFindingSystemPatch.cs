#if UNTESTED

using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Sandbox.Game.GameSystems.Conveyors;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using TorchPlugin.Shared.Tools;
using VRage.Algorithms;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyPathFindingSystem<IMyConveyorEndpoint>))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    // ReSharper disable once UnusedType.Global
    public static class MyPathFindingSystemPatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled /*&& Config.FixConveyor*/;
            Config.PropertyChanged += OnConfigChanged;
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            enabled = Config.Enabled /*&& Config.FixConveyor*/;
            if (!enabled)
                Cache.Clear();
        }

        private static readonly UintCache<ulong> Cache = new UintCache<ulong>(329 * 60, 1024);
        private static int NotCached;

#if DEBUG
        public static string CacheReport
        {
            get
            {
                var skipped = NotCached;
                NotCached = 0;
                return $"{Cache.Report}; not cached: {skipped}";
            }
        }
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Update()
        {
            Cache.Cleanup();
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyPathFindingSystem<IMyConveyorEndpoint>.Reachable))]
        [EnsureCode("d804599b")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool ReachablePrefix(IMyConveyorEndpoint from,
            IMyConveyorEndpoint to,
            Predicate<IMyConveyorEndpoint> vertexFilter,
            Predicate<IMyConveyorEndpoint> vertexTraversable,
            Predicate<IMyPathEdge<IMyConveyorEndpoint>> edgeTraversable,
            ref bool __result,
            ref ulong __state)
        {
            if (!enabled)
                return true;

            if (vertexFilter != null || vertexTraversable != null || edgeTraversable != null)
            {
#if DEBUG
                NotCached++;
#endif
                return true;
            }

            var fromKey = (ulong)from.GetHashCode();
            var toKey = (ulong)to.GetHashCode();
            var key = fromKey | (toKey << 32);
            if (Cache.TryGetValue(key, out var value))
            {
                // Call the original in case of cache collisions (very low probability)
                value ^= (uint)fromKey;
                if (value > 1)
                    return true;

                __result = value != 0;
                return false;
            }

            __state = key;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(MyPathFindingSystem<IMyConveyorEndpoint>.Reachable))]
        [EnsureCode("d804599b")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ReachablePostfix(bool __result, ulong __state)
        {
            if (__state == 0)
                return;

            Cache.Store(__state, (__result ? 1u : 0u) ^ (uint)__state, 15 * 60 + ((uint)__state & 255));
        }
    }
}

#endif