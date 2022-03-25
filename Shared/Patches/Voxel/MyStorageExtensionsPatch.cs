using System.ComponentModel;
using System.Threading;
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
            enabled = Config.Enabled && Config.FixVoxel;
        }

        private const int Capacity = 8;
        private static readonly MyStorageData[] Pool = new MyStorageData[Capacity];
        private static readonly int[] Used = new int[Capacity];

        static MyStorageExtensionsPatch()
        {
            for (var i = 0; i < Capacity; i++)
            {
                var storageData = new MyStorageData();
                storageData.Resize(Vector3I.One);
                Pool[i] = storageData;
            }

            Config.PropertyChanged += OnConfigChanged;
        }

        private static void OnConfigChanged(object sender, PropertyChangedEventArgs e)
        {
            Configure();
        }

        // ReSharper disable once UnusedMember.Local
        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch(nameof(IMyStorageExtensions.GetMaterialAt),
            new[] { typeof(IMyStorage), typeof(Vector3I) },
            new[] { ArgumentType.Normal, ArgumentType.Ref })]
        [EnsureCode("d214d704")]
        private static bool GetMaterialAtVector3IPrefix(IMyStorage self, ref Vector3I voxelCoords, ref MyVoxelMaterialDefinition __result)
        {
            if (!enabled)
                return true;

            var i = 0;
            for (; i < Capacity; i++)
            {
                if (Interlocked.CompareExchange(ref Used[i], 1, 0) == 0)
                    break;
            }
            if (i >= Capacity)
                return true;

            var target = Pool[i];

            target.ClearContent(0);
            target.ClearMaterials(0);

            self.ReadRange(target, MyStorageDataTypeFlags.ContentAndMaterial, 0, voxelCoords, voxelCoords);

            var materialIndex = target.Material(0);
            __result = materialIndex == 255 ? null : MyDefinitionManager.Static.GetVoxelMaterialDefinition(materialIndex);

            Used[i] = 0;

            return false;
        }
    }
}