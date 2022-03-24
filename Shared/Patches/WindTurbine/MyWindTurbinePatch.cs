using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using SpaceEngineers.Game.Entities.Blocks;
using TorchPlugin.Shared.Tools;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyWindTurbine))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class MyWindTurbinePatch
    {
        private static IPluginConfig Config => Common.Config;

        // These patches need restart to be enabled/disabled
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled; // && Config.FixTargeting;
        }

        private static readonly BoolCache Cache = new BoolCache(300, 293 * 60, 64);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clean()
        {
            Cache.Clean();
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("IsInAtmosphere", MethodType.Getter)]
        [EnsureCode("641370cb")]
        private static bool IsInAtmosphereGetterPrefix(MyWindTurbine __instance, ref bool __result)
        {
            if (!enabled)
                return true;

            if (Cache.TryGetValue(__instance.EntityId, out var result))
            {
                __result = result;
                return false;
            }

            return true;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPostfix]
        [HarmonyPatch("IsInAtmosphere", MethodType.Getter)]
        [EnsureCode("641370cb")]
        private static void IsInAtmosphereGetterPostfix(MyWindTurbine __instance, ref bool __result)
        {
            if (!enabled)
                return;

            Cache.Store(__instance.EntityId, __result);
        }
    }
}