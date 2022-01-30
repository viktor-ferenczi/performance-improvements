using System;
using System.Reflection;
using HarmonyLib;
using Shared.Logging;
using VRage.Plugins;

namespace DedicatedPlugin
{
    // ReSharper disable once UnusedType.Global
    public class Plugin : IPlugin
    {
        private const string Name = "PluginTemplate";
        private static readonly IPluginLogger Log = new KeenPluginLogger(Name);
        private static readonly Harmony Harmony = new Harmony(Name);
        private static readonly object InitializationMutex = new object();
        private static bool initialized;
        public static Plugin Instance;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public void Init(object gameInstance)
        {
            Instance = this;

            Log.Info("Loading");

            Log.Debug("Patching");
            try
            {
                Harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Log.Critical("Patching failed", ex);
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
                Log.Critical("Dispose failed", ex);
            }

            Instance = null;
        }

        public void Update()
        {
            EnsureInitialized();
            try
            {
                CustomUpdate();
            }
            catch (Exception ex)
            {
                Log.Critical("Update failed", ex);
            }
        }

        private void EnsureInitialized()
        {
            lock (InitializationMutex)
            {
                if (initialized)
                    return;

                Log.Info("Initializing");
                try
                {
                    Initialize();
                }
                catch (Exception ex)
                {
                    Log.Critical("Failed to initialize plugin", ex);
                }
                Log.Debug("Successfully initialized");
                initialized = true;
            }
        }

        private void Initialize()
        {
            // TODO: Put your one time initialization code here. It is executed on first update, not on loading the plugin.
        }

        private void CustomUpdate()
        {
            // TODO: Put your update code here. It is called on every simulation frame!
        }
    }
}