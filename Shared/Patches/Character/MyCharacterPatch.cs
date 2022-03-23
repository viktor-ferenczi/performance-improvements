using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Sandbox.Game.Entities.Character;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches.Entity
{
    [HarmonyPatch(typeof(MyCharacter))]
    public static class MyCharacterPatch
    {
        private static IPluginConfig Config => Common.Config;

        // These patches need restart to be enabled/disabled
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled; // && Config.FixTargeting;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch("RigidBody_ContactPointCallback")]
        [EnsureCode("230531d5")]
        private static IEnumerable<CodeInstruction> RigidBody_ContactPointCallbackTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var otherPhysicsBody = il.GetField(fi => fi.Name.Contains("otherPhysicsBody"));

            var i = il.FindIndex(ci => ci.opcode == OpCodes.Ldfld && ci.operand as FieldInfo == otherPhysicsBody);

            Debug.Assert(il[i + 1].opcode == OpCodes.Brfalse);
            var skipRendering = (Label)il[i + 1].operand;

            Debug.Assert(il[i - 1].opcode == OpCodes.Ldloc_0);
            i--;

            il.Insert(i++, new CodeInstruction(OpCodes.Call, AccessTools.PropertyGetter(typeof(Sandbox.Game.Multiplayer.Sync), "IsDedicated")));
            il.Insert(i, new CodeInstruction(OpCodes.Brtrue, skipRendering));

            il.RecordPatchedCode();
            return il.AsEnumerable();
        }
    }
}