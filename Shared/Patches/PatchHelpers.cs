using ClientPlugin.PerformanceImprovements.Shared.Config;
using Shared.Logging;

namespace Shared.Patches
{
    public static class PatchHelpers
    {
        public static void Init(IPluginLogger log, IPluginConfig cfg)
        {
            MySpinWaitPatch.Log = log;
            MyCubeGridPatch.Log = log;

            MySpinWaitPatch.Config = cfg;
            MySpinWaitPatch.Config = cfg;
            MyCubeGridPatch.Config = cfg;
            MyP2PQoSAdapterPatch.Config = cfg;
        }
    }
}