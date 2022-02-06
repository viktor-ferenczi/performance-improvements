#if !DISABLE_MERGE_PASTE_UPDATES

using HarmonyLib;
using Sandbox.Game.GameSystems.Conveyors;

namespace Shared.Patches
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
            // Configuration is done in MyCubeGridPatch.MergeGridInternalPrefix
            return !MyCubeGridPatch.IsMergingInProgress;
        }
    }
}

#endif