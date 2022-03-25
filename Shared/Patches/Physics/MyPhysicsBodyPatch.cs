using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using Sandbox.Engine.Physics;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyPhysicsBody))]
    public static class MyPhysicsBodyPatch
    {
        private static IPluginConfig Config => Common.Config;

        // These patches need restart to be enabled/disabled
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixPhysics;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MyPhysicsBody.RigidBody), MethodType.Getter)]
        [EnsureCode("fc347f5d")]
        private static IEnumerable<CodeInstruction> RigidBodyGetterTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var i = il.FindIndex(ci => ci.opcode == OpCodes.Brtrue_S);
            var parentIsNotNullLabel = (Label)il[i].operand;
            il.Insert(i++, new CodeInstruction(OpCodes.Dup));
            il.Insert(i + 1, new CodeInstruction(OpCodes.Pop));

            var j = il.FindIndex(ci => ci.labels.Contains(parentIsNotNullLabel));
            il.RemoveRange(j, 3);
            il[j].labels.Add(parentIsNotNullLabel);

            il.RecordPatchedCode();
            return il;
        }
    }
}