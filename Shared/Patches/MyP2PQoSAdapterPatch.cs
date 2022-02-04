#if !DISABLE_NETWORK_STATISTICS

using System.Reflection;
using System.Threading;
using HarmonyLib;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch]
    public class MyP2PQoSAdapterPatch
    {
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

#endif