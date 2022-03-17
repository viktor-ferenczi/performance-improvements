using System;
using System.Reflection;
using HarmonyLib;
using Sandbox.Game.Entities;
using Shared.Logging;

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

        public static void PatchInits()
        {
            MyScriptCompilerPatch.Init();
        }

        public static void PatchUpdates()
        {
            MySafeZonePatch.Clean();
        }
    }
}