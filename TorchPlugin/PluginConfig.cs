using System;
using System.IO;
using System.Xml.Serialization;
using Shared.Logging;
using Torch;
using Torch.Views;

namespace TorchPlugin
{
    [Serializable]
    public class PluginConfig : ViewModel
    {
        private static IPluginLogger Log => Plugin.Log;
        private static readonly string ConfigFileName = $"{Plugin.PluginName}.cfg";
        private static string ConfigFilePath => Path.Combine(Plugin.Instance.StoragePath, ConfigFileName);

        // ReSharper disable once InconsistentNaming
        private static PluginConfig __instance;
        public static PluginConfig Instance => __instance ?? (__instance = new PluginConfig());

        private static XmlSerializer ConfigSerializer => new XmlSerializer(typeof(PluginConfig));

        #region Properties

        private bool enabled = true;

        [Display(Description = "Enables/disables the plugin", Name = "Enable Plugin", Order = 1)]
        public bool Enabled
        {
            get => enabled;
            set
            {
                enabled = value;
                OnPropertyChanged(nameof(Enabled));
            }
        }

        // TODO: Define your properties here

        #endregion

        #region Persistence

        protected override void OnPropertyChanged(string propName = "")
        {
            Save();
        }

        public void Save()
        {
            var path = ConfigFilePath;
            try
            {
                using (var streamWriter = new StreamWriter(path))
                {
                    ConfigSerializer.Serialize(streamWriter, __instance);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to save configuration file: {0}", path);
            }
        }

        public void Load()
        {
            var path = ConfigFilePath;
            try
            {
                if (!File.Exists(path))
                {
                    Log.Warning("Missing configuration file, saving defaults: {0}", path);
                    Save();
                    return;
                }

                using (var streamReader = new StreamReader(path))
                {
                    if (!(ConfigSerializer.Deserialize(streamReader) is PluginConfig config))
                    {
                        Log.Error("Failed to deserialize configuration file: {0}", path);
                        return;
                    }

                    __instance = config;
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to load configuration file:", path);
            }
        }

        #endregion
    }
}