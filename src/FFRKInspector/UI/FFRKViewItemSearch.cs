// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.FFRKViewItemSearch
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.GameData;
using FFRKInspector.GameData.AppInit;
using FFRKInspector.Proxy;
using FFRKInspector.UI.ListViewFields;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  internal class FFRKViewItemSearch : UserControl
  {
    private IContainer components = (IContainer) null;
    private ListListViewBinding<BasicItemDropStats> mBinding;
    private List<BasicItemDropStats> mUnfilteredResults;
    private ItemStaminaPerDropField mStaminaPerDropField;
    private bool mParametersShowing;
    private GroupBox groupBoxItemAndLocation;
    private Label labelRarity;
    private DeselectableListBox listBoxRarity;
    private Label label1;
    private DeselectableListBox listBoxRealmSynergy;
    private ListViewEx listViewResults;
    private Label Battle;
    private DeselectableListBox listBoxBattle;
    private Label labelDungeon;
    private DeselectableListBox listBoxDungeon;
    private Label labelHelp;
    private Button buttonSearch;
    private Label label2;
    private DeselectableListBox listBoxWorld;
    private Button buttonResetAll;
    private TextBox textBoxNameFilter;
    private Label label3;
    private CheckBox checkBoxRepeatable;
    private Label label4;
    private NumericUpDown numericUpDownMinBattles;
    private GroupBox groupBoxSampleSize;
    private GroupBox groupBoxAdditional;
    private RadioButton radioButtonMinSamples;
    private RadioButton radioButtonHelp;
    private RadioButton radioButtonAllSamples;
    private Button buttonHideParameters;
    private NumericUpDown numericUpDownLowSampleThreshold;
    private CheckBox checkBoxUseStamToReach;
    private CheckBox checkBoxNoInactive;

    public FFRKViewItemSearch()
    {
      this.InitializeComponent();
      this.mBinding = new ListListViewBinding<BasicItemDropStats>();
      this.mUnfilteredResults = new List<BasicItemDropStats>();
      this.mParametersShowing = true;
      this.listViewResults.LoadSettings();
      this.mStaminaPerDropField = new ItemStaminaPerDropField("Stamina/Drop", this.checkBoxUseStamToReach.Checked);
      this.listViewResults.AddField((IListViewField) new ItemNameField("Item", FieldWidthStyle.Percent, 15));
      this.listViewResults.AddField((IListViewField) new ItemWorldField("World", FieldWidthStyle.Percent, 10));
      this.listViewResults.AddField((IListViewField) new ItemDungeonField("Dungeon", FieldWidthStyle.Percent, 15));
      this.listViewResults.AddField((IListViewField) new ItemBattleField(nameof (Battle), FieldWidthStyle.Percent, 15));
      this.listViewResults.AddField((IListViewField) new ItemRarityField("Rarity"));
      this.listViewResults.AddField((IListViewField) new ItemSynergyField("Synergy"));
      this.listViewResults.AddField((IListViewField) new ItemDropsPerRunField("Drops/Run"));
      this.listViewResults.AddField((IListViewField) this.mStaminaPerDropField);
      this.listViewResults.AddField((IListViewField) new ItemTotalDropsField("Total Drops"));
      this.listViewResults.AddField((IListViewField) new ItemTimesRunField("Times Run"));
      this.listViewResults.AddField((IListViewField) new ItemBattleStaminaField("Stamina"));
      this.listViewResults.AddField((IListViewField) new ItemStaminaToReachField("Stamina to Reach"));
      this.listViewResults.AddField((IListViewField) new ItemRepeatableField("Is Repeatable"));
      this.listViewResults.DataBinding = (IListViewBinding) this.mBinding;
    }

    private void FFRKViewItemSearch_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.listBoxRealmSynergy.Items.Clear();
      this.listBoxWorld.Items.Clear();
      this.listBoxDungeon.Items.Clear();
      this.listBoxBattle.Items.Clear();
      this.listBoxRarity.Items.Clear();
      this.listBoxRarity.Items.AddRange(Enum.GetValues(typeof (SchemaConstants.Rarity)).Cast<object>().ToArray<object>());
      this.listBoxRealmSynergy.Items.AddRange((object[]) RealmSynergy.Values.Where<RealmSynergy.SynergyValue>((Func<RealmSynergy.SynergyValue, bool>) (x => x.Realm != RealmSynergy.Value.None_2)).ToArray<RealmSynergy.SynergyValue>());
      List<KeyValuePair<FFRKInspector.DataCache.Worlds.Key, FFRKInspector.DataCache.Worlds.Data>> list = FFRKProxy.Instance.Cache.Worlds.ToList<KeyValuePair<FFRKInspector.DataCache.Worlds.Key, FFRKInspector.DataCache.Worlds.Data>>();
      list.Sort((Comparison<KeyValuePair<FFRKInspector.DataCache.Worlds.Key, FFRKInspector.DataCache.Worlds.Data>>) ((x, y) => x.Value.Name.CompareTo(y.Value.Name)));
      this.listBoxWorld.Items.AddRange((object[]) list.Select<KeyValuePair<FFRKInspector.DataCache.Worlds.Key, FFRKInspector.DataCache.Worlds.Data>, FFRKViewItemSearch.WorldListItem>((Func<KeyValuePair<FFRKInspector.DataCache.Worlds.Key, FFRKInspector.DataCache.Worlds.Data>, FFRKViewItemSearch.WorldListItem>) (x => new FFRKViewItemSearch.WorldListItem(x.Key, x.Value))).ToArray<FFRKViewItemSearch.WorldListItem>());
      this.DisableDungeonList();
      this.DisableBattlesList();
    }

    private void listBoxWorld_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listBoxWorld.SelectedItem == null)
        return;
      this.DisableBattlesList();
      FFRKViewItemSearch.WorldListItem item = (FFRKViewItemSearch.WorldListItem) this.listBoxWorld.SelectedItem;
      this.listBoxDungeon.Items.Clear();
      this.listBoxDungeon.Enabled = true;
      List<KeyValuePair<FFRKInspector.DataCache.Dungeons.Key, FFRKInspector.DataCache.Dungeons.Data>> list = FFRKProxy.Instance.Cache.Dungeons.Where<KeyValuePair<FFRKInspector.DataCache.Dungeons.Key, FFRKInspector.DataCache.Dungeons.Data>>((Func<KeyValuePair<FFRKInspector.DataCache.Dungeons.Key, FFRKInspector.DataCache.Dungeons.Data>, int, bool>) ((x, y) => (int) x.Value.WorldId == (int) item.WorldId)).ToList<KeyValuePair<FFRKInspector.DataCache.Dungeons.Key, FFRKInspector.DataCache.Dungeons.Data>>();
      list.Sort((Comparison<KeyValuePair<FFRKInspector.DataCache.Dungeons.Key, FFRKInspector.DataCache.Dungeons.Data>>) ((x, y) => x.Key.DungeonId.CompareTo(y.Key.DungeonId)));
      this.listBoxDungeon.Items.AddRange((object[]) list.Select<KeyValuePair<FFRKInspector.DataCache.Dungeons.Key, FFRKInspector.DataCache.Dungeons.Data>, FFRKViewItemSearch.DungeonListItem>((Func<KeyValuePair<FFRKInspector.DataCache.Dungeons.Key, FFRKInspector.DataCache.Dungeons.Data>, FFRKViewItemSearch.DungeonListItem>) (x => new FFRKViewItemSearch.DungeonListItem(x.Key, x.Value))).ToArray<FFRKViewItemSearch.DungeonListItem>());
    }

    private void listBoxDungeon_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.listBoxDungeon.SelectedItem == null)
        return;
      FFRKViewItemSearch.DungeonListItem item = (FFRKViewItemSearch.DungeonListItem) this.listBoxDungeon.SelectedItem;
      this.listBoxBattle.Items.Clear();
      this.listBoxBattle.Enabled = true;
      List<KeyValuePair<FFRKInspector.DataCache.Battles.Key, FFRKInspector.DataCache.Battles.Data>> list = FFRKProxy.Instance.Cache.Battles.Where<KeyValuePair<FFRKInspector.DataCache.Battles.Key, FFRKInspector.DataCache.Battles.Data>>((Func<KeyValuePair<FFRKInspector.DataCache.Battles.Key, FFRKInspector.DataCache.Battles.Data>, int, bool>) ((x, y) => (int) x.Value.DungeonId == (int) item.DungeonId)).ToList<KeyValuePair<FFRKInspector.DataCache.Battles.Key, FFRKInspector.DataCache.Battles.Data>>();
      list.Sort((Comparison<KeyValuePair<FFRKInspector.DataCache.Battles.Key, FFRKInspector.DataCache.Battles.Data>>) ((x, y) => x.Key.BattleId.CompareTo(y.Key.BattleId)));
      this.listBoxBattle.Items.AddRange((object[]) list.Select<KeyValuePair<FFRKInspector.DataCache.Battles.Key, FFRKInspector.DataCache.Battles.Data>, FFRKViewItemSearch.BattleListItem>((Func<KeyValuePair<FFRKInspector.DataCache.Battles.Key, FFRKInspector.DataCache.Battles.Data>, FFRKViewItemSearch.BattleListItem>) (x => new FFRKViewItemSearch.BattleListItem(x.Key, x.Value))).ToArray<FFRKViewItemSearch.BattleListItem>());
    }

    private void listBoxBattle_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void listBoxWorld_SelectionCleared(object sender, EventArgs e) => this.DisableDungeonList();

    private void listBoxDungeon_SelectionCleared(object sender, EventArgs e) => this.DisableBattlesList();

    private void DisableDungeonList()
    {
      this.listBoxDungeon.Items.Clear();
      this.listBoxDungeon.Items.Add((object) "(Choose a world to populate this list)");
      this.listBoxDungeon.Enabled = false;
      this.DisableBattlesList();
    }

    private void DisableBattlesList()
    {
      this.listBoxBattle.Items.Clear();
      this.listBoxBattle.Items.Add((object) "(Choose a dungeon to populate this list)");
      this.listBoxBattle.Enabled = false;
    }

    private void DoSearch()
    {
      DbOpFilterDrops dbOpFilterDrops = new DbOpFilterDrops(FFRKProxy.Instance.Database);
      dbOpFilterDrops.Name.Value = this.textBoxNameFilter.Text;
      foreach (RealmSynergy.SynergyValue selectedItem in this.listBoxRealmSynergy.SelectedItems)
        dbOpFilterDrops.Synergies.AddValue(selectedItem);
      if (this.listBoxWorld.Enabled)
      {
        foreach (FFRKViewItemSearch.WorldListItem selectedItem in this.listBoxWorld.SelectedItems)
          dbOpFilterDrops.Worlds.AddValue(selectedItem.WorldId);
      }
      if (this.listBoxBattle.Enabled)
      {
        foreach (FFRKViewItemSearch.BattleListItem selectedItem in this.listBoxBattle.SelectedItems)
          dbOpFilterDrops.Battles.AddValue(selectedItem.BattleId);
      }
      if (this.listBoxDungeon.Enabled)
      {
        foreach (FFRKViewItemSearch.DungeonListItem selectedItem in this.listBoxDungeon.SelectedItems)
          dbOpFilterDrops.Dungeons.AddValue(selectedItem.DungeonId);
      }
      foreach (SchemaConstants.Rarity selectedItem in this.listBoxRarity.SelectedItems)
        dbOpFilterDrops.Rarities.AddValue(selectedItem);
      dbOpFilterDrops.OnRequestComplete += new DbOpFilterDrops.DataReadyCallback(this.DbOpFilterDrops_OnRequestComplete);
      FFRKProxy.Instance.Database.BeginExecuteRequest((IDbRequest) dbOpFilterDrops);
    }

    private void buttonSearch_Click(object sender, EventArgs e) => this.DoSearch();

    private void DbOpFilterDrops_OnRequestComplete(List<BasicItemDropStats> items) => this.BeginInvoke((Action) (() =>
    {
      this.mUnfilteredResults = items;
      this.InplaceFilterDrops();
    }));

    private void buttonResetAll_Click(object sender, EventArgs e)
    {
      this.listBoxRealmSynergy.SelectedItems.Clear();
      this.listBoxWorld.SelectedItems.Clear();
      this.listBoxDungeon.SelectedItems.Clear();
      this.listBoxBattle.SelectedItems.Clear();
      this.listBoxRarity.SelectedItems.Clear();
      this.textBoxNameFilter.Clear();
      this.radioButtonMinSamples.Checked = false;
      this.checkBoxRepeatable.Checked = false;
      this.mBinding.Collection.Clear();
      this.listViewResults.VirtualListSize = 0;
    }

    private void buttonHideParameters_Click(object sender, EventArgs e)
    {
      int num = this.groupBoxItemAndLocation.Height + 6;
      if (this.mParametersShowing)
      {
        this.buttonHideParameters.Text = "Show Parameters ↓";
        this.mParametersShowing = false;
        num = -num;
      }
      else
      {
        this.buttonHideParameters.Text = "Hide Parameters ↑";
        this.mParametersShowing = true;
      }
      this.groupBoxItemAndLocation.Visible = this.mParametersShowing;
      this.buttonHideParameters.Top += num;
      this.groupBoxSampleSize.Top += num;
      this.groupBoxAdditional.Top += num;
      int y = this.listViewResults.Top + num;
      int height = this.listViewResults.Bottom - y;
      this.listViewResults.SetBounds(0, y, 0, height, BoundsSpecified.Y | BoundsSpecified.Height);
    }

    private void InplaceFilterDrops()
    {
      IEnumerable<BasicItemDropStats> source = (IEnumerable<BasicItemDropStats>) this.mUnfilteredResults;
      if (this.radioButtonMinSamples.Checked && this.numericUpDownMinBattles.Value > 0M)
        source = source.Where<BasicItemDropStats>((Func<BasicItemDropStats, bool>) (x => (Decimal) x.TimesRun >= this.numericUpDownMinBattles.Value));
      else if (this.radioButtonHelp.Checked && this.numericUpDownLowSampleThreshold.Value > 0M)
        source = source.Where<BasicItemDropStats>((Func<BasicItemDropStats, bool>) (x => (Decimal) x.TimesRun <= this.numericUpDownLowSampleThreshold.Value));
      if (this.checkBoxRepeatable.Checked)
        source = source.Where<BasicItemDropStats>((Func<BasicItemDropStats, bool>) (x => x.IsBattleRepeatable));
      if (this.checkBoxNoInactive.Checked)
      {
        AppInitData data = FFRKProxy.Instance.GameState.AppInitData;
        if (data != null)
          source = source.Where<BasicItemDropStats>((Func<BasicItemDropStats, bool>) (x => data.Worlds.Exists((Predicate<DataWorld>) (y => (int) y.Id == (int) x.WorldId && y.KeptOutAt > data.User.StartTimeOfToday))));
      }
      this.mBinding.Collection = source.ToList<BasicItemDropStats>();
      this.listViewResults.VirtualListSize = this.mBinding.Collection.Count;
      this.listViewResults.Invalidate();
    }

    private void radioButtonMinSamples_CheckedChanged(object sender, EventArgs e)
    {
      this.numericUpDownLowSampleThreshold.Enabled = this.radioButtonHelp.Checked;
      this.numericUpDownMinBattles.Enabled = this.radioButtonMinSamples.Checked;
      this.InplaceFilterDrops();
    }

    private void radioButtonHelp_CheckedChanged(object sender, EventArgs e)
    {
      this.numericUpDownLowSampleThreshold.Enabled = this.radioButtonHelp.Checked;
      this.numericUpDownMinBattles.Enabled = this.radioButtonMinSamples.Checked;
      this.InplaceFilterDrops();
    }

    private void radioButtonAllSamples_CheckedChanged(object sender, EventArgs e)
    {
      this.numericUpDownLowSampleThreshold.Enabled = this.radioButtonHelp.Checked;
      this.numericUpDownMinBattles.Enabled = this.radioButtonMinSamples.Checked;
      this.InplaceFilterDrops();
    }

    private void numericUpDownLowSampleThreshold_ValueChanged(object sender, EventArgs e) => this.InplaceFilterDrops();

    private void numericUpDownMinBattles_ValueChanged(object sender, EventArgs e) => this.InplaceFilterDrops();

    private void checkBoxRepeatable_CheckedChanged(object sender, EventArgs e) => this.InplaceFilterDrops();

    private void checkBoxUseStamToReach_CheckedChanged(object sender, EventArgs e)
    {
      this.mStaminaPerDropField.UseStaminaToReachForNonRepeatable = this.checkBoxUseStamToReach.Checked;
      this.listViewResults.Invalidate();
    }

    private void checkBoxNoInactive_CheckedChanged(object sender, EventArgs e)
    {
      if (this.checkBoxNoInactive.Checked && FFRKProxy.Instance.GameState.AppInitData == null)
      {
        int num = (int) MessageBox.Show("This feature requires FFRK Inspector to have been running when you first launched FFRK.  Please close and restart FFRK and try again.");
        this.checkBoxNoInactive.Checked = false;
      }
      else
        this.InplaceFilterDrops();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.groupBoxItemAndLocation = new GroupBox();
      this.textBoxNameFilter = new TextBox();
      this.label3 = new Label();
      this.label2 = new Label();
      this.listBoxWorld = new DeselectableListBox();
      this.labelHelp = new Label();
      this.Battle = new Label();
      this.listBoxBattle = new DeselectableListBox();
      this.labelDungeon = new Label();
      this.listBoxDungeon = new DeselectableListBox();
      this.label1 = new Label();
      this.listBoxRealmSynergy = new DeselectableListBox();
      this.labelRarity = new Label();
      this.listBoxRarity = new DeselectableListBox();
      this.label4 = new Label();
      this.numericUpDownMinBattles = new NumericUpDown();
      this.checkBoxRepeatable = new CheckBox();
      this.buttonResetAll = new Button();
      this.buttonSearch = new Button();
      this.groupBoxSampleSize = new GroupBox();
      this.numericUpDownLowSampleThreshold = new NumericUpDown();
      this.radioButtonMinSamples = new RadioButton();
      this.radioButtonHelp = new RadioButton();
      this.radioButtonAllSamples = new RadioButton();
      this.groupBoxAdditional = new GroupBox();
      this.checkBoxNoInactive = new CheckBox();
      this.checkBoxUseStamToReach = new CheckBox();
      this.buttonHideParameters = new Button();
      this.listViewResults = new ListViewEx();
      this.groupBoxItemAndLocation.SuspendLayout();
      this.numericUpDownMinBattles.BeginInit();
      this.groupBoxSampleSize.SuspendLayout();
      this.numericUpDownLowSampleThreshold.BeginInit();
      this.groupBoxAdditional.SuspendLayout();
      this.SuspendLayout();
      this.groupBoxItemAndLocation.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBoxItemAndLocation.Controls.Add((Control) this.textBoxNameFilter);
      this.groupBoxItemAndLocation.Controls.Add((Control) this.label3);
      this.groupBoxItemAndLocation.Controls.Add((Control) this.label2);
      this.groupBoxItemAndLocation.Controls.Add((Control) this.listBoxWorld);
      this.groupBoxItemAndLocation.Controls.Add((Control) this.labelHelp);
      this.groupBoxItemAndLocation.Controls.Add((Control) this.Battle);
      this.groupBoxItemAndLocation.Controls.Add((Control) this.listBoxBattle);
      this.groupBoxItemAndLocation.Controls.Add((Control) this.labelDungeon);
      this.groupBoxItemAndLocation.Controls.Add((Control) this.listBoxDungeon);
      this.groupBoxItemAndLocation.Controls.Add((Control) this.label1);
      this.groupBoxItemAndLocation.Controls.Add((Control) this.listBoxRealmSynergy);
      this.groupBoxItemAndLocation.Controls.Add((Control) this.labelRarity);
      this.groupBoxItemAndLocation.Controls.Add((Control) this.listBoxRarity);
      this.groupBoxItemAndLocation.Location = new Point(16, 8);
      this.groupBoxItemAndLocation.Name = "groupBoxItemAndLocation";
      this.groupBoxItemAndLocation.Size = new Size(963, 233);
      this.groupBoxItemAndLocation.TabIndex = 0;
      this.groupBoxItemAndLocation.TabStop = false;
      this.groupBoxItemAndLocation.Text = "Item and Location";
      this.textBoxNameFilter.Location = new Point(227, 200);
      this.textBoxNameFilter.Name = "textBoxNameFilter";
      this.textBoxNameFilter.Size = new Size(217, 20);
      this.textBoxNameFilter.TabIndex = 23;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(2, 203);
      this.label3.Name = "label3";
      this.label3.Size = new Size(213, 13);
      this.label3.TabIndex = 22;
      this.label3.Text = "Show only items with a name that contains: ";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(184, 45);
      this.label2.Name = "label2";
      this.label2.Size = new Size(35, 13);
      this.label2.TabIndex = 16;
      this.label2.Text = "World";
      this.listBoxWorld.FormattingEnabled = true;
      this.listBoxWorld.Items.AddRange(new object[3]
      {
        (object) "FF1",
        (object) "FF2",
        (object) "FF3"
      });
      this.listBoxWorld.Location = new Point(187, 63);
      this.listBoxWorld.Name = "listBoxWorld";
      this.listBoxWorld.Size = new Size(186, 121);
      this.listBoxWorld.TabIndex = 15;
      this.listBoxWorld.SelectionCleared += new EventHandler(this.listBoxWorld_SelectionCleared);
      this.listBoxWorld.SelectedIndexChanged += new EventHandler(this.listBoxWorld_SelectedIndexChanged);
      this.labelHelp.AutoSize = true;
      this.labelHelp.Location = new Point(18, 23);
      this.labelHelp.Name = "labelHelp";
      this.labelHelp.Size = new Size(582, 13);
      this.labelHelp.TabIndex = 14;
      this.labelHelp.Text = "Enter search parameters here.  Selecting nothing for a parameter will match everything.  You may select at most one world.";
      this.Battle.AutoSize = true;
      this.Battle.Location = new Point(613, 45);
      this.Battle.Name = "Battle";
      this.Battle.Size = new Size(34, 13);
      this.Battle.TabIndex = 13;
      this.Battle.Text = "Battle";
      this.listBoxBattle.FormattingEnabled = true;
      this.listBoxBattle.Items.AddRange(new object[1]
      {
        (object) "Zozo (Elite) - Whatever"
      });
      this.listBoxBattle.Location = new Point(616, 63);
      this.listBoxBattle.Name = "listBoxBattle";
      this.listBoxBattle.SelectionMode = SelectionMode.MultiExtended;
      this.listBoxBattle.Size = new Size(225, 121);
      this.listBoxBattle.TabIndex = 12;
      this.listBoxBattle.SelectedIndexChanged += new EventHandler(this.listBoxBattle_SelectedIndexChanged);
      this.labelDungeon.AutoSize = true;
      this.labelDungeon.Location = new Point(376, 45);
      this.labelDungeon.Name = "labelDungeon";
      this.labelDungeon.Size = new Size(51, 13);
      this.labelDungeon.TabIndex = 11;
      this.labelDungeon.Text = "Dungeon";
      this.listBoxDungeon.FormattingEnabled = true;
      this.listBoxDungeon.Items.AddRange(new object[3]
      {
        (object) "Zozo",
        (object) "Phantom Train",
        (object) "Darill's Tomb"
      });
      this.listBoxDungeon.Location = new Point(379, 63);
      this.listBoxDungeon.Name = "listBoxDungeon";
      this.listBoxDungeon.Size = new Size(231, 121);
      this.listBoxDungeon.TabIndex = 10;
      this.listBoxDungeon.SelectionCleared += new EventHandler(this.listBoxDungeon_SelectionCleared);
      this.listBoxDungeon.SelectedIndexChanged += new EventHandler(this.listBoxDungeon_SelectedIndexChanged);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(97, 45);
      this.label1.Name = "label1";
      this.label1.Size = new Size(78, 13);
      this.label1.TabIndex = 5;
      this.label1.Text = "Realm Synergy";
      this.listBoxRealmSynergy.FormattingEnabled = true;
      this.listBoxRealmSynergy.Items.AddRange(new object[14]
      {
        (object) "Core",
        (object) "I",
        (object) "II",
        (object) "III",
        (object) "IV",
        (object) "V",
        (object) "VI",
        (object) "VII",
        (object) "VIII",
        (object) "IX",
        (object) "X",
        (object) "XI",
        (object) "XII",
        (object) "XIII"
      });
      this.listBoxRealmSynergy.Location = new Point(100, 63);
      this.listBoxRealmSynergy.Name = "listBoxRealmSynergy";
      this.listBoxRealmSynergy.SelectionMode = SelectionMode.MultiExtended;
      this.listBoxRealmSynergy.Size = new Size(82, 121);
      this.listBoxRealmSynergy.TabIndex = 4;
      this.labelRarity.AutoSize = true;
      this.labelRarity.Location = new Point(9, 45);
      this.labelRarity.Name = "labelRarity";
      this.labelRarity.Size = new Size(34, 13);
      this.labelRarity.TabIndex = 3;
      this.labelRarity.Text = "Rarity";
      this.listBoxRarity.FormattingEnabled = true;
      this.listBoxRarity.Items.AddRange(new object[5]
      {
        (object) "1",
        (object) "2",
        (object) "3",
        (object) "4",
        (object) "5"
      });
      this.listBoxRarity.Location = new Point(12, 63);
      this.listBoxRarity.Name = "listBoxRarity";
      this.listBoxRarity.SelectionMode = SelectionMode.MultiExtended;
      this.listBoxRarity.Size = new Size(82, 121);
      this.listBoxRarity.TabIndex = 2;
      this.label4.AutoSize = true;
      this.label4.Location = new Point(362, 67);
      this.label4.Name = "label4";
      this.label4.Size = new Size(48, 13);
      this.label4.TabIndex = 20;
      this.label4.Text = "samples.";
      this.numericUpDownMinBattles.Enabled = false;
      this.numericUpDownMinBattles.Location = new Point(301, 64);
      this.numericUpDownMinBattles.Maximum = new Decimal(new int[4]
      {
        1000,
        0,
        0,
        0
      });
      this.numericUpDownMinBattles.Name = "numericUpDownMinBattles";
      this.numericUpDownMinBattles.Size = new Size(57, 20);
      this.numericUpDownMinBattles.TabIndex = 19;
      this.numericUpDownMinBattles.ValueChanged += new EventHandler(this.numericUpDownMinBattles_ValueChanged);
      this.checkBoxRepeatable.AutoSize = true;
      this.checkBoxRepeatable.Location = new Point(10, 19);
      this.checkBoxRepeatable.Name = "checkBoxRepeatable";
      this.checkBoxRepeatable.Size = new Size(154, 17);
      this.checkBoxRepeatable.TabIndex = 21;
      this.checkBoxRepeatable.Text = "Show only repeatable runs.";
      this.checkBoxRepeatable.UseVisualStyleBackColor = true;
      this.checkBoxRepeatable.CheckedChanged += new EventHandler(this.checkBoxRepeatable_CheckedChanged);
      this.buttonResetAll.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonResetAll.Location = new Point(761, 579);
      this.buttonResetAll.Name = "buttonResetAll";
      this.buttonResetAll.Size = new Size(120, 27);
      this.buttonResetAll.TabIndex = 17;
      this.buttonResetAll.Text = "Reset all selections";
      this.buttonResetAll.UseVisualStyleBackColor = true;
      this.buttonResetAll.Click += new EventHandler(this.buttonResetAll_Click);
      this.buttonSearch.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonSearch.Location = new Point(887, 579);
      this.buttonSearch.Name = "buttonSearch";
      this.buttonSearch.Size = new Size(92, 27);
      this.buttonSearch.TabIndex = 2;
      this.buttonSearch.Text = "Search";
      this.buttonSearch.UseVisualStyleBackColor = true;
      this.buttonSearch.Click += new EventHandler(this.buttonSearch_Click);
      this.groupBoxSampleSize.Controls.Add((Control) this.numericUpDownLowSampleThreshold);
      this.groupBoxSampleSize.Controls.Add((Control) this.radioButtonMinSamples);
      this.groupBoxSampleSize.Controls.Add((Control) this.radioButtonHelp);
      this.groupBoxSampleSize.Controls.Add((Control) this.label4);
      this.groupBoxSampleSize.Controls.Add((Control) this.radioButtonAllSamples);
      this.groupBoxSampleSize.Controls.Add((Control) this.numericUpDownMinBattles);
      this.groupBoxSampleSize.Location = new Point(16, 280);
      this.groupBoxSampleSize.Name = "groupBoxSampleSize";
      this.groupBoxSampleSize.Size = new Size(515, 101);
      this.groupBoxSampleSize.TabIndex = 18;
      this.groupBoxSampleSize.TabStop = false;
      this.groupBoxSampleSize.Text = "Sample Size";
      this.numericUpDownLowSampleThreshold.Enabled = false;
      this.numericUpDownLowSampleThreshold.Location = new Point(444, 41);
      this.numericUpDownLowSampleThreshold.Maximum = new Decimal(new int[4]
      {
        50,
        0,
        0,
        0
      });
      this.numericUpDownLowSampleThreshold.Name = "numericUpDownLowSampleThreshold";
      this.numericUpDownLowSampleThreshold.Size = new Size(54, 20);
      this.numericUpDownLowSampleThreshold.TabIndex = 21;
      this.numericUpDownLowSampleThreshold.ValueChanged += new EventHandler(this.numericUpDownLowSampleThreshold_ValueChanged);
      this.radioButtonMinSamples.AutoSize = true;
      this.radioButtonMinSamples.Location = new Point(12, 65);
      this.radioButtonMinSamples.Name = "radioButtonMinSamples";
      this.radioButtonMinSamples.Size = new Size(266, 17);
      this.radioButtonMinSamples.TabIndex = 2;
      this.radioButtonMinSamples.Text = "I just want my stuff!  Show only battles with at least ";
      this.radioButtonMinSamples.UseVisualStyleBackColor = true;
      this.radioButtonMinSamples.CheckedChanged += new EventHandler(this.radioButtonMinSamples_CheckedChanged);
      this.radioButtonHelp.AutoSize = true;
      this.radioButtonHelp.Location = new Point(12, 42);
      this.radioButtonHelp.Name = "radioButtonHelp";
      this.radioButtonHelp.Size = new Size(401, 17);
      this.radioButtonHelp.TabIndex = 1;
      this.radioButtonHelp.Text = "I want to help!  Show only results that need more data.  Low sample threshold = ";
      this.radioButtonHelp.UseVisualStyleBackColor = true;
      this.radioButtonHelp.CheckedChanged += new EventHandler(this.radioButtonHelp_CheckedChanged);
      this.radioButtonAllSamples.AutoSize = true;
      this.radioButtonAllSamples.Checked = true;
      this.radioButtonAllSamples.Location = new Point(12, 19);
      this.radioButtonAllSamples.Name = "radioButtonAllSamples";
      this.radioButtonAllSamples.Size = new Size(224, 17);
      this.radioButtonAllSamples.TabIndex = 0;
      this.radioButtonAllSamples.TabStop = true;
      this.radioButtonAllSamples.Text = "Show all results, regardless of sample size.";
      this.radioButtonAllSamples.UseVisualStyleBackColor = true;
      this.radioButtonAllSamples.CheckedChanged += new EventHandler(this.radioButtonAllSamples_CheckedChanged);
      this.groupBoxAdditional.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBoxAdditional.Controls.Add((Control) this.checkBoxNoInactive);
      this.groupBoxAdditional.Controls.Add((Control) this.checkBoxUseStamToReach);
      this.groupBoxAdditional.Controls.Add((Control) this.checkBoxRepeatable);
      this.groupBoxAdditional.Location = new Point(537, 280);
      this.groupBoxAdditional.Name = "groupBoxAdditional";
      this.groupBoxAdditional.Size = new Size(442, 101);
      this.groupBoxAdditional.TabIndex = 19;
      this.groupBoxAdditional.TabStop = false;
      this.groupBoxAdditional.Text = "Additional Options";
      this.checkBoxNoInactive.AutoSize = true;
      this.checkBoxNoInactive.Location = new Point(10, 65);
      this.checkBoxNoInactive.Name = "checkBoxNoInactive";
      this.checkBoxNoInactive.Size = new Size(195, 17);
      this.checkBoxNoInactive.TabIndex = 23;
      this.checkBoxNoInactive.Text = "Don't show runs for inactive events.";
      this.checkBoxNoInactive.UseVisualStyleBackColor = true;
      this.checkBoxNoInactive.CheckedChanged += new EventHandler(this.checkBoxNoInactive_CheckedChanged);
      this.checkBoxUseStamToReach.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.checkBoxUseStamToReach.CheckAlign = ContentAlignment.TopLeft;
      this.checkBoxUseStamToReach.Checked = true;
      this.checkBoxUseStamToReach.CheckState = CheckState.Checked;
      this.checkBoxUseStamToReach.Location = new Point(10, 41);
      this.checkBoxUseStamToReach.Name = "checkBoxUseStamToReach";
      this.checkBoxUseStamToReach.Size = new Size(421, 33);
      this.checkBoxUseStamToReach.TabIndex = 22;
      this.checkBoxUseStamToReach.Text = "For non-repeatable runs, factor in Stamina to Reach when computing Stamina/Drop";
      this.checkBoxUseStamToReach.TextAlign = ContentAlignment.TopLeft;
      this.checkBoxUseStamToReach.UseVisualStyleBackColor = true;
      this.checkBoxUseStamToReach.CheckedChanged += new EventHandler(this.checkBoxUseStamToReach_CheckedChanged);
      this.buttonHideParameters.Location = new Point(16, 247);
      this.buttonHideParameters.Name = "buttonHideParameters";
      this.buttonHideParameters.Size = new Size((int) sbyte.MaxValue, 27);
      this.buttonHideParameters.TabIndex = 20;
      this.buttonHideParameters.Text = "Hide Parameters ↑";
      this.buttonHideParameters.UseVisualStyleBackColor = true;
      this.buttonHideParameters.Click += new EventHandler(this.buttonHideParameters_Click);
      this.listViewResults.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.listViewResults.DataBinding = (IListViewBinding) null;
      this.listViewResults.FullRowSelect = true;
      this.listViewResults.HideSelection = false;
      this.listViewResults.Location = new Point(16, 387);
      this.listViewResults.Name = "listViewResults";
      this.listViewResults.SettingsKey = "FFRKViewItemSearch_ListViewResults";
      this.listViewResults.Size = new Size(963, 186);
      this.listViewResults.TabIndex = 1;
      this.listViewResults.UseCompatibleStateImageBehavior = false;
      this.listViewResults.View = View.Details;
      this.listViewResults.VirtualMode = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.buttonHideParameters);
      this.Controls.Add((Control) this.groupBoxAdditional);
      this.Controls.Add((Control) this.groupBoxSampleSize);
      this.Controls.Add((Control) this.buttonSearch);
      this.Controls.Add((Control) this.listViewResults);
      this.Controls.Add((Control) this.groupBoxItemAndLocation);
      this.Controls.Add((Control) this.buttonResetAll);
      this.DoubleBuffered = true;
      this.Name = nameof (FFRKViewItemSearch);
      this.Size = new Size(997, 622);
      this.Load += new EventHandler(this.FFRKViewItemSearch_Load);
      this.groupBoxItemAndLocation.ResumeLayout(false);
      this.groupBoxItemAndLocation.PerformLayout();
      this.numericUpDownMinBattles.EndInit();
      this.groupBoxSampleSize.ResumeLayout(false);
      this.groupBoxSampleSize.PerformLayout();
      this.numericUpDownLowSampleThreshold.EndInit();
      this.groupBoxAdditional.ResumeLayout(false);
      this.groupBoxAdditional.PerformLayout();
      this.ResumeLayout(false);
    }

    private class WorldListItem
    {
      private FFRKInspector.DataCache.Worlds.Key mKey;
      private FFRKInspector.DataCache.Worlds.Data mData;

      public uint WorldId => this.mKey.WorldId;

      public WorldListItem(FFRKInspector.DataCache.Worlds.Key key, FFRKInspector.DataCache.Worlds.Data data)
      {
        this.mKey = key;
        this.mData = data;
      }

      public override string ToString() => this.mData.Name;
    }

    private class DungeonListItem
    {
      private FFRKInspector.DataCache.Dungeons.Key mKey;
      private FFRKInspector.DataCache.Dungeons.Data mData;

      public uint DungeonId => this.mKey.DungeonId;

      public DungeonListItem(FFRKInspector.DataCache.Dungeons.Key key, FFRKInspector.DataCache.Dungeons.Data data)
      {
        this.mKey = key;
        this.mData = data;
      }

      public override string ToString()
      {
        string name = this.mData.Name;
        if (this.mData.Type == SchemaConstants.DungeonType.Elite)
          name += " (Elite)";
        return name;
      }
    }

    private class BattleListItem
    {
      private FFRKInspector.DataCache.Battles.Key mKey;
      private FFRKInspector.DataCache.Battles.Data mData;

      public uint BattleId => this.mKey.BattleId;

      public BattleListItem(FFRKInspector.DataCache.Battles.Key key, FFRKInspector.DataCache.Battles.Data data)
      {
        this.mKey = key;
        this.mData = data;
      }

      public override string ToString() => this.mData.Name;
    }

    private delegate string GetListViewField(BasicItemDropStats item);
  }
}
