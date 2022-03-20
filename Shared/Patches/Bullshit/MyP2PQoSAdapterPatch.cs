using System.Threading;
using HarmonyLib;
using Shared.Config;
using Shared.Plugin;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch]
    public static class MyP2PQoSAdapterPatch
    {
        private static IPluginConfig Config => Common.Config;

        private static int counter = 60;

        // Replaces most of stats updates with sleeps
        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("VRage.EOS.MyP2PQoSAdapter", "UpdateStats")]
        private static bool UpdateStatsPrefix()
        {
            var config = Config;
            if (!config.Enabled || !config.FixP2PUpdateStats)
                return true;

            // The very first call must pass through,
            // otherwise the game crashes where it depends on the output data!
            if (--counter < 0)
            {
                counter = 49;
                return true;
            }

            Thread.Sleep(1);
            return false;
        }
    }
}