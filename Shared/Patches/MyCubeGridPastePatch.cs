using HarmonyLib;
using Sandbox.Game.Entities;
using Sandbox.Game.World;
using Shared.Extensions;
using Shared.Patches.Patching;

namespace Shared.Patches
{
    [HarmonyPatchKey("FixGridPaste", "Grids")]
    public static class MyCubeGridPastePatch
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once RedundantAssignment
        [HarmonyPrefix]
        [HarmonyPatch(typeof(MyCubeGrid),"PasteBlocksServer")]
        private static bool PasteBlocksServerPrefix(ref bool? __state)
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
        [HarmonyPatch(typeof(MyCubeGrid),"PasteBlocksServer")]
        private static void PasteBlocksServerPostfix(bool? __state)
        {
            if (__state == null)
                return;

            MySession.Static.SetUpdateAllowed((bool)__state);
        }
    }
}