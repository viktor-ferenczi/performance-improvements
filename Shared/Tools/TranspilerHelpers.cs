using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Text;
using HarmonyLib;
using Shared.Tools;

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

        public static string FormatCode(this List<CodeInstruction> il)
        {
            var sb = new StringBuilder();

            var hash = il.HashInstructions().CombineHashCodes().ToString("x8");
            sb.Append($"// {hash}\r\n");

            foreach(var ci in il)
            {
                sb.Append(ci);
                sb.Append("\r\n");
            }

            return sb.ToString();
        }

        public static void RecordOriginalCode(this List<CodeInstruction> il, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
        {
#if DEBUG
            RecordCode(il, callerFilePath, callerMemberName, "original");
#endif
        }

        public static void RecordPatchedCode(this List<CodeInstruction> il, [CallerFilePath] string callerFilePath = "", [CallerMemberName] string callerMemberName = "")
        {
#if DEBUG
            RecordCode(il, callerFilePath, callerMemberName, "patched");
#endif
        }

        private static void RecordCode(List<CodeInstruction> il, string callerFilePath, string callerMemberName, string suffix)
        {
            if (!File.Exists(callerFilePath))
                return;

            var text = il.FormatCode();

            Debug.Assert(callerFilePath.Length > 0);
            var dir = Path.GetDirectoryName(callerFilePath);
            Debug.Assert(dir != null);

            const string expectedMemberNameSuffix = "Transpiler";
            Debug.Assert(callerMemberName.Length > expectedMemberNameSuffix.Length);
            Debug.Assert(callerMemberName.EndsWith(expectedMemberNameSuffix));
            var name = callerMemberName.Substring(0, callerMemberName.Length - expectedMemberNameSuffix.Length);

            var path = Path.Combine(dir, $"{name}.{suffix}.il");

            File.WriteAllText(path, text);
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