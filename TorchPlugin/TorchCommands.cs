using Torch.Commands;
using Torch.Commands.Permissions;
using VRage.Game.ModAPI;

namespace TorchPlugin
{
    [Category(Plugin.PluginName)]
    public class TorchCommands : CommandModule
    {
        private void Respond(string message)
        {
            Context?.Respond(message);
        }

        private void RespondWithInfo()
        {
            var config = PluginConfig.Instance;
            Respond($"{Plugin.PluginName} plugin is enabled: {Format(config.Enabled)}");
            // TODO: Respond with your current configuration values
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
            PluginConfig.Instance.Enabled = true;
            RespondWithInfo();
        }

        // ReSharper disable once UnusedMember.Global
        [Command("disable", "Disables the plugin")]
        [Permission(MyPromoteLevel.Admin)]
        public void Disable()
        {
            PluginConfig.Instance.Enabled = false;
            RespondWithInfo();
        }

        // TODO: Add your chat commands here
    }
}