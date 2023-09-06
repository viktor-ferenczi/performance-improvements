using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using HarmonyLib;
using Sandbox.Engine.Voxels;
using Sandbox.Game.Entities;
using Shared.Config;
using Shared.Logging;
using Shared.Plugin;
using Shared.Tools;
using VRage.Utils;
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
        private static IPluginLogger Logger => Common.Plugin.Log;
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

            // Find the nested loop
            var i = il.FindAllIndex(ci => ci.opcode == OpCodes.Ldloc_2)[1];
            while (i < il.Count && il[i].opcode != OpCodes.Pop) i++;
            i++;

            // Remove the nested loop
            var j = il.FindAllIndex(ci => ci.opcode == OpCodes.Endfinally)[1];
            var nop = new CodeInstruction(OpCodes.Nop);
            nop.labels = il.Skip(i).Take(j + 1 - i).Select(ci => ci.labels).SelectMany(l => l).ToList();
            il.RemoveRange(i, j + 1 - i);
            il.Insert(i++, nop);

            // Call a replacement instead
            var resultListGetter = il.FindPropertyGetter("m_resultList");
            il.Insert(i++, new CodeInstruction(OpCodes.Call, resultListGetter)); // static MyClusterTree.m_resultList
            var objectsDataField = il.GetField(fi => fi.Name == "m_objectsData");
            il.Insert(i++, new CodeInstruction(OpCodes.Ldarg_0)); // this
            il.Insert(i++, new CodeInstruction(OpCodes.Ldfld, objectsDataField)); // this.m_objectsData
            il.Insert(i++, new CodeInstruction(OpCodes.Ldloc_2)); // source
            il.Insert(i++, new CodeInstruction(OpCodes.Ldloca_S, (byte)1)); // inflated1
            il.Insert(i, new CodeInstruction(OpCodes.Call, NestedLoopMethodInfo));

            il.RecordPatchedCode();
            return il;
        }

        // This class must be EXACTLY IDENTICAL to MyClusterTree.MyObjectData
        private class MyObjectData
        {
            public ulong Id;
            public MyClusterTree.MyCluster Cluster;
            public MyClusterTree.IMyActivationHandler ActivationHandler;
            public BoundingBoxD AABB;
            public int StaticId;
            public string Tag;
            public long EntityId;
        }

        private static void NestedLoop(List<MyClusterTree.MyCluster> resultList, object objectsDataAsObject, object sourceAsObject, ref BoundingBoxD inflated1)
        {
            // Dictionary<ulong, MyObjectData>
            var objectsData = Unsafe.As<Dictionary<ulong, MyObjectData>>(objectsDataAsObject);

            // HashSet<MyObjectData>
            var source = Unsafe.As<HashSet<MyObjectData>>(sourceAsObject);

            // Original:
            // foreach (MyClusterTree.MyCluster mResult in resultList)
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

            MyLog.Default.WriteLine($"!!! NestedLoop 1: resultList count {resultList.Count}");

            // Optimized
            HashSet<ulong> collidedObjectKeys = new HashSet<ulong>(); // FIXME: Reuse a single HashSet per thread (thread local)
            foreach (MyClusterTree.MyCluster collidedCluster in resultList)
            {
                // MyLog.Default.WriteLine($"!!! NestedLoop 1b: collidedCluster.Objects count {collidedCluster.Objects.Count}");
                foreach (var key in collidedCluster.Objects)
                {
                    collidedObjectKeys.Add(key);
                }
            }

            MyLog.Default.WriteLine($"!!! NestedLoop 2: collidedObjectKeys count {collidedObjectKeys.Count}");
            MyLog.Default.WriteLine($"!!! NestedLoop 3: Counter {Counter}");

            foreach (var pair in objectsData)
            {
                if (collidedObjectKeys.Contains(pair.Key))
                {
                    Counter++;
                    source.Add(pair.Value);
                    inflated1.Include(pair.Value.AABB.GetInflated(MyClusterTree.IdealClusterSize / 2f));
                }
            }

            MyLog.Default.WriteLine($"!!! NestedLoop 4: Counter {Counter}");
        }
    }
}