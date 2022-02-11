using System.Diagnostics.CodeAnalysis;
using System.Reflection;
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

        private static MethodBase TargetMethod()
        {
            // use reflections (with or without AccessTools) to return the MethodInfo of the original method
            var type = AccessTools.TypeByName("VRage.Scripting.Rewriters.PerfCountingRewriter");
            return AccessTools.Method(type, "Rewrite");
        }

        // ReSharper disable once InconsistentNaming
        private static bool Prefix(SyntaxTree syntaxTree, int modId, ref SyntaxTree __result)
        {
            if (!Config.Enabled || !Config.DisableModApiStatistics)
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