using Torch.Commands;
using Torch.Commands.Permissions;
using VRage.Game.ModAPI;

namespace TorchPlugin
{
    [Category("pfi")]
    public class Commands : CommandModule
    {
        private void Respond(string message)
        {
            Context?.Respond(message);
        }

        private void RespondWithInfo()
        {
            var config = Plugin.Config;
            Respond($"{Plugin.PluginName} plugin is enabled: {Format(config.Enabled)}");
            Respond($"Fix spin_lock: {Format(config.FixSpinWait)}");
            Respond($"Fix grid_merge: {Format(config.FixGridMerge)}");
            Respond($"Fix grid_paste: {Format(config.FixGridPaste)}");
            Respond($"Fix p2p_update_stats: {Format(config.FixP2PUpdateStats)}");
        }

        private static string Format(bool value) => value ? "Yes" : "No";

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
            Plugin.Config.Enabled = true;
            RespondWithInfo();
        }

        // ReSharper disable once UnusedMember.Global
        [Command("disable", "Disables the plugin")]
        [Permission(MyPromoteLevel.Admin)]
        public void Disable()
        {
            Plugin.Config.Enabled = false;
            RespondWithInfo();
        }

        // ReSharper disable once UnusedMember.Global
        [Command("fix", "Enables or disables a fix")]
        [Permission(MyPromoteLevel.Admin)]
        public void Fix(string name, string flag)
        {
            var config = Plugin.Config;

            if (!TryParseBoolean(flag, out var parsedFlag))
            {
                Respond($"Invalid boolean value: {flag}");
                return;
            }

            switch (name)
            {
                case "spin_lock":
                    config.FixSpinWait = parsedFlag;
                    break;

                case "grid_merge":
                    config.FixGridMerge = parsedFlag;
                    break;

                case "grid_paste":
                    config.FixGridPaste = parsedFlag;
                    break;

                case "p2p_update_stats":
                    config.FixP2PUpdateStats = parsedFlag;
                    break;

                default:
                    Respond($"Unknown fix: {name}");
                    Respond($"Valid fix names:");
                    Respond($"  spin_lock");
                    Respond($"  grid_merge");
                    Respond($"  grid_paste");
                    Respond($"  p2p_update_stats");
                    return;
            }

            RespondWithInfo();
        }

        private static bool TryParseBoolean(string text, out bool result)
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
    }
}