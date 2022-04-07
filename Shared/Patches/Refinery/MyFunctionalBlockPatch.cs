using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Sandbox.Game.Entities.Cube;
using Shared.Config;
using Shared.Logging;
using Shared.Plugin;
using Shared.Tools;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyFunctionalBlock))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    // ReSharper disable once UnusedType.Global
    public static class MyFunctionalBlockPatch
    {
        private static IPluginConfig Config => Common.Config;
        private static IPluginLogger Log => Common.Logger;

        private static readonly RwLockHashSet<MyFunctionalBlock> Running = new RwLockHashSet<MyFunctionalBlock>(64);

        [HarmonyPrefix]
        [HarmonyPatch("OnTimerTick")]
        [EnsureCode("b87c2110")]
        private static bool OnTimerTickPrefix(MyFunctionalBlock __instance, ref bool __state)
        {
            Running.BeginReading();
            var alreadyRunning = Running.Contains(__instance);
            Running.FinishReading();

            if (alreadyRunning)
            {
                Log.Warning("Ignoring attempt to run OnTimerTick concurrently for block: {0}", __instance.DebugName);
                return false;
            }

            Running.BeginWriting();
            Running.Add(__instance);
            Running.FinishWriting();

            __state = true;
            return true;
        }

        [HarmonyPostfix]
        [HarmonyPatch("OnTimerTick")]
        [EnsureCode("b87c2110")]
        private static void OnTimerTickPostfix(MyFunctionalBlock __instance, bool __state)
        {
            if (!__state)
                return;

            Running.BeginWriting();
            Running.Remove(__instance);
            Running.FinishWriting();
        }
    }
}