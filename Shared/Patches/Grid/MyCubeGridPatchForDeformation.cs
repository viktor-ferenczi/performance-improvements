using System.Collections.Generic;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Cube;
using Shared.Plugin;

namespace Shared.Patches.Grid
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyCubeGrid))]
    public static class MyCubeGridPatchForDeformation
    {
        private static readonly Dictionary<long, int> DamageCounters = new Dictionary<long, int>();

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once SuggestBaseTypeForParameter
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyCubeGrid.ApplyDestructionDeformation))]
        private static bool ApplyDestructionDeformationPrefix(MyCubeGrid __instance, MySlimBlock block, ref float damage, long attackerId)
        {
            if (!MyEntities.TryGetEntityById(attackerId, out var attacker) || !(attacker is MyVoxelBase))
                return true;

            var key = __instance.EntityId ^ block.Position.X ^ (long) block.Position.Y << 21 ^ (long) block.Position.Z << 42;

            int collisionCount;
            lock (DamageCounters)
            {
                collisionCount = DamageCounters[key] = DamageCounters.GetValueOrDefault(key) + 1;
            }

            if (collisionCount >= 30)
            {
                Common.Logger.Info($"Destroyed: {(ulong) key:x8}");
                damage = 1000.0f;
            }

            return true;
        }

        public static void Update()
        {
            if (Common.Plugin.Tick % 60 != 0)
                return;

            lock (DamageCounters)
            {
                DamageCounters.Clear();
            }
        }
    }
}