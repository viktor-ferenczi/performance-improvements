using System;
using System.Xml.Serialization;
using Shared.Config;
using Shared.Patches.Patching;
using Torch;
using Torch.Views;

namespace TorchPlugin
{
    [Serializable]
    public class PluginConfig : ViewModel
    {
        private bool enabled;
        [XmlElement("Options")]
// maybe later
#if USE_HARMONY || true
        public HarmonyPatcher Patcher { get; set; } = new HarmonyPatcher();
#else
        public TorchPatcher Patcher { get; set; } = new TorchPatcher();
#endif
        public bool Enabled
        {
            get => enabled;
            set => SetValue(ref enabled, value);
        }
    }
}