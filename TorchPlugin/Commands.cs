using Shared.Config;
using Shared.Plugin;
using Torch.Commands;
using Torch.Commands.Permissions;
using VRage.Game.ModAPI;

namespace TorchPlugin
{
    public class Commands : CommandModule
    {
        private static IPluginConfig Config => Common.Config;

        private void Respond(string message)
        {
            Context?.Respond(message);
        }

        private void RespondWithHelp()
        {
            Respond("Performance Improvements commands:");
            Respond("  !pfi help");
            Respond("  !pfi info");
            Respond("    Prints the current configuration settings.");
            Respond("  !pfi enable");
            Respond("    Enables the plugin");
            Respond("  !pfi disable");
            Respond("    Disables the plugin");
            Respond("  !pfi fix <name> <value>");
            Respond("    Enables or disables a specific fix");
            RespondWithListOfFixes();
            Respond("Valid bool values:");
            Respond("  False: 0 off no n false f");
            Respond("  True: 1 on yes y false f");
        }

        private void RespondWithListOfFixes()
        {
            Respond("Supported fixes:");
            Respond("  grid_merge: Fix grid merge");
            Respond("  grid_paste: Fix grid paste");
            Respond("  gc: Fix garbage collection");
            Respond("  grid_groups: Fix grid groups");
            Respond("  cache_mods: Cache compiled mods");
            Respond("  cache_scripts: Cache compiled scripts");
            Respond("  api_stats: Disable Mod API statistics");
            Respond("  safe_zone: Fix safe zone lag");
            // Respond("  targeting: Fix allocations in targeting (needs restart)");
            Respond("  wind_turbine: Fix wind turbine performance");
            Respond("  voxel: Fix voxel performance");
            Respond("  physics: Fix physics performance (needs restart)");
            Respond("  character: Fix character performance (needs restart)");
            Respond("  memory: Fix frequent memory allocations");
            // Respond("  access: Less frequent update of block access rights");
            Respond("  block_limit: Less frequent sync of block counts for limit");
            Respond("  safe_action: Cache actions allowed by the safe zone");
            // Respond("  terminal: Less frequent update of PB access to blocks");
            Respond("  text_panel: Text panel performance fixes");
            Respond("  conveyor: Conveyor network performance fixes");
            Respond("  log_flooding: Rate limit logs with flooding potential");
            Respond("  wheel_trail: Disable tracking of wheel trails on server");
            Respond("  projection: Disable functional blocks in projected grids (does not affect welding)");
            Respond("  airtight: Reduce the GC pressure of air tightness (needs restart)");
            //BOOL_OPTION Respond("  option_name: Option label");
        }

        private void RespondWithInfo()
        {
            var config = Plugin.Instance.Config;
            Respond($"{Plugin.PluginName} plugin is enabled: {Format(config.Enabled)}");
            Respond($"grid_merge: {Format(config.FixGridMerge)}");
            Respond($"grid_paste: {Format(config.FixGridPaste)}");
            Respond($"gc: {Format(config.FixGarbageCollection)}");
            // Respond($"grid_groups: {Format(config.FixGridGroups)}");
            Respond($"cache_mods: {Format(config.CacheMods)}");
            Respond($"cache_scripts: {Format(config.CacheScripts)}");
            Respond($"api_stats: {Format(config.DisableModApiStatistics)}");
            Respond($"safe_zone: {Format(config.FixSafeZone)}");
            // Respond($"targeting: {Format(config.FixTargeting)}");
            Respond($"wind_turbine: {Format(config.FixWindTurbine)}");
            Respond($"voxel: {Format(config.FixVoxel)}");
            Respond($"physics: {Format(config.FixPhysics)}");
            Respond($"character: {Format(config.FixCharacter)}");
            Respond($"memory: {Format(config.FixMemory)}");
            // Respond($"access: {Format(config.FixAccess)}");
            Respond($"block_limit: {Format(config.FixBlockLimit)}");
            Respond($"safe_action: {Format(config.FixSafeAction)}");
            // Respond($"terminal: {Format(config.FixTerminal)}");
            Respond($"text_panel: {Format(config.FixTextPanel)}");
            Respond($"conveyor: {Format(config.FixConveyor)}");
            Respond($"log_flooding: {Format(config.FixLogFlooding)}");
            Respond($"wheel_trail: {Format(config.FixWheelTrail)}");
            Respond($"projection: {Format(config.FixProjection)}");
            Respond($"airtight: {Format(config.FixAirtight)}");
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

        [Command("pfi help", "Performance Improvements: Help")]
        [Permission(MyPromoteLevel.None)]
        public void Help()
        {
            RespondWithHelp();
        }

        // ReSharper disable once UnusedMember.Global
        [Command("pfi info", "Performance Improvements: Prints the current settings")]
        [Permission(MyPromoteLevel.None)]
        public void Info()
        {
            RespondWithInfo();
        }

        // ReSharper disable once UnusedMember.Global
        [Command("pfi enable", "Performance Improvements: Enables the plugin")]
        [Permission(MyPromoteLevel.Admin)]
        public void Enable()
        {
            Config.Enabled = true;
            RespondWithInfo();
        }

        // ReSharper disable once UnusedMember.Global
        [Command("pfi disable", "Performance Improvements: Disables the plugin")]
        [Permission(MyPromoteLevel.Admin)]
        public void Disable()
        {
            Config.Enabled = false;
            RespondWithInfo();
        }

        // ReSharper disable once UnusedMember.Global
        [Command("pfi fix", "Performance Improvements: Enables or disables a specific fix")]
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

                case "gc":
                    Config.FixGarbageCollection = parsedFlag;
                    break;

                /* Disabled due to inability to patch generics (methods of MyGroups)
                case "grid_groups":
                    Config.FixGridGroups = parsedFlag;
                    break;
                */

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

                // case "targeting":
                //     Config.FixTargeting = parsedFlag;
                //     break;

                case "wind_turbine":
                    Config.FixWindTurbine = parsedFlag;
                    break;

                case "voxel":
                    Config.FixVoxel = parsedFlag;
                    break;

                case "physics":
                    Config.FixPhysics = parsedFlag;
                    break;

                case "character":
                    Config.FixCharacter = parsedFlag;
                    break;

                case "memory":
                    Config.FixMemory = parsedFlag;
                    break;

                case "access":
                    Config.FixAccess = parsedFlag;
                    break;

                case "block_limit":
                    Config.FixBlockLimit = parsedFlag;
                    break;

                case "safe_action":
                    Config.FixSafeAction = parsedFlag;
                    break;

                case "terminal":
                    Config.FixTerminal = parsedFlag;
                    break;

                case "text_panel":
                    Config.FixTextPanel = parsedFlag;
                    break;

                case "conveyor":
                    Config.FixConveyor = parsedFlag;
                    break;

                case "log_flooding":
                    Config.FixLogFlooding = parsedFlag;
                    break;

                case "wheel_trail":
                    Config.FixWheelTrail = parsedFlag;
                    break;

                case "projection":
                    Config.FixProjection = parsedFlag;
                    break;

                case "airtight":
                    Config.FixAirtight = parsedFlag;
                    break;

                /*BOOL_OPTION
                case "option_name":
                    Config.OptionName = parsedFlag;
                    break;

                BOOL_OPTION*/
                default:
                    Respond($"Unknown fix: {name}");
                    RespondWithListOfFixes();
                    return;
            }

            RespondWithInfo();
        }
    }
}