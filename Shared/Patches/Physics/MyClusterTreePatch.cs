using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using HarmonyLib;
using Sandbox.Engine.Voxels;
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
            Debug.Assert(typeof(HashSet<>) != null, "1");
            Debug.Assert(typeof(Dictionary<,>) != null, "2");
            Debug.Assert(NestedLoopMethodInfo != null, "3");
            Debug.Assert(ResultList != null, "4");
            Debug.Assert(MyObjectDataType != null, "5");
            Debug.Assert(HashSetMyObjectDataType != null, "6");
            Debug.Assert(DictionaryUlongMyObjectDataType != null, "7");
        }

        public static void Configure()
        {
            enabled = Config.Enabled && Config.FixPhysics;

            if (enabled)
            {
                // The default value of MyVoxelPhysicsBody.m_staticForCluster
                // was cleared from true to the default false in 1.203.022.
                // FIXME: Revert it just in case. Test whether it has any effect.
                // var field = AccessTools.Field(typeof(MyVoxelPhysicsBody), "m_staticForCluster");
                // field.SetValue(null, true);
            }
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

            // Jump over the original nested loops
            var i = il.FindIndex(ci => ci.opcode == OpCodes.Ldloc_S && ci.operand is LocalBuilder lb && lb.LocalIndex == 15) - 1;
            Debug.Assert(il[i].opcode == OpCodes.Br_S);

            // Make m_objectsData available as a local variable
            var objectsDataField = il.GetField(fi => fi.Name == "m_objectsData");
            var localObjectsDataField = gen.DeclareLocal(objectsDataField.FieldType);
            il.Insert(i++, new CodeInstruction(OpCodes.Ldloc_0)); // this
            il.Insert(i++, new CodeInstruction(OpCodes.Ldfld, objectsDataField)); // this.m_objectsData
            il.Insert(i++, new CodeInstruction(OpCodes.Stloc_S, localObjectsDataField));

            // Insert replacement code from method
            var targetVariables = TargetMethodInfo.GetMethodBody()?.LocalVariables;
            Debug.Assert(targetVariables != null, "localVariables");
            var argMap = new LocalVariableInfo[]
            {
                localObjectsDataField, // Dictionary<ulong, MyObjectData> this.m_objectsData
                targetVariables.First(v => v.LocalIndex == 2), // HashSet<MyObjectData> source
                targetVariables.First(v => v.LocalIndex == 1), // ref BoundingBoxD inflated1
            };
            var typeMap = new Dictionary<string, Type>()
            {
                { nameof(MyObjectData), MyObjectDataType },
                { nameof(HashSet<MyObjectData>), HashSetMyObjectDataType },
                { nameof(Dictionary<ulong, MyObjectData>), DictionaryUlongMyObjectDataType },
            };
            i = il.InsertCodeFromMethod(gen, TargetMethodInfo, i, NestedLoopMethodInfo, argMap, typeMap);

            var leave = il.FindAllIndex(ci => ci.opcode == OpCodes.Ldloc_S && ci.operand is LocalBuilder lb && lb.LocalIndex == 15)[1] + 3;
            Debug.Assert(il[leave].opcode == OpCodes.Leave_S, "Leave_S");
            il.Insert(i++, new CodeInstruction(OpCodes.Leave_S, il[leave].operand));

            // FIXME: VerifyCallStack does not work properly
            // var callStackImbalance = il.VerifyCallStack();
            // Debug.Assert(callStackImbalance == 0, $"VerifyCallStack: {callStackImbalance}");

            il.RecordPatchedCode();
            return il;
        }

        private static void NestedLoop(Dictionary<ulong, MyObjectData> m_objectsData, HashSet<MyObjectData> source, ref BoundingBoxD inflated1)
        {
            var m_resultList = (List<MyClusterTree.MyCluster>)ResultList.GetValue(null);

            // Original:
            // foreach (MyClusterTree.MyCluster mResult in m_resultList)
            // {
            //     foreach (MyObjectData myObjectData
            //              in m_objectsData
            //                  .Where(x => mResult.Objects.Contains(x.Key))
            //                  .Select(x => x.Value))
            //     {
            //         source.Add(myObjectData);
            //         inflated1.Include(myObjectData.AABB.GetInflated(MyClusterTree.IdealClusterSize / 2f));
            //     }
            // }

            // Optimized
            HashSet<ulong> collidedObjectKeys = new HashSet<ulong>(256); // FIXME: Reuse a single HashSet per thread (thread local)
            foreach (MyClusterTree.MyCluster collidedCluster in m_resultList)
            {
                foreach (var key in collidedCluster.Objects)
                {
                    collidedObjectKeys.Add(key);
                }
            }

            var relevantObjectData = m_objectsData
                .Where(x => collidedObjectKeys.Contains(x.Key))
                .Select(x => x.Value);

            foreach (var ob in relevantObjectData)
            {
                Counter++;
                source.Add(ob);
                inflated1.Include(ob.AABB.GetInflated(MyClusterTree.IdealClusterSize / 2f));
            }
        }
    }
}