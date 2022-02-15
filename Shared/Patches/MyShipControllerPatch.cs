using System.Threading;
using HarmonyLib;
using Sandbox.Game.Entities;
using Shared.Config;
using Shared.Plugin;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyShipController))]
    public static class MyShipControllerPatch
    {
        private static IPluginConfig Config => Common.Config;

        private static readonly ThreadLocal<int> CallDepth = new ThreadLocal<int>();
        public static bool IsInUpdateAfterSimulation => CallDepth.Value > 0;

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyShipController.UpdateAfterSimulation))]
        private static bool UpdateAfterSimulationPrefix()
        {
            if (!Config.Enabled || !Config.FixThrusters)
                return true;

            CallDepth.Value++;

            return true;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPostfix]
        [HarmonyPatch(nameof(MyShipController.UpdateAfterSimulation))]
        private static void UpdateAfterSimulationPostfix()
        {
            if (!IsInUpdateAfterSimulation)
                return;

            CallDepth.Value--;
        }
    }    
}