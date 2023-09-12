using HarmonyLib;
using Sandbox.Game.GameSystems.Conveyors;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using VRage.Algorithms;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyPathFindingSystem<IMyConveyorEndpoint>.Enumerator))]
    public static class MyPathFindingSystemEnumeratorPatch
    {
        private static IPluginConfig Config => Common.Config;

#if DEBUG
        private static readonly ConveyorStat Stat = new ConveyorStat();
        public static string Report(int period) => Stat.CountReport(period);
#endif

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyPathFindingSystem<IMyConveyorEndpoint>.Enumerator.MoveNext))]
        [EnsureCode("05a0ee2c")]
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
#if DEBUG
            Stat.CountCall();
#endif
            return true;
        }
    }
}