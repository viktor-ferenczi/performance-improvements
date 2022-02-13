#if BROKEN

using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.GameSystems;
using Shared.Config;
using Shared.Plugin;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyEntityThrustComponent))]
    public static class MyEntityThrustComponentPatch
    {
        private static IPluginConfig Config => Common.Config;

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("RecomputeThrustParameters")]
        // ReSharper disable once InconsistentNaming
        private static bool RecomputeThrustParametersPrefix(MyEntityThrustComponent __instance)
        {
            if (MyCharacterJetpackComponentPatch.IsInTurnOnJetpack)
                return true;

            if (!Config.Enabled || !Config.FixThrusters)
                return true;

            if (!(__instance.Entity is MyCubeGrid grid))
                return true;

            return grid.EntityId % 60 == Common.Plugin.Tick % 60;
        }
    }
}

#endif