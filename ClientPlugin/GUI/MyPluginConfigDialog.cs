using System;
using System.Text;
using Sandbox;
using Sandbox.Graphics.GUI;
using Shared.Plugin;
using VRage;
using VRage.Utils;
using VRageMath;

namespace ClientPlugin.GUI
{
    public class MyPluginConfigDialog : MyGuiScreenBase
    {
        private const string Caption = "Performance Improvements Configuration";
        public override string GetFriendlyName() => "MyPluginConfigDialog";

        private MyLayoutTable layoutTable;

        private MyGuiControlLabel enabledLabel;
        private MyGuiControlCheckbox enabledCheckbox;

#if CAUSES_SIMLOAD_INCREASE
        private MyGuiControlLabel fixSpinWaitLabel;
        private MyGuiControlCheckbox fixSpinWaitCheckbox;
#endif

        private MyGuiControlLabel fixGridMergeLabel;
        private MyGuiControlCheckbox fixGridMergeCheckbox;

        private MyGuiControlLabel fixGridPasteLabel;
        private MyGuiControlCheckbox fixGridPasteCheckbox;

        private MyGuiControlLabel fixP2PUpdateStatsLabel;
        private MyGuiControlCheckbox fixP2PUpdateStatsCheckbox;

        private MyGuiControlLabel fixGarbageCollectionLabel;
        private MyGuiControlCheckbox fixGarbageCollectionCheckbox;

        private MyGuiControlLabel fixThrustersLabel;
        private MyGuiControlCheckbox fixThrustersCheckbox;

        /*BOOL_OPTION
        private MyGuiControlLabel optionNameLabel;
        private MyGuiControlCheckbox optionNameCheckbox;

        BOOL_OPTION*/
        private MyGuiControlLabel disableModApiStatisticsLabel;
        private MyGuiControlCheckbox disableModApiStatisticsCheckbox;

        private MyGuiControlMultilineText infoText;
        private MyGuiControlButton closeButton;

        public MyPluginConfigDialog() : base(new Vector2(0.5f, 0.5f), MyGuiConstants.SCREEN_BACKGROUND_COLOR, new Vector2(0.5f, 0.7f), false, null, MySandboxGame.Config.UIBkOpacity, MySandboxGame.Config.UIOpacity)
        {
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
            LayoutControls();
        }

        private void CreateControls()
        {
            AddCaption(Caption);

            var config = Common.Config;
            CreateCheckbox(out enabledLabel, out enabledCheckbox, config.Enabled, value => config.Enabled = value, "Enabled", "Enables the plugin");

#if CAUSES_SIMLOAD_INCREASE
            CreateCheckbox(out fixSpinWaitLabel, out fixSpinWaitCheckbox, config.FixSpinWait, value => config.FixSpinWait = value, "Fix spin wait", "Alternative spin wait algorithm to reduce CPU load (MySpinWait.SpinOnce)");
#endif
            CreateCheckbox(out fixGridMergeLabel, out fixGridMergeCheckbox, config.FixGridMerge, value => config.FixGridMerge = value, "Fix grid merge", "Disables conveyor updates during grid merge (MyCubeGrid.MergeGridInternal)");
            CreateCheckbox(out fixGridPasteLabel, out fixGridPasteCheckbox, config.FixGridPaste, value => config.FixGridPaste = value, "Fix grid paste", "Disables updates during grid paste (MyCubeGrid.PasteBlocksServer)");
            CreateCheckbox(out fixP2PUpdateStatsLabel, out fixP2PUpdateStatsCheckbox, config.FixP2PUpdateStats, value => config.FixP2PUpdateStats = value, "Fix P2P update stats", "Eliminates 98% of EOS P2P network statistics updates (VRage.EOS.MyP2PQoSAdapter.UpdateStats)");
            CreateCheckbox(out fixGarbageCollectionLabel, out fixGarbageCollectionCheckbox, config.FixGarbageCollection, value => config.FixGarbageCollection = value, "Fix garbage collection", "Eliminates long pauses on starting and stopping large worlds by disabling selected GC.Collect calls");
            CreateCheckbox(out fixThrustersLabel, out fixThrustersCheckbox, config.FixThrusters, value => config.FixThrusters = value, "Fix thrusters", "Throttles the maximum thrust calculation to happen only once a second");
            //BOOL_OPTION CreateCheckbox(out optionNameLabel, out optionNameCheckbox, config.OptionName, value => config.OptionName = value, "Option label", "Option tooltip");
            CreateCheckbox(out disableModApiStatisticsLabel, out disableModApiStatisticsCheckbox, config.DisableModApiStatistics, value => config.DisableModApiStatistics = value, "Disables Mod API statistics", "Disables the collection of Mod API call statistics to eliminate the overhead (affects only world loading)");

            EnableDisableFixes();

            enabledCheckbox.IsCheckedChanged += EnableDisableFixes;

            infoText = new MyGuiControlMultilineText
            {
                Name = "InfoText",
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP,
                TextAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP,
                TextBoxAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP,
                Text = new StringBuilder("\r\nIt is safe to change these options during the game.\r\nPlease send me feedback on the SE Mods Discord\r\nwhether they worked out. Thanks!")
            };

            closeButton = new MyGuiControlButton(originAlign: MyGuiDrawAlignEnum.HORISONTAL_RIGHT_AND_VERTICAL_CENTER, text: MyTexts.Get(MyCommonTexts.Ok), onButtonClick: OnOk);
        }

