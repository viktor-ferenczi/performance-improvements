using System;
using HarmonyLib;
using Sandbox.Game.Entities.Blocks;

namespace ClientPlugin.Patches
{
    // ReSharper disable once UnusedType.Global
    [HarmonyPatch(typeof(MyMechanicalConnectionBlockBase))]
    public static class MyMechanicalConnectionBlockBasePatch
    {
        private static readonly Random Rng = new Random();
        private static int counter;

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch("CheckSafetyDetach")]
        private static bool Prefix()
        {
            // Skip ~98% of calls
            if (--counter >= 0)
                return false;

            lock (Rng)
                counter = Rng.Next() & 127;

            return true;
        }
    }
}