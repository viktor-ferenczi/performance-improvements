using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Cube;
using Sandbox.Game.GameSystems.Conveyors;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [HarmonyPatch(typeof(MyCubeGrid))]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class MyCubeGridPatchForConveyor
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyPatch("ScheduleDirtyRegion")]
        [HarmonyPrefix]
        [EnsureCode("2d84dd69")]
        private static bool ScheduleDirtyRegionPrefix(MyCubeGrid __instance, bool ___m_dirtyRegionScheduled)
        {
            if (Config.FixConveyor && !___m_dirtyRegionScheduled)
            {
                MyGridConveyorSystemPatch.InvalidateCache(__instance);
            }

            return true;
        }

        [HarmonyPatch("AddCubeBlock")]
        [HarmonyPostfix]
        [EnsureCode("3c5e9e27")]
        private static void AddCubeBlockPostfix(MyCubeGrid __instance, MySlimBlock __result)
        {
            if (Config.FixConveyor && __result?.FatBlock is IMyConveyorEndpointBlock)
            {
                MyGridConveyorSystemPatch.InvalidateCache(__instance);
            }
        }

        [HarmonyPatch("RemoveBlock")]
        [HarmonyPostfix]
        [EnsureCode("e14b1a49")]
        private static void RemoveBlockPostfix(MyCubeGrid __instance, MySlimBlock block)
        {
            if (Config.FixConveyor && block?.FatBlock is IMyConveyorEndpointBlock)
            {
                MyGridConveyorSystemPatch.InvalidateCache(__instance);
            }
        }

        [HarmonyPatch("CreateSplit")]
        [HarmonyPostfix]
        [EnsureCode("d23d5039")]
        private static void CreateSplitPostfix(MyCubeGrid __instance)
        {
            if (Config.FixConveyor)
            {
                MyGridConveyorSystemPatch.DropCache(__instance);
            }
        }

        [HarmonyPatch("MergeGridInternal")]
        [HarmonyPostfix]
        [EnsureCode("ddf218c3")]
        private static void MergeGridInternalPostfix(MyCubeGrid __instance, MyCubeGrid gridToMerge)
        {
            if (Config.FixConveyor)
            {
                MyGridConveyorSystemPatch.DropCache(__instance);
                MyGridConveyorSystemPatch.DropCache(gridToMerge);
            }
        }

        [HarmonyPatch("OnRemovedFromScene")]
        [HarmonyPostfix]
        [EnsureCode("7ff2585f")]
        private static void OnRemovedFromScenePostfix(MyCubeGrid __instance)
        {
            if (Config.FixConveyor)
            {
                MyGridConveyorSystemPatch.DropCache(__instance);
            }
        }

        [HarmonyPatch("BeforeDelete")]
        [HarmonyPostfix]
        [EnsureCode("162ca019")]
        private static void BeforeDeletePostfix(MyCubeGrid __instance)
        {
            if (Config.FixConveyor)
            {
                MyGridConveyorSystemPatch.DropCache(__instance);
            }
        }

        [HarmonyPatch("ChangeGridOwnership")]
        [HarmonyPostfix]
        [EnsureCode("bcc83412")]
        private static void ChangeGridOwnershipPostfix(MyCubeGrid __instance)
        {
            if (Config.FixConveyor)
            {
                MyGridConveyorSystemPatch.DropCache(__instance);
            }
        }
    }
}