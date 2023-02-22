using System;
using System.IO;
using Shared.Config;
using Shared.Logging;
using Shared.Patches;

namespace Shared.Plugin
{
    public static class Common
    {
        private const int CacheExpirationDays = 90;

        public static ICommonPlugin Plugin { get; private set; }
        public static IPluginLogger Logger { get; private set; }
        public static IPluginConfig Config { get; private set; }

        public static string GameVersion;
        
        public static string DataDir;
        public static string CacheDir;
        public static bool BetaVersion;

        private static string CacheGameVersionPath => Path.Combine(CacheDir, "GameVersion.txt");

        public static void SetPlugin(ICommonPlugin plugin, string gameVersion, string storageDir)
        {
            Plugin = plugin;
            Logger = plugin.Log;
            Config = plugin.Config;
            
            GameVersion = gameVersion;
            BetaVersion = string.Compare(GameVersion, "01_202_000", StringComparison.Ordinal) >= 0;
            if (BetaVersion)
            {
                Logger.Info("Beta game version detected, optimizing accordingly");
            }

            DataDir = Path.Combine(storageDir, "PerformanceImprovements");
            CacheDir = Path.Combine(DataDir, "Cache");

            CleanupCache();
            
            PatchHelpers.Configure();
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