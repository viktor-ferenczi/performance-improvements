using System;
using System.IO;
using ClientPlugin.GUI;
using HarmonyLib;
using Sandbox.Graphics.GUI;
using Shared.Config;
using Shared.Logging;
using Shared.Patches;
using Shared.Plugin;
using VRage.FileSystem;
using VRage.Game;
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

        public IPluginConfig Config => config?.Data;
        private PersistentConfig<PluginConfig> config;
        private static readonly string ConfigFileName = $"{Name}.cfg";

        private static bool initialized;
        private static bool failed;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public void Init(object gameInstance)
        {
            Instance = this;

            Log.Info("Loading");

            var configPath = Path.Combine(MyFileSystem.UserDataPath, ConfigFileName);
            config = PersistentConfig<PluginConfig>.Load(Log, configPath);

            Common.SetPlugin(this);

            if (!PatchHelpers.HarmonyPatchAll(Log, new Harmony(Name)))
            {
                failed = true;
                return;
            }

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
            EnsureInitialized();
            try
            {
                if (!failed)
                {
                    CustomUpdate();
                    Tick++;
                }
            }
            catch (Exception ex)
            {
                Log.Critical(ex, "Update failed");
                failed = true;
            }
        }

        private void EnsureInitialized()
        {
            if (initialized || failed)
                return;

            Log.Info("Initializing");
            try
            {
                var gameVersion = MyFinalBuildConstants.APP_VERSION_STRING.ToString();
                Common.Init(gameVersion, MyFileSystem.UserDataPath);
                PatchHelpers.PatchInits();
                Initialize();
            }
            catch (Exception ex)
            {
                Log.Critical(ex, "Failed to initialize plugin");
                failed = true;
                return;
            }

            Log.Debug("Successfully initialized");
            initialized = true;
        }

        private void Initialize()
        {
            // TODO: Put your one time initialization code here. It is executed on first update, not on loading the plugin.
        }

        private void CustomUpdate()
        {
            PatchHelpers.PatchUpdates();
        }

        // ReSharper disable once UnusedMember.Global
        public void OpenConfigDialog()
        {
            MyGuiSandbox.AddScreen(new MyPluginConfigDialog());
        }
    }
}