#if UNTESTED

using HarmonyLib;

namespace Shared.Patches
{
    [HarmonyPatch("VRage.Scripting.Rewriters.PerfCountingRewriter")]
    public static class PerfCountingRewriterPatch
    {
        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once RedundantAssignment
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch("Rewrite")]
        private static bool Prefix(object syntaxTree, ref object __result)
        {
            __result = syntaxTree;
            return false;
        }
    }
}

#endif