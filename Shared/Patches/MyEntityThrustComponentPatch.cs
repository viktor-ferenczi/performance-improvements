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
        private struct GridThrustState
        {
            public long EntityId;
            public Vector3 Control;
            public Vector3 Override;
            public bool HasPower;
            public bool HasThrust;
        }

        private static IPluginConfig Config => Common.Config;

        // Latest actioned thrust values
        private const int Count = 1 << 10;
        private const long Mask = Count - 1;
        private static readonly GridThrustState[] States = new GridThrustState[Count];

        // Save the recalculation below this change threshold (make it configurable if needed)
        private const double ControlThreshold = 0.01;
        private const double OverrideThreshold = 0.001;

        public static void Update()
        {
            // Expire a slot each tick, cleans up old grids in Count ticks
            States[Common.Plugin.Tick & Mask].EntityId = 0;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch("RecomputeThrustParameters")]
        private static bool RecomputeThrustParametersPrefix(MyEntityThrustComponent __instance, ref Vector3 ___m_totalThrustOverride)
        {
            if (!(__instance.Entity is MyCubeGrid grid))
                return true;

            if (!Config.Enabled || !Config.FixThrusters)
                return true;

            // Latest thrust control state is saved based on a hash, which allows for
            // an algorithm which does not allocate any memory.

            // Hash collisions are detected by mismatching EntityId, which disables
            // the optimization for the affected grids. But no functionality is broken.

            // Hash is just the lower bits of EntityId, because it is already random
            var hash = (int)(grid.EntityId & Mask);

            // Hash collision?
            var latest = States[hash];
            if (latest.EntityId != 0 && latest.EntityId != grid.EntityId)
            {
                // Optimize only the first grid with that hash,
                // allow recalculation all the time for the other
                // ones with the same hash
                return true;
            }

            // Current state
            var current = new GridThrustState
            {
                EntityId = grid.EntityId,
                Control = __instance.AutopilotEnabled ? __instance.AutoPilotControlThrust + __instance.ControlThrust : __instance.ControlThrust,
                Override = ___m_totalThrustOverride / Vector3.Max(Vector3.One, __instance.MaxThrustOverride ?? Vector3.One),
                HasPower = __instance.HasPower,
                HasThrust = __instance.HasThrust
            };

            // Allow for recalculation for the first time,
            // once every second to monitor power and fuel availability,
            // on power or fuel change and on zeroing control or override
            if (latest.EntityId == 0 ||
                Common.Plugin.Tick % 60 == grid.EntityId % 60 ||
                latest.HasPower != current.HasPower ||
                latest.HasThrust != current.HasThrust ||
                !Vector3.IsZero(latest.Control) && Vector3.IsZero(current.Control) ||
                !Vector3.IsZero(latest.Override) && Vector3.IsZero(current.Override))
            {
                States[hash] = current;
                return true;
            }

            // Change in control
            var controlChange = current.Control - latest.Control;
            var overrideChange = current.Override - latest.Override;

            // Allow for immediate recalculation above the specified relative change in control or override
            if (controlChange.AbsMax() > ControlThreshold ||
                overrideChange.AbsMax() > OverrideThreshold)
            {
                States[hash] = current;
                return true;
            }

            // Suppress recalculation
            // Suppress the RecomputeThrustParameters calls in the original method
            return false;
        }
    }
}