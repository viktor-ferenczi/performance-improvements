using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Sandbox.Game.Entities;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using TorchPlugin.Shared.Tools;
using VRage.Game;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyCubeBlock))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    // ReSharper disable once UnusedType.Global
    public static class MyCubeBlockPatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixAccess;
            Config.PropertyChanged += OnConfigChanged;
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            enabled = Config.Enabled && Config.FixAccess;
            if (!enabled)
                Cache.Clear();
        }

        private static readonly UintCache<long> Cache = new UintCache<long>(397 * 60, 2048);

#if DEBUG
        public static string CacheReport => Cache.Report;
#endif

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Update()
        {
            Cache.Cleanup();
        }

        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyCubeBlock.GetUserRelationToOwner))]
        [EnsureCode("6a33d947")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool GetUserRelationToOwnerPrefix(MyCubeBlock __instance, long identityId, MyRelationsBetweenPlayerAndBlock defaultNoUser, ref MyRelationsBetweenPlayerAndBlock __result, ref long __state)
        {
            if (!enabled)
                return true;

            var key = __instance.EntityId ^ identityId ^ (long)defaultNoUser;
            if (Cache.TryGetValue(key, out var value))
            {
                // Call the original in case of cache collisions (very low probability)
                value ^= (uint)identityId;
                if (value > (uint)MyRelationsBetweenPlayerAndBlock.Friends)
                    return true;

                __result = (MyRelationsBetweenPlayerAndBlock)value;
                return false;
            }

            __state = key;
            return false;
        }

        [HarmonyPostfix]
        [HarmonyPatch(nameof(MyCubeBlock.GetUserRelationToOwner))]
        [EnsureCode("6a33d947")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void GetUserRelationToOwnerPostfix(long identityId, MyRelationsBetweenPlayerAndBlock __result, long __state)
        {
            if (__state == 0)
                return;

            Cache.Store(__state, (uint)__result ^ (uint)identityId, 15 * 60 + ((uint)__state & 255));
        }
    }
}