using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Sandbox.Game.Entities.Cube;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [HarmonyPatch(typeof(MyShipConnector))]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class MyShipConnectorPatchForConveyor
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyPatch("OnConnectionStateChanged")]
        [HarmonyPostfix]
        [EnsureCode("5c9de4df")]
        private static void OnConnectionStateChangedPostfix(MyShipConnector __instance)
        {
            if (Config.FixConveyor)
            {
                var grid = __instance.CubeGrid;
                if (grid != null)
                {
                    MyGridConveyorSystemPatch.DropCache(grid);
                }
            }
        }
    }
}