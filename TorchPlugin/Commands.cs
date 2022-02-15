using System.Linq;
using Shared.Extensions;
using Shared.Patches;
using Shared.Patches.Patching;
using Torch.Commands;
using Torch.Commands.Permissions;
using VRage.Game.ModAPI;

namespace TorchPlugin
{
    [Category("pfi")]
    public class Commands : CommandModule
    {
        private static PluginConfig Config => Plugin.Instance.Config;

        private void Respond(string message)
        {
            Context?.Respond(message);
        }

        private void RespondWithInfo()
        {
            var config = Plugin.Instance.Config;
            Respond($"{Plugin.PluginName} plugin is enabled: {config.Enabled.ToYesNo()}");
            foreach (var (_, node) in Plugin.Instance.PatchTree.Root)
            {
                var indent = 0;
                PrintRecursive(node, ref indent);
            }
        }

        private void PrintRecursive(PatchInfoTree.Node node, ref int printIndent)
        {
            Respond($"{string.Empty.PadRight(printIndent * 3, ' ')}{node.DisplayName}: {node.Enabled.ToYesNo()}");
            
            printIndent++;
            
            foreach (var (_, treeNode) in node.Children)
            {
                PrintRecursive(treeNode, ref printIndent);
            }

            printIndent--;
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

            var dehumanized = name.Dehumanize();
            var node = Plugin.Instance.PatchTree.WalkTree().FirstOrDefault(b => b.Key == dehumanized);

            if (node is null)
            {
                Respond("Cannot find node with given name");
                return;
            }

            node.Enabled = parsedFlag;

            RespondWithInfo();
        }
    }
}