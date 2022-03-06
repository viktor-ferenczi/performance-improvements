using System;
using System.IO;
using Shared.Config;
using Shared.Logging;
using VRage.FileSystem;
using VRage.Game;

namespace Shared.Plugin
{
    public static class Common
    {
        private const int CacheExpirationDays = 90;

        public static ICommonPlugin Plugin { get; private set; }
        public static IPluginLogger Logger { get; private set; }
        public static IPluginConfig Config { get; private set; }

        // MyFileSystem.UserDataPath ~= C:\Users\%USERNAME%\AppData\Roaming\SpaceEngineers
        public static string DataDir => dataDir ?? (dataDir = Path.Combine(MyFileSystem.UserDataPath, "PerformanceImprovements"));
        public static string dataDir;

        public static string CacheDir => cacheDir ?? (cacheDir = Path.Combine(DataDir, "Cache"));
        public static string cacheDir;

        private static string CacheGameVersionPath => Path.Combine(CacheDir, "GameVersion.txt");

        public static string GameVersion => MyFinalBuildConstants.APP_VERSION_STRING.ToString();

        public static void SetPlugin(ICommonPlugin plugin)
        {
            Plugin = plugin;
            Logger = plugin.Log;
            Config = plugin.Config;
        }

        public static void Init()
        {
            CleanupCache();
        }

        private static void CleanupCache()
        {
            Directory.CreateDirectory(CacheDir);

            var hasVersionChanged = !File.Exists(CacheGameVersionPath) || File.ReadAllText(CacheGameVersionPath) != GameVersion;

            var now = DateTime.UtcNow;
            foreach (var path in Directory.EnumerateFiles(CacheDir, "*.cache", SearchOption.AllDirectories))
            {
                if (hasVersionChanged || (now - File.GetCreationTimeUtc(path)).TotalDays >= CacheExpirationDays)
                    File.Delete(path);
            }

            if (hasVersionChanged)
                File.WriteAllText(CacheGameVersionPath, GameVersion);
        }
    }
}