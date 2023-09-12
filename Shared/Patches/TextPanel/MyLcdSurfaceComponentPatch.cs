#if !CLIENT

using HarmonyLib;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using SpaceEngineers.Game.EntityComponents.Blocks;

namespace Shared.Patches.TextPanel
{
    [HarmonyPatch(typeof(MyLcdSurfaceComponent))]
    public static class MyLcdSurfaceComponentPatch
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyPrefix]
        [HarmonyPatch("UpdateHideableScreenVisibility")]
        [EnsureCode("44504995")]
        private static bool UpdateHideableScreenVisibilityPrefix()
        {
            return !Config.FixTextPanel;
        }
    }
}

#endif