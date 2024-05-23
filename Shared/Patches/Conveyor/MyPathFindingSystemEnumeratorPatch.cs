#if DEBUG

// Patching generics does not work anymore
#if DISABLED

using HarmonyLib;
using Sandbox.Game.GameSystems.Conveyors;
using Shared.Tools;
using VRage.Algorithms;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyPathFindingSystem<IMyConveyorEndpoint>.Enumerator))]
    public static class MyPathFindingSystemEnumeratorPatch
    {
        private static readonly ConveyorStat Stat = new ConveyorStat();
        public static string Report(int period) => Stat.CountReport(period);

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
            Stat.CountCall();
            return true;
        }
    }
}

#endif
#endif
