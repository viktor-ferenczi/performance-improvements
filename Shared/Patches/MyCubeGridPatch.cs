#if !DISABLE_MERGE_PASTE_UPDATES

using System.Threading;
using ClientPlugin.PerformanceImprovements.Shared.Config;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Shared.Extensions;
using Shared.Logging;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyCubeGrid))]
    public static class MyCubeGridPatch
    {
        public static IPluginLogger Log;
        public static IPluginConfig Config;
        private static readonly ThreadLocal<bool> IsMerging = new ThreadLocal<bool>();
        public static bool IsMergingInProgress => IsMerging.Value;

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("MergeGridInternal")]
        private static bool MergeGridInternalPrefix()
        {
            if (!Config.Enabled || !Config.FixGridMerge)
                return true;

            if (IsMerging.Value)
            {
                Log.Warning("Ignoring recursive MyCubeGrid.MergeGridInternal call!");
                return false;
            }

            IsMerging.Value = true;

            return true;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPostfix]
        [HarmonyPatch("MergeGridInternal")]
        private static void MergeGridInternalPostfix(MyCubeGrid __instance)
        {
            // Skip the postfix if the prefix did not run
            if (!IsMerging.Value)
                return;

            IsMerging.Value = false;

            // Update the conveyor system only after the merge is complete
            __instance.GridSystems.ConveyorSystem.FlagForRecomputation();
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once RedundantAssignment
        [HarmonyPrefix]
        [HarmonyPatch("PasteBlocksServer")]
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
        // ReSharper disable once InconsistentNaming
        [HarmonyPostfix]
        [HarmonyPatch("PasteBlocksServer")]
        private static void PasteBlocksServerPostfix(bool? __state)
        {
            if (__state == null)
                return;

            MySession.Static.SetUpdateAllowed((bool)__state);
        }
    }
}

#endif