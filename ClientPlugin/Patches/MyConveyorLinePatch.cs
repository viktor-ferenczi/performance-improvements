#if !DISABLE_USELESS_UPDATES

using HarmonyLib;
using Sandbox.Game.GameSystems.Conveyors;

namespace ClientPlugin.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyConveyorLine))]
    public static class MyConveyorLinePatch
    {
        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyConveyorLine.UpdateIsWorking))]
        private static bool UpdateIsWorkingPrefix()
        {
            return !MyCubeGridPatch.IsMergingInProgress;
        }
    }
}

#endif