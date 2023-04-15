// Caching of MyDefinitionId.ToString() results ported from Performance Improvements 1.10.5

// Reasons:
// https://support.keenswh.com/spaceengineers/pc/topic/27997-servers-deadlocked-on-load
// https://support.keenswh.com/spaceengineers/pc/topic/24210-performance-pre-calculate-or-cache-mydefinitionid-tostring-results

#if DEBUG

// Uncomment this to enable the verification that all formatting actually matches the first one (effectively disables caching)
// #define VERIFY_RESULT

// Uncomment to enable logging statistics
#define LOG_STATS

#endif

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Shared.Config;
using Shared.Logging;
using Shared.Plugin;
using Shared.Tools;
using VRage.Game;

namespace ClientPlugin.Patches
{
    [HarmonyPatch]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class MyDefinitionIdToStringPatch
    {
        private static IPluginLogger Logger => Common.Plugin.Log;
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixMemory;
            Config.PropertyChanged += OnConfigChanged;
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            enabled = Config.Enabled && Config.FixMemory;
            if (!enabled)
                TwoLayerCache.Clear();
        }
        
        private static readonly TwoLayerCache<long, string> TwoLayerCache = new TwoLayerCache<long, string>();
        
        private const long FillPeriod = 17 * 60;  // ticks
        private static long nextFill = FillPeriod;

#if LOG_STATS
        public static string CacheReport => $"L1: {TwoLayerCache.ImmutableReport} | L2: {TwoLayerCache.Report}";
#endif

        public static void Update()
        {
            var tick = Common.Plugin.Tick;
            if (tick < nextFill)
                return;

            nextFill = tick + FillPeriod;
            
#if LOG_STATS
            Logger.Info(CacheReport);
#endif

            TwoLayerCache.FillImmutableCache();
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once RedundantAssignment
        [HarmonyPatch(typeof(MyDefinitionId), nameof(MyDefinitionId.DropToStringCache))]
        [HarmonyPrefix]
        // Patch this unconditionally, I don't trust Keen to fix this properly
        // [EnsureCode("16f436d2")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool DropToStringCachePrefix()
        {
            TwoLayerCache.Clear();

#if LOG_STATS
            Logger.Info("Cache cleared");
#endif
            
            return false;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once RedundantAssignment
        [HarmonyPatch(typeof(MyDefinitionId), nameof(MyDefinitionId.ToString))]
        [HarmonyPrefix]
        // Patch this unconditionally, I don't trust Keen to fix this properly
        //[EnsureCode("f97ce300")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool ToStringPrefix(MyDefinitionId __instance, ref string __result)
        {
            if (TwoLayerCache.TryGetValue(__instance.GetHashCodeLong(), out __result))
            {
#if VERIFY_RESULT
                var expectedName = Format(__instance);
                Debug.Assert(__result == expectedName);
#endif
                return false;
            }

            var result = Format(__instance);
            TwoLayerCache.Store(__instance.GetHashCodeLong(), result);

            __result = result;
            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string Format(MyDefinitionId definitionId)
        {
            const string DefinitionNull = "(null)";
            
            var typeId = definitionId.TypeId;
            var typeName = typeId.IsNull ? DefinitionNull : typeId.ToString();
            
            var subtypeName = definitionId.SubtypeName;
            if (string.IsNullOrEmpty(subtypeName))
            {
                subtypeName = DefinitionNull;
            }

            // Not optimizing the allocation, since the results are cached anyway
            return $"{typeName}/{subtypeName}";
        }
    }
}