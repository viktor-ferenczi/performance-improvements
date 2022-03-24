using HarmonyLib;
using Sandbox.Definitions;
using Sandbox.Engine.Voxels;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using VRage.Game;
using VRage.Game.Voxels;
using VRage.Voxels;
using VRageMath;

namespace Shared.Patches
{
    [HarmonyPatch(typeof(IMyStorageExtensions))]
    public static class MyStorageExtensionsPatch
    {
        private static IPluginConfig Config => Common.Config;

        // These patches need restart to be enabled/disabled
        private static bool enabled;

        public static void Configure()
        {
            enabled = Config.Enabled; // && Config.FixTargeting;
        }

        // ReSharper disable once UnusedMember.Local
        [HarmonyPrefix]
        [HarmonyPatch(nameof(IMyStorageExtensions.GetMaterialAt),
            new[] { typeof(IMyStorage), typeof(Vector3I) },
            new[] { ArgumentType.Normal, ArgumentType.Ref })]
        [EnsureCode("d214d704")]
        private static bool GetMaterialAtVector3IPrefix(IMyStorage storage, ref Vector3I voxelCoords, ref MyVoxelMaterialDefinition __result)
        {
            if (!enabled)
                return true;

            MyStorageData target = new MyStorageData();
            target.Resize(Vector3I.One);
            storage.ReadRange(target, MyStorageDataTypeFlags.ContentAndMaterial, 0, voxelCoords, voxelCoords);
            byte materialIndex = target.Material(0);
            __result = materialIndex == byte.MaxValue ? null : MyDefinitionManager.Static.GetVoxelMaterialDefinition(materialIndex);

            return false;
        }
    }
}