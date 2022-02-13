using System.Threading;
using HarmonyLib;
using Sandbox.Game.Entities;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    // For template type examples see MyCubeGridGroups
    [HarmonyPatch(typeof(VRage.Groups.MyGroups<MyCubeGrid, MyGridLogicalGroupData>))]
    public static class MyGroupsPatch
    {
        private static readonly ThreadLocal<int> MergeGroupsCallDepth = new ThreadLocal<int>();
        public static bool IsInMergeGroups => MergeGroupsCallDepth.Value > 0;

        private static readonly ThreadLocal<int> BreakLinkCallDepth = new ThreadLocal<int>();
        public static bool IsInBreakLink => BreakLinkCallDepth.Value > 0;

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("MergeGroups")]
        private static bool MergeGroupsPrefix()
        {
            MergeGroupsCallDepth.Value++;

            return true;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPostfix]
        [HarmonyPatch("MergeGroups")]
        private static void MergeGroupsPostfix()
        {
            if (!IsInMergeGroups)
                return;

            MergeGroupsCallDepth.Value--;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("BreakLink")]
        private static bool BreakLinkPrefix()
        {
            BreakLinkCallDepth.Value++;

            return true;
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPostfix]
        [HarmonyPatch("BreakLink")]
        private static void BreakLinkPostfix()
        {
            if (!IsInBreakLink)
                return;

            BreakLinkCallDepth.Value--;
        }
    }
}