using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Sandbox.Engine.Voxels;
using Shared.Config;
using Shared.Plugin;
using Shared.Tools;
using VRage.Game.Voxels;
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
        [HarmonyTranspiler]
        [HarmonyPatch(nameof(IMyStorageExtensions.GetMaterialAt),
            new[] { typeof(IMyStorage), typeof(Vector3I) },
            new[] { ArgumentType.Normal, ArgumentType.Ref })]
        [EnsureCode("d214d704")]
        private static IEnumerable<CodeInstruction> GetMaterialAtVector3ITranspiler(IEnumerable<CodeInstruction> instructions)
        {
            if (!enabled)
                return instructions;

            var il = instructions.ToList();
            il.RecordOriginalCode();



            il.RecordPatchedCode();
            return il.AsEnumerable();
        }
    }
}