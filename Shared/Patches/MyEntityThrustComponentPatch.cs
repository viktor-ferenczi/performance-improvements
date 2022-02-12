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
        
        private static int count;
        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("RecomputeThrustParameters")]
        private static bool RecomputeThrustParametersPrefix()
        {
            if (MyCharacterJetpackComponentPatch.IsInTurnOnJetpack)
                return true;
            
            if (!Config.Enabled || !Config.FixThrusters)
            {
                return true;
            }
            
            if (count >= 60)
            {
                count = 0;
                return true;
            }
            
            count++;
            return false;
        }
    }
}