using HarmonyLib;
using Sandbox.Game.EntityComponents;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyResourceDistributorComponent))]
    public static class MyResourceDistributorComponentPatch
    {
        private static IPluginConfig Config => Common.Config;

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyResourceDistributorComponent.UpdateBeforeSimulation))]
        [EnsureCode("cd93ffd6")]
        private static bool UpdateBeforeSimulation(MyResourceDistributorComponent __instance)
        {
            if (!MyGroupsPatch.IsInMergeGroups && !MyGroupsPatch.IsInBreakLink)
                return true;

            if (!Config.Enabled || !Config.FixGridGroups)
                return true;

            __instance.MarkForUpdate();
            return false;
        }
    }
}