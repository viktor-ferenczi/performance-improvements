using System.Threading;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Shared.Config;
using Shared.Plugin;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyCubeGrid))]
    public static class MyCubeGridPatch
    {
        private static IPluginConfig Config => Common.Config;

        private static readonly ThreadLocal<int> CallDepth = new ThreadLocal<int>();
        public static bool IsInMergeGridInternal => CallDepth.Value > 0;

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyCubeGrid.MergeGridInternal))]
        private static bool MergeGridInternalPrefix()
        {
            if (!Config.Enabled || !Config.FixGridMerge)
                return true;

            CallDepth.Value++;

            return true;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPostfix]
        [HarmonyPatch(nameof(MyCubeGrid.MergeGridInternal))]
        private static void MergeGridInternalPostfix(MyCubeGrid __instance)
        {
            if (!IsInMergeGridInternal)
                return;

            if (--CallDepth.Value > 0)
                return;

            // Update the conveyor system only after the merge is complete
            __instance.GridSystems.ConveyorSystem.FlagForRecomputation();
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once RedundantAssignment
        [HarmonyPrefix]
        [HarmonyPatch(nameof(MyCubeGrid.PasteBlocksServer))]
        private static bool PasteBlocksServerPrefix(ref bool? __state)
        {
            if (!Config.Enabled || !Config.FixGridPaste)
                return true;

            // Disable updates for the duration of the paste,
            // it eliminates most spin lock contention
            __state = MySession.Static.m_updateAllowed;
            MySession.Static.m_updateAllowed = false;
            return true;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPostfix]
        [HarmonyPatch(nameof(MyCubeGrid.PasteBlocksServer))]
        private static void PasteBlocksServerPostfix(bool? __state)
        {
            if (__state == null)
                return;

            MySession.Static.m_updateAllowed = (bool)__state;
        }
    }
}