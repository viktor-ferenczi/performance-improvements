#if !CLIENT

using HarmonyLib;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches.TextPanel
{
    [HarmonyPatch]
    public static class MyLcdSurfaceComponentPatch
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyPrefix]
        [HarmonyPatch("IsInAtmosphere", MethodType.Getter)]
        [EnsureCode("xxx")]
        private static bool UpdateHideableScreenVisibilityPrefix()
        {
            return !Config.FixTextPanel;
        }
    }
}

#endif