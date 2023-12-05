using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using Sandbox;
using Sandbox.Definitions;
using Shared.Config;
using Shared.Logging;
using Shared.Plugin;
using Shared.Tools;
using VRage.Game;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyDefinitionManager))]
    // ReSharper disable once UnusedType.Global
    public static class MyDefinitionManagerPatch
    {
        private static IPluginLogger Logger => Common.Plugin.Log;
        private static IPluginConfig Config => Common.Config;

        private static readonly RateLimiter RateLimiter = new RateLimiter(10);

        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixPhysics;
            Reset();
        }

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MyDefinitionManager.GetBlueprintDefinition))]
        [EnsureCode("0a2a4cf1")]
        private static IEnumerable<CodeInstruction> GetBlueprintDefinitionTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            var il = instructions.ToList();
            il.RecordOriginalCode();

            var i = il.FindIndex(ci => ci.opcode == OpCodes.Ldsfld);
            var j = il.FindIndex(ci => ci.opcode == OpCodes.Ldnull);
            il.RemoveRange(i, j - i);

            il.Insert(i++, new CodeInstruction(OpCodes.Ldarg_1));
            il.Insert(i, new CodeInstruction(OpCodes.Call, AccessTools.Method(typeof(MyDefinitionManagerPatch), nameof(LogWithRateLimit))));

            il.RecordPatchedCode();
            return il;
        }

        private static void LogWithRateLimit(MyDefinitionId blueprintId)
        {
            if (!enabled || RateLimiter.Check())
            {
                MySandboxGame.Log.WriteLine($"No blueprint with Id '{blueprintId}'");
            }
        }

        public static void Update()
        {
            if (!enabled && Common.Plugin.Tick % 3600 == 0)
            {
                Reset();
            }
        }

        private static void Reset()
        {
            var skipped = RateLimiter.Reset();
            if (skipped != 0)
            {
                Logger.Warning($"LogFlooding: Skipped {skipped} 'No blueprint with Id' messages");
            }
        }
    }
}