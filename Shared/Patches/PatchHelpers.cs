using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Shared.Logging;
using Shared.Tools;

// #define LIST_TYPES

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

#if DEBUG
#if LIST_TYPES
            log.Info("All types:");
            foreach (var typ in AccessTools.AllTypes())
            {
                log.Info(typ.FullName);
            }
#endif
#endif

            log.Debug("Scanning for conflicting code changes");
            try
            {
                var codeChanges = EnsureCode.Verify().ToList();
                if (codeChanges.Count != 0)
                {
                    log.Critical("Detected conflicting code changes:");
                    foreach (var codeChange in codeChanges)
                        log.Info(codeChange.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                log.Critical(ex, "Failed to scan for conflicting code changes");
                return false;
            }

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
            MySafeZonePatch.Configure();
            MyLargeTurretTargetingSystemPatch.Configure();

            // FIXME: Make this configurable!
            // PhysicsFixes.SetClusterSize(3000f);
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