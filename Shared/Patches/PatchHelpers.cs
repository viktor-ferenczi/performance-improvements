using System;
using System.Reflection;
using HarmonyLib;
using Shared.Logging;
using Shared.Patches.Physics;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    public static class PatchHelpers
    {
        public static bool HarmonyPatchAll(IPluginLogger log, Harmony harmony)
        {
#if DEBUG
            Harmony.DEBUG = true;
#endif

            log.Debug("Applying Harmony patches");
            try
            {
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                log.Critical(ex, "Failed to apply Harmony patches");
                return false;
            }

            return true;
        }

        // Called after loading configuration, but before patching
        public static void Configure()
        {
            MyLargeTurretTargetingSystemPatch.Configure();

            // FIXME: Make this configurable!
            PhysicsFixes.SetClusterSize(3000f);
        }

        // Called after patching is done
        public static void PatchInits()
        {
            MyScriptCompilerPatch.Init();
        }

        // Called on every update
        public static void PatchUpdates()
        {
            MySafeZonePatch.Clean();
            MyLargeTurretTargetingSystemPatch.Clean();
        }
    }
}