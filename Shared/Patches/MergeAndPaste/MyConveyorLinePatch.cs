using System.Runtime.CompilerServices;
using HarmonyLib;
using Sandbox.Game.GameSystems.Conveyors;
using Shared.Tools;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyConveyorLine))]
    public static class MyConveyorLinePatch
    {
        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyConveyorLine.UpdateIsWorking))]
        [EnsureCode("6a58a31c")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool UpdateIsWorkingPrefix()
        {
            // For better performance the configuration is done in MyCubeGridPatch.MergeGridInternalPrefix
            // This is because UpdateIsWorking is called frequently, so we should not recall the plugin config here.
            return !MyCubeGridPatchForMergeAndPaste.IsInMergeGridInternal;
        }
    }
}