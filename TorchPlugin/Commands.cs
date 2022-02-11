using Shared.Config;
using Shared.Plugin;
using Torch.Commands;
using Torch.Commands.Permissions;
using VRage.Game.ModAPI;

namespace TorchPlugin
{
    [Category("pfi")]
    public class Commands : CommandModule
    {
        private static IPluginConfig Config => Common.Config;

        private void Respond(string message)
        {
            Context?.Respond(message);
        }

        private void RespondWithInfo()
        {
            var config = Plugin.Instance.Config;
            Respond($"{Plugin.PluginName} plugin is enabled: {Format(config.Enabled)}");
#if WORKS_BUT_INCREASES_SIMULATION_LOAD
            Respond($"spin_wait: {Format(config.FixSpinWait)}");
#endif
            Respond($"grid_merge: {Format(config.FixGridMerge)}");
            Respond($"grid_paste: {Format(config.FixGridPaste)}");
            Respond($"p2p_stats: {Format(config.FixP2PUpdateStats)}");
            Respond($"gc: {Format(config.FixGarbageCollection)}");
            Respond($"api_stats: {Format(config.DisableModApiStatistics)}");
        }

        // Custom formatters
        private static string Format(bool value) => value ? "Yes" : "No";

        // Custom parsers
        private static bool TryParseBool(string text, out bool result)
        {
            switch (text.ToLower())
            {
                case "1":
                case "on":
                case "yes":
                case "y":
                case "true":
                case "t":
                    result = true;
                    return true;

                case "0":
                case "off":
                case "no":
                case "n":
                case "false":
                case "f":
                    result = false;
                    return true;
            }

            result = false;
            return false;
        }

        // ReSharper disable once UnusedMember.Global
        [Command("info", "Prints the current settings")]
        [Permission(MyPromoteLevel.None)]
        public void Info()
        {
            RespondWithInfo();
        }

        // ReSharper disable once UnusedMember.Global
        [Command("enable", "Enables the plugin")]
        [Permission(MyPromoteLevel.Admin)]
        public void Enable()
        {
            Config.Enabled = true;
            RespondWithInfo();
        }

        // ReSharper disable once UnusedMember.Global
        [Command("disable", "Disables the plugin")]
        [Permission(MyPromoteLevel.Admin)]
        public void Disable()
        {
            Config.Enabled = false;
            RespondWithInfo();
        }

        // ReSharper disable once UnusedMember.Global
        [Command("fix", "Enables or disables a fix")]
        [Permission(MyPromoteLevel.Admin)]
        public void Fix(string name, string flag)
        {
            if (!TryParseBool(flag, out var parsedFlag))
            {
                Respond($"Invalid boolean value: {flag}");
                return;
            }

            switch (name)
            {
#if WORKS_BUT_INCREASES_SIMULATION_LOAD
                case "spin_wait":
                    Config.FixSpinWait = parsedFlag;
                    break;
#endif

                case "grid_merge":
                    Config.FixGridMerge = parsedFlag;
                    break;

                case "grid_paste":
                    Config.FixGridPaste = parsedFlag;
                    break;

                case "p2p_stats":
                    Config.FixP2PUpdateStats = parsedFlag;
                    break;

                case "gc":
                    Config.FixGarbageCollection = parsedFlag;
                    break;

                case "api_stats":
                    Config.DisableModApiStatistics = parsedFlag;
                    break;

                default:
                    Respond($"Unknown fix: {name}");
                    Respond($"Valid fix names:");
#if WORKS_BUT_INCREASES_SIMULATION_LOAD
                    Respond($"  spin_wait");
#endif
                    Respond($"  grid_merge");
                    Respond($"  grid_paste");
                    Respond($"  p2p_stats");
                    Respond($"  gc");
                    Respond($"  api_stats");
                    return;
            }

            RespondWithInfo();
        }
    }
}