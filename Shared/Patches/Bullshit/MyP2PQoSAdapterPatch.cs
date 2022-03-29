using System.Threading;
using HarmonyLib;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch]
    public static class MyP2PQoSAdapterPatch
    {
        private static IPluginConfig Config => Common.Config;

        private static int counter = -5 * 60;

        // Replaces most of stats updates with sleeps
        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("VRage.EOS.MyP2PQoSAdapter", "UpdateStats")]
        [EnsureCode("033ad607")]
        private static bool UpdateStatsPrefix()
        {
            if (counter < 0 || !Config.Enabled || !Config.FixP2PUpdateStats)
                return true;

            if (counter++ > 47)
            {
                counter = 0;
                return true;
            }

            Thread.Sleep(1);
            return false;
        }
    }
}