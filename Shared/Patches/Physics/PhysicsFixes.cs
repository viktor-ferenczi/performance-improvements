using VRageMath;
using VRageMath.Spatial;

namespace Shared.Patches.Physics
{
    public static class PhysicsFixes
    {
        public static void SetClusterSize(float clusterSize)
        {
            MyClusterTree.IdealClusterSize = new Vector3(clusterSize);
            MyClusterTree.IdealClusterSizeHalfSqr = MyClusterTree.IdealClusterSize * MyClusterTree.IdealClusterSize / 4f;
            MyClusterTree.MinimumDistanceFromBorder = MyClusterTree.IdealClusterSize / 50f;
            MyClusterTree.MaximumForSplit = MyClusterTree.IdealClusterSize * 2f;
            MyClusterTree.MaximumClusterSize = 5f * clusterSize;
        }
    }
}