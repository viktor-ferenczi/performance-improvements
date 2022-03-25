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

        private static readonly UintCache<long> Cache = new UintCache<long>(293 * 60, 64);

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

            if (Cache.TryGetValue(__instance.EntityId, out var value))
            {
                __result = value != 0;
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

            var entityId = __instance.EntityId;
            Cache.Store(entityId, __result ? 1u : 0u, (uint)(entityId & 31));
        }
    }
}