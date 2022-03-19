using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;

namespace Shared.Patches
{
    public static class TranspilerHelpers
    {
        public delegate bool FieldInfoPredicate(FieldInfo fi);

        public static FieldInfo GetField(this List<CodeInstruction> il, FieldInfoPredicate predicate)
        {
            var ci = il.Find(i => (i.opcode == OpCodes.Ldfld || i.opcode == OpCodes.Stfld) && i.operand is FieldInfo fi && predicate(fi));
            if (ci == null)
                throw new CodeInstructionNotFound("No code instruction found loading or storing a field matching the given predicate");

            return (FieldInfo)ci.operand;
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