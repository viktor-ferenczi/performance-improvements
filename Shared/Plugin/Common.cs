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

        public static string GameVersion { get; private set; }
        public const string PluginVersion = "1.11.16.0";

        public static string DataDir { get; private set; }
        public static string CacheDir { get; private set; }
        public static string DebugDir { get; private set; }

        private static string CacheGameVersionPath => Path.Combine(CacheDir, "GameVersion.txt");
        private static string PluginVersionPath => Path.Combine(DebugDir, "PluginVersion.txt");

        public static void SetPlugin(ICommonPlugin plugin, string gameVersion, string storageDir)
        {
            Plugin = plugin;
            Logger = plugin.Log;
            Config = plugin.Config;

            GameVersion = gameVersion;

            DataDir = Path.Combine(storageDir, "PerformanceImprovements");
            CacheDir = Path.Combine(DataDir, "Cache");
            DebugDir = Path.Combine(DataDir, "Debug");

            var hasGameVersionChanged = !File.Exists(CacheGameVersionPath) || File.ReadAllText(CacheGameVersionPath) != GameVersion;
            var hasPluginVersionChanged = !File.Exists(PluginVersionPath) || File.ReadAllText(PluginVersionPath) != PluginVersion;

            CleanupCache(hasGameVersionChanged);
            CleanupDebug(hasGameVersionChanged || hasPluginVersionChanged);

            PatchHelpers.Configure();
        }

        private static void CleanupCache(bool clear)
        {
            Directory.CreateDirectory(CacheDir);

            var now = DateTime.UtcNow;
            foreach (var path in Directory.EnumerateFiles(CacheDir, "*.cache", SearchOption.AllDirectories))
            {
                if (clear || (now - File.GetCreationTimeUtc(path)).TotalDays >= CacheExpirationDays)
                    File.Delete(path);
            }

            if (clear)
                File.WriteAllText(CacheGameVersionPath, GameVersion);
        }

        private static void CleanupDebug(bool clear)
        {
            Directory.CreateDirectory(DebugDir);

            if (!clear)
                return;

            foreach (var path in Directory.EnumerateFiles(DebugDir, "*.il", SearchOption.AllDirectories))
            {
                File.Delete(path);
            }

            File.WriteAllText(PluginVersionPath, PluginVersion);
        }
    }
}