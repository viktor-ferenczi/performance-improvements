using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Sandbox.Game.Multiplayer;
using Shared.Config;
using Shared.Plugin;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyPlayerCollection))]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class MyPlayerCollectionPatch
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyPlayerCollection.SendDirtyBlockLimits))]
        public static bool SendDirtyBlockLimitsPrefix()
        {
            if (!Config.Enabled || !Config.FixBlockLimit)
                return true;

            return Common.Plugin.Tick % 180 == 0;
        }
    }
}