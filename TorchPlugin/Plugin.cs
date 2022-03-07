#define USE_HARMONY

using System;
using System.IO;
using System.Text;
using System.Windows.Controls;
using HarmonyLib;
using Sandbox.Game;
using Shared.Config;
using Shared.Logging;
using Shared.Patches;
using Shared.Plugin;
using Torch;
using Torch.API;
using Torch.API.Managers;
using Torch.API.Plugins;
using Torch.API.Session;
using Torch.Session;
using VRage.Utils;

namespace TorchPlugin
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Plugin : TorchPluginBase, IWpfPlugin, ICommonPlugin
    {
        public const string PluginName = "PerformanceImprovements";
        public static Plugin Instance { get; private set; }

        public long Tick { get; private set; }

        public IPluginLogger Log => Logger;
        private static readonly IPluginLogger Logger = new PluginLogger(PluginName);

        public IPluginConfig Config => config?.Data;
        private PersistentConfig<PluginConfig> config;
        private static readonly string ConfigFileName = $"{PluginName}.cfg";

        // ReSharper disable once UnusedMember.Global
        public UserControl GetControl() => control ?? (control = new ConfigView());
        private ConfigView control;

        private TorchSessionManager sessionManager;

        private bool initialized;
        private bool failed;

        // ReSharper disable once UnusedMember.Local
        private readonly Commands commands = new Commands();

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override void Init(ITorchBase torch)
        {
            base.Init(torch);

            Instance = this;

            Log.Info("Init");

            var configPath = Path.Combine(StoragePath, ConfigFileName);
            config = PersistentConfig<PluginConfig>.Load(Log, configPath);

            Common.SetPlugin(this);

#if USE_HARMONY
            if (!PatchHelpers.HarmonyPatchAll(Log, new Harmony(Name)))
            {
                failed = true;
                return;
            }
#endif

            sessionManager = torch.Managers.GetManager<TorchSessionManager>();
            sessionManager.SessionStateChanged += SessionStateChanged;

            try
            {
                var gameVersionNumber = MyPerGameSettings.BasicGameInfo.GameVersion ?? 0;
                var gameVersion = new StringBuilder(MyBuildNumbers.ConvertBuildNumberFromIntToString(gameVersionNumber)).ToString();
                Common.Init(gameVersion, StoragePath);
                PatchHelpers.PatchInits();
                Initialize();
                initialized = true;
            }
            catch (Exception e)
            {
                Log.Error(e, "Initialization failed");
                failed = true;
            }
        }

        private void Initialize()
        {
            // TODO: Put your one time initialization here
        }

        private void SessionStateChanged(ITorchSession session, TorchSessionState newstate)
        {
            switch (newstate)
            {
                case TorchSessionState.Loading:
                    Log.Debug("Loading");
                    break;

                case TorchSessionState.Loaded:
                    Log.Debug("Loaded");
                    break;

                case TorchSessionState.Unloading:
                    Log.Debug("Unloading");
                    break;

                case TorchSessionState.Unloaded:
                    Log.Debug("Unloaded");
                    break;
            }
        }

        public override void Dispose()
        {
            if (initialized)
            {
                Log.Debug("Disposing");

                sessionManager.SessionStateChanged -= SessionStateChanged;
                sessionManager = null;

                Log.Debug("Disposed");
            }

            Instance = null;

            base.Dispose();
        }

        public override void Update()
        {
            try
            {
                if (!failed)
                {
                    CustomUpdate();
                    Tick++;
                }
            }
            catch (Exception e)
            {
                Log.Error(e, "Update failed");
                failed = true;
            }
        }

        private void CustomUpdate()
        {
            PatchHelpers.PatchUpdates();
        }
    }
}