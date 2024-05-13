using System.Diagnostics.CodeAnalysis;
using System.Threading;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Shared.Config;
using Shared.Tools;
using Shared.Plugin;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyCubeGrid))]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public static class MyCubeGridPatchForMergeAndPaste
    {
        private static IPluginConfig Config => Common.Config;

        private static readonly ThreadLocal<int> CallDepth = new ThreadLocal<int>();
        public static bool IsInMergeGridInternal => CallDepth.Value > 0;

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("MergeGridInternal")]
        [EnsureCode("ddf218c3")]
        private static bool MergeGridInternalPrefix(ref bool __state)
        {
            if (!Config.Enabled || !Config.FixGridMerge)
                return true;

            CallDepth.Value++;

            __state = true;
            return true;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPostfix]
        [HarmonyPatch("MergeGridInternal")]
        [EnsureCode("ddf218c3")]
        private static void MergeGridInternalPostfix(MyCubeGrid __instance, bool __state)
        {
            if (!__state)
                return;

            if (--CallDepth.Value > 0)
                return;

            // Update the conveyor system only after the merge is complete
            __instance.GridSystems.ConveyorSystem.FlagForRecomputation();
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("PasteBlocksServer")]
        [EnsureCode("abb0d7f4")]
        private static bool PasteBlocksServerPrefix(ref bool? __state)
        {
            if (!Config.Enabled || !Config.FixGridPaste)
                return true;

            // Disable updates for the duration of the paste,
            // it eliminates most spin lock contention
            __state = MySession.Static.IsUpdateAllowed();
            MySession.Static.SetUpdateAllowed(false);
            return true;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPostfix]
        [HarmonyPatch("PasteBlocksServer")]
        [EnsureCode("abb0d7f4")]
        private static void PasteBlocksServerPostfix(bool? __state)
        {
            if (__state == null)
                return;

            MySession.Static.SetUpdateAllowed((bool)__state);
        }
    }
}