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
        private bool detectCodeChanges = true;
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
        private bool fixWindTurbine = true;
        private bool fixVoxel = true;
        private bool fixPhysics = true;
        private bool fixCharacter = true;
        private bool fixMemory = true;
        private bool fixAccess = true;
        private bool fixBlockLimit = true;
        private bool fixSafeAction = true;
        private bool fixTerminal = true;
        private bool fixTextPanel = true;
        private bool fixConveyor = true;
        private bool fixLogFlooding = true;
        private bool fixWheelTrail = true;
        private bool fixProjection = true;
        private bool fixAirtight = true;
        //BOOL_OPTION private bool optionName = true;

        public bool Enabled
        {
            get => enabled;
            set => SetValue(ref enabled, value);
        }

        public bool DetectCodeChanges
        {
            get => detectCodeChanges;
            set => SetValue(ref detectCodeChanges, value);
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

        public bool FixWindTurbine
        {
            get => fixWindTurbine;
            set => SetValue(ref fixWindTurbine, value);
        }

        public bool FixVoxel
        {
            get => fixVoxel;
            set => SetValue(ref fixVoxel, value);
        }

        public bool FixPhysics
        {
            get => fixPhysics;
            set => SetValue(ref fixPhysics, value);
        }

        public bool FixCharacter
        {
            get => fixCharacter;
            set => SetValue(ref fixCharacter, value);
        }

        public bool FixMemory
        {
            get => fixMemory;
            set => SetValue(ref fixMemory, value);
        }

        public bool FixAccess
        {
            get => fixAccess;
            set => SetValue(ref fixAccess, value);
        }

        public bool FixBlockLimit
        {
            get => fixBlockLimit;
            set => SetValue(ref fixBlockLimit, value);
        }

        public bool FixSafeAction
        {
            get => fixSafeAction;
            set => SetValue(ref fixSafeAction, value);
        }

        public bool FixTerminal
        {
            get => fixTerminal;
            set => SetValue(ref fixTerminal, value);
        }

        public bool FixTextPanel
        {
            get => fixTextPanel;
            set => SetValue(ref fixTextPanel, value);
        }

        public bool FixConveyor
        {
            get => fixConveyor;
            set => SetValue(ref fixConveyor, value);
        }

        public bool FixLogFlooding
        {
            get => fixLogFlooding;
            set => SetValue(ref fixLogFlooding, value);
        }

        public bool FixWheelTrail
        {
            get => fixWheelTrail;
            set => SetValue(ref fixWheelTrail, value);
        }

        public bool FixProjection
        {
            get => fixProjection;
            set => SetValue(ref fixProjection, value);
        }

        public bool FixAirtight
        {
            get => fixAirtight;
            set => SetValue(ref fixAirtight, value);
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