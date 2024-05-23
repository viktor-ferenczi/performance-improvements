#if DEBUG

// Patching generics does not work anymore
#if DISABLED

using HarmonyLib;
using Sandbox.Game.GameSystems.Conveyors;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using VRage.Algorithms;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyPathFindingSystem<IMyConveyorEndpoint>))]
    public static class MyPathFindingSystemPatch
    {
        private static IPluginConfig Config => Common.Config;

        private static readonly ConveyorStat Stat = new ConveyorStat();
        public static string Report(int period) => Stat.FullReport(period);

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyPathFindingSystem<IMyConveyorEndpoint>.Reachable))]
        [EnsureCode("d804599b")]
        private static bool ReachablePrefix()
        {
            Stat.CountCall();
            return true;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPostfix]
        [HarmonyPatch(nameof(MyPathFindingSystem<IMyConveyorEndpoint>.Reachable))]
        [EnsureCode("d804599b")]
        private static void ReachablePostfix(bool __result)
        {
            if (!__result)
            {
                Stat.CountFailure();
            }
        }
    }
}

#endif
#endif