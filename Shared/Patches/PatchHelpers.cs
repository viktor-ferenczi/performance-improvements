// #define LIST_ALL_TYPES

using System;
using System.Linq;
using System.Reflection;
using HarmonyLib;
using Shared.Logging;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    public static class PatchHelpers
    {
        public static bool HarmonyPatchAll(IPluginLogger log, Harmony harmony, bool handleExceptions = true)
        {
#if DEBUG && LIST_ALL_TYPES
            log.Info("All types:");
            foreach (var typ in AccessTools.AllTypes())
            {
                log.Info(typ.FullName);
            }
#endif

            var isOldDotNetFramework = Environment.Version.Major < 5;
            if (isOldDotNetFramework &&
                Common.Plugin.Config.DetectCodeChanges &&
                Environment.GetEnvironmentVariable("SE_PLUGIN_DISABLE_METHOD_VERIFICATION") == null &&
                !WineDetector.IsRunningInWineOrProton())
            {
                log.Debug("Scanning for conflicting code changes");
                var throwOnFailedVerification = !handleExceptions || Environment.GetEnvironmentVariable("SE_PLUGIN_THROW_ON_FAILED_METHOD_VERIFICATION") != null;
                try
                {
                    var codeChanges = EnsureCode.Verify().ToList();
                    if (codeChanges.Count != 0)
                    {
                        log.Critical("Detected conflicting code changes:");
                        foreach (var codeChange in codeChanges)
                            log.Info(codeChange.ToString());
                        
                        if (throwOnFailedVerification)
                        {
                            throw new Exception("Detected conflicting code changes");
                        }
                        
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    log.Error(ex, "Failed to scan for conflicting code changes");
                    
                    if (throwOnFailedVerification)
                    {
                        throw;
                    }
                    
                    return false;
                }
            }
            else
            {
                log.Warning("Conflicting code change detection is disabled in plugin configuration");
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
            MyScriptCompilerPatch.Configure();
            MySafeZonePatch.Configure();
            MySessionComponentSafeZonesPatch.Configure();
            MyPhysicsPatch.Configure();
            MyPhysicsBodyPatch.Configure();
            MyClusterTreePatch.Configure();
            MyCharacterPatch.Configure();
            MyStorageExtensionsPatch.Configure();
            MyWindTurbinePatch.Configure();
            MyDefinitionIdToStringPatch.Configure();
            // MyCubeBlockPatch.Configure();
            // MyTerminalBlockPatch.Configure();
            // MyGridTerminalSystemPatch.Configure();

            // FIXME: Make this configurable!
            // PhysicsFixes.SetClusterSize(3000f);
        }

        // Called on every update
        public static void PatchUpdates()
        {
            MyDefinitionIdToStringPatch.Update();
            MySafeZonePatch.Update();
            MySessionComponentSafeZonesPatch.Update();
            MyWindTurbinePatch.Update();
            MyGridConveyorSystemPatch.Update();
            // MyCubeBlockPatch.Update();
            // MyTerminalBlockPatch.Update();
            // MyGridTerminalSystemPatch.Update();

#if DEBUG
            const int period = 10 * 60; // Ticks
            if (Common.Plugin.Tick % period == 0)
            {
                var log = Common.Plugin.Log;
                log.Info("Cache hit rates:");
                log.Info($"- MySafeZonePatch IsSafe: {MySafeZonePatch.IsSafeCacheReport}");
                log.Info($"- MySafeZonePatch IsActionAllowed: {MySafeZonePatch.IsActionAllowedCacheReport}");
                log.Info($"- MySessionComponentSafeZonesPatch: {MySessionComponentSafeZonesPatch.CacheReport}");
                log.Info($"- MyWindTurbinePatch: {MyWindTurbinePatch.CacheReport}");
                // log.Info($"- MyPathFindingSystemPatch: {MyPathFindingSystemPatch.Report(period)}");
                // log.Info($"- MyPathFindingSystemEnumeratorPatch: {MyPathFindingSystemEnumeratorPatch.Report(period)}");
                log.Info($"- MyGridConveyorSystemPatch: {MyGridConveyorSystemPatch.PullItemReports}");
                foreach (var report in MyGridConveyorSystemPatch.CacheReports)
                {
                    log.Info($"- MyGridConveyorSystemPatch: {report}");
                }
                // log.Info($"- MyLargeTurretTargetingSystemPatch VisibilityCache: {MyLargeTurretTargetingSystemPatch.VisibilityCacheReport}");
                // log.Info($"- MyCubeBlockPatch: {MyCubeBlockPatch.CacheReport}");
                // log.Info($"- MyTerminalBlockPatch: {MyTerminalBlockPatch.CacheReport}");
                // log.Info($"- MyGridTerminalSystemPatch: {MyGridTerminalSystemPatch.InhibitorReport}");
            }
#endif
        }
    }
}