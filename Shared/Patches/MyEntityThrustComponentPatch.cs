using HarmonyLib;
using Sandbox.Game.GameSystems;
using Sandbox.Game.World;
using Shared.Config;
using Shared.Plugin;
using VRageMath;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyEntityThrustComponent))]
    public static class MyEntityThrustComponentPatch
    {
        private const float Threshold = 0.01f;

        private static IPluginConfig Config => Common.Config;

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("set_ControlThrust")]
        private static bool SetControlThrustPrefix(Vector3 value, Vector3 ___m_controlThrust)
        {
            if (!Config.Enabled || !Config.FixThrusters)
                return true;

            if (Vector3.IsZero(value) && !Vector3.IsZero(___m_controlThrust))
                return true;

            return (value - ___m_controlThrust).AbsMax() >= Threshold;
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("set_AutoPilotControlThrust")]
        private static bool AutoPilotControlThrustPrefix(Vector3 value, Vector3 ___m_autoPilotControlThrust)
        {
            if (!Config.Enabled || !Config.FixThrusters)
                return true;

            if (Vector3.IsZero(value) && !Vector3.IsZero(___m_autoPilotControlThrust))
                return true;

            return (value - ___m_autoPilotControlThrust).AbsMax() >= Threshold;
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        [HarmonyPostfix]
        [HarmonyPatch("RecalculatePlanetaryInfluence")]
        private static void RecalculatePlanetaryInfluencePostfix(ref int ___m_nextPlanetaryInfluenceRecalculation)
        {
            ___m_nextPlanetaryInfluenceRecalculation = MySession.Static.GameplayFrameCounter + 100;
        }
    }
}