using System.Reflection;
using System.Threading;
using HarmonyLib;

namespace ClientPlugin.Patches
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

        // Replace 95% of stats updates with 1ms sleeps
        // ReSharper disable once UnusedMember.Local
        private static bool Prefix()
        {
            if (counter-- == 0)
            {
                counter = 20;
                return true;
            }

            Thread.Sleep(1);
            return false;
        }
    }
}