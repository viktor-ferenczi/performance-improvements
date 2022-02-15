using Shared.Patches.Patching;
using Torch;

namespace TorchPlugin.ViewModels
{
    public class ConfigViewModel : ViewModel
    {
        public PluginConfig Config { get; set; }
        public PatchInfoTree InfoTree { get; set; }
    }
}