        private void OnOk(MyGuiControlButton _) => CloseScreen();

        private void CreateCheckbox(out MyGuiControlLabel labelControl, out MyGuiControlCheckbox checkboxControl, bool value, Action<bool> store, string label, string tooltip)
        {
            labelControl = new MyGuiControlLabel
            {
                Text = label,
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP
            };

            checkboxControl = new MyGuiControlCheckbox(toolTip: tooltip)
            {
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP,
                Enabled = true,
                IsChecked = value
            };
            checkboxControl.IsCheckedChanged += cb => store(cb.IsChecked);
        }

        private void EnableDisableFixes(MyGuiControlCheckbox _ = null)
        {
            var enabled = enabledCheckbox.IsChecked;

#if CAUSES_SIMLOAD_INCREASE
            fixSpinWaitCheckbox.Enabled = enabled;
#endif
            fixGridMergeCheckbox.Enabled = enabled;
            fixGridPasteCheckbox.Enabled = enabled;
            fixP2PUpdateStatsCheckbox.Enabled = enabled;
            fixGarbageCollectionCheckbox.Enabled = enabled;
            fixThrustersCheckbox.Enabled = enabled;
            //BOOL_OPTION optionNameCheckbox.Enabled = enabled;
            disableModApiStatisticsCheckbox.Enabled = enabled;
        }

        private void LayoutControls()
        {
            var size = Size ?? Vector2.One;
            layoutTable = new MyLayoutTable(this, -0.4f * size, 0.8f * size);
            layoutTable.SetColumnWidths(400f, 100f);
            layoutTable.SetRowHeights(90f, 60f, 60f, 60f, 60f, 60f, 60f, /*BOOL_OPTION 60f,BOOL_OPTION*/ 150f, 60f);

            var row = 0;

            layoutTable.Add(enabledLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(enabledCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

#if CAUSES_SIMLOAD_INCREASE
            layoutTable.Add(fixSpinWaitLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixSpinWaitCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;
#endif

            layoutTable.Add(fixGridMergeLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixGridMergeCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(fixGridPasteLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixGridPasteCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(fixP2PUpdateStatsLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixP2PUpdateStatsCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(fixGarbageCollectionLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixGarbageCollectionCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(fixThrustersLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixThrustersCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            /*BOOL_OPTION
            layoutTable.Add(optionNameLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(optionNameCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            BOOL_OPTION*/
            layoutTable.Add(disableModApiStatisticsLabel, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(disableModApiStatisticsCheckbox, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(infoText, MyAlignH.Left, MyAlignV.Top, row, 0, colSpan: 2);
            row++;

            layoutTable.Add(closeButton, MyAlignH.Center, MyAlignV.Center, row, 0, colSpan: 2);
            // row++;
        }
    }
}