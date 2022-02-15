using System.Reflection;
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

        private static int counter;

        // ReSharper disable once UnusedMember.Local
        private static MethodBase TargetMethod()
        {
            var type = AccessTools.TypeByName("VRage.EOS.MyP2PQoSAdapter");
            return AccessTools.Method(type, "UpdateStats");
        }

        // Replaces most of stats updates with sleeps
        // ReSharper disable once UnusedMember.Local
        private static bool Prefix()
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