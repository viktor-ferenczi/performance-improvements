#if TORCH || DEDICATED

using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Sandbox.Game.Entities.Blocks;
using Sandbox.Game.Multiplayer;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [HarmonyPatch(typeof(MyWheel))]
    public static class MyWheelPatch
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyPrefix]
        [HarmonyPatch("CheckTrail")]
        [EnsureCode("b3d278e9")]
        private static bool CheckTrailPrefix()
        {
            if (!Sync.IsDedicated)
                return true;

            return !Config.FixWheelTrail;
        }
    }
}

#endif