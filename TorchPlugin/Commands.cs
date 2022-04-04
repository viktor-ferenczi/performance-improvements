using Shared.Config;
using Shared.Plugin;
using Torch.Commands;
using Torch.Commands.Permissions;
using VRage.Game.ModAPI;

namespace TorchPlugin
{
    [System.ComponentModel.Category("pfi")]
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
            Respond($"grid_merge: {Format(config.FixGridMerge)}");
            Respond($"grid_paste: {Format(config.FixGridPaste)}");
            Respond($"p2p_stats: {Format(config.FixP2PUpdateStats)}");
            Respond($"gc: {Format(config.FixGarbageCollection)}");
            Respond($"grid_groups: {Format(config.FixGridGroups)}");
            Respond($"cache_mods: {Format(config.CacheMods)}");
            Respond($"cache_scripts: {Format(config.CacheScripts)}");
            Respond($"api_stats: {Format(config.DisableModApiStatistics)}");
            Respond($"safe_zone: {Format(config.FixSafeZone)}");
            Respond($"targeting: {Format(config.FixTargeting)}");
            Respond($"wind_turbine: {Format(config.FixWindTurbine)}");
            Respond($"voxel: {Format(config.FixVoxel)}");
            Respond($"physics: {Format(config.FixPhysics)}");
            Respond($"entity: {Format(config.FixEntity)}");
            Respond($"character: {Format(config.FixCharacter)}");
            Respond($"memory: {Format(config.FixMemory)}");
            Respond($"endshoot: {Format(config.FixEndShoot)}");
            //BOOL_OPTION Respond($"option_name: {Format(config.OptionName)}");
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
                
                case "grid_groups":
                    Config.FixGridGroups = parsedFlag;
                    break;

                case "cache_mods":
                    Config.CacheMods = parsedFlag;
                    break;

                case "cache_scripts":
                    Config.CacheScripts = parsedFlag;
                    break;

                case "api_stats":
                    Config.DisableModApiStatistics = parsedFlag;
                    break;

                case "safe_zone":
                    Config.FixSafeZone = parsedFlag;
                    break;

                case "targeting":
                    Config.FixTargeting = parsedFlag;
                    break;

                case "wind_turbine":
                    Config.FixWindTurbine = parsedFlag;
                    break;

                case "voxel":
                    Config.FixVoxel = parsedFlag;
                    break;

                case "physics":
                    Config.FixPhysics = parsedFlag;
                    break;

                case "entity":
                    Config.FixEntity = parsedFlag;
                    break;

                case "character":
                    Config.FixCharacter = parsedFlag;
                    break;

                case "memory":
                    Config.FixMemory = parsedFlag;
                    break;

                case "endshoot":
                    Config.FixEndShoot = parsedFlag;
                    break;

                /*BOOL_OPTION
                case "option_name":
                    Config.OptionName = parsedFlag;
                    break;

                BOOL_OPTION*/
                default:
                    Respond($"Unknown fix: {name}");
                    Respond("Valid fix names:");
                    Respond("  grid_merge");
                    Respond("  grid_paste");
                    Respond("  p2p_stats");
                    Respond("  gc");
                    Respond("  grid_groups");
                    Respond("  grid_groups");
                    Respond("  cache_mods");
                    Respond("  cache_scripts");
                    Respond("  api_stats");
                    Respond("  safe_zone");
                    Respond("  targeting");
                    Respond("  wind_turbine");
                    Respond("  voxel");
                    Respond("  physics");
                    Respond("  entity");
                    Respond("  character");
                    Respond("  memory");
                    Respond("  endshoot");
                    //BOOL_OPTION Respond("  option_name");
                    return;
            }

            RespondWithInfo();
        }
    }
}