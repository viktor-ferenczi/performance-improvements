using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Sandbox.Definitions;
using Sandbox.Game.Entities.Cube;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyRefinery))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    // ReSharper disable once UnusedType.Global
    public static class MyRefineryPatch
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyTranspiler]
        [HarmonyPatch("RebuildQueue")]
        [EnsureCode("fcdf9f6e")]
        private static IEnumerable<CodeInstruction> RebuildQueueTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator generator)
        {
            // if (!Config.Enabled || !Config.FixBroadcast)
            //     return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            MoveBlueprintClassesIntoLocalVariable(il, generator);
            MovePrerequisitesIntoLocalVariable(il, generator);

            il.RecordPatchedCode();
            return il;
        }

        private static void MoveBlueprintClassesIntoLocalVariable(List<CodeInstruction> il, ILGenerator generator)
        {
            // Create new local variable
            var blueprintClasses = generator.DeclareLocal(typeof(List<MyBlueprintClassDefinition>));

            // Find where m_refineryDef.BlueprintClasses is retrieved and store it without labels
            var i = il.FindIndex(ci => ci.opcode == OpCodes.Ldfld && ci.operand is FieldInfo fi && fi.Name.Contains("m_refineryDef")) - 1;
            var getBlueprintClasses = il.Skip(i).Take(3).Select(ci => ci.WithoutLabels()).ToList();
            getBlueprintClasses.Add(new CodeInstruction(OpCodes.Stloc_S, blueprintClasses));

            // Store m_refineryDef.BlueprintClasses into blueprintClasses before the outermost for loop
            var j = il.FindIndex(ci => ci.opcode == OpCodes.Stloc_0) + 1;
            il.InsertRange(j, getBlueprintClasses);
            j += getBlueprintClasses.Count;

            // Replace all uses of m_refineryDef.BlueprintClasses with local variable blueprintClasses
            for (;;)
            {
                i = il.FindLastIndex(ci => ci.opcode == OpCodes.Ldfld && ci.operand is FieldInfo fi && fi.Name.Contains("m_refineryDef")) - 1;
                if (i < j)
                    break;

                var labels = il[i].labels;
                il.RemoveRange(i, 3);
                il.Insert(i, new CodeInstruction(OpCodes.Ldloc_S, blueprintClasses));
                il[i].labels = labels;
            }
        }

        private static void MovePrerequisitesIntoLocalVariable(List<CodeInstruction> il, ILGenerator generator)
        {
            // Create new local variable
            var prerequisites = generator.DeclareLocal(typeof(MyBlueprintDefinitionBase.Item[]));

            // Find where blueprintDefinitionBase.Prerequisites is retrieved and store it without labels
            var i = il.FindIndex(ci => ci.opcode == OpCodes.Ldfld && ci.operand is FieldInfo fi && fi.Name.Contains("Prerequisites")) - 1;
            var labels = il[i].labels;
            var getPrerequisites = il.Skip(i).Take(2).Select(ci => ci.WithoutLabels()).ToList();
            getPrerequisites.Add(new CodeInstruction(OpCodes.Stloc_S, prerequisites));
            var blueprintDefinitionBaseLocalIndex = ((LocalBuilder)getPrerequisites[0].operand).LocalIndex;

            // Replace with local variable reference, while keeping the labels
            il.RemoveRange(i, 2);
            il.Insert(i, new CodeInstruction(OpCodes.Ldloc_S, prerequisites));
            il[i].labels = labels;

            // Find where blueprintDefinitionBase is set by the foreach loop
            var j = il.FindIndex(ci => ci.opcode == OpCodes.Stloc_S && ci.operand is LocalBuilder lb && lb.LocalIndex == blueprintDefinitionBaseLocalIndex);

            // Store blueprintDefinitionBase.Prerequisites into local variable
            il.InsertRange(j + 1, getPrerequisites);
        }
    }
}