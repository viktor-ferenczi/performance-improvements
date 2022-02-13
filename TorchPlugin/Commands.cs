using Shared.Config;
using Shared.Extensions;
using Shared.Patches;
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
            Respond($"{Plugin.PluginName} plugin is enabled: {config.Enabled.ToYesNo()}");
#if CAUSES_SIMLOAD_INCREASE
            Respond($"spin_wait: {Format(config.FixSpinWait)}");
#endif
            Respond($"grid_merge: {config.FixGridMerge.ToYesNo()}");
            Respond($"grid_paste: {config.FixGridPaste.ToYesNo()}");
            Respond($"p2p_stats: {config.FixP2PUpdateStats.ToYesNo()}");
            Respond($"gc: {config.FixGarbageCollection.ToYesNo()}");
            Respond($"thrusters: {config.FixThrusters.ToYesNo()}");
            Respond($"grid_groups: {config.FixGridGroups.ToYesNo()}");
            //BOOL_OPTION Respond($"option_name: {Format(config.OptionName)}");
            Respond($"api_stats: {config.DisableModApiStatistics.ToYesNo()}");
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
            if (!ParsingTools.TryParseBool(flag, out var parsedFlag))
            {
                Respond($"Invalid boolean value: {flag}");
                return;
            }

            switch (name)
            {
#if CAUSES_SIMLOAD_INCREASE
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
                
                case "thrusters":
                    Config.FixThrusters = parsedFlag;
                    break;

                case "grid_groups":
                    Config.FixGridGroups = parsedFlag;
                    break;

                /*BOOL_OPTION
                case "option_name":
                    Config.OptionName = parsedFlag;
                    break;

                BOOL_OPTION*/
                case "api_stats":
                    Config.DisableModApiStatistics = parsedFlag;
                    break;

                default:
                    Respond($"Unknown fix: {name}");
                    Respond($"Valid fix names:");
#if CAUSES_SIMLOAD_INCREASE
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