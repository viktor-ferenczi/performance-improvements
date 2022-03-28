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
        [EnsureCode("34748389")]
        private static IEnumerable<CodeInstruction> UpdateBroadcastersInRangeTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!Config.Enabled || !Config.FixBroadcast)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();

            var i = il.FindIndex(ci => ci.opcode == OpCodes.Newobj);
            Debug.Assert(il[i + 2].opcode == OpCodes.Newobj);

            var getHashSet = AccessTools.DeclaredMethod(typeof(MyDataReceiverPatch), nameof(GetHashSet));

            il.RemoveAt(i);
            il.Insert(i++, new CodeInstruction(OpCodes.Ldc_I4_0));
            il.Insert(i++, new CodeInstruction(OpCodes.Call, getHashSet));

            i++;

            il.RemoveAt(i);
            il.Insert(i++, new CodeInstruction(OpCodes.Ldc_I4_1));
            il.Insert(i, new CodeInstruction(OpCodes.Call, getHashSet));

            il.RecordPatchedCode();
            return il;
        }

        private static readonly ThreadLocal<HashSet<long>[]> Pool = new ThreadLocal<HashSet<long>[]>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<long> GetHashSet(int i)
        {
            var hashSets = Pool.Value;
            if (hashSets == null)
            {
                hashSets = new[]
                {
                    new HashSet<long>(),
                    new HashSet<long>()
                };
                Pool.Value = hashSets;
            }

            var hashSet = hashSets[i];
            hashSet.Clear();
            return hashSet;
        }
    }
}