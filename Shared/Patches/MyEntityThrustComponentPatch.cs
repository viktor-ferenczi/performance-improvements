using HarmonyLib;
using Sandbox.Game.Entities;
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

        private const int Bits = 8;
        private const int Count = 1 << Bits;
        private const int Mask = Count - 1;

        private static readonly uint[] Entity = new uint[Count];
        private static readonly Vector3[] PreviousControl = new Vector3[Count];
        private static readonly Vector3[] PreviousAutopilot = new Vector3[Count];

        private static IPluginConfig Config => Common.Config;

        public static void Update()
        {
            Entity[Common.Plugin.Tick & Mask] = 0;
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("set_ControlThrust")]
        private static bool SetControlThrustPrefix(MyEntityThrustComponent __instance, Vector3 value, ref Vector3 ___m_controlThrust)
        {
            if (MyShipControllerPatch.IsInUpdateAfterSimulation)
            {
                ___m_controlThrust = Vector3.Zero;
                return false;
            }

            if (!Config.Enabled || !Config.FixThrusters)
                return true;

            if (!(__instance.Entity is MyCubeGrid grid))
                return true;

            var id = (uint)(grid.EntityId >> Bits);
            var hash = grid.EntityId & Mask;
            if (Entity[hash] == 0)
            {
                Entity[hash] = id;
                PreviousControl[hash] = value;
                return true;
            }

            if (Entity[hash] != id)
                return true;

            var previous = PreviousControl[hash];
            if (Vector3.IsZero(value) && !Vector3.IsZero(previous))
            {
                PreviousControl[hash] = value;
                return true;
            }

            if ((value - previous).AbsMax() < Threshold)
            {
                ___m_controlThrust = previous;
                return false;
            }

            PreviousControl[hash] = value;
            return true;
        }

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("set_AutoPilotControlThrust")]
        private static bool AutoPilotControlThrustPrefix(MyEntityThrustComponent __instance, Vector3 value, ref Vector3 ___m_autoPilotControlThrust)
        {
            if (!Config.Enabled || !Config.FixThrusters)
                return true;

            if (!(__instance.Entity is MyCubeGrid grid))
                return true;

            var id = (uint)(grid.EntityId >> Bits);
            var hash = grid.EntityId & Mask;
            if (Entity[hash] == 0)
            {
                Entity[hash] = id;
                PreviousAutopilot[hash] = value;
                return true;
            }

            if (Entity[hash] != id)
                return true;

            var previous = PreviousAutopilot[hash];
            if (Vector3.IsZero(value) && !Vector3.IsZero(previous))
            {
                PreviousAutopilot[hash] = value;
                return true;
            }

            if ((value - previous).AbsMax() < Threshold)
            {
                ___m_autoPilotControlThrust = previous;
                return false;
            }

            PreviousAutopilot[hash] = value;
            return true;
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