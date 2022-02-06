using System;
using ClientPlugin.PerformanceImprovements.Shared.Config;
using Torch;
using Torch.Views;

namespace TorchPlugin
{
    [Serializable]
    public class PluginConfig : ViewModel, IPluginConfig
    {
        [Display(GroupName = "General", Name = "Enable plugin", Order = 1, Description = "Enables/disables the plugin")]
        public bool Enabled
        {
            get => enabled;
            set => SetValue(ref enabled, value);
        }

        private bool enabled = true;

        [Display(GroupName = "Fixes", Name = "Spin lock", Order = 1, Description = "Enables the MySpinWait.SpinOnce fix")]
        public bool FixSpinWait
        {
            get => fixSpinWait;
            set => SetValue(ref fixSpinWait, value);
        }

        private bool fixSpinWait = true;

        [Display(GroupName = "Fixes", Name = "Grid merge", Order = 1, Description = "Enables the MyCubeGrid.MergeGridInternal fix")]
        public bool FixGridMerge
        {
            get => fixMerge;
            set => SetValue(ref fixMerge, value);
        }

        private bool fixMerge = true;

        [Display(GroupName = "Fixes", Name = "Grid paste", Order = 1, Description = "Enables the MyCubeGrid.PasteBlocksServer fix")]
        public bool FixGridPaste
        {
            get => fixPast;
            set => SetValue(ref fixPast, value);
        }

        private bool fixPast = true;

        [Display(GroupName = "Fixes", Name = "P2P stats update", Order = 1, Description = "Enables the VRage.EOS.MyP2PQoSAdapter.UpdateStats fix")]
        public bool FixP2PUpdateStats
        {
            get => fixUpdateStat;
            set => SetValue(ref fixUpdateStat, value);
        }

        private bool fixUpdateStat = true;
    }
}