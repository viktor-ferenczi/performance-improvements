using System.ComponentModel;

namespace Shared.Config
{
    public interface IPluginConfig : INotifyPropertyChanged
    {
        // Enables the plugin
        bool Enabled { get; set; }

        // Enables checking for code changes (disable this in the XML config file on Proton/Linux)
        bool DetectCodeChanges { get; set; }

        // Disables conveyor updates during grid merge (MyCubeGrid.MergeGridInternal)
        bool FixGridMerge { get; set; }

        // Disables updates during grid paste (MyCubeGrid.PasteBlocksServer)
        bool FixGridPaste { get; set; }

        // Disables all explicit GC.* calls, which may cause long pauses on starting and stopping large worlds
        bool FixP2PUpdateStats { get; set; }

        // Throttles thruster grid updates to only happen once a second
        bool FixGarbageCollection { get; set; }

        // Disables resource updates while grids are being moved between groups
        bool FixGridGroups { get; set; }

        // Caches compiled mods for faster world load
        bool CacheMods { get; set; }

        // Caches compiled in-game scripts (PB programs) to reduce lag
        bool CacheScripts { get; set; }

        // Disables Mod API statistics
        bool DisableModApiStatistics { get; set; }

        // Caches frequent recalculations in safe zones
        bool FixSafeZone { get; set; }

        // Reduces memory allocations in the turret targeting system (needs restart)
        bool FixTargeting { get; set; }

        // Caches the result of MyWindTurbine.IsInAtmosphere
        bool FixWindTurbine { get; set; }

        // Reduces memory allocations in IMyStorageExtensions.GetMaterialAt
        bool FixVoxel { get; set; }

        // Optimizes the MyPhysicsBody.RigidBody getter (needs restart)
        bool FixPhysics { get; set; }

        // Disables character footprint logic on server side (needs restart)
        bool FixCharacter { get; set; }

        // Optimizes frequent memory allocations
        bool FixMemory { get; set; }

        // Caches the result of MyCubeBlock.GetUserRelationToOwner and MyTerminalBlock.HasPlayerAccessReason
        bool FixAccess { get; set; }

        // Suppresses frequent calls to MyPlayerCollection.SendDirtyBlockLimits
        bool FixBlockLimit { get; set; }

        // Caches the result of MySafeZone.IsActionAllowed and MySessionComponentSafeZones.IsActionAllowedForSafezone for 2 seconds
        bool FixSafeAction { get; set; }

        // Suppresses frequent calls to MyGridTerminalSystem.UpdateGridBlocksOwnership updating IsAccessibleForProgrammableBlock unnecessarily often
        bool FixTerminal { get; set; }

        // Disables UpdateVisibility of LCD surfaces on multiplayer servers
        bool FixTextPanel { get; set; }

        // Caches conveyor network lookups
        bool FixConveyor { get; set; }

        // Rate limited excessive logging from MyDefinitionManager.GetBlueprintDefinition
        bool FixLogFlooding { get; set; }

        // Disable the tracking of wheel trails on server, where they are not needed at all (trails are only visual elements)
        bool FixWheelTrail { get; set; }

        // Disable functional blocks in projected grids without affecting the blocks built from the projection
        bool FixProjection { get; set; }

        // Reuses collections in the air tightness calculations to reduce GC pressure on opening/closing doors (needs restart)
        bool FixAirtight { get; set; }

        /*BOOL_OPTION
        // Option tooltip
        bool OptionName { get; set; }

        BOOL_OPTION*/
    }
}