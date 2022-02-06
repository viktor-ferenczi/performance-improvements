using System.Windows.Controls;

namespace TorchPlugin
{
    // ReSharper disable once UnusedType.Global
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ConfigView : UserControl
    {
        public ConfigView()
        {
            InitializeComponent();
            DataContext = Plugin.Config;
        }
    }
}