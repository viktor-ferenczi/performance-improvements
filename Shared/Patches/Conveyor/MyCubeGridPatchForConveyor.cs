using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Blocks;
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
        [EnsureCode("a2674a04")]
        private static void MergeGridInternalPostfix(MyCubeGrid __instance, MyCubeGrid gridToMerge)
        {
            if (Config.FixConveyor)
            {
                MyGridConveyorSystemPatch.DropCache(__instance);
                MyGridConveyorSystemPatch.DropCache(gridToMerge);
            }
        }

        [HarmonyPatch("MechanicalConnectionBlockAttachUpdateStatusChanged")]
        [HarmonyPostfix]
        [EnsureCode("bda9b059")]
        private static void MechanicalConnectionBlockAttachUpdateStatusChangedPostfix(MyCubeGrid __instance, MyMechanicalConnectionBlockBase mechConBlock)
        {
            if (Config.FixConveyor)
            {
                MyGridConveyorSystemPatch.DropCache(__instance);
                var topGrid = mechConBlock?.TopGrid;
                if (topGrid != null)
                {
                    MyGridConveyorSystemPatch.DropCache(topGrid);
                }
            }
        }

        [HarmonyPatch("OnAddedToScene")]
        [HarmonyPostfix]
        [EnsureCode("b5021de4")]
        private static void OnAddedToScenePostfix(MyCubeGrid __instance)
        {
            if (Config.FixConveyor)
            {
                MyGridConveyorSystemPatch.InvalidateCache(__instance);
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

        [HarmonyPatch("OnAddedToGroup", typeof(MyGridLogicalGroupData))]
        [HarmonyPostfix]
        [EnsureCode("9ad2772a")]
        private static void OnAddedToGroupPostfix(MyCubeGrid __instance)
        {
            if (Config.FixConveyor)
            {
                MyGridConveyorSystemPatch.InvalidateCache(__instance);
            }
        }

        [HarmonyPatch("OnRemovedFromGroup", typeof(MyGridLogicalGroupData))]
        [HarmonyPostfix]
        [EnsureCode("805f7bc7")]
        private static void OnRemovedFromGroupPostfix(MyCubeGrid __instance)
        {
            if (Config.FixConveyor)
            {
                MyGridConveyorSystemPatch.InvalidateCache(__instance);
            }
        }

        [HarmonyPatch("NotifyBlockAdded")]
        [HarmonyPostfix]
        [EnsureCode("2c18b95e")]
        private static void NotifyBlockAddedPostfix(MyCubeGrid __instance, MySlimBlock block)
        {
            if (Config.FixConveyor && block?.FatBlock is IMyConveyorEndpointBlock)
            {
                MyGridConveyorSystemPatch.InvalidateCache(__instance);
            }
        }

        [HarmonyPatch("NotifyBlockRemoved")]
        [HarmonyPostfix]
        [EnsureCode("d0b27a63")]
        private static void NotifyBlockRemovedPostfix(MyCubeGrid __instance, MySlimBlock block)
        {
            if (Config.FixConveyor && block?.FatBlock is IMyConveyorEndpointBlock)
            {
                MyGridConveyorSystemPatch.InvalidateCache(__instance);
            }
        }

        [HarmonyPatch("NotifyBlockOwnershipChange")]
        [HarmonyPostfix]
        [EnsureCode("19a731ee")]
        private static void NotifyBlockOwnershipChangePostfix(MyCubeGrid __instance)
        {
            // Suboptimal, because the block is not passed in here,
            // therefore it cannot be checked whether it is a conveyor one
            if (Config.FixConveyor)
            {
                MyGridConveyorSystemPatch.InvalidateCache(__instance);
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