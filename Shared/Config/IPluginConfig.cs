namespace Shared.Config
{
    public interface IPluginConfig
    {
        // Enables the plugin
        bool Enabled { get; set; }

        // Enables the MySpinWait.SpinOnce fix
        bool FixSpinWait { get; set; }

        // Enables the MyCubeGrid.MergeGridInternal fix
        bool FixGridMerge { get; set; }

        // Enables the MyCubeGrid.PasteBlocksServer fix
        bool FixGridPaste { get; set; }

        // Enables the VRage.EOS.MyP2PQoSAdapter.UpdateStats fix
        bool FixP2PUpdateStats { get; set; }
    }
}