using System;
using System.Reflection;
using HarmonyLib;
using Shared.Logging;
using Shared.Patches;
using Torch;
using Torch.API;
using Torch.API.Managers;
using Torch.API.Session;
using Torch.Session;

namespace TorchPlugin
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class Plugin : TorchPluginBase
    {
        public const string PluginName = "PerformanceImprovements";
        public static readonly IPluginLogger Log = new TorchPluginLogger(PluginName);
        public static Plugin Instance;
        public static long Tick;

        private static readonly Harmony Harmony = new Harmony(PluginName);

        private TorchSessionManager sessionManager;
        private bool Initialized => sessionManager != null;

        // ReSharper disable once UnusedMember.Local
        private readonly TorchCommands commands = new TorchCommands();

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public override void Init(ITorchBase torch)
        {
            base.Init(torch);

            Instance = this;

            Log.Info("Init");

            MySpinWaitPatch.Log = Log;
            MyCubeGridPatch.Log = Log;

            Log.Debug("Patching");
            try
            {
                Harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Log.Critical(ex, "Patching failed");
                return;
            }

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

            if (!Initialized)
                return;

            Log.Debug("Disposing");

            sessionManager.SessionStateChanged -= SessionStateChanged;
            sessionManager = null;

            Log.Debug("Disposed");

            base.Dispose();
        }

        private void OnLoaded()
        {
            // TODO: Put your one time initialization here
        }

        private void OnUnloading()
        {
            // TODO: Make sure to save any persistent modifications here
        }

        public override void Update()
        {
#if DEBUG
            MySpinWaitPatch.LogStats(Tick, 600);
#endif

            // MyPathFindingSystemPatch.LogStats(300);
            // MyPathFindingSystemEnumeratorPatch.LogStats(300);

            Tick++;
        }
    }
}