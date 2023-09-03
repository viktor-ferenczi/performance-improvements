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
    [HarmonyPatch]
    public static class MyPhysicsPatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixPhysics;
        }

        // Just a quick and dirty transpiler to set a thread number explicitly in MyPhysics.LoadData:
        // int? havokThreadCount = MyVRage.Platform.System.OptimalHavokThreadCount;

        static IEnumerable<MethodBase> TargetMethods()
        {
            var declaredMethod = AccessTools.DeclaredMethod(typeof(MyPhysics), nameof(MyPhysics.LoadData));
            yield return declaredMethod;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        // [EnsureCode("fc347f5d")]
        private static IEnumerable<CodeInstruction> LoadDataTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            // This PC has a i9-12900HK, which has 6 performance cores
            var havokThreadCount = Math.Min(16, Environment.ProcessorCount);
            var i = il.FindIndex(ci => ci.opcode == OpCodes.Stloc_0);
            il.Insert(++i, new CodeInstruction(OpCodes.Ldc_I4, havokThreadCount));
            il.Insert(++i, new CodeInstruction(OpCodes.Stloc_0));

            il.RecordPatchedCode();
            return il;
        }
    }
}