﻿#if !CLIENT

using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using SpaceEngineers.Game.EntityComponents.Blocks;

namespace Shared.Patches.TextPanel
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [HarmonyPatch(typeof(MyLcdSurfaceComponent))]
    public static class MyLcdSurfaceComponentPatch
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyPrefix]
        [HarmonyPatch("UpdateVisibility")]
        [EnsureCode("3e177a11")]
        private static bool UpdateVisibilityPrefix()
        {
            return !Config.FixTextPanel;
        }
    }
}

#endif