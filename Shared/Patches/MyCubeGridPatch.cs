using System.Threading;
using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Shared.Config;
using Shared.Extensions;
using Shared.Plugin;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyCubeGrid))]
    public static class MyCubeGridPatch
    {
        private static IPluginConfig Config => Common.Config;

        private static readonly ThreadLocal<int> CallDepth = new ThreadLocal<int>();
        public static bool IsMergingInProgress => CallDepth.Value > 0;

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("MergeGridInternal")]
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
        [HarmonyPatch("MergeGridInternal")]
        private static void MergeGridInternalPostfix(MyCubeGrid __instance)
        {
            if (!IsMergingInProgress)
                return;

            ;
            if (--CallDepth.Value > 0)
                return;

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