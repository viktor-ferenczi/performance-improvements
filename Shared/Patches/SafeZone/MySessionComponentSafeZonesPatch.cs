using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Sandbox.Game.Entities;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using VRage.Game.Entity;
using VRage.Game.ObjectBuilders.Components;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MySessionComponentSafeZones))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    public static class MySessionComponentSafeZonesPatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixSafeAction;
            Config.PropertyChanged += OnConfigChanged;
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            enabled = Config.Enabled && Config.FixSafeAction;
            if (!enabled)
            {
                Cache.Clear();
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Update()
        {
            Cache.Cleanup();
        }

        private static readonly UintCache<long> Cache = new UintCache<long>(113 * 60, 128);

#if DEBUG
        public static string CacheReport => Cache.Report;
#endif

        [HarmonyPrefix]
        [HarmonyPatch("IsActionAllowedForSafezone", typeof(MyEntity), typeof(MySafeZoneAction), typeof(long))]
        [EnsureCode("a8373d73")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsActionAllowedForSafezonePrefix(MyEntity entity, MySafeZoneAction action, long sourceEntityId, ref bool __result, ref long __state)
        {
            if (!enabled)
                return true;

            if (entity == null)
                return false;

            var entityId = entity.EntityId;
            var key = entityId ^ sourceEntityId ^ (long)action;
            if (Cache.TryGetValue(key, out var value))
            {
                // In case of very rare key collision just run the original
                value ^= (uint)entityId;
                if (value > 1)
                    return true;

                __result = value != 0;
                return false;
            }

            __state = key;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch("IsActionAllowedForSafezone", typeof(MyEntity), typeof(MySafeZoneAction), typeof(long))]
        [EnsureCode("a8373d73")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void IsActionAllowedForSafezonePostfix(MyEntity entity, bool __result, long __state)
        {
            if (__state == 0)
                return;

            var entityIdLow32Bits = (uint)entity.EntityId;
            Cache.Store(__state, (__result ? 1u : 0u) ^ entityIdLow32Bits, 120u + (entityIdLow32Bits & 15));
        }
    }
}