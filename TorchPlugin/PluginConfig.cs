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
        private bool fixMerge = true;
        private bool fixPast = true;
        private bool fixUpdateStat = true;
        private bool fixGarbageCollection = true;
        private bool fixGridGroups = true;
        private bool cacheMods = false;
        private bool cacheScripts = false;
        private bool disableModApiStatistics = true;
        private bool fixSafeZone = false;
        private bool fixTargetingAlloc = false;
        //BOOL_OPTION private bool optionName = false;

        [Display(Order = 1, GroupName = "General", Name = "Enable plugin", Description = "Enable the plugin (all fixes)")]
        public bool Enabled
        {
            get => enabled;
            set => SetValue(ref enabled, value);
        }

        [Display(Order = 2, GroupName = "Fixes", Name = "Grid merge", Description = "Disable conveyor updates during grid merge (MyCubeGrid.MergeGridInternal)")]
        public bool FixGridMerge
        {
            get => fixMerge;
            set => SetValue(ref fixMerge, value);
        }

        [Display(Order = 3, GroupName = "Fixes", Name = "Grid paste", Description = "Disable updates during grid paste (MyCubeGrid.PasteBlocksServer)")]
        public bool FixGridPaste
        {
            get => fixPast;
            set => SetValue(ref fixPast, value);
        }

        [Display(Order = 4, GroupName = "Fixes", Name = "P2P stats update", Description = "Eliminate 98% of EOS P2P network statistics updates (VRage.EOS.MyP2PQoSAdapter.UpdateStats)")]
        public bool FixP2PUpdateStats
        {
            get => fixUpdateStat;
            set => SetValue(ref fixUpdateStat, value);
        }

        [Display(Order = 5, GroupName = "Fixes", Name = "Fix garbage collection", Description = "Eliminate long pauses on starting and stopping large worlds by disabling selected GC.Collect calls")]
        public bool FixGarbageCollection
        {
            get => fixGarbageCollection;
            set => SetValue(ref fixGarbageCollection, value);
        }

        [Display(Order = 6, GroupName = "Fixes", Name = "Fix grid groups", Description = "Disable resource updates while grids are being moved between groups")]
        public bool FixGridGroups
        {
            get => fixGridGroups;
            set => SetValue(ref fixGridGroups, value);
        }

        [Display(Order = 7, GroupName = "Fixes", Name = "Cache compiled mods", Description = "Caches compiled mods for faster world load")]
        public bool CacheMods
        {
            get => cacheMods;
            set => SetValue(ref cacheMods, value);
        }

        [Display(Order = 8, GroupName = "Fixes", Name = "Cache compiled scripts", Description = "Caches compiled in-game scripts (PB programs) to reduce lag")]
        public bool CacheScripts
        {
            get => cacheScripts;
            set => SetValue(ref cacheScripts, value);
        }

        [Display(Order = 9, GroupName = "Fixes", Name = "Disable Mod API statistics", Description = "Disable the collection of Mod API call statistics to eliminate the overhead")]
        public bool DisableModApiStatistics
        {
            get => disableModApiStatistics;
            set => SetValue(ref disableModApiStatistics, value);
        }

        [Display(Order = 10, GroupName = "Fixes", Name = "Lower safe zone CPU load", Description = "Caches frequent recalculations in safe zones")]
        public bool FixSafeZone
        {
            get => fixSafeZone;
            set => SetValue(ref fixSafeZone, value);
        }

        [Display(Order = 11, GroupName = "Fixes", Name = "Fix allocations in targeting", Description = "Reduces memory allocations in the turret targeting system")]
        public bool FixTargetingAlloc
        {
            get => fixTargetingAlloc;
            set => SetValue(ref fixTargetingAlloc, value);
        }

        /*BOOL_OPTION
        [Display(Order = 11, GroupName = "Fixes", Name = "Option label", Description = "Option tooltip")]
        public bool OptionName
        {
            get => optionName;
            set => SetValue(ref optionName, value);
        }

        BOOL_OPTION*/
    }
}