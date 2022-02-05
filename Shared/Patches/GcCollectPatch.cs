#if !TORCH

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Havok;
using Sandbox.Engine.Voxels.Planet;
using Sandbox.Game.Multiplayer;
using Sandbox.Game.World;
using VRage;

namespace Shared.Patches
{
    // This patch has been ported with the permission of the author:
    // https://github.com/zznty/Torch/blob/master/Torch/Patches/GcCollectPatch.cs

    // ReSharper disable once UnusedType.Global
    [HarmonyPatch]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    internal static class GcCollectPatch
    {
        // These methods freeze for seconds due to forcing a full GC
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.Method(typeof(MyPlanetTextureMapProvider), nameof(MyPlanetTextureMapProvider.GetHeightmap));
            yield return AccessTools.Method(typeof(MyPlanetTextureMapProvider), nameof(MyPlanetTextureMapProvider.GetDetailMap));
            yield return AccessTools.Method(typeof(MyPlanetTextureMapProvider), nameof(MyPlanetTextureMapProvider.GetMaps));
            yield return AccessTools.Method(typeof(MySession), nameof(MySession.Unload));
            yield return AccessTools.Method(typeof(HkBaseSystem), nameof(HkBaseSystem.Quit));
            yield return AccessTools.Method(typeof(MySimpleProfiler), nameof(MySimpleProfiler.LogPerformanceTestResults));
            yield return AccessTools.Constructor(typeof(MySession), new[] { typeof(MySyncLayer), typeof(bool) });
        }

        // Remove all GC calls from the bytecode of the above methods
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> CollectRemovalTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.operand is MethodInfo operand &&
                    operand.DeclaringType == typeof(GC))
                {
                    yield return instruction.Clone(OpCodes.Nop);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}

#endif