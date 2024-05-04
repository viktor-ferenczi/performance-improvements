using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Sandbox.Game.GameSystems;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using VRage.Collections;
using VRageMath;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyGridGasSystem))]
    public static class MyGridGasSystemPatch
    {
        private const int MaxPoolSize = 127;
        private const int LowWatermark = 16;

        private static IPluginConfig Config => Common.Config;

        private static MyConcurrentList<HashSet<Vector3I>> pool = new MyConcurrentList<HashSet<Vector3I>>(128);

        private static readonly Random Rng = new Random();

        private static readonly MethodInfo GetMethod = AccessTools.DeclaredMethod(typeof(MyGridGasSystemPatch), nameof(Get));
        private static readonly MethodInfo ReturnMethod = AccessTools.DeclaredMethod(typeof(MyGridGasSystemPatch), nameof(Return));

        private static HashSet<Vector3I> Get()
        {
            if (pool.Count == 0)
                return new HashSet<Vector3I>(1024);

            return pool.Pop();
        }

        private static void Return(HashSet<Vector3I> hashSet)
        {
            if (pool == null)
                pool = new MyConcurrentList<HashSet<Vector3I>>();

            hashSet.Clear();

            if (pool.Count < MaxPoolSize)
            {
                pool.Add(hashSet);
            }
            else
            {
                pool[Rng.Next() % pool.Count] = hashSet;
            }
        }

        public static void Update()
        {
            if (!Config.FixAirtight)
                return;

            if (Common.Plugin.Tick % 300 != 0)
                return;

            if (pool.Count <= LowWatermark)
                return;

            pool.RemoveAt(Rng.Next() % pool.Count);

#if DEBUG
            Common.Logger.Info($"AirTight object pool size: {pool.Count}");
#endif
        }

        private static void PatchFirstNew(List<CodeInstruction> il)
        {
            var iNew = il.FindIndex(ci =>
                ci.opcode == OpCodes.Newobj &&
                ci.operand is ConstructorInfo c &&
                c.DeclaringType != null &&
                c.DeclaringType.Name.StartsWith("HashSet"));

            Debug.Assert(iNew >= 0);

            il[iNew] = new CodeInstruction(OpCodes.Call, GetMethod);
        }

        private static void PatchReturnBeforeRet(List<CodeInstruction> il, params CodeInstruction[] loadInstance)
        {
            Debug.Assert(loadInstance.Length > 0);

            var iRet = il.FindLastIndex(ci => ci.opcode == OpCodes.Ret);
            Debug.Assert(iRet >= 0);

            var iCode = iRet;

            foreach (var ci in loadInstance)
            {
                il.Insert(iRet++, ci);
            }

            il.Insert(iRet++, new CodeInstruction(OpCodes.Call, ReturnMethod));

            il[iCode].labels.AddRange(il[iRet].labels);
            il[iRet].labels.Clear();
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch("ShrinkData")]
        [EnsureCode("3646a149")]
        private static IEnumerable<CodeInstruction> ShrinkDataTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!Config.FixAirtight)
                return instructions;

            // See MyScriptCompiler.Compile.il
            var il = instructions.ToList();
            il.RecordOriginalCode();

            // This hash set is local to the method
            PatchFirstNew(il);
            PatchReturnBeforeRet(il, new CodeInstruction(OpCodes.Ldloc_S, 4));

            il.RecordPatchedCode();
            return il;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch("RefreshRoomBlocks")]
        [EnsureCode("2db5daff")]
        private static IEnumerable<CodeInstruction> RefreshRoomBlocksTranspiler(IEnumerable<CodeInstruction> instructions, ILGenerator gen)
        {
            if (!Config.FixAirtight)
                return instructions;

            // See MyScriptCompiler.Compile.il
            var il = instructions.ToList();
            il.RecordOriginalCode();

            // This hash set may be persisted into m_rooms[0].Blocks, in which case
            // we need to reuse the previous hash set from there (if any)
            PatchFirstNew(il);
            ReturnPreviousBlocks(il);
            ReturnTemporaryBlocks(il, gen);

            il.RecordPatchedCode();
            return il;
        }

        private static readonly MethodInfo MyOxygenRoomGetBlocksMethod = AccessTools.PropertyGetter(typeof(MyOxygenRoom), nameof(MyOxygenRoom.Blocks));

        private static void ReturnPreviousBlocks(List<CodeInstruction> il)
        {
            // Setting room.Blocks
            var iSetBlocks = il.FindIndex(ci =>
                ci.opcode == OpCodes.Callvirt &&
                ci.operand is MethodInfo mi &&
                mi.Name == "set_Blocks");
            Debug.Assert(iSetBlocks >= 0);

            // Return room.Blocks for reuse before it gets overwritten
            il.Insert(iSetBlocks++, new CodeInstruction(OpCodes.Ldarg_1)); // room
            il.Insert(iSetBlocks++, new CodeInstruction(OpCodes.Callvirt, MyOxygenRoomGetBlocksMethod)); // room.Blocks
            il.Insert(iSetBlocks, new CodeInstruction(OpCodes.Call, ReturnMethod));
        }

        private static void ReturnTemporaryBlocks(List<CodeInstruction> il, ILGenerator gen)
        {
            // Getting room.Blocks
            var iSetIsAirtight = il.FindIndex(ci =>
                ci.opcode == OpCodes.Callvirt &&
                ci.operand is MethodInfo mi &&
                mi.Name == "set_IsAirtight");
            Debug.Assert(iSetIsAirtight >= 0);

            // If room.Blocks is not returned, then return the temporary other hash set

            var iRet = il.FindLastIndex(ci => ci.opcode == OpCodes.Ret);
            Debug.Assert(iRet >= 0);

            var skip = gen.DefineLabel();
            il[iRet].labels.Add(skip);

            var iLoadFlag1 = iSetIsAirtight - 1;
            il.Insert(iRet++, il[iLoadFlag1]); // Load flag1

            il.Insert(iRet++, new CodeInstruction(OpCodes.Brtrue, skip));
            il.Insert(iRet++, new CodeInstruction(OpCodes.Ldloc_3)); // Load other
            il.Insert(iRet, new CodeInstruction(OpCodes.Call, ReturnMethod));
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch("GetRoomBlocks")]
        [EnsureCode("2db0ee52")]
        private static IEnumerable<CodeInstruction> GetRoomBlocksTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!Config.FixAirtight)
                return instructions;

            // See MyScriptCompiler.Compile.il
            var il = instructions.ToList();
            il.RecordOriginalCode();

            // This hash set is returned from the method, so cannot be collected here.
            // They will be returned in RemoveBlock.
            PatchFirstNew(il);

            il.RecordPatchedCode();
            return il;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyTranspiler]
        [HarmonyPatch("RemoveBlock")]
        [EnsureCode("241c139e")]
        private static IEnumerable<CodeInstruction> RemoveBlockTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!Config.FixAirtight)
                return instructions;

            // See MyScriptCompiler.Compile.il
            var il = instructions.ToList();
            il.RecordOriginalCode();

            // Find all places where Blocks is set to null
            var iSetBlocks = il
                .FindAllIndex(ci =>
                    ci.opcode == OpCodes.Callvirt &&
                    ci.operand is MethodInfo mi &&
                    mi.Name == "set_Blocks");

            // Return all such hash sets before removing the references to them
            iSetBlocks.Reverse();
            foreach (var iSetBlock in iSetBlocks)
            {
                var i = iSetBlock - 1;
                if (il[i].opcode != OpCodes.Ldnull)
                    continue;

                il.Insert(i++, new CodeInstruction(OpCodes.Dup));
                il.Insert(i++, new CodeInstruction(OpCodes.Callvirt, MyOxygenRoomGetBlocksMethod)); // Get .Blocks
                il.Insert(i, new CodeInstruction(OpCodes.Call, ReturnMethod));
            }

            il.RecordPatchedCode();
            return il;
        }
    }
}