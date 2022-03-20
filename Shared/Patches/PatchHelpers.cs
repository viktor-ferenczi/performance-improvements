using System;
using System.Reflection;
using HarmonyLib;
using Shared.Logging;
using VRageMath;
using VRageMath.Spatial;

namespace Shared.Patches
{
    // ReSharper disable once UnusedType.Global
    public static class PatchHelpers
    {
        public static bool HarmonyPatchAll(IPluginLogger log, Harmony harmony)
        {
#if DEBUG
            Harmony.DEBUG = true;
#endif

            log.Debug("Applying Harmony patches");
            try
            {
                harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                log.Critical(ex, "Failed to apply Harmony patches");
                return false;
            }

            return true;
        }

        public static void PatchInits()
        {
            // FIXME: Make it configurable!
            const float clusterSize = 4000f;
            MyClusterTree.IdealClusterSize = new Vector3(clusterSize);
            MyClusterTree.IdealClusterSizeHalfSqr = MyClusterTree.IdealClusterSize * MyClusterTree.IdealClusterSize / 4f;
            MyClusterTree.MinimumDistanceFromBorder = MyClusterTree.IdealClusterSize / 50f;
            MyClusterTree.MaximumForSplit = MyClusterTree.IdealClusterSize * 2f;
            MyClusterTree.MaximumClusterSize = 5f * clusterSize;

            MyScriptCompilerPatch.Init();
        }

        public static void PatchUpdates()
        {
            MySafeZonePatch.Clean();
            MyLargeTurretTargetingSystemPatch.Clean();
        }
    }
}