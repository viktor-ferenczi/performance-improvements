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
using Shared.Plugin;
using VRage;

namespace Shared.Patches
{
    // This patch has been ported with the permission of the author:
    // https://github.com/zznty/Torch/blob/master/Torch/Patches/GcCollectPatch.cs

    // ReSharper disable once UnusedType.Global
    [HarmonyPatch]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    [SuppressMessage("ReSharper", "UnusedParameter.Local")]
    public static class GcCollectPatch
    {
        private static IPluginLogger Log => Common.Logger;
        private static IPluginConfig Config => Common.Config;

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
                    var parameterInfos = operand.GetParameters();
                    CodeInstruction replacement;
                    switch (parameterInfos.Length)
                    {
                        case 0:
                            replacement = instruction.Clone(Replacement0Info);
                            break;

                        case 1:
                            replacement = instruction.Clone(Replacement1Info);
                            break;

                        case 2:
                            replacement = instruction.Clone(Replacement2Info);
                            break;

                        case 3:
                            replacement = instruction.Clone(Replacement3Info);
                            break;

                        default:
                            throw new Exception($"Encountered an unknown overload of GC.Collect() with {parameterInfos.Length} parameters");
                    }

                    replacement.labels.AddRange(instruction.labels);

                    yield return replacement;
                    continue;
                }

                yield return instruction;
            }
        }

        private static readonly MethodInfo Replacement0Info = AccessTools.DeclaredMethod(typeof(GcCollectPatch), nameof(Replacement0));
        private static readonly MethodInfo Replacement1Info = AccessTools.DeclaredMethod(typeof(GcCollectPatch), nameof(Replacement1));
        private static readonly MethodInfo Replacement2Info = AccessTools.DeclaredMethod(typeof(GcCollectPatch), nameof(Replacement2));
        private static readonly MethodInfo Replacement3Info = AccessTools.DeclaredMethod(typeof(GcCollectPatch), nameof(Replacement3));

        public static void Replacement0()
        {
            if (Config.Enabled && Config.FixGarbageCollection)
            {
#if DEBUG
                Log.Debug("Skipping GC.Collect()");
#endif
                return;
            }

#if DEBUG
            Log.Debug("Calling GC.Collect()");
#endif

            GC.Collect();
        }

        public static void Replacement1(int generation)
        {
            if (Config.Enabled && Config.FixGarbageCollection)
            {
#if DEBUG
                Log.Debug("Skipping GC.Collect()");
#endif
                return;
            }

#if DEBUG
            Log.Debug("Calling GC.Collect()");
#endif

            GC.Collect(generation);
        }

        public static void Replacement2(int generation, GCCollectionMode mode)
        {
            if (Config.Enabled && Config.FixGarbageCollection)
            {
#if DEBUG
                Log.Debug("Skipping GC.Collect()");
#endif
                return;
            }

#if DEBUG
            Log.Debug("Calling GC.Collect()");
#endif

            GC.Collect(generation, mode);
        }

        public static void Replacement3(int generation, GCCollectionMode mode, bool blocking)
        {
            if (Config.Enabled && Config.FixGarbageCollection)
            {
#if DEBUG
                Log.Debug("Skipping GC.Collect()");
#endif
                return;
            }

#if DEBUG
            Log.Debug("Calling GC.Collect()");
#endif

            GC.Collect(generation, mode, blocking);
        }
    }
}