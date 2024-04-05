using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Sandbox.Game.Entities.Cube;
using Shared.Config;
using Shared.Plugin;
using VRage.Game.Entity;

namespace Shared.Patches
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [HarmonyPatch(typeof(MyEntity))]
    public static class MyEntityPatchForProjection
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyPostfix]
        [HarmonyPatch(nameof(MyEntity.OnAddedToScene))]
        private static void OnAddedToScenePostfix(MyEntity __instance)
        {
            /* Disables functional blocks on grids with no physics.
             * These should only be the projected functional blocks.
             *
             * Unfortunately we cannot test functionalBlock.CubeGrid?.Projector
             * here, because that is set after the preview grid is added to the scene.
             * Setting NeedsUpdate to NONE does not work here. Solving it differently
             * would have a constant CPU overhead, which should be avoided.
             *
             * It may have side-effects should a plugin provide physics-less subgrids.
             * In such a case disable this fix and use the Multigrid Projector plugin
             * to fix this specific case only for the welders in a different way.
             */
            if (Config.FixProjection &&
                __instance is MyFunctionalBlock functionalBlock &&
                functionalBlock.CubeGrid?.Physics == null)
            {
                functionalBlock.Enabled = false;
            }
        }
    }
}