using HarmonyLib;
using Sandbox.Engine.Voxels;
using Sandbox.Game.Entities;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(MyVoxelPhysicsBody))]
    // ReSharper disable once UnusedType.Global
    public static class MyVoxelPhysicsBodyPatch
    {
        [HarmonyPrefix]
        [HarmonyPatch(MethodType.Constructor, typeof(MyVoxelBase), typeof(float), typeof(float), typeof(bool))]
        // ReSharper disable once InconsistentNaming
        // ReSharper disable once RedundantAssignment
        private static bool ConstructorPrefix(ref bool ___m_staticForCluster)
        {
            // Reverting to the 1.202 default
            ___m_staticForCluster = true;

            return true;
        }
    }
}