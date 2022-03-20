using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace Shared.Patches
{
    public static class TranspilerHelpers
    {
        public delegate bool OpcodePredicate(OpCode opcode);
        public delegate bool FieldInfoPredicate(FieldInfo fi);

        public static FieldInfo GetField(this List<CodeInstruction> il, FieldInfoPredicate predicate)
        {
            var ci = il.Find(i => (i.opcode == OpCodes.Ldfld || i.opcode == OpCodes.Stfld) && i.operand is FieldInfo fi && predicate(fi));
            if (ci == null)
                throw new CodeInstructionNotFound("No code instruction found loading or storing a field matching the given predicate");

            return (FieldInfo)ci.operand;
        }

        public static Label GetLabel(this List<CodeInstruction> il, OpcodePredicate predicate)
        {
            var ci = il.Find(i => i.operand is Label && predicate(i.opcode));
            if (ci == null)
                throw new CodeInstructionNotFound("No label found matching the opcode predicate");

            return (Label)ci.operand;
        }

        public static void RemoveFieldInitialization(this List<CodeInstruction> il, string name)
        {
            var i = il.FindIndex(ci => ci.opcode == OpCodes.Stfld && ci.operand is FieldInfo fi && fi.Name.Contains(name));
            if (i < 2)
                throw new CodeInstructionNotFound($"No code instruction found initializing field: {name}");

            Debug.Assert(il[i - 2].opcode == OpCodes.Ldarg_0);
            Debug.Assert(il[i - 1].opcode == OpCodes.Newobj);

            il.RemoveRange(i-2, 3);
        }
    }

    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public class CodeInstructionNotFound : Exception
    {
        public CodeInstructionNotFound()
        {
        }

        public CodeInstructionNotFound(string message)
            : base(message)
        {
        }

        public CodeInstructionNotFound(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}