using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Shared.Patches.Patching;

#if !TORCH

namespace Shared.Config
{
    public class PluginConfig : INotifyPropertyChanged
    {
        [XmlElement("Options")] 
        public HarmonyPatcher Patcher { get; set; } = new();
        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool Enabled { get; set; }
    }
}

#endif