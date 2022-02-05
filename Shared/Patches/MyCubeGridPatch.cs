#if !DISABLE_MERGE_PASTE_UPDATES

using System.Threading;
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
        private static readonly ThreadLocal<bool> IsMerging = new ThreadLocal<bool>();
        public static bool IsMergingInProgress => IsMerging.Value;

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("MergeGridInternal")]
        private static bool MergeGridInternalPrefix()
        {
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
            if (!IsMerging.Value)
            {
                Log.Warning("Leaving MyCubeGrid.MergeGridInternal without entering before (it should not happen)");
                return;
            }

            IsMerging.Value = false;

            // Update the conveyor system only after the merge is complete
            __instance.GridSystems.ConveyorSystem.FlagForRecomputation();
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once RedundantAssignment
        [HarmonyPrefix]
        [HarmonyPatch("PasteBlocksServer")]
        private static bool PasteBlocksServerPrefix(ref bool __state)
        {
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
        private static void PasteBlocksServerPostfix(bool __state)
        {
            MySession.Static.SetUpdateAllowed(__state);
        }
    }
}

#endif