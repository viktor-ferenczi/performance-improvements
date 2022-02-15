using System.Windows;
using System.Windows.Controls;
using Shared.Plugin;

namespace TorchPlugin
{
    // ReSharper disable once UnusedType.Global
    // ReSharper disable once RedundantExtendsListEntry
    public partial class ConfigView : UserControl
    {
        public ConfigView()
        {
            InitializeComponent();
        }

        private void SaveOnClick(object sender, RoutedEventArgs e)
        {
            Plugin.Instance.Save();
        }
    }
}