using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.GameSystems;
using Shared.Config;
using Shared.Plugin;
using VRageMath;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyEntityThrustComponent))]
    public static class MyEntityThrustComponentPatch
    {
        private static IPluginConfig Config => Common.Config;

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
        [HarmonyPatch("UpdateBeforeSimulation")]
        private static bool UpdateBeforeSimulationPrefix(MyEntityThrustComponent __instance, ref bool ___m_thrustsChanged)
        {
            // Would the recalculation run normally?
            if (!___m_thrustsChanged)
                return true;

            if (!Config.Enabled || !Config.FixThrusters)
                return true;

            if (!(__instance.Entity is MyCubeGrid grid))
                return true;

            // Latest control thrust is saved up to Count grids based on a hash,
            // which allows for an algorithm which does not allocate any memory.
            // Hash collisions may cause slightly more recalculations, because
            // they overwrite each other's latest control thrust vectors causing
            // large differences all the time. It should happen only rarely.

            // Hash is just the lower bits of EntityId, because it is already random
            var hash = (int)(grid.EntityId & Mask);
            var latestControlThrust = LatestActionedControlThrusts[hash];

            // Always allow for zeroing the control thrust
            var currentControlThrust = __instance.ControlThrust;
            if (currentControlThrust == Vector3.Zero)
            {
                if (latestControlThrust == Vector3.Zero)
                {
                    // Suppress the RecomputeThrustParameters calls in the original method
                    ___m_thrustsChanged = false;
                    return true;
                }

                // Allow calling RecomputeThrustParameters,
                // record that it was called for the zero control vector
                LatestActionedControlThrusts[hash] = Vector3.Zero;
                return true;
            }

            // Suppress the recomputation if the change in the control vector is small
            var change = currentControlThrust - latestControlThrust;
            if (change.AbsMax() < ChangeThreshold)
            {
                // Suppress the RecomputeThrustParameters calls in the original method
                ___m_thrustsChanged = false;
                return true;
            }

            // Allow calling RecomputeThrustParameters,
            // record that it was called for the current control vector
            LatestActionedControlThrusts[hash] = currentControlThrust;
            return true;
        }
    }
}