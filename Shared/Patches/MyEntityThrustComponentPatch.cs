using System;
using System.Threading;
using HarmonyLib;
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
        private static readonly ThreadLocal<Random> Rng = new ThreadLocal<Random>();

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("RecomputeThrustParameters")]
        private static bool RecomputeThrustParametersPrefix()
        {
            if (MyCharacterJetpackComponentPatch.IsInTurnOnJetpack)
                return true;

            if (!Config.Enabled || !Config.FixThrusters)
                return true;

            var rng = Rng.Value ?? (Rng.Value = new Random());
            return rng.Next() % 60 == 0;
        }
    }
}