using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.GameSystems;
using Shared.Config;
using Shared.Patches.Patching;
using Shared.Plugin;
using VRageMath;

namespace Shared.Patches
{
    [HarmonyPatchKey("FixThrusters", "Grids")]
    // ReSharper disable once UnusedType.Global
    public static class MyEntityThrustComponentPatch
    {
        private static readonly ThreadLocal<Random> Rng = new ThreadLocal<Random>();

        // Latest actioned ControlThrust values
        // XYZ values are between -1..+1
        private const int Count = 1 << 12;
        private const int Mask = Count - 1;
        private static readonly Vector3[] LatestActionedControlThrusts = new Vector3[Count];

        // Save the recalculation below this change threshold (make it configurable if needed)
        private const double ChangeThreshold = 0.01;

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MyEntityThrustComponent),"RecomputeThrustParameters")]
        private static bool RecomputeThrustParametersPrefix()
        {
            // Would the recalculation run normally?
            if (!___m_thrustsChanged)
                return true;

            var rng = Rng.Value ?? (Rng.Value = new Random());
            return rng.Next() % 60 == 0;
        }
    }
}