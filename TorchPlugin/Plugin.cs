#define USE_HARMONY

using System;
using System.IO;
using System.Windows.Controls;
using HarmonyLib;
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

namespace TorchPlugin
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Plugin : TorchPluginBase, IWpfPlugin, ICommonPlugin
    {
        public const string PluginName = "PerformanceImprovements";
        public static long Tick { get; private set; }
        public static Plugin Instance { get; private set; }

        public IPluginLogger Log => Logger;
        private static readonly IPluginLogger Logger = new PluginLogger(PluginName);

        public IPluginConfig Config => config?.Data;
        private PersistentConfig<PluginConfig> config;
        private static readonly string ConfigFileName = $"{PluginName}.cfg";

        // ReSharper disable once UnusedMember.Global
        public UserControl GetControl() => control ?? (control = new ConfigView());
        private ConfigView control;

        private TorchSessionManager sessionManager;
        private bool Initialized => sessionManager != null;
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
                    OnLoaded();
                    break;

                case TorchSessionState.Unloading:
                    Log.Debug("Unloading");
                    OnUnloading();
                    break;

                case TorchSessionState.Unloaded:
                    Log.Debug("Unloaded");
                    break;
            }
        }

        public override void Dispose()
        {
            Instance = null;

            if (Initialized)
            {
                Log.Debug("Disposing");

                sessionManager.SessionStateChanged -= SessionStateChanged;
                sessionManager = null;

                Log.Debug("Disposed");
            }

            base.Dispose();
        }

        private void OnLoaded()
        {
            try
            {
                // TODO: Put your one time initialization here
            }
            catch (Exception e)
            {
                Log.Error(e, "OnLoaded failed");
                failed = true;
            }
        }

        private void OnUnloading()
        {
            try
            {
                // TODO: Make sure to save any persistent modifications here
            }
            catch (Exception e)
            {
                Log.Error(e, "OnUnloading failed");
                failed = true;
            }
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
#if CAUSES_SIMLOAD_INCREASE
#if DEBUG
            MySpinWaitPatch.LogStats(Tick, 600);
#endif
#endif

            // MyPathFindingSystemPatch.LogStats(300);
            // MyPathFindingSystemEnumeratorPatch.LogStats(300);
        }
    }
}