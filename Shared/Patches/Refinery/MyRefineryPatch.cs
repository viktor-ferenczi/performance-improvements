using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using HarmonyLib;
using Sandbox.Definitions;
using Sandbox.Game.Entities.Cube;
using Shared.Config;
using Shared.Logging;
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
        private static IPluginLogger Log => Common.Logger;

        private static readonly ThreadLocal<List<KeyValuePair<int, MyBlueprintDefinitionBase>>> Pool = new ThreadLocal<List<KeyValuePair<int, MyBlueprintDefinitionBase>>>();

        [HarmonyPrefix]
        [HarmonyPatch("RebuildQueue")]
        [EnsureCode("fcdf9f6e")]
        private static bool RebuildQueuePrefix(MyRefinery __instance, out bool ___m_queueNeedsRebuild, MyRefineryDefinition ___m_refineryDef)
        {
            // Pooled list instances, separate once per thread, so calling this method concurrently is safe
            var tmpSortedBlueprints = Pool.Value;
            if (tmpSortedBlueprints == null)
            {
                tmpSortedBlueprints = new List<KeyValuePair<int, MyBlueprintDefinitionBase>>(64);
                Pool.Value = tmpSortedBlueprints;
            }

            // Code of this method is copied from MyRefinery.RebuildQueue, then modified to work as a prefix patch
            ___m_queueNeedsRebuild = false;
            __instance.ClearQueue(false);
            var array = __instance.InputInventory.GetItems().ToArray();
            var blueprintClasses = ___m_refineryDef.BlueprintClasses;
            for (var key = 0; key < array.Length; ++key)
            {
                foreach (var classDefinition in blueprintClasses)
                {
                    foreach (var blueprintDefinitionBase in classDefinition)
                    {
                        var flag = false;
                        var content = array[key].Content;
                        var other = new MyDefinitionId(content.TypeId, content.SubtypeId);
                        var prerequisites = blueprintDefinitionBase.Prerequisites;
                        for (var index2 = 0; index2 < prerequisites.Length; ++index2)
                        {
                            if (prerequisites[index2].Id.Equals(other))
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                            continue;

                        tmpSortedBlueprints.Add(new KeyValuePair<int, MyBlueprintDefinitionBase>(key, blueprintDefinitionBase));
                    }
                }
            }

            // Detect faulty logic, where more than 1 item added to the tmpSortedBlueprints for one or more items in array
            if (tmpSortedBlueprints.Count > array.Length)
            {
                Log.Warning($"RebuildQueuePrefix: tmpSortedBlueprints.Count > array.Length; Refinery: {__instance.DebugName}");
                Log.Warning($"RebuildQueuePrefix: tmpSortedBlueprints.Count = {tmpSortedBlueprints.Count}");
                Log.Warning($"RebuildQueuePrefix: array.Length = {array.Length}");

                for (var i = 0; i < tmpSortedBlueprints.Count; i++)
                {
                    var p = tmpSortedBlueprints[i];
                    Log.Warning($"RebuildQueuePrefix: tmpSortedBlueprints[{i}] = ({p.Key}, {p.Value})");
                }

                for (var i = 0; i < array.Length; i++)
                {
                    Log.Warning($"RebuildQueuePrefix: array[{i}] = {array[i].ToString()}");
                }

                // Ignoring the second loop, so it does not crash
                tmpSortedBlueprints.Clear();
                return false;
            }

            for (var index = 0; index < tmpSortedBlueprints.Count; ++index)
            {
                var blueprint = tmpSortedBlueprints[index].Value;
                var myFixedPoint = MyFixedPoint.MaxValue;
                foreach (var prerequisite in blueprint.Prerequisites)
                {
                    var amount = array[index].Amount;
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

            tmpSortedBlueprints.Clear();
            return false;
        }
    }
}