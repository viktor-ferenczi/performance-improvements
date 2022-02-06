#if !TORCH

namespace ClientPlugin.PerformanceImprovements.Shared.Config
{
    public class PluginConfig: IPluginConfig
    {
        public bool Enabled { get; set; } = true;
        public bool FixSpinWait { get; set; } = true;
        public bool FixGridMerge { get; set; } = true;
        public bool FixGridPaste { get; set; } = true;
        public bool FixP2PUpdateStats { get; set; } = true;
    }
}

#endif