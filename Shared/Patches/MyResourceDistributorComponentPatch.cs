using HarmonyLib;
using Sandbox.Game.EntityComponents;
using Shared.Config;
using Shared.Patches.Patching;
using Shared.Plugin;

namespace Shared.Patches
{
    [HarmonyPatchKey("FixGridGroups", "Grids")]
    // ReSharper disable once UnusedType.Global
    public static class MyResourceDistributorComponentPatch
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MyResourceDistributorComponent), nameof(MyResourceDistributorComponent.UpdateBeforeSimulation))]
        private static bool UpdateBeforeSimulation(MyResourceDistributorComponent __instance)
        {
            if (!MyGroupsPatch.IsInMergeGroups && !MyGroupsPatch.IsInBreakLink)
                return true;

            __instance.MarkForUpdate();
            return false;
        }
    }
}