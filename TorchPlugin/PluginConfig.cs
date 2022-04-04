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
        private bool detectCodeChanges = true;
        private bool fixMerge = true;
        private bool fixPast = true;
        private bool fixUpdateStat = true;
        private bool fixGarbageCollection = true;
        private bool fixGridGroups = true;
        private bool cacheMods = true;
        private bool cacheScripts = true;
        private bool disableModApiStatistics = true;
        private bool fixSafeZone = true;
        private bool fixTargeting = true;
        private bool fixWindTurbine = true;
        private bool fixVoxel = true;
        private bool fixPhysics = true;
        private bool fixEntity = true;
        private bool fixCharacter = true;
        private bool fixMemory = false;
        private bool fixEndShoot = false;
        //BOOL_OPTION private bool optionName = false;

        [Display(Order = 1, GroupName = "General", Name = "Enable plugin", Description = "Enable the plugin (all fixes)")]
        public bool Enabled
        {
            get => enabled;
            set => SetValue(ref enabled, value);
        }

        [Display(Order = 2, GroupName = "General", Name = "Detect code changes", Description = "Disable the plugin if game code changes are detected before patching")]
        public bool DetectCodeChanges
        {
            get => detectCodeChanges;
            set => SetValue(ref detectCodeChanges, value);
        }

        [Display(Order = 3, GroupName = "Fixes", Name = "Grid merge", Description = "Disable conveyor updates during grid merge (MyCubeGrid.MergeGridInternal)")]
        public bool FixGridMerge
        {
            get => fixMerge;
            set => SetValue(ref fixMerge, value);
        }

        [Display(Order = 4, GroupName = "Fixes", Name = "Grid paste", Description = "Disable updates during grid paste (MyCubeGrid.PasteBlocksServer)")]
        public bool FixGridPaste
        {
            get => fixPast;
            set => SetValue(ref fixPast, value);
        }

        [Display(Order = 5, GroupName = "Fixes", Name = "P2P stats update", Description = "Eliminate 98% of EOS P2P network statistics updates (VRage.EOS.MyP2PQoSAdapter.UpdateStats)")]
        public bool FixP2PUpdateStats
        {
            get => fixUpdateStat;
            set => SetValue(ref fixUpdateStat, value);
        }

        [Display(Order = 6, GroupName = "Fixes", Name = "Fix garbage collection", Description = "Eliminate long pauses on starting and stopping large worlds by disabling selected GC.Collect calls")]
        public bool FixGarbageCollection
        {
            get => fixGarbageCollection;
            set => SetValue(ref fixGarbageCollection, value);
        }

        [Display(Order = 7, GroupName = "Fixes", Name = "Fix grid groups", Description = "Disable resource updates while grids are being moved between groups")]
        public bool FixGridGroups
        {
            get => fixGridGroups;
            set => SetValue(ref fixGridGroups, value);
        }

        [Display(Order = 8, GroupName = "Fixes", Name = "Cache compiled mods", Description = "Caches compiled mods for faster world load")]
        public bool CacheMods
        {
            get => cacheMods;
            set => SetValue(ref cacheMods, value);
        }

        [Display(Order = 9, GroupName = "Fixes", Name = "Cache compiled scripts", Description = "Caches compiled in-game scripts (PB programs) to reduce lag")]
        public bool CacheScripts
        {
            get => cacheScripts;
            set => SetValue(ref cacheScripts, value);
        }

        [Display(Order = 10, GroupName = "Fixes", Name = "Disable Mod API statistics", Description = "Disable the collection of Mod API call statistics to eliminate the overhead")]
        public bool DisableModApiStatistics
        {
            get => disableModApiStatistics;
            set => SetValue(ref disableModApiStatistics, value);
        }

        [Display(Order = 11, GroupName = "Fixes", Name = "Lower safe zone CPU load", Description = "Caches frequent recalculations in safe zones")]
        public bool FixSafeZone
        {
            get => fixSafeZone;
            set => SetValue(ref fixSafeZone, value);
        }

        [Display(Order = 12, GroupName = "Fixes", Name = "Fix allocations in targeting (needs restart)", Description = "Reduces memory allocations in the turret targeting system (needs restart)")]
        public bool FixTargeting
        {
            get => fixTargeting;
            set => SetValue(ref fixTargeting, value);
        }

        [Display(Order = 13, GroupName = "Fixes", Name = "Fix wind turbine performance", Description = "Caches the result of MyWindTurbine.IsInAtmosphere")]
        public bool FixWindTurbine
        {
            get => fixWindTurbine;
            set => SetValue(ref fixWindTurbine, value);
        }

        [Display(Order = 14, GroupName = "Fixes", Name = "Fix voxel performance", Description = "Reduces memory allocations in IMyStorageExtensions.GetMaterialAt")]
        public bool FixVoxel
        {
            get => fixVoxel;
            set => SetValue(ref fixVoxel, value);
        }

        [Display(Order = 15, GroupName = "Fixes", Name = "Fix physics performance (needs restart)", Description = "Optimizes the MyPhysicsBody.RigidBody getter and the HkShape comparer (needs restart)")]
        public bool FixPhysics
        {
            get => fixPhysics;
            set => SetValue(ref fixPhysics, value);
        }

        [Display(Order = 16, GroupName = "Fixes", Name = "Fix entity performance (needs restart)", Description = "Optimizes MyEntity.InScene getter (needs restart)")]
        public bool FixEntity
        {
            get => fixEntity;
            set => SetValue(ref fixEntity, value);
        }

        [Display(Order = 17, GroupName = "Fixes", Name = "Fix character performance (needs restart)", Description = "Disables character footprint logic on server side (needs restart)")]
        public bool FixCharacter
        {
            get => fixCharacter;
            set => SetValue(ref fixCharacter, value);
        }

        [Display(Order = 18, GroupName = "Fixes", Name = "Fix frequent memory allocations", Description = "Optimizes frequent memory allocations")]
        public bool FixMemory
        {
            get => fixMemory;
            set => SetValue(ref fixMemory, value);
        }

        [Display(Order = 19, GroupName = "Fixes", Name = "Fix crash on grinding active turrets", Description = "Adds a missing call to EndShoot on server side, fixing subsequent issues on client side")]
        public bool FixEndShoot
        {
            get => fixEndShoot;
            set => SetValue(ref fixEndShoot, value);
        }

        /*BOOL_OPTION
        [Display(Order = 19, GroupName = "Fixes", Name = "Option label", Description = "Option tooltip")]
        public bool OptionName
        {
            get => optionName;
            set => SetValue(ref optionName, value);
        }

        BOOL_OPTION*/
    }
}