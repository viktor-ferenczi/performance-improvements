using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using Havok;
using Sandbox.Engine.Voxels.Planet;
using Sandbox.Game.Multiplayer;
using Sandbox.Game.World;
using VRage;
using Torch.Managers.PatchManager;
using Torch.Managers.PatchManager.MSIL;

namespace TorchPlugin.Patches
{
    // This patch has been ported with the permission of the author:
    // https://github.com/zznty/Torch/blob/master/Torch/Patches/GcCollectPatch.cs

    [PatchShim]
    // ReSharper disable once UnusedType.Global
    internal static class GcCollectPatch
    {
        // These methods freeze for seconds due to forcing a full GC
        private static readonly MethodBase[] Targets =
        {
            AccessTools.Method(typeof(MyPlanetTextureMapProvider), nameof(MyPlanetTextureMapProvider.GetHeightmap)),
            AccessTools.Method(typeof(MyPlanetTextureMapProvider), nameof(MyPlanetTextureMapProvider.GetDetailMap)),
            AccessTools.Method(typeof(MyPlanetTextureMapProvider), nameof(MyPlanetTextureMapProvider.GetMaps)),
            AccessTools.Method(typeof(MySession), nameof(MySession.Unload)),
            AccessTools.Method(typeof(HkBaseSystem), nameof(HkBaseSystem.Quit)),
            AccessTools.Method(typeof(MySimpleProfiler), nameof(MySimpleProfiler.LogPerformanceTestResults)),
            AccessTools.Constructor(typeof(MySession), new[] { typeof(MySyncLayer), typeof(bool) }),
        };

        // ReSharper disable once UnusedMember.Global
        public static void Patch(PatchContext context)
        {
            foreach (var target in Targets)
            {
                var transpilerMethodInfo = AccessTools.Method(typeof(GcCollectPatch), nameof(CollectRemovalTranspiler));
                context.GetPattern(target).Transpilers.Add(transpilerMethodInfo);
            }
        }

        // Remove all GC calls from the bytecode of the above methods
        private static IEnumerable<MsilInstruction> CollectRemovalTranspiler(IEnumerable<MsilInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.Operand is MsilOperandInline<MethodInfo> operand &&
                    operand.Value.DeclaringType == typeof(GC))
                {
                    yield return instruction.CopyWith(OpCodes.Nop);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}