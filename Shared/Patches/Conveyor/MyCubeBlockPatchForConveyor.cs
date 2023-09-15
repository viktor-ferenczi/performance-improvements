using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.GameSystems.Conveyors;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [HarmonyPatch(typeof(MyCubeBlock))]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class MyCubeBlockPatchForConveyor
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyPatch("ComponentStack_IsFunctionalChanged")]
        [HarmonyPostfix]
        [EnsureCode("30d7b946")]
        private static void ComponentStack_IsFunctionalChangePostfix(MyCubeBlock __instance)
        {
            if (Config.FixConveyor && __instance is IMyConveyorEndpointBlock)
            {
                var grid = __instance.CubeGrid;
                if (grid != null)
                {
                    MyGridConveyorSystemPatch.InvalidateCache(grid);
                }
            }
        }
    }
}