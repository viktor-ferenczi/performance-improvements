using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Sandbox.Game.Weapons;
using Sandbox.Game.Weapons.Guns.Barrels;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
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

        // Triggering OnEndShoot on the server and all clients once the turret becomes non-functional
        [HarmonyPostfix]
        [HarmonyPatch("OnStopWorking")]
        [EnsureCode("11a9bafc")]
        private static void OnStopWorkingPostfix(MyLargeTurretBase __instance)
        {
            if (!Config.Enabled || !Config.FixEndShoot)
                return;

            if (Sandbox.Game.Multiplayer.Sync.IsServer && __instance.IsShooting)
                __instance.EndShoot(MyShootActionEnum.PrimaryAction);
        }

        // Client side crash protection, just in case the server would not have the fix
        [HarmonyPrefix]
        [HarmonyPatch("UpdateShooting")]
        [EnsureCode("3d95e342")]
        private static bool UpdateShootingPrefix(MyLargeTurretBase __instance, MyLargeBarrelBase ___m_barrel)
        {
            if (!Config.Enabled || !Config.FixEndShoot)
                return true;

            return __instance != null && ___m_barrel != null && !__instance.Closed && !__instance.MarkedForClose;
        }
    }
}