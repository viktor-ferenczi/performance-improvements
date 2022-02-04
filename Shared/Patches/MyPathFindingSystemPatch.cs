#if DISABLE_PATHFINDING

using HarmonyLib;
using Sandbox.Game.GameSystems.Conveyors;
using VRage.Algorithms;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyPathFindingSystem<IMyConveyorEndpoint>))]
    public static class MyPathFindingSystemPatch
    {
        private static Stats calls;

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyPathFindingSystem<IMyConveyorEndpoint>.Reachable))]
        private static bool Reachable()
        {
            calls.Increment();
            return true;
        }

        public static void LogStats(int period)
        {
            if (Plugin.Tick % period != 0)
                return;

            var seconds = period / 60;

            // There can be some minimal inconsistency, but that's okay for logging purposes
            Plugin.Log.Debug("Reachable {0}", calls.Format(seconds));

            calls.Reset();
        }
    }
}

#endif