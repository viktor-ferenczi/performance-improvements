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
        private bool fixCharacter = true;
        private bool fixMemory = true;
        private bool fixAccess = false;
        private bool fixBlockLimit = true;
        private bool fixSafeAction = true;
        private bool fixTerminal = false;
        private bool fixTextPanel = false;
        private bool fixConveyor = false;
        private bool fixLogFlooding = false;
        private bool fixWheelTrail = false;
        private bool fixProjection = false;
        private bool fixAirtight = false;
        //BOOL_OPTION private bool optionName = false;

        [Display(Order = 1, GroupName = "General", Name = "Enable plugin", Description = "Enable the plugin (all fixes)")]
        public bool Enabled
        {
            get => enabled;
            set => SetValue(ref enabled, value);
        }

        [Display(Order = 2, GroupName = "General", Name = "Detect code changes", Description = "Disable the plugin if any changes to the game code are detected before patching")]
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

        // Disabled due to inability to patch generics (methods of MyGroups)
        [Display(Order = 7, GroupName = "Fixes", Name = "Fix grid groups", Description = "Disable resource updates while grids are being moved between groups", Enabled = false)]
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

        [Display(Order = 12, Visible = false, GroupName = "Fixes", Name = "Fix allocations in targeting (needs restart)", Description = "Reduces memory allocations in the turret targeting system (needs restart)")]
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

        [Display(Order = 15, GroupName = "Fixes", Name = "Fix physics performance (needs restart)", Description = "Optimizes the MyPhysicsBody.RigidBody getter (needs restart)")]
        public bool FixPhysics
        {
            get => fixPhysics;
            set => SetValue(ref fixPhysics, value);
        }

        [Display(Order = 17, GroupName = "Fixes", Name = "Fix character performance (needs restart)", Description = "Disables character footprint logic on server side (needs restart)")]
        public bool FixCharacter
        {
            get => fixCharacter;
            set => SetValue(ref fixCharacter, value);
        }

        [Display(Order = 18, Visible = false, GroupName = "Fixes", Name = "Fix frequent memory allocations", Description = "Optimizes frequent memory allocations")]
        public bool FixMemory
        {
            get => fixMemory;
            set => SetValue(ref fixMemory, value);
        }

        [Display(Order = 19, GroupName = "Fixes", Name = "Less frequent update of block access rights", Description = "Caches the result of MyCubeBlock.GetUserRelationToOwner and MyTerminalBlock.HasPlayerAccessReason")]
        public bool FixAccess
        {
            get => fixAccess;
            set => SetValue(ref fixAccess, value);
        }

        [Display(Order = 21, GroupName = "Fixes", Name = "Less frequent sync of block counts for limit checking", Description = "Suppresses frequent calls to MyPlayerCollection.SendDirtyBlockLimits")]
        public bool FixBlockLimit
        {
            get => fixBlockLimit;
            set => SetValue(ref fixBlockLimit, value);
        }

        [Display(Order = 22, GroupName = "Fixes", Name = "Cache actions allowed by the safe zone", Description = "Caches the result of MySafeZone.IsActionAllowed and MySessionComponentSafeZones.IsActionAllowedForSafezone for 2 seconds")]
        public bool FixSafeAction
        {
            get => fixSafeAction;
            set => SetValue(ref fixSafeAction, value);
        }

        [Display(Order = 23, GroupName = "Fixes", Name = "Less frequent update of PB access to blocks", Description = "Suppresses frequent calls to MyGridTerminalSystem.UpdateGridBlocksOwnership updating IsAccessibleForProgrammableBlock unnecessarily often")]
        public bool FixTerminal
        {
            get => fixTerminal;
            set => SetValue(ref fixTerminal, value);
        }

        [Display(Order = 24, GroupName = "Fixes", Name = "Text panel performance fixes", Description = "Disables UpdateVisibility of LCD surfaces on multiplayer servers (disable this if LCDs flicker on clients)")]
        public bool FixTextPanel
        {
            get => fixTextPanel;
            set => SetValue(ref fixTextPanel, value);
        }

        [Display(Order = 25, GroupName = "Fixes", Name = "Conveyor network performance fixes", Description = "Caches conveyor network lookups")]
        public bool FixConveyor
        {
            get => fixConveyor;
            set => SetValue(ref fixConveyor, value);
        }

        [Display(Order = 26, GroupName = "Fixes", Name = "Rate limit logs with flooding potential", Description = "Rate limited excessive logging from MyDefinitionManager.GetBlueprintDefinition")]
        public bool FixLogFlooding
        {
            get => fixLogFlooding;
            set => SetValue(ref fixLogFlooding, value);
        }

        [Display(Order = 27, GroupName = "Fixes", Name = "Disable tracking of wheel trails on server", Description = "Disable the tracking of wheel trails on server, where they are not needed at all (trails are only visual elements)")]
        public bool FixWheelTrail
        {
            get => fixWheelTrail;
            set => SetValue(ref fixWheelTrail, value);
        }

        [Display(Order = 28, GroupName = "Fixes", Name = "Disable functional blocks in projected grids (does not affect welding)", Description = "Disable functional blocks in projected grids without affecting the blocks built from the projection")]
        public bool FixProjection
        {
            get => fixProjection;
            set => SetValue(ref fixProjection, value);
        }

        [Display(Order = 29, GroupName = "Fixes", Name = "Reduce the GC pressure of air tightness (needs restart)", Description = "Reuses collections in the air tightness calculations to reduce GC pressure on opening/closing doors (needs restart)")]
        public bool FixAirtight
        {
            get => fixAirtight;
            set => SetValue(ref fixAirtight, value);
        }

        /*BOOL_OPTION
        [Display(Order = 30, GroupName = "Fixes", Name = "Option label", Description = "Option tooltip")]
        public bool OptionName
        {
            get => optionName;
            set => SetValue(ref optionName, value);
        }

        BOOL_OPTION*/
    }
}