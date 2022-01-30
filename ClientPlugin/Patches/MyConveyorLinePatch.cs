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
        [HarmonyPatch("UpdateIsWorking")]
        private static bool UpdateIsWorkingPrefix()
        {
            return MyCubeGridPatch.IsConveyorUpdateEnabled;
        }
    }
}