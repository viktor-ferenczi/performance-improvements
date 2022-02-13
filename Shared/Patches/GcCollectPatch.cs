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
using Shared.Config;
using Shared.Logging;
using Shared.Patches.Patching;
using Shared.Plugin;
using VRage;

namespace Shared.Patches
{
    // This patch has been ported with the permission of the author:
    // https://github.com/zznty/Torch/blob/master/Torch/Patches/GcCollectPatch.cs

    // ReSharper disable once UnusedType.Global
    [HarmonyPatchKey("FixGarbageCollection")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public static class GcCollectPatch
    {
        private static IPluginLogger Log => Common.Logger;

        // These methods freeze for seconds due to forcing a full GC
        static IEnumerable<MethodBase> TargetMethods()
        {
            yield return AccessTools.DeclaredMethod(typeof(MyPlanetTextureMapProvider), nameof(MyPlanetTextureMapProvider.GetHeightmap));
            yield return AccessTools.DeclaredMethod(typeof(MyPlanetTextureMapProvider), nameof(MyPlanetTextureMapProvider.GetDetailMap));
            yield return AccessTools.DeclaredMethod(typeof(MyPlanetTextureMapProvider), nameof(MyPlanetTextureMapProvider.GetMaps));
            yield return AccessTools.DeclaredMethod(typeof(MySession), nameof(MySession.Unload));
            yield return AccessTools.DeclaredMethod(typeof(HkBaseSystem), nameof(HkBaseSystem.Quit));
            yield return AccessTools.DeclaredMethod(typeof(MySimpleProfiler), nameof(MySimpleProfiler.LogPerformanceTestResults));
            yield return AccessTools.Constructor(typeof(MySession), new[] { typeof(MySyncLayer), typeof(bool) });
        }

        // Remove all GC calls from the bytecode of the above methods
        [HarmonyTranspiler]
        private static IEnumerable<CodeInstruction> CollectRemovalTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            foreach (var instruction in instructions)
            {
                if (instruction.opcode == OpCodes.Call &&
                    instruction.operand is MethodInfo operand &&
                    operand.DeclaringType == typeof(GC) &&
                    operand.Name == "Collect")
                {
                    yield return new(OpCodes.Nop);
                    continue;
                }

                yield return instruction;
            }
        }
    }
}