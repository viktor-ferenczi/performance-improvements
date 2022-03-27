using System.ComponentModel;
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
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixWindTurbine;
        }

        static MyWindTurbinePatch()
        {
            Config.PropertyChanged += OnConfigChanged;
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            Configure();

            if (!enabled)
                Cache.Clear();
        }

        private static readonly UintCache<long> Cache = new UintCache<long>(111 * 60);

#if DEBUG
        public static string CacheReport => Cache.Report;
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clean()
        {
            Cache.Clean();
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("IsInAtmosphere", MethodType.Getter)]
        [EnsureCode("641370cb")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsInAtmosphereGetterPrefix(MyWindTurbine __instance, ref bool __result, ref bool __state)
        {
            if (!enabled)
                return true;

            if (Cache.TryGetValue(__instance.EntityId, out var value))
            {
                __result = value != 0;
                return false;
            }

            __state = true;
            return true;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPostfix]
        [HarmonyPatch("IsInAtmosphere", MethodType.Getter)]
        [EnsureCode("641370cb")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void IsInAtmosphereGetterPostfix(MyWindTurbine __instance, ref bool __result, bool __state)
        {
            if (!__state)
                return;

            var entityId = __instance.EntityId;
            Cache.Store(entityId, __result ? 1u : 0u, 900u + (uint)(entityId & 63));
        }
    }
}