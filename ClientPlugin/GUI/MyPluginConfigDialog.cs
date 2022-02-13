using System;
using System.Text;
using Sandbox;
using Sandbox.Graphics.GUI;
using Shared.Patches.Patching;
using Shared.Plugin;
using VRage;
using VRage.Utils;
using VRageMath;

namespace ClientPlugin.GUI
{
    public class MyPluginConfigDialog : MyGuiScreenBase
    {
        private readonly HarmonyPatcher patcher;
        private const string Caption = "Performance Improvements Configuration";
        public override string GetFriendlyName() => "MyPluginConfigDialog";

        public MyPluginConfigDialog(HarmonyPatcher patcher) : base(new Vector2(0.5f, 0.5f), MyGuiConstants.SCREEN_BACKGROUND_COLOR,
            new Vector2(0.5f, 0.7f), false, null, 
            MySandboxGame.Config.UIBkOpacity, MySandboxGame.Config.UIOpacity)
        {
            this.patcher = patcher;
            EnabledBackgroundFade = true;
            m_closeOnEsc = true;
            m_drawEvenWithoutFocus = true;
            CanHideOthers = true;
            CanBeHidden = true;
            CloseButtonEnabled = true;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            RecreateControls(true);
        }

        public override void RecreateControls(bool constructor)
        {
            base.RecreateControls(constructor);
            CreateControls();
        }

        private void CreateControls()
        {
            AddCaption(Caption);
            AddControl(new MyGuiControlCheckbox(new (-.2f, -.3f))
            {
                IsChecked = Plugin.Instance.Config.Enabled,
                IsCheckedChanged = c => Plugin.Instance.Config.Enabled = c.IsChecked,
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP
            });
            AddControl(new CheckboxTreeControl(patcher.PatchInfos, new(-.2f, -.3f), new(.5f, .7f)));
        }
    }
}