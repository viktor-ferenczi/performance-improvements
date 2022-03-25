using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using Havok;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    [HarmonyPatch]
    public static class HkShapePatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixPhysics;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch("Havok.HkShape+HandleEqualityComparer", "Equals")]
        [EnsureCode("8933ef75")]
        private static IEnumerable<CodeInstruction> HkShapeEqualsTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var handle = AccessTools.DeclaredField(typeof(HkShape), "m_handle");
            var toInt64 = AccessTools.DeclaredMethod(typeof(IntPtr), nameof(IntPtr.ToInt64));

            il = new List<CodeInstruction>
            {
                new CodeInstruction(OpCodes.Ldarg_1),
                new CodeInstruction(OpCodes.Ldfld, handle),
                new CodeInstruction(OpCodes.Call, toInt64),
                new CodeInstruction(OpCodes.Ldarg_2),
                new CodeInstruction(OpCodes.Ldfld, handle),
                new CodeInstruction(OpCodes.Call, toInt64),
                new CodeInstruction(OpCodes.Ceq),
                new CodeInstruction(OpCodes.Ret),
            };

            il.RecordPatchedCode();
            return il;
        }
    }
}