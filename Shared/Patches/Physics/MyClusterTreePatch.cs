using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Sandbox.Engine.Voxels;
using Sandbox.Game.Entities;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using VRageMath;
using VRageMath.Spatial;

namespace Shared.Patches
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class MyObjectData
    {
        public ulong Id;
        public MyClusterTree.MyCluster Cluster;
        public MyClusterTree.IMyActivationHandler ActivationHandler;
        public BoundingBoxD AABB;
        public int StaticId;
        public string Tag;
        public long EntityId;
    }

    [HarmonyPatch(typeof(MyClusterTree))]
    public static class MyClusterTreePatch
    {
        private static IPluginConfig Config => Common.Config;
        private static bool enabled;

        private static readonly MethodInfo TargetMethodInfo = AccessTools.DeclaredMethod(typeof(MyClusterTree), nameof(MyClusterTree.ReorderClusters));
        private static readonly MethodInfo NestedLoopMethodInfo = AccessTools.DeclaredMethod(typeof(MyClusterTreePatch), nameof(NestedLoop));
        private static readonly PropertyInfo ResultList = AccessTools.DeclaredProperty("VRageMath.Spatial.MyClusterTree:m_resultList");
        private static readonly Type MyObjectDataType = AccessTools.GetTypesFromAssembly(typeof(MyClusterTree).Assembly).FirstOrDefault(t => t.Name.Contains("MyObjectData", StringComparison.InvariantCulture));
        private static readonly Type HashSetMyObjectDataType = typeof(HashSet<>).MakeGenericType(MyObjectDataType);
        private static readonly Type DictionaryUlongMyObjectDataType = typeof(Dictionary<,>).MakeGenericType(typeof(ulong), MyObjectDataType);

        public static long Counter;

        static MyClusterTreePatch()
        {
            Debug.Assert(TargetMethodInfo != null, "TargetMethodInfo");
            Debug.Assert(NestedLoopMethodInfo != null, "NestedLoopMethodInfo");
            Debug.Assert(ResultList != null, "ResultList");
            Debug.Assert(MyObjectDataType != null, "MyObjectDataType");
            Debug.Assert(HashSetMyObjectDataType != null, "HashSetMyObjectDataType");
            Debug.Assert(DictionaryUlongMyObjectDataType != null, "DictionaryUlongMyObjectDataType");
        }

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixPhysics;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MyClusterTree.ReorderClusters))]
        // [EnsureCode("xxx")]
        private static IEnumerable<CodeInstruction> ReorderClustersTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator gen)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            // Find the insertion point for the replacement code
            var i = il.FindAllIndex(ci => ci.opcode == OpCodes.Ldloc_2)[1];
            while (i < il.Count && il[i].opcode != OpCodes.Pop) i++;
            i++;

            // Remove the original nested loops
            var j = il.FindAllIndex(ci => ci.opcode == OpCodes.Endfinally)[1];
            // for (var k = i; k <= j; k++)
            // {
            //     il[k].opcode = OpCodes.Nop;
            //     il[k].operand = null;
            //     il[k].blocks.Clear();
            // }
            var nop = new CodeInstruction(OpCodes.Nop);
            nop.labels = il.Skip(i).Take(j + 1 - i).Select(ci => ci.labels).SelectMany(l => l).ToList();
            il.RemoveRange(i, j + 1 - i);
            il.Insert(i++, nop);

            // Make m_objectsData available as a local variable
            var resultListGetter = il.FindPropertyGetter("m_resultList");
            var resultListVariable = gen.DeclareLocal(ResultList.PropertyType);
            il.Insert(i++, new CodeInstruction(OpCodes.Call, resultListGetter)); // static MyClusterTree.m_resultList
            il.Insert(i++, new CodeInstruction(OpCodes.Stloc_S, resultListVariable));

            // Make m_objectsData available as a local variable
            var objectsDataField = il.GetField(fi => fi.Name == "m_objectsData");
            var localObjectsVariable = gen.DeclareLocal(objectsDataField.FieldType);
            il.Insert(i++, new CodeInstruction(OpCodes.Ldarg_0)); // this
            il.Insert(i++, new CodeInstruction(OpCodes.Ldfld, objectsDataField)); // this.m_objectsData
            il.Insert(i++, new CodeInstruction(OpCodes.Stloc_S, localObjectsVariable));

            // Insert replacement code from method
            var sourceLocalVariable = TargetMethodInfo.GetMethodBody()?.LocalVariables.First(v => v.LocalIndex == 2) ?? throw new Exception("Cannot find source variable");
            var inflated1LocalVariable = TargetMethodInfo.GetMethodBody()?.LocalVariables.First(v => v.LocalIndex == 1) ?? throw new Exception("Cannot find inflated1 variable");
            var argMap = new[]
            {
                resultListVariable, // List<MyClusterTree.MyCluster> MyClusterTree.m_resultList
                localObjectsVariable, // Dictionary<ulong, MyObjectData> this.m_objectsData
                sourceLocalVariable, // HashSet<MyObjectData> source
                inflated1LocalVariable, // ref BoundingBoxD inflated1
            };
            var typeMap = new Dictionary<string, Type>
            {
                { nameof(MyObjectData), MyObjectDataType },
                { nameof(HashSet<MyObjectData>), HashSetMyObjectDataType },
                { nameof(Dictionary<ulong, MyObjectData>), DictionaryUlongMyObjectDataType },
            };
            il.InsertCodeFromMethod(gen, TargetMethodInfo, i, NestedLoopMethodInfo, argMap, typeMap);

            // FIXME: VerifyCallStack does not work properly
            // var callStackImbalance = il.VerifyCallStack();
            // Debug.Assert(callStackImbalance == 0, $"VerifyCallStack: {callStackImbalance}");

            il.RecordPatchedCode();
            return il;
        }

        private static void NestedLoop(List<MyClusterTree.MyCluster> m_resultList, Dictionary<ulong, MyObjectData> m_objectsData, HashSet<MyObjectData> source, BoundingBoxD inflated1)
        {
            // Original:
            foreach (MyClusterTree.MyCluster mResult in m_resultList)
            {
                foreach (MyObjectData myObjectData
                         in m_objectsData
                             .Where(x => mResult.Objects.Contains(x.Key))
                             .Select(x => x.Value))
                {
                    source.Add(myObjectData);
                    inflated1.Include(myObjectData.AABB.GetInflated(MyClusterTree.IdealClusterSize / 2f));
                }
            }

            // // Optimized
            // HashSet<ulong> collidedObjectKeys = new HashSet<ulong>(256); // FIXME: Reuse a single HashSet per thread (thread local)
            // foreach (MyClusterTree.MyCluster collidedCluster in m_resultList)
            // {
            //     foreach (var key in collidedCluster.Objects)
            //     {
            //         collidedObjectKeys.Add(key);
            //     }
            // }
            //
            // var relevantObjectData = m_objectsData
            //     .Where(x => collidedObjectKeys.Contains(x.Key))
            //     .Select(x => x.Value);
            //
            // foreach (var ob in relevantObjectData)
            // {
            //     Counter++;
            //     source.Add(ob);
            //     inflated1.Include(ob.AABB.GetInflated(MyClusterTree.IdealClusterSize / 2f));
            // }
        }
    }
}