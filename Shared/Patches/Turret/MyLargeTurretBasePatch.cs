using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Sandbox.Game.Weapons;
using Shared.Config;
using Shared.Plugin;
using VRage.Game.ModAPI;

namespace Shared.Patches.Turret
{
    [HarmonyPatch(typeof(MyLargeTurretBase))]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    // ReSharper disable once UnusedType.Global
    public static class MyLargeTurretBasePatch
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyPostfix]
        [HarmonyPatch("OnStopWorking")]
        private static void OnStopWorkingPostfix(MyLargeTurretBase __instance)
        {
            if (!Config.Enabled || !Config.FixEndShoot)
                return;

            if (Sandbox.Game.Multiplayer.Sync.IsServer && __instance.IsShooting)
                __instance.EndShoot(MyShootActionEnum.PrimaryAction);
        }
    }
}