using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using HarmonyLib;
using Sandbox.Definitions;
using Shared.Config;
using Shared.Logging;
using Shared.Plugin;
using Shared.Tools;
using VRage.Game;

/* Avoid double lookup. It is a transpiler to access the private member efficiently.

  public MyBlueprintDefinitionBase GetBlueprintDefinition(MyDefinitionId blueprintId)
  {
    return !this.m_definitions.m_blueprintsById.ContainsKey(blueprintId) ? (MyBlueprintDefinitionBase) null : this.m_definitions.m_blueprintsById[blueprintId];
  }

  =>

  public MyBlueprintDefinitionBase GetBlueprintDefinition(MyDefinitionId blueprintId)
  {
    return this.m_definitions.m_blueprintsById.GetValueOrDefault(blueprintId);
  }

 */

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyDefinitionManager))]
    // ReSharper disable once UnusedType.Global
    public static class MyDefinitionManagerPatch
    {
        private static IPluginLogger Logger => Common.Plugin.Log;
        private static IPluginConfig Config => Common.Config;

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MyDefinitionManager.GetBlueprintDefinition))]
        [EnsureCode("c4168dc7")]
        // ReSharper disable once UnusedMember.Local
        private static IEnumerable<CodeInstruction> GetBlueprintDefinitionTranspiler(
            IEnumerable<CodeInstruction> instructions)
        {
            if (!Config.Enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var i = il.FindIndex(ci => ci.opcode == OpCodes.Ldarg_1);
            il.RemoveRange(i + 1, il.Count - i - 1);

            var getValueOrDefault =
                AccessTools.Method(typeof(Dictionary<MyDefinitionId, MyDefinitionBase>), "GetValueOrDefault");
            il.Add(new CodeInstruction(OpCodes.Call, getValueOrDefault));

            il.Add(new CodeInstruction(OpCodes.Ret));

            il.RecordPatchedCode();
            return il;
        }
    }
}