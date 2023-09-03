using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Sandbox.Engine.Physics;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyPhysics))]
    public static class MyPhysicsPatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixPhysics;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MyPhysics.LoadData))]
        [EnsureCode("2bb5480c")]
        private static IEnumerable<CodeInstruction> LoadDataTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var havokThreadCount = Math.Min(16, Environment.ProcessorCount);
            var i = il.FindIndex(ci => ci.opcode == OpCodes.Stloc_0);
            il.Insert(++i, new CodeInstruction(OpCodes.Ldc_I4, havokThreadCount));
            il.Insert(++i, new CodeInstruction(OpCodes.Stloc_0));

            il.RecordPatchedCode();
            return il;
        }
    }
}