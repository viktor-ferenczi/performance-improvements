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
        [XmlElement("Options")]
// maybe later
#if USE_HARMONY || true
        public HarmonyPatcher Patcher { get; set; } = new();
#else
        public TorchPatcher Patcher { get; set; } = new();
#endif
        public bool Enabled { get; set; }
    }
}