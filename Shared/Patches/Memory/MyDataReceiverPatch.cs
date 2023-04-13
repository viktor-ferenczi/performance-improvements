using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Threading;
using HarmonyLib;
using Sandbox.Game.Entities;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyDataReceiver))]
    [SuppressMessage("ReSharper", "UnusedType.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    public static class MyDataReceiverPatch
    {
        private static IPluginConfig Config => Common.Config;

        [HarmonyTranspiler]
        [HarmonyPatch(nameof(MyDataReceiver.UpdateBroadcastersInRange))]
        [EnsureCode("17abb432")]
        private static IEnumerable<CodeInstruction> UpdateBroadcastersInRangeTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!Config.Enabled || !Config.FixBroadcast)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var i = il.FindIndex(ci => ci.opcode == OpCodes.Newobj);
            Debug.Assert(il[i + 1].opcode == OpCodes.Stloc_1);
            Debug.Assert(il[i + 2].opcode == OpCodes.Ldarg_0);
            
            // 1.202.048
            // Keen optimized the code to use only a single HashSet, but they still allocate it all the time... Pooling is still needed.
            var getHashSet = AccessTools.DeclaredMethod(typeof(MyDataReceiverPatch), nameof(GetHashSet));
            il[i] = new CodeInstruction(OpCodes.Call, getHashSet);

            il.RecordPatchedCode();
            return il;
        }

        private static readonly ThreadLocal<HashSet<long>[]> PairPool = new ThreadLocal<HashSet<long>[]>();
        private static readonly ThreadLocal<HashSet<long>> Pool = new ThreadLocal<HashSet<long>>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<long> GetHashSetPair(int i)
        {
            var hashSets = PairPool.Value;
            if (hashSets == null)
            {
                hashSets = new[]
                {
                    new HashSet<long>(),
                    new HashSet<long>()
                };
                PairPool.Value = hashSets;
            }

            var hashSet = hashSets[i];
            hashSet.Clear();
            return hashSet;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<long> GetHashSet()
        {
            var hashSet = Pool.Value;
            if (hashSet == null)
            {
                hashSet = new HashSet<long>();
                Pool.Value = hashSet;
            }

            hashSet.Clear();
            return hashSet;
        }
    }
}