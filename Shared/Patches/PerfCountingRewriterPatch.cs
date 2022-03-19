using System.Diagnostics.CodeAnalysis;
using HarmonyLib;
using Microsoft.CodeAnalysis;
using Shared.Config;
using Shared.Logging;
using Shared.Plugin;

namespace Shared.Patches
{
    [HarmonyPatch]
    [SuppressMessage("ReSharper", "UnusedMember.Local")]
    // ReSharper disable once UnusedType.Global
    public static class PerfCountingRewriterPatch
    {
        private static IPluginLogger Log => Common.Logger;
        private static IPluginConfig Config => Common.Config;

        // ReSharper disable once InconsistentNaming
        [HarmonyPrefix]
        [HarmonyPatch("VRage.Scripting.Rewriters.PerfCountingRewriter", "Rewrite")]
        private static bool RewritePrefix(SyntaxTree syntaxTree, int modId, ref SyntaxTree __result)
        {
            if (!Config.Enabled || !Config.DisableModApiStatistics && !Config.CacheMods)
            {
#if DEBUG
                Log.Debug("Keeping the injection of Mod API call statistics code; modId={0}", modId);
#endif
                return true;
            }

#if DEBUG
            Log.Debug("Skipping the injection of Mod API call statistics code; modId={0}", modId);
#endif
            __result = syntaxTree;
            return false;
        }
    }
}