#if DISABLE_PATHFINDING

using HarmonyLib;
using Sandbox.Game.GameSystems.Conveyors;
using VRage.Algorithms;

namespace ClientPlugin.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyPathFindingSystem<IMyConveyorEndpoint>.Enumerator))]
    public static class MyPathFindingSystemEnumeratorPatch
    {
        private static Stats calls;

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyPathFindingSystem<IMyConveyorEndpoint>.Enumerator.MoveNext))]
        private static bool MoveNextPrefix(
            // object __instance,
            // ref bool __result,
            // ref object ___m_currentVertex,
            // object ___m_parent,
            // object ___m_vertexFilter,
            // object ___m_vertexTraversable,
            // object ___m_edgeTraversable
            )
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
            Plugin.Log.Debug("MoveNext {0}", calls.Format(seconds));

            calls.Reset();
        }
    }
}
#endif