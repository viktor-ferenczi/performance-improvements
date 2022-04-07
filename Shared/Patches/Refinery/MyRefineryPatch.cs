using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Sandbox.Definitions;
using Sandbox.Game.Entities.Cube;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using VRage;
using VRage.Game;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyRefinery))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    // ReSharper disable once UnusedType.Global
    public static class MyRefineryPatch
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyPrefix]
        [HarmonyPatch("RebuildQueue")]
        [EnsureCode("fcdf9f6e")]
        private static bool RebuildQueuePrefix(MyRefinery __instance, out bool ___m_queueNeedsRebuild, List<KeyValuePair<int, MyBlueprintDefinitionBase>> ___m_tmpSortedBlueprints, MyRefineryDefinition ___m_refineryDef)
        {
            ___m_queueNeedsRebuild = false;
            __instance.ClearQueue(false);
            ___m_tmpSortedBlueprints.Clear();
            var array = __instance.InputInventory.GetItems().ToArray();
            var blueprintClasses = ___m_refineryDef.BlueprintClasses;
            for (var key = 0; key < array.Length; ++key)
            {
                for (var index1 = 0; index1 < blueprintClasses.Count; ++index1)
                {
                    foreach (var blueprintDefinitionBase in blueprintClasses[index1])
                    {
                        bool flag = false;
                        MyDefinitionId other = new MyDefinitionId(array[key].Content.TypeId, array[key].Content.SubtypeId);
                        var prerequisites = blueprintDefinitionBase.Prerequisites;
                        for (var index2 = 0; index2 < prerequisites.Length; ++index2)
                        {
                            if (prerequisites[index2].Id.Equals(other))
                            {
                                flag = true;
                                break;
                            }
                        }

                        if (flag)
                        {
                            ___m_tmpSortedBlueprints.Add(new KeyValuePair<int, MyBlueprintDefinitionBase>(key, blueprintDefinitionBase));
                            break;
                        }
                    }
                }
            }

            for (var index = 0; index < ___m_tmpSortedBlueprints.Count; ++index)
            {
                MyBlueprintDefinitionBase blueprint = ___m_tmpSortedBlueprints[index].Value;
                MyFixedPoint myFixedPoint = MyFixedPoint.MaxValue;
                foreach (MyBlueprintDefinitionBase.Item prerequisite in blueprint.Prerequisites)
                {
                    MyFixedPoint amount = array[index].Amount;
                    if (amount == 0)
                    {
                        myFixedPoint = 0;
                        break;
                    }

                    myFixedPoint = MyFixedPoint.Min(amount * (1f / (float)prerequisite.Amount), myFixedPoint);
                }

                if (blueprint.Atomic)
                    myFixedPoint = MyFixedPoint.Floor(myFixedPoint);
                if (myFixedPoint > 0 && myFixedPoint != MyFixedPoint.MaxValue)
                    __instance.InsertQueueItemRequest(-1, blueprint, myFixedPoint);
            }

            ___m_tmpSortedBlueprints.Clear();

            return false;
        }
    }
}