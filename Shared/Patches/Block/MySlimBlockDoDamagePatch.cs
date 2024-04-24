using System.Collections.Generic;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Cube;
using Shared.Plugin;
using VRage.Game.ModAPI;
using VRage.Utils;

namespace Shared.Patches.Grid
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MySlimBlock))]
    public static class MySlimBlockDoDamagePatch
    {
        private static readonly Dictionary<long, int> DamageCounters = new Dictionary<long, int>();

        // ReSharper disable once InconsistentNaming
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once SuggestBaseTypeForParameter
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MySlimBlock.DoDamage), typeof(float), typeof(MyStringHash), typeof(MyHitInfo?), typeof(bool), typeof(long))]
        private static bool DoDamagePrefix(MySlimBlock __instance, ref float damage, long attackerId)
        {
            var grid = __instance.CubeGrid;
            if (grid == null)
                return false;

            if (!MyEntities.TryGetEntityById(attackerId, out var attacker))
                return true;

            // if (!(attacker is MyVoxelBase))
            //     return true;

            var key = grid.EntityId ^ __instance.Position.X ^ (long) __instance.Position.Y << 21 ^ (long) __instance.Position.Z << 42;

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