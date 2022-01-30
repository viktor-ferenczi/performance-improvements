using HarmonyLib;
using System.Threading;
using ParallelTasks;

namespace ClientPlugin.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MySpinWait))]
    public static class MySpinWaitPatch
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MySpinWait.SpinOnce))]
        private static bool SpinOncePrefix(ref int ___m_count)
        {
            if (___m_count < 10)
            {
                ___m_count++;
                return false;
            }

            if (!Thread.Yield())
                Thread.Sleep(1);

            return false;
        }
    }
}