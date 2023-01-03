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
        public static bool HarmonyPatchAll(IPluginLogger log, Harmony harmony)
        {
#if DEBUG && LIST_ALL_TYPES
            log.Info("All types:");
            foreach (var typ in AccessTools.AllTypes())
            {
                log.Info(typ.FullName);
            }
#endif

            if (Common.Plugin.Config.DetectCodeChanges)
            {
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
            } else {
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
            MySafeZonePatch.Configure();
            MySessionComponentSafeZonesPatch.Configure();
            MyLargeTurretTargetingSystemPatch.Configure();
            MyPhysicsBodyPatch.Configure();
            HkShapePatch.Configure();
            MyEntityPatch.Configure();
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

        // Called after patching is done
        public static void PatchInits()
        {
            MyScriptCompilerPatch.Init();
        }

        // Called on every update
        public static void PatchUpdates()
        {
            MySafeZonePatch.Update();
            MySessionComponentSafeZonesPatch.Update();
            MyLargeTurretTargetingSystemPatch.Update();
            MyWindTurbinePatch.Update();
            MyDefinitionIdToStringPatch.Update();
            // MyCubeBlockPatch.Update();
            // MyTerminalBlockPatch.Update();
            // MyGridTerminalSystemPatch.Update();

#if DEBUG
            if (Common.Plugin.Tick % 1200 == 0)
            {
                var log = Common.Plugin.Log;
                log.Info("Cache hit rates:");
                log.Info($"- MySafeZonePatch IsSafe: {MySafeZonePatch.IsSafeCacheReport}");
                log.Info($"- MySafeZonePatch IsActionAllowed: {MySafeZonePatch.IsActionAllowedCacheReport}");
                log.Info($"- MySessionComponentSafeZonesPatch: {MySessionComponentSafeZonesPatch.CacheReport}");
                log.Info($"- MyLargeTurretTargetingSystemPatch ArrayCache: {MyLargeTurretTargetingSystemPatch.ArrayCacheReport}");
                // log.Info($"- MyLargeTurretTargetingSystemPatch VisibilityCache: {MyLargeTurretTargetingSystemPatch.VisibilityCacheReport}");
                log.Info($"- MyWindTurbinePatch: {MyWindTurbinePatch.CacheReport}");
                log.Info($"- MyDefinitionIdToStringPatch: {MyDefinitionIdToStringPatch.CacheReport}");
                // log.Info($"- MyCubeBlockPatch: {MyCubeBlockPatch.CacheReport}");
                // log.Info($"- MyTerminalBlockPatch: {MyTerminalBlockPatch.CacheReport}");
                // log.Info($"- MyGridTerminalSystemPatch: {MyGridTerminalSystemPatch.InhibitorReport}");
            }
#endif
        }
    }
}