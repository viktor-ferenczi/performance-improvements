using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using TorchPlugin.Shared.Tools;
using VRage.Game;

namespace Shared.Patches
{
    [HarmonyPatch]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class MyDefinitionIdToStringPatch
    {
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
                Cache.Clear();
        }

        private static readonly CacheForever<long, string> Cache = new CacheForever<long, string>();
        private static long tick;
        private static long nextFill;
        private const long FillPeriod = 60; // !!! 117 * 60;

#if DEBUG
        public static string CacheReport => $"{Cache.ImmutableReport} | {Cache.Report}";
#endif

        public static void Update()
        {
            if ((tick = Common.Plugin.Tick) < nextFill)
                return;

            nextFill = tick + FillPeriod;

            Cache.FillImmutableCache();
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPatch(typeof(MyDefinitionId), nameof(MyDefinitionId.ToString))]
        [HarmonyPrefix]
        [EnsureCode("b404a007")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool MyDefinitionIdToStringPrefix(MyDefinitionId __instance, ref string __result)
        {
            if (!enabled)
                return true;

            if (Cache.TryGetValue(__instance.GetHashCodeLong(), out __result))
            {
#if DEBUG
                var expectedName = Format(__instance);
                Debug.Assert(__result == expectedName);
#endif
                return false;
            }

            var result = Format(__instance);
            Cache.Store(__instance.GetHashCodeLong(), result);

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
                subtypeName = DefinitionNull;

            var sb = ObjectPools.StringBuilder.Get(typeName.Length + 1 + subtypeName.Length);
            var text = sb.Append(typeName).Append('/').Append(subtypeName).ToString();
            ObjectPools.StringBuilder.Return(sb);

            return text;
        }
    }
}