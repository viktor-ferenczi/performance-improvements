using System.Threading;
using HarmonyLib;
using Sandbox.Game.Entities;
using Shared.Config;
using Shared.Patches.Patching;
using Shared.Plugin;

namespace Shared.Patches
{
    [HarmonyPatchKey("FixGridMerge", "Grids")]
    // ReSharper disable once UnusedType.Global
    public static class MyCubeGridPatch
    {
        private static readonly ThreadLocal<int> CallDepth = new ThreadLocal<int>();
        public static bool IsInMergeGridInternal => CallDepth.Value > 0;

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MyCubeGrid), "MergeGridInternal")]
        private static bool MergeGridInternalPrefix()
        {
            CallDepth.Value++;
            return true;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPostfix]
        [HarmonyPatch(typeof(MyCubeGrid), "MergeGridInternal")]
        private static void MergeGridInternalPostfix(MyCubeGrid __instance)
        {
            if (!IsInMergeGridInternal)
                return;
            
            if (--CallDepth.Value > 0)
                return;

            // Update the conveyor system only after the merge is complete
            __instance.GridSystems.ConveyorSystem.FlagForRecomputation();
        }
    }
}