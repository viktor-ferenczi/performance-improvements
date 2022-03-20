#if !TORCH

using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

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
        private bool fixGridMerge = true;
        private bool fixGridPaste = true;
        private bool fixP2PUpdateStats = true;
        private bool fixGarbageCollection = true;
        private bool fixGridGroups = true;
        private bool cacheMods = true;
        private bool cacheScripts = true;
        private bool disableModApiStatistics = true;
        private bool fixSafeZone = true;
        private bool fixTargeting = true;
//BOOL_OPTION private bool optionName = true;

        public bool Enabled
        {
            get => enabled;
            set => SetValue(ref enabled, value);
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

        public bool FixGridGroups
        {
            get => fixGridGroups;
            set => SetValue(ref fixGridGroups, value);
        }

        public bool CacheMods
        {
            get => cacheMods;
            set => SetValue(ref cacheMods, value);
        }

        public bool CacheScripts
        {
            get => cacheScripts;
            set => SetValue(ref cacheScripts, value);
        }

        public bool DisableModApiStatistics
        {
            get => disableModApiStatistics;
            set => SetValue(ref disableModApiStatistics, value);
        }

        public bool FixSafeZone
        {
            get => fixSafeZone;
            set => SetValue(ref fixSafeZone, value);
        }

        public bool FixTargeting
        {
            get => fixTargeting;
            set => SetValue(ref fixTargeting, value);
        }

        /*BOOL_OPTION
        public bool OptionName
        {
            get => optionName;
            set => SetValue(ref optionName, value);
        }

        BOOL_OPTION*/
    }
}

#endif