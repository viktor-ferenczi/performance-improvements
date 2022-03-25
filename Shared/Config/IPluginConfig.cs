using System.ComponentModel;

namespace Shared.Config
{
    public interface IPluginConfig : INotifyPropertyChanged
    {
        // Enables the plugin
        bool Enabled { get; set; }

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

        // Optimizes the MyPhysicsBody.RigidBody getter and the HkShape comparer (needs restart)
        bool FixPhysics { get; set; }

        // Optimizes MyEntity.InScene getter (needs restart)
        bool FixEntity { get; set; }

        // Disables character footprint logic on server side (needs restart)
        bool FixCharacter { get; set; }

        // Optimizes frequent memory allocations
        bool FixMemory { get; set; }

        /*BOOL_OPTION
        // Option tooltip
        bool OptionName { get; set; }

        BOOL_OPTION*/
    }
}