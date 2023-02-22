#if UNTESTED

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.Entities.Cube;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using TorchPlugin.Shared.Tools;
using VRage.Game;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyTerminalBlock))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    // ReSharper disable once UnusedType.Global
    public static class MyTerminalBlockPatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixAccess; // !!!
            Config.PropertyChanged += OnConfigChanged;
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            enabled = Config.Enabled && Config.FixAccess;
            if (!enabled)
                Cache.Clear();
        }

        private static readonly UintCache<long> Cache = new UintCache<long>(317 * 60, 2048);

#if DEBUG
        public static string CacheReport => Cache.Report;
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Update()
        {
            Cache.Cleanup();
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyTerminalBlock.HasPlayerAccessReason))]
        [EnsureCode("1f8f024a")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool HasPlayerAccessReasonPrefix(MyCubeBlock __instance, long identityId, MyRelationsBetweenPlayerAndBlock defaultNoUser, ref MyTerminalBlock.AccessRightsResult __result, ref long __state)
        {
            if (!enabled)
                return true;

            var key = __instance.EntityId ^ identityId ^ (long)defaultNoUser;
            if (Cache.TryGetValue(key, out var value))
            {
                // Call the original in case of a cache collision (very low probability)
                value ^= (uint)identityId;
                if (value > (uint)MyTerminalBlock.AccessRightsResult.None)
                    return true;

                __result = (MyTerminalBlock.AccessRightsResult)value;
                return false;
            }

            __state = key;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(MyTerminalBlock.HasPlayerAccessReason))]
        [EnsureCode("1f8f024a")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void HasPlayerAccessReasonPostfix(long identityId, MyTerminalBlock.AccessRightsResult __result, long __state)
        {
            if (__state == 0)
                return;

            Cache.Store(__state, (uint)__result ^ (uint)identityId, 12 * 60 + ((uint)__state & 511));
        }
    }
}

#endif