using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#if !TORCH

namespace Shared.Config
{
    public class PluginConfig : IPluginConfig
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void SetValue<T>(ref T field, T value, [CallerMemberName] string propName = "")
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return;

            field = value;

            OnPropertyChanged(propName);
        }

        private void OnPropertyChanged([CallerMemberName] string propName = "")
        {
            PropertyChangedEventHandler propertyChanged = PropertyChanged;
            if (propertyChanged == null)
                return;

            propertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private bool enabled = true;
        private bool fixSpinWait = true;
        private bool fixGridMerge = true;
        private bool fixGridPaste = true;
        private bool fixP2PUpdateStats = true;
        private bool fixGarbageCollection = true;
        private bool fixThrusters = true;
        private bool disableModApiStatistics = true;

        public bool Enabled
        {
            get => enabled;
            set => SetValue(ref enabled, value);
        }

        public bool FixSpinWait
        {
            get => fixSpinWait;
            set => SetValue(ref fixSpinWait, value);
        }

        public bool FixGridMerge
        {
            get => fixGridMerge;
            set => SetValue(ref fixGridMerge, value);
        }

        public bool FixGridPaste
        {
            get => fixGridPaste;
            set => SetValue(ref fixGridPaste, value);
        }

        public bool FixP2PUpdateStats
        {
            get => fixP2PUpdateStats;
            set => SetValue(ref fixP2PUpdateStats, value);
        }

        public bool FixGarbageCollection
        {
            get => fixGarbageCollection;
            set => SetValue(ref fixGarbageCollection, value);
        }
        
        public bool FixThrusters
        {
            get => fixThrusters;
            set => SetValue(ref fixThrusters, value);
        }

        public bool DisableModApiStatistics
        {
            get => disableModApiStatistics;
            set => SetValue(ref disableModApiStatistics, value);
        }
    }
}

#endif