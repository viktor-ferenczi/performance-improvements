using System;
using System.IO;
using ClientPlugin.GUI;
using HarmonyLib;
using Sandbox.Graphics.GUI;
using Shared.Config;
using Shared.Logging;
using Shared.Patches;
using Shared.Patches.Patching;
using Shared.Plugin;
using VRage.FileSystem;
using VRage.Plugins;

namespace ClientPlugin
{
    // ReSharper disable once UnusedType.Global
    public class Plugin : IPlugin, ICommonPlugin
    {
        public const string Name = "PerformanceImprovements";
        public static Plugin Instance { get; private set; }

        public long Tick { get; private set; }

        public IPluginLogger Log => Logger;
        private static readonly IPluginLogger Logger = new PluginLogger(Name);

        public PluginConfig Config => config?.Data;
        private PersistentConfig<PluginConfig> config;
        private static readonly string ConfigFileName = $"{Name}.cfg";

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public void Init(object gameInstance)
        {
            Instance = this;

            Log.Info("Loading");

            var configPath = Path.Combine(MyFileSystem.UserDataPath, ConfigFileName);
            config = PersistentConfig<PluginConfig>.Load(Log, configPath);

            Common.SetPlugin(this);

            if (Config.Enabled)
                Config.Patcher.ApplyEnabled();

            Log.Debug("Successfully loaded");
        }

        public void Dispose()
        {
            try
            {
                // TODO: Save state and close resources here, called when the game exists (not guaranteed!)
                // IMPORTANT: Do NOT call harmony.UnpatchAll() here! It may break other plugins.
            }
            catch (Exception ex)
            {
                Log.Critical(ex, "Dispose failed");
            }

            Instance = null;
        }

        public void Update()
        {
        }

        private void CustomUpdate()
        {
#if CAUSES_SIMLOAD_INCREASE
#if DEBUG
            MySpinWaitPatch.LogStats(Tick, 600);
#endif
#endif

            // MyPathFindingSystemPatch.LogStats(300);
            // MyPathFindingSystemEnumeratorPatch.LogStats(300);
        }

        // ReSharper disable once UnusedMember.Global
        public void OpenConfigDialog()
        {
            var screen = new MyPluginConfigDialog(Config.Patcher);
            screen.Closed += ScreenOnClosed;
            MyGuiSandbox.AddScreen(screen);
        }

        private void ScreenOnClosed(MyGuiScreenBase source, bool isUnloading)
        {
            source.Closed -= ScreenOnClosed;
            config.Save();
        }
    }
}