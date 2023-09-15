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
    public class PluginConfigDialog : MyGuiScreenBase
    {
        private const string Caption = "Performance Improvements Configuration";
        public override string GetFriendlyName() => "MyPluginConfigDialog";

        private MyLayoutTable layoutTable;

        private MyGuiControlLabel enabledLabel;
        private MyGuiControlCheckbox enabledCheckbox;

        private MyGuiControlLabel fixGridMergeLabel;
        private MyGuiControlCheckbox fixGridMergeCheckbox;

        private MyGuiControlLabel fixGridPasteLabel;
        private MyGuiControlCheckbox fixGridPasteCheckbox;

        private MyGuiControlLabel fixP2PUpdateStatsLabel;
        private MyGuiControlCheckbox fixP2PUpdateStatsCheckbox;

        private MyGuiControlLabel fixGarbageCollectionLabel;
        private MyGuiControlCheckbox fixGarbageCollectionCheckbox;

        private MyGuiControlLabel fixGridGroupsLabel;
        private MyGuiControlCheckbox fixGridGroupsCheckbox;

        private MyGuiControlLabel cacheModsLabel;
        private MyGuiControlCheckbox cacheModsCheckbox;

        private MyGuiControlLabel cacheScriptsLabel;
        private MyGuiControlCheckbox cacheScriptsCheckbox;

        private MyGuiControlLabel fixSafeZoneLabel;
        private MyGuiControlCheckbox fixSafeZoneCheckbox;

        // private MyGuiControlLabel fixTargetingLabel;
        // private MyGuiControlCheckbox fixTargetingCheckbox;

        private MyGuiControlLabel fixWindTurbineLabel;
        private MyGuiControlCheckbox fixWindTurbineCheckbox;

        private MyGuiControlLabel fixVoxelLabel;
        private MyGuiControlCheckbox fixVoxelCheckbox;

        private MyGuiControlLabel fixPhysicsLabel;
        private MyGuiControlCheckbox fixPhysicsCheckbox;

        private MyGuiControlLabel fixCharacterLabel;
        private MyGuiControlCheckbox fixCharacterCheckbox;

        private MyGuiControlLabel fixMemoryLabel;
        private MyGuiControlCheckbox fixMemoryCheckbox;

        private MyGuiControlLabel fixAccessLabel;
        private MyGuiControlCheckbox fixAccessCheckbox;

        private MyGuiControlLabel fixBlockLimitLabel;
        private MyGuiControlCheckbox fixBlockLimitCheckbox;

        private MyGuiControlLabel fixSafeActionLabel;
        private MyGuiControlCheckbox fixSafeActionCheckbox;

        private MyGuiControlLabel fixTerminalLabel;
        private MyGuiControlCheckbox fixTerminalCheckbox;

        private MyGuiControlLabel fixTextPanelLabel;
        private MyGuiControlCheckbox fixTextPanelCheckbox;

        private MyGuiControlLabel fixConveyorLabel;
        private MyGuiControlCheckbox fixConveyorCheckbox;

        /*BOOL_OPTION
        private MyGuiControlLabel optionNameLabel;
        private MyGuiControlCheckbox optionNameCheckbox;

        BOOL_OPTION*/
        private MyGuiControlLabel disableModApiStatisticsLabel;
        private MyGuiControlCheckbox disableModApiStatisticsCheckbox;

        private MyGuiControlMultilineText infoText;
        private MyGuiControlButton closeButton;

        public PluginConfigDialog() : base(new Vector2(0.5f, 0.5f), MyGuiConstants.SCREEN_BACKGROUND_COLOR, new Vector2(0.9f, 0.9f), false, null, MySandboxGame.Config.UIBkOpacity, MySandboxGame.Config.UIOpacity)
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
            CreateCheckbox(out enabledLabel, out enabledCheckbox, config.Enabled, value => config.Enabled = value, "Enabled", "Enable the plugin (all fixes)");

            CreateCheckbox(out fixGridMergeLabel, out fixGridMergeCheckbox, config.FixGridMerge, value => config.FixGridMerge = value, "Fix grid merge", "Disable conveyor updates during grid merge (MyCubeGrid.MergeGridInternal)");
            CreateCheckbox(out fixGridPasteLabel, out fixGridPasteCheckbox, config.FixGridPaste, value => config.FixGridPaste = value, "Fix grid paste", "Disable updates during grid paste (MyCubeGrid.PasteBlocksServer)");
            CreateCheckbox(out fixP2PUpdateStatsLabel, out fixP2PUpdateStatsCheckbox, config.FixP2PUpdateStats, value => config.FixP2PUpdateStats = value, "Fix P2P update stats", "Eliminate 98% of EOS P2P network statistics updates (VRage.EOS.MyP2PQoSAdapter.UpdateStats)");
            CreateCheckbox(out fixGarbageCollectionLabel, out fixGarbageCollectionCheckbox, config.FixGarbageCollection, value => config.FixGarbageCollection = value, "Fix garbage collection", "Eliminate long pauses on starting and stopping large worlds by disabling selected GC.Collect calls");
            CreateCheckbox(out fixGridGroupsLabel, out fixGridGroupsCheckbox, config.FixGridGroups, value => config.FixGridGroups = value, "Fix grid groups", "Disable resource updates while grids are being moved between groups");

            // 1.10.9 (2023-07-30): Disabled mod compilation on client
            // as per request of avaness. Reason:
            // "Plugin loader added compilation symbols to mods, so it breaks
            //  build info in really weird ways because it uses that symbol"
            CreateCheckbox(out cacheModsLabel, out cacheModsCheckbox, false, value => config.CacheMods = value, "Cache compiled mods", "Caches compiled mods for faster world load" + "\n\n1.10.9 (2023-07-30): Disabled mod compilation on client\nas per request of avaness. Reason:\n\"Plugin loader added compilation symbols to mods, so it breaks\nbuild info in really weird ways because it uses that symbol.\"", false);

            CreateCheckbox(out cacheScriptsLabel, out cacheScriptsCheckbox, config.CacheScripts, value => config.CacheScripts = value, "Cache compiled scripts", "Caches compiled in-game scripts (PB programs) to reduce lag");
            CreateCheckbox(out disableModApiStatisticsLabel, out disableModApiStatisticsCheckbox, config.DisableModApiStatistics, value => config.DisableModApiStatistics = value, "Disable Mod API statistics", "Disable the collection of Mod API call statistics to eliminate the overhead (affects only world loading)");
            CreateCheckbox(out fixSafeZoneLabel, out fixSafeZoneCheckbox, config.FixSafeZone, value => config.FixSafeZone = value, "Fix safe zone lag", "Caches frequent recalculations in safe zones");
            // CreateCheckbox(out fixTargetingLabel, out fixTargetingCheckbox, config.FixTargeting, value => config.FixTargeting = value, "Fix allocations in targeting (needs restart)", "Reduces memory allocations in the turret targeting system (needs restart)");
            CreateCheckbox(out fixWindTurbineLabel, out fixWindTurbineCheckbox, config.FixWindTurbine, value => config.FixWindTurbine = value, "Fix wind turbine performance", "Caches the result of MyWindTurbine.IsInAtmosphere");
            CreateCheckbox(out fixVoxelLabel, out fixVoxelCheckbox, config.FixVoxel, value => config.FixVoxel = value, "Fix voxel performance", "Reduces memory allocations in IMyStorageExtensions.GetMaterialAt");
            CreateCheckbox(out fixPhysicsLabel, out fixPhysicsCheckbox, config.FixPhysics, value => config.FixPhysics = value, "Fix physics performance (needs restart)", "Optimizes the MyPhysicsBody.RigidBody getter (needs restart)");
            CreateCheckbox(out fixCharacterLabel, out fixCharacterCheckbox, config.FixCharacter, value => config.FixCharacter = value, "Fix character performance (needs restart)", "Disables character footprint logic on server side (needs restart)");
            CreateCheckbox(out fixMemoryLabel, out fixMemoryCheckbox, config.FixMemory, value => config.FixMemory = value, "Fix frequent memory allocations", "Optimizes frequent memory allocations in various parts of the game");
            CreateCheckbox(out fixAccessLabel, out fixAccessCheckbox, config.FixAccess, value => config.FixAccess = value, "Less frequent update of block access rights", "Caches the result of MyCubeBlock.GetUserRelationToOwner and MyTerminalBlock.HasPlayerAccessReason");
            CreateCheckbox(out fixBlockLimitLabel, out fixBlockLimitCheckbox, config.FixBlockLimit, value => config.FixBlockLimit = value, "Less frequent sync of block counts for limit checking", "Suppresses frequent calls to MyPlayerCollection.SendDirtyBlockLimits");
            CreateCheckbox(out fixSafeActionLabel, out fixSafeActionCheckbox, config.FixSafeAction, value => config.FixSafeAction = value, "Cache actions allowed by the safe zone", "Caches the result of MySafeZone.IsActionAllowed and MySessionComponentSafeZones.IsActionAllowedForSafezone for 2 seconds");
            // CreateCheckbox(out fixTerminalLabel, out fixTerminalCheckbox, config.FixTerminal, value => config.FixTerminal = value, "Less frequent update of PB access to blocks", "Suppresses frequent calls to MyGridTerminalSystem.UpdateGridBlocksOwnership updating IsAccessibleForProgrammableBlock unnecessarily often");
            // CreateCheckbox(out fixTextPanelLabel, out fixTextPanelCheckbox, config.FixTextPanel, value => config.FixTextPanel = value, "Text panel performance fixes", "Disables UpdateVisibility of LCD surfaces on multiplayer servers");
            CreateCheckbox(out fixConveyorLabel, out fixConveyorCheckbox, config.FixConveyor, value => config.FixConveyor = value, "Conveyor network performance fixes", "Caches conveyor network lookups");
            //BOOL_OPTION CreateCheckbox(out optionNameLabel, out optionNameCheckbox, config.OptionName, value => config.OptionName = value, "Option label", "Option tooltip");

            EnableDisableFixes();

            enabledCheckbox.IsCheckedChanged += EnableDisableFixes;

            infoText = new MyGuiControlMultilineText
            {
                Name = "InfoText",
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP,
                TextAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP,
                TextBoxAlign = MyGuiDrawAlignEnum.HORISONTAL_LEFT_AND_VERTICAL_TOP,
                Text = new StringBuilder("\r\nIt is safe to change these options during the game.\r\nPlease send me feedback on the SE Mods Discord\r\nhow well they worked out. Thanks!")
            };

            closeButton = new MyGuiControlButton(originAlign: MyGuiDrawAlignEnum.HORISONTAL_RIGHT_AND_VERTICAL_CENTER, text: MyTexts.Get(MyCommonTexts.Ok), onButtonClick: OnOk);
        }

        private void OnOk(MyGuiControlButton _) => CloseScreen();

        private void CreateCheckbox(out MyGuiControlLabel labelControl, out MyGuiControlCheckbox checkboxControl, bool value, Action<bool> store, string label, string tooltip, bool enabled = true)
        {
            labelControl = new MyGuiControlLabel
            {
                Text = label,
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP,
                Enabled = enabled,
            };

            checkboxControl = new MyGuiControlCheckbox(toolTip: tooltip)
            {
                OriginAlign = MyGuiDrawAlignEnum.HORISONTAL_CENTER_AND_VERTICAL_TOP,
                IsChecked = value,
                Enabled = enabled,
                CanHaveFocus = enabled
            };
            if (enabled)
            {
                checkboxControl.IsCheckedChanged += cb => store(cb.IsChecked);
            }
            else
            {
                checkboxControl.IsCheckedChanged += cb => { cb.IsChecked = value; };
            }
        }

        private void EnableDisableFixes(MyGuiControlCheckbox _ = null)
        {
            var enabled = enabledCheckbox.IsChecked;
            fixGridMergeCheckbox.Enabled = enabled;
            fixGridPasteCheckbox.Enabled = enabled;
            fixP2PUpdateStatsCheckbox.Enabled = enabled;
            fixGarbageCollectionCheckbox.Enabled = enabled;
            fixGridGroupsCheckbox.Enabled = enabled;
            cacheModsCheckbox.Enabled = enabled;
            cacheScriptsCheckbox.Enabled = enabled;
            disableModApiStatisticsCheckbox.Enabled = enabled;
            fixSafeZoneCheckbox.Enabled = enabled;
            // fixTargetingCheckbox.Enabled = enabled;
            fixWindTurbineCheckbox.Enabled = enabled;
            fixVoxelCheckbox.Enabled = enabled;
            fixPhysicsCheckbox.Enabled = enabled;
            fixCharacterCheckbox.Enabled = enabled;
            fixMemoryCheckbox.Enabled = enabled;
            fixAccessCheckbox.Enabled = enabled;
            fixBlockLimitCheckbox.Enabled = enabled;
            fixSafeActionCheckbox.Enabled = enabled;
            // fixTerminalCheckbox.Enabled = enabled;
            // fixTextPanelCheckbox.Enabled = enabled;
            fixConveyorCheckbox.Enabled = enabled;
            //BOOL_OPTION optionNameCheckbox.Enabled = enabled;
        }

        private void LayoutControls()
        {
            layoutTable = new MyLayoutTable(this, new Vector2(-0.4f, -0.4f), new Vector2(0.8f, 0.8f));
            layoutTable.SetColumnWidths(60f, 440f, 60f, 440f);
            layoutTable.SetRowHeights(150f, 60f, 60f, 60f, 60f, 60f, 60f, 60f, 60f, 60f, 60f, 60f, 150f);

            layoutTable.Add(enabledCheckbox, MyAlignH.Left, MyAlignV.Center, 0, 0);
            layoutTable.Add(enabledLabel, MyAlignH.Left, MyAlignV.Center, 0, 1);

            var row = 1;

            layoutTable.Add(fixGridMergeCheckbox, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixGridMergeLabel, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(fixGridPasteCheckbox, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixGridPasteLabel, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(fixP2PUpdateStatsCheckbox, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixP2PUpdateStatsLabel, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(fixGarbageCollectionCheckbox, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixGarbageCollectionLabel, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(fixGridGroupsCheckbox, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixGridGroupsLabel, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(cacheModsCheckbox, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(cacheModsLabel, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(cacheScriptsCheckbox, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(cacheScriptsLabel, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(disableModApiStatisticsCheckbox, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(disableModApiStatisticsLabel, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            layoutTable.Add(fixSafeZoneCheckbox, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixSafeZoneLabel, MyAlignH.Left, MyAlignV.Center, row, 1);
            row++;

            // layoutTable.Add(fixTargetingCheckbox, MyAlignH.Left, MyAlignV.Center, row, 0);
            // layoutTable.Add(fixTargetingLabel, MyAlignH.Left, MyAlignV.Center, row, 1);
            // row++;

            layoutTable.Add(fixWindTurbineCheckbox, MyAlignH.Left, MyAlignV.Center, row, 0);
            layoutTable.Add(fixWindTurbineLabel, MyAlignH.Left, MyAlignV.Center, row, 1);
            row = 1;

            layoutTable.Add(fixVoxelCheckbox, MyAlignH.Left, MyAlignV.Center, row, 2);
            layoutTable.Add(fixVoxelLabel, MyAlignH.Left, MyAlignV.Center, row, 3);
            row++;

            layoutTable.Add(fixPhysicsCheckbox, MyAlignH.Left, MyAlignV.Center, row, 2);
            layoutTable.Add(fixPhysicsLabel, MyAlignH.Left, MyAlignV.Center, row, 3);
            row++;

            layoutTable.Add(fixCharacterCheckbox, MyAlignH.Left, MyAlignV.Center, row, 2);
            layoutTable.Add(fixCharacterLabel, MyAlignH.Left, MyAlignV.Center, row, 3);
            row++;

            layoutTable.Add(fixMemoryCheckbox, MyAlignH.Left, MyAlignV.Center, row, 2);
            layoutTable.Add(fixMemoryLabel, MyAlignH.Left, MyAlignV.Center, row, 3);
            row++;

            // layoutTable.Add(fixAccessCheckbox, MyAlignH.Left, MyAlignV.Center, row, 2);
            // layoutTable.Add(fixAccessLabel, MyAlignH.Left, MyAlignV.Center, row, 3);
            // row++;

            layoutTable.Add(fixBlockLimitCheckbox, MyAlignH.Left, MyAlignV.Center, row, 2);
            layoutTable.Add(fixBlockLimitLabel, MyAlignH.Left, MyAlignV.Center, row, 3);
            row++;

            layoutTable.Add(fixSafeActionCheckbox, MyAlignH.Left, MyAlignV.Center, row, 2);
            layoutTable.Add(fixSafeActionLabel, MyAlignH.Left, MyAlignV.Center, row, 3);
            row++;

            // layoutTable.Add(fixTerminalCheckbox, MyAlignH.Left, MyAlignV.Center, row, 2);
            // layoutTable.Add(fixTerminalLabel, MyAlignH.Left, MyAlignV.Center, row, 3);
            // row++;

            // layoutTable.Add(fixTextPanelCheckbox, MyAlignH.Left, MyAlignV.Center, row, 2);
            // layoutTable.Add(fixTextPanelLabel, MyAlignH.Left, MyAlignV.Center, row, 3);
            // row++;

            layoutTable.Add(fixConveyorCheckbox, MyAlignH.Left, MyAlignV.Center, row, 2);
            layoutTable.Add(fixConveyorLabel, MyAlignH.Left, MyAlignV.Center, row, 3);
            row++;

            /*BOOL_OPTION
            layoutTable.Add(optionNameCheckbox, MyAlignH.Left, MyAlignV.Center, row, 2);
            layoutTable.Add(optionNameLabel, MyAlignH.Left, MyAlignV.Center, row, 3);
            row++;

            BOOL_OPTION*/
            layoutTable.Add(infoText, MyAlignH.Left, MyAlignV.Top, 12, 0, colSpan: 2);
            layoutTable.Add(closeButton, MyAlignH.Center, MyAlignV.Center, 12, 2, colSpan: 2);
        }
    }
}