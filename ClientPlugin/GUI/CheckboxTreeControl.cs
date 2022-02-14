using System.Collections.Generic;
using System.Linq;
using Humanizer;
using Sandbox.Graphics.GUI;
using Shared.Patches.Patching;
using VRage.Utils;
using VRageMath;

namespace ClientPlugin.GUI
{
    public class CheckboxTreeControl : MyGuiControlParent
    {
        private readonly PatchInfoTree tree = new PatchInfoTree();
        private Vector2 positionOffset;
        
        public CheckboxTreeControl(IReadOnlyDictionary<string, PatchInfo> dictionary, Vector2 pos, Vector2 size) : base(pos, size)
        {
            foreach (var (key, patchInfo) in dictionary)
            {
                tree.Add(key, patchInfo);
            }
            foreach (var node in tree.Root.Values)
            {
                CreateCheckbox(node);
            }
        }
    
        private void CreateCheckbox(PatchInfoTree.Node node)
        {
            positionOffset.Y += .04f;
            var checkbox = new MyGuiControlCheckbox(positionOffset)
            {
                IsChecked = node.Enabled,
                IsCheckedChanged = c => node.Enabled = c.IsChecked,
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP
            };
            Controls.Add(checkbox);
    
            node.PropertyChanged += (sender, e) =>
            {
                checkbox.IsChecked = node.Enabled;
            };
            
            Controls.Add(new MyGuiControlLabel(positionOffset + new Vector2(.04f, .01f))
            {
                Text = node.DisplayName,
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP,
                DrawAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP
            });
    
            positionOffset.X += .04f;
            foreach (var (_, controlTreeNode) in node.Children)
            {
                CreateCheckbox(controlTreeNode);
            }
            positionOffset.X -= .04f;
        }
    }
}