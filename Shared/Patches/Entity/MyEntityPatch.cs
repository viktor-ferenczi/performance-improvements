using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using VRage.Game.Entity;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyEntity))]
    public static class MyEntityPatch
    {
        private static IPluginConfig Config => Common.Config;

        // These patches need restart to be enabled/disabled
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixEntity;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MyEntity.InScene), MethodType.Getter)]
        [EnsureCode("60f67776")]
        private static IEnumerable<CodeInstruction> InSceneGetterTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var i = il.FindIndex(ci => ci.opcode == OpCodes.Brfalse_S);
            var renderIsNullLabel = (Label)il[i].operand;
            il.Insert(i++, new CodeInstruction(OpCodes.Dup));
            il.RemoveRange(i + 1, 2);

            var j = il.FindIndex(ci => ci.labels.Contains(renderIsNullLabel));
            il[j].labels.Remove(renderIsNullLabel);
            il.Insert(j, new CodeInstruction(OpCodes.Pop));
            il[j].labels.Add(renderIsNullLabel);

            il.RecordPatchedCode();
            return il;
        }
    }
}