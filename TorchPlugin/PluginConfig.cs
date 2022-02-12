using System;
using Shared.Config;
using Torch;
using Torch.Views;

namespace TorchPlugin
{
    [Serializable]
    public class PluginConfig : ViewModel, IPluginConfig
    {
        private bool enabled = true;
        private bool fixSpinWait = true;
        private bool fixMerge = true;
        private bool fixPast = true;
        private bool fixUpdateStat = true;
        private bool fixGarbageCollection = true;
        private bool fixThrusters = true;
        //BOOL_OPTION private bool optionName = true;
        private bool disableModApiStatistics = true;

        [Display(Order = 1, GroupName = "General", Name = "Enable plugin", Description = "Enables/disables the plugin")]
        public bool Enabled
        {
            get => enabled;
            set => SetValue(ref enabled, value);
        }

        [Display(Order = 2, GroupName = "Fixes", Name = "Spin lock", Description = "Enables the MySpinWait.SpinOnce fix")]
        public bool FixSpinWait
        {
            get => fixSpinWait;
            set => SetValue(ref fixSpinWait, value);
        }

        [Display(Order = 3, GroupName = "Fixes", Name = "Grid merge", Description = "Enables the MyCubeGrid.MergeGridInternal fix")]
        public bool FixGridMerge
        {
            get => fixMerge;
            set => SetValue(ref fixMerge, value);
        }

        [Display(Order = 4, GroupName = "Fixes", Name = "Grid paste", Description = "Enables the MyCubeGrid.PasteBlocksServer fix")]
        public bool FixGridPaste
        {
            get => fixPast;
            set => SetValue(ref fixPast, value);
        }

        [Display(Order = 5, GroupName = "Fixes", Name = "P2P stats update", Description = "Enables the VRage.EOS.MyP2PQoSAdapter.UpdateStats fix")]
        public bool FixP2PUpdateStats
        {
            get => fixUpdateStat;
            set => SetValue(ref fixUpdateStat, value);
        }

        [Display(Order = 6, GroupName = "Fixes", Name = "Fix garbage collection", Description = "Eliminates long pauses on starting and stopping large worlds by disabling selected GC.Collect calls")]
        public bool FixGarbageCollection
        {
            get => fixGarbageCollection;
            set => SetValue(ref fixGarbageCollection, value);
        }

        [Display(Order = 7, GroupName = "Fixes", Name = "Fix thrusters", Description = "Throttles the maximum thrust calculation to happen only once a second")]
        public bool FixThrusters
        {
            get => fixThrusters;
            set => SetValue(ref fixThrusters, value);
        }

        /*BOOL_OPTION
        [Display(Order = 9, GroupName = "Fixes", Name = "Option label", Description = "Option tooltip")]
        public bool OptionName
        {
            get => optionName;
            set => SetValue(ref optionName, value);
        }

        BOOL_OPTION*/
        [Display(Order = 19, GroupName = "Fixes", Name = "Disables Mod API statistics", Description = "Disables the collection of Mod API call statistics to eliminate the overhead")]
        public bool DisableModApiStatistics
        {
            get => disableModApiStatistics;
            set => SetValue(ref disableModApiStatistics, value);
        }
    }
}