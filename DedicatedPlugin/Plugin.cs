using System;
using System.IO;
using System.Reflection;
using ClientPlugin.PerformanceImprovements.Shared.Config;
using HarmonyLib;
using Shared.Logging;
using Shared.Patches;
using VRage.FileSystem;
using VRage.Plugins;

namespace DedicatedPlugin
{
    // ReSharper disable once UnusedType.Global
    public class Plugin : IPlugin
    {
        public const string Name = "PerformanceImprovements";
        public static readonly IPluginLogger Log = new PluginLogger(Name);
        public static long Tick { get; private set; }

        private static readonly Harmony Harmony = new Harmony(Name);

        private static readonly string ConfigFileName = $"{Name}.cfg";
        private PersistentConfig<PluginConfig> config;
        public static PluginConfig Config => instance?.config?.Data;

        private static Plugin instance;
        private static readonly object InitializationMutex = new object();
        private static bool initialized;
        private static bool failed;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        public void Init(object gameInstance)
        {
            instance = this;

            Log.Info("Loading");

            var configPath = Path.Combine(MyFileSystem.UserDataPath, ConfigFileName);
            config = PersistentConfig<PluginConfig>.Load(Log, configPath);

            PatchHelpers.Init(Log, Config);

            Log.Debug("Patching");
            try
            {
                Harmony.PatchAll(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Log.Critical(ex, "Patching failed");
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

            instance = null;
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
            lock (InitializationMutex)
            {
                if (initialized || failed)
                    return;

                Log.Info("Initializing");
                try
                {
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
        }

        private void Initialize()
        {
            // TODO: Put your one time initialization code here. It is executed on first update, not on loading the plugin.
        }

        private void CustomUpdate()
        {
#if DEBUG
            MySpinWaitPatch.LogStats(Tick, 600);
#endif

            // MyPathFindingSystemPatch.LogStats(300);
            // MyPathFindingSystemEnumeratorPatch.LogStats(300);
        }
    }
}