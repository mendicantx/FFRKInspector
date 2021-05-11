// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.FFRKViewInventory
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Analyzer;
using FFRKInspector.DataCache.Items;
using FFRKInspector.GameData;
using FFRKInspector.GameData.Party;
using FFRKInspector.Proxy;
using FFRKInspector.UI.DatabaseUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  public class FFRKViewInventory : UserControl
  {
    private ComboBox comboBoxFilterType;
    private ComboBox comboBoxScoreSelection;
    private ComboBox comboBoxSynergy;
    private ComboBox comboBoxUpgradeMode;
    private ComboBox comboBoxViewMode;
    private IContainer components = (IContainer) null;
    private DataGridViewEx dataGridViewBuddies;
    private DataGridView dataGridViewEquipment;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
    private DataGridViewTextBoxColumn dgcATK;
    private DataGridViewTextBoxColumn dgcCategory;
    private DataGridViewComboBoxColumn dgcCharacterDefensiveStat;
    private DataGridViewTextBoxColumn dgcCharacterLevel;
    private DataGridViewTextBoxColumn dgcCharacterMaxLevel;
    private DataGridViewTextBoxColumn dgcCharacterName;
    private DataGridViewComboBoxColumn dgcCharacterOffensiveStat;
    private DataGridViewCheckBoxColumn dgcCharacterOptimize;
    private DataGridViewTextBoxColumn dgcDEF;
    private DataGridViewTextBoxColumn dgcItem;
    private DataGridViewTextBoxColumn dgcItemID;
    private DataGridViewTextBoxColumn dgcLevel;
    private DataGridViewTextBoxColumn dgcMAG;
    private DataGridViewTextBoxColumn dgcMND;
    private DataGridViewTextBoxColumn dgcRarity;
    private DataGridViewTextBoxColumn dgcRES;
    private DataGridViewTextBoxColumn dgcScore;
    private DataGridViewTextBoxColumn dgcSynergy;
    private DataGridViewTextBoxColumn dgcType;
    private ContextMenuStrip exportContext;
    private ToolStripMenuItem exportCSVInventoryToolStripMenuItem;
    private ToolStripMenuItem exportJSONInventoryToolStripMenuItem;
    private GroupBox groupBox3;
    private GroupBox groupBox4;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private Label label5;
    private Label label6;
    private Label label7;
    private LinkLabel linkLabelAlgo;
    private LinkLabel linkLabelMissing;
    private EquipmentAnalyzer mAnalyzer;
    private AnalyzerSettings mAnalyzerSettings;
    private DataBuddyInformation[] mBuddies;
    private ListListViewBinding<DataBuddyInformation> mBuddyList;
    private DataEquipmentInformation[] mEquipments;

    public FFRKViewInventory()
    {
      this.InitializeComponent();
      this.mBuddyList = new ListListViewBinding<DataBuddyInformation>();
      foreach (RealmSynergy.SynergyValue Synergy in RealmSynergy.Values)
        this.comboBoxSynergy.Items.Add((object) new FFRKViewInventory.SynergyFormatter(Synergy));
      this.comboBoxViewMode.SelectedIndex = 0;
      this.comboBoxUpgradeMode.SelectedIndex = 0;
      this.comboBoxSynergy.SelectedIndex = 0;
      this.comboBoxFilterType.SelectedIndex = 0;
      this.comboBoxScoreSelection.SelectedIndex = 2;
      this.dgcCharacterDefensiveStat.CellTemplate = (DataGridViewCell) new EnumDataViewGridComboBoxCell<AnalyzerSettings.DefensiveStat>();
      this.dgcCharacterOffensiveStat.CellTemplate = (DataGridViewCell) new EnumDataViewGridComboBoxCell<AnalyzerSettings.OffensiveStat>();
    }

    private void comboBoxFilterType_SelectedIndexChanged(object sender, EventArgs e) => this.RecalculateInventory();

    private void ComboBoxFilterType_SelectedIndexChanged(object sender, EventArgs e) => throw new NotImplementedException();

    private void comboBoxScoreSelection_SelectedIndexChanged(object sender, EventArgs e) => this.RecomputeAllScores();

    private void comboBoxSynergy_SelectedIndexChanged(object sender, EventArgs e) => this.RecomputeAllItemStats();

    private void comboBoxUpgradeMode_SelectedIndexChanged(object sender, EventArgs e) => this.RecomputeAllItemStats();

    private void comboBoxViewMode_SelectedIndexChanged(object sender, EventArgs e) => this.RecomputeAllItemStats();

    private FFRKViewInventory.GridEquipStats ComputeDisplayStats(
      DataEquipmentInformation equip)
    {
      FFRKViewInventory.ViewUpgradeModeComboIndex selectedIndex = (FFRKViewInventory.ViewUpgradeModeComboIndex) this.comboBoxUpgradeMode.SelectedIndex;
      RealmSynergy.SynergyValue synergyValue = RealmSynergy.Values.ElementAt<RealmSynergy.SynergyValue>(this.comboBoxSynergy.SelectedIndex);
      Data data;
      bool flag = FFRKProxy.Instance.Cache.Items.TryGetValue(new Key()
      {
        ItemId = equip.EquipmentId
      }, out data);
      FFRKViewInventory.GridEquipStats gridEquipStats = new FFRKViewInventory.GridEquipStats();
      switch (selectedIndex)
      {
        case FFRKViewInventory.ViewUpgradeModeComboIndex.CurrentUpgradeCurrentLevel:
          gridEquipStats.Stats.Atk = new short?((int) equip.SeriesId == (int) synergyValue.GameSeries ? equip.SeriesAtk : equip.Atk);
          gridEquipStats.Stats.Mag = new short?((int) equip.SeriesId == (int) synergyValue.GameSeries ? equip.SeriesMag : equip.Mag);
          gridEquipStats.Stats.Acc = new short?((int) equip.SeriesId == (int) synergyValue.GameSeries ? equip.SeriesAcc : equip.Acc);
          gridEquipStats.Stats.Def = new short?((int) equip.SeriesId == (int) synergyValue.GameSeries ? equip.SeriesDef : equip.Def);
          gridEquipStats.Stats.Res = new short?((int) equip.SeriesId == (int) synergyValue.GameSeries ? equip.SeriesRes : equip.Res);
          gridEquipStats.Stats.Eva = new short?((int) equip.SeriesId == (int) synergyValue.GameSeries ? equip.SeriesEva : equip.Eva);
          gridEquipStats.Stats.Mnd = new short?((int) equip.SeriesId == (int) synergyValue.GameSeries ? equip.SeriesMnd : equip.Mnd);
          gridEquipStats.Level = equip.Level;
          gridEquipStats.MaxLevel = equip.LevelMax;
          if ((int) equip.SeriesId == (int) synergyValue.GameSeries)
            gridEquipStats.Level = StatCalculator.EffectiveLevelWithSynergy(gridEquipStats.Level);
          return gridEquipStats;
        case FFRKViewInventory.ViewUpgradeModeComboIndex.CurrentUpgradeMaxLevel:
          gridEquipStats.MaxLevel = StatCalculator.MaxLevel(equip.Rarity);
          break;
        case FFRKViewInventory.ViewUpgradeModeComboIndex.MaxLevelThroughExistingCombine:
          int max_times = ((IEnumerable<DataEquipmentInformation>) this.mEquipments).Count<DataEquipmentInformation>((Func<DataEquipmentInformation, bool>) (x => (int) x.EquipmentId == (int) equip.EquipmentId && (int) x.InstanceId != (int) equip.InstanceId && x.Rarity <= equip.Rarity));
          gridEquipStats.MaxLevel = StatCalculator.MaxLevel(StatCalculator.EvolveAsMuchAsPossible(equip.BaseRarity, equip.Rarity, max_times));
          break;
        default:
          gridEquipStats.MaxLevel = StatCalculator.MaxLevel(StatCalculator.Evolve(equip.BaseRarity, SchemaConstants.EvolutionLevel.PlusPlus));
          break;
      }
      gridEquipStats.Level = gridEquipStats.MaxLevel;
      if ((int) equip.SeriesId == (int) synergyValue.GameSeries)
        gridEquipStats.Level = StatCalculator.EffectiveLevelWithSynergy(gridEquipStats.Level);
      if (flag && data.MaxStats != null && data.BaseStats != null)
      {
        gridEquipStats.Stats.Atk = new short?(StatCalculator.ComputeStatForLevel(equip.BaseRarity, data.BaseStats.Atk, data.MaxStats.Atk, gridEquipStats.Level));
        gridEquipStats.Stats.Mag = new short?(StatCalculator.ComputeStatForLevel(equip.BaseRarity, data.BaseStats.Mag, data.MaxStats.Mag, gridEquipStats.Level));
        gridEquipStats.Stats.Acc = new short?(StatCalculator.ComputeStatForLevel(equip.BaseRarity, data.BaseStats.Acc, data.MaxStats.Acc, gridEquipStats.Level));
        gridEquipStats.Stats.Def = new short?(StatCalculator.ComputeStatForLevel(equip.BaseRarity, data.BaseStats.Def, data.MaxStats.Def, gridEquipStats.Level));
        gridEquipStats.Stats.Res = new short?(StatCalculator.ComputeStatForLevel(equip.BaseRarity, data.BaseStats.Res, data.MaxStats.Res, gridEquipStats.Level));
        gridEquipStats.Stats.Eva = new short?(StatCalculator.ComputeStatForLevel(equip.BaseRarity, data.BaseStats.Eva, data.MaxStats.Eva, gridEquipStats.Level));
        gridEquipStats.Stats.Mnd = new short?(StatCalculator.ComputeStatForLevel(equip.BaseRarity, data.BaseStats.Mnd, data.MaxStats.Mnd, gridEquipStats.Level));
        return gridEquipStats;
      }
      byte vlevel2 = StatCalculator.EffectiveLevelWithSynergy(equip.Level);
      gridEquipStats.Stats.Atk = new short?(StatCalculator.ComputeStatForLevel2(equip.Atk, equip.Level, equip.SeriesAtk, vlevel2, gridEquipStats.Level));
      gridEquipStats.Stats.Mag = new short?(StatCalculator.ComputeStatForLevel2(equip.Mag, equip.Level, equip.SeriesMag, vlevel2, gridEquipStats.Level));
      gridEquipStats.Stats.Acc = new short?(StatCalculator.ComputeStatForLevel2(equip.Acc, equip.Level, equip.SeriesAcc, vlevel2, gridEquipStats.Level));
      gridEquipStats.Stats.Def = new short?(StatCalculator.ComputeStatForLevel2(equip.Def, equip.Level, equip.SeriesDef, vlevel2, gridEquipStats.Level));
      gridEquipStats.Stats.Res = new short?(StatCalculator.ComputeStatForLevel2(equip.Res, equip.Level, equip.SeriesRes, vlevel2, gridEquipStats.Level));
      gridEquipStats.Stats.Eva = new short?(StatCalculator.ComputeStatForLevel2(equip.Eva, equip.Level, equip.SeriesEva, vlevel2, gridEquipStats.Level));
      gridEquipStats.Stats.Mnd = new short?(StatCalculator.ComputeStatForLevel2(equip.Mnd, equip.Level, equip.SeriesMnd, vlevel2, gridEquipStats.Level));
      return gridEquipStats;
    }

    private void dataGridViewBuddies_CellValueChanged(object sender, DataGridViewCellEventArgs e)
    {
      if (this.mAnalyzerSettings == null)
        return;
      DataGridViewColumn column = this.dataGridViewBuddies.Columns[e.ColumnIndex];
      DataGridViewRow row = this.dataGridViewBuddies.Rows[e.RowIndex];
      AnalyzerSettings.PartyMemberSettings mAnalyzerSetting = this.mAnalyzerSettings[((DataBuddyInformation) row.Tag).Name];
      if (mAnalyzerSetting != null && this.dataGridViewBuddies.CurrentCell != null)
      {
        bool flag = false;
        if (column == this.dgcCharacterOffensiveStat)
          flag = this.SetValueIfDirty<AnalyzerSettings.OffensiveStat>(row.Cells[e.ColumnIndex].Value, ref mAnalyzerSetting.OffensiveStat);
        else if (column == this.dgcCharacterDefensiveStat)
          flag = this.SetValueIfDirty<AnalyzerSettings.DefensiveStat>(row.Cells[e.ColumnIndex].Value, ref mAnalyzerSetting.DefensiveStat);
        else if (column == this.dgcCharacterOptimize)
          flag = this.SetValueIfDirty<bool>(row.Cells[e.ColumnIndex].Value, ref mAnalyzerSetting.Score);
        if (flag)
          this.RecomputeAllScores();
      }
    }

    private void dataGridViewEquipment_CellContentClick(object sender, DataGridViewCellEventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void exportCSVInventoryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        saveFileDialog1.Filter = "CSV files (*.csv)|*.csv";
        saveFileDialog1.FilterIndex = 0;
        saveFileDialog1.RestoreDirectory = false;
        SaveFileDialog saveFileDialog2 = saveFileDialog1;
        if (saveFileDialog2.ShowDialog() == DialogResult.OK)
        {
          using (Stream stream = saveFileDialog2.OpenFile())
          {
            using (StreamWriter streamWriter = new StreamWriter(stream))
            {
              DataGridViewRow dataGridViewRow = new DataGridViewRow();
              for (int index = 0; index <= this.dataGridViewEquipment.Columns.Count - 1; ++index)
              {
                if (index > 0)
                  streamWriter.Write(",");
                streamWriter.Write("\"" + this.dataGridViewEquipment.Columns[index].HeaderText + "\"");
              }
              streamWriter.WriteLine();
              for (int index1 = 0; index1 <= this.dataGridViewEquipment.Rows.Count - 1; ++index1)
              {
                if (index1 > 0)
                  streamWriter.WriteLine();
                DataGridViewRow row = this.dataGridViewEquipment.Rows[index1];
                for (int index2 = 0; index2 <= this.dataGridViewEquipment.Columns.Count - 1; ++index2)
                {
                  if (index2 > 0)
                    streamWriter.Write(",");
                  string str = row.Cells[index2].Value.ToString().Replace(',', ' ').Replace('＋', '+');
                  streamWriter.Write(str);
                }
              }
            }
          }
        }
        int num = (int) MessageBox.Show(string.Format("Inventory successfully exported."));
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("FFRK Inspector encountered an error while exporting the data.  " + ex.Message);
      }
    }

    private void exportJSONInventoryToolStripMenuItem_Click(object sender, EventArgs e)
    {
      try
      {
        SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        saveFileDialog1.Filter = "Text files (*.txt)|*.txt";
        saveFileDialog1.FilterIndex = 0;
        saveFileDialog1.RestoreDirectory = false;
        SaveFileDialog saveFileDialog2 = saveFileDialog1;
        if (saveFileDialog2.ShowDialog() == DialogResult.OK)
        {
          using (Stream stream = saveFileDialog2.OpenFile())
          {
            using (StreamWriter streamWriter = new StreamWriter(stream))
            {
              string format = "{{\"n\":\"{0}\",\"l\":{1}}}";
              DataGridViewRow dataGridViewRow = new DataGridViewRow();
              streamWriter.Write("[");
              for (int index = 0; index <= this.dataGridViewEquipment.Rows.Count - 1; ++index)
              {
                if (index > 0)
                  streamWriter.WriteLine(',');
                string str1 = this.dataGridViewEquipment.Rows[index].Cells["dgcItem"].Value.ToString().Replace('"', '"').Replace('＋', ' ').Replace('+', ' ').Trim();
                string str2 = new string(this.dataGridViewEquipment.Rows[index].Cells["dgcLevel"].Value.ToString().TakeWhile<char>((Func<char, bool>) (val => char.IsNumber(val))).ToArray<char>());
                streamWriter.Write(string.Format(format, (object) str1, (object) str2));
              }
              streamWriter.Write("]");
            }
          }
        }
        int num = (int) MessageBox.Show(string.Format("Names and levels successfully exported to JSON."));
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("FFRK Inspector encountered an error while exporting the data.  " + ex.Message);
      }
    }

    private void FFRKProxy_OnPartyList(DataPartyDetails party) => this.BeginInvoke((Action) (() =>
    {
      this.mAnalyzer.Items = party.Equipments;
      this.mAnalyzer.Buddies = party.Buddies;
      this.mAnalyzer.Run();
      this.UpdatePartyGrid(((IEnumerable<DataBuddyInformation>) party.Buddies).ToList<DataBuddyInformation>());
      this.UpdateEquipmentGrid(party.Equipments);
    }));

    private void FFRKViewInventory_Load(object sender, EventArgs e)
    {
      if (this.DesignMode || FFRKProxy.Instance == null)
        return;
      this.mAnalyzerSettings = AnalyzerSettings.DefaultSettings;
      this.mAnalyzerSettings.LevelConsideration = this.TranslateLevelConsideration((FFRKViewInventory.ScoreUpgradeModeComboIndex) this.comboBoxScoreSelection.SelectedIndex);
      this.mAnalyzer = new EquipmentAnalyzer(this.mAnalyzerSettings);
      FFRKProxy.Instance.OnPartyList += new FFRKProxy.FFRKPartyListDelegate(this.FFRKProxy_OnPartyList);
      DataPartyDetails partyDetails = FFRKProxy.Instance.GameState.PartyDetails;
      if (partyDetails != null)
      {
        this.mAnalyzer.Items = partyDetails.Equipments;
        this.mAnalyzer.Buddies = partyDetails.Buddies;
        this.mAnalyzer.Run();
        this.UpdatePartyGrid(((IEnumerable<DataBuddyInformation>) partyDetails.Buddies).ToList<DataBuddyInformation>());
        this.UpdateEquipmentGrid(partyDetails.Equipments);
      }
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FFRKViewInventory));
      this.dataGridViewEquipment = new DataGridView();
      this.exportContext = new ContextMenuStrip(this.components);
      this.exportCSVInventoryToolStripMenuItem = new ToolStripMenuItem();
      this.exportJSONInventoryToolStripMenuItem = new ToolStripMenuItem();
      this.comboBoxUpgradeMode = new ComboBox();
      this.groupBox3 = new GroupBox();
      this.groupBox4 = new GroupBox();
      this.label6 = new Label();
      this.comboBoxFilterType = new ComboBox();
      this.linkLabelAlgo = new LinkLabel();
      this.label7 = new Label();
      this.linkLabelMissing = new LinkLabel();
      this.comboBoxScoreSelection = new ComboBox();
      this.label5 = new Label();
      this.label4 = new Label();
      this.label2 = new Label();
      this.comboBoxSynergy = new ComboBox();
      this.label1 = new Label();
      this.comboBoxViewMode = new ComboBox();
      this.label3 = new Label();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
      this.dataGridViewBuddies = new DataGridViewEx();
      this.dgcCharacterName = new DataGridViewTextBoxColumn();
      this.dgcCharacterLevel = new DataGridViewTextBoxColumn();
      this.dgcCharacterMaxLevel = new DataGridViewTextBoxColumn();
      this.dgcCharacterOptimize = new DataGridViewCheckBoxColumn();
      this.dgcCharacterOffensiveStat = new DataGridViewComboBoxColumn();
      this.dgcCharacterDefensiveStat = new DataGridViewComboBoxColumn();
      this.dgcItemID = new DataGridViewTextBoxColumn();
      this.dgcItem = new DataGridViewTextBoxColumn();
      this.dgcCategory = new DataGridViewTextBoxColumn();
      this.dgcType = new DataGridViewTextBoxColumn();
      this.dgcRarity = new DataGridViewTextBoxColumn();
      this.dgcSynergy = new DataGridViewTextBoxColumn();
      this.dgcLevel = new DataGridViewTextBoxColumn();
      this.dgcATK = new DataGridViewTextBoxColumn();
      this.dgcMAG = new DataGridViewTextBoxColumn();
      this.dgcMND = new DataGridViewTextBoxColumn();
      this.dgcDEF = new DataGridViewTextBoxColumn();
      this.dgcRES = new DataGridViewTextBoxColumn();
      this.dgcScore = new DataGridViewTextBoxColumn();
      ((ISupportInitialize) this.dataGridViewEquipment).BeginInit();
      this.exportContext.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      ((ISupportInitialize) this.dataGridViewBuddies).BeginInit();
      this.SuspendLayout();
      this.dataGridViewEquipment.AllowUserToAddRows = false;
      this.dataGridViewEquipment.AllowUserToDeleteRows = false;
      this.dataGridViewEquipment.AllowUserToResizeRows = false;
      gridViewCellStyle1.BackColor = Color.FromArgb(252, 213, 180);
      this.dataGridViewEquipment.AlternatingRowsDefaultCellStyle = gridViewCellStyle1;
      this.dataGridViewEquipment.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dataGridViewEquipment.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewEquipment.Columns.AddRange((DataGridViewColumn) this.dgcItemID, (DataGridViewColumn) this.dgcItem, (DataGridViewColumn) this.dgcCategory, (DataGridViewColumn) this.dgcType, (DataGridViewColumn) this.dgcRarity, (DataGridViewColumn) this.dgcSynergy, (DataGridViewColumn) this.dgcLevel, (DataGridViewColumn) this.dgcATK, (DataGridViewColumn) this.dgcMAG, (DataGridViewColumn) this.dgcMND, (DataGridViewColumn) this.dgcDEF, (DataGridViewColumn) this.dgcRES, (DataGridViewColumn) this.dgcScore);
      this.dataGridViewEquipment.ContextMenuStrip = this.exportContext;
      this.dataGridViewEquipment.Location = new Point(13, 92);
      this.dataGridViewEquipment.MultiSelect = false;
      this.dataGridViewEquipment.Name = "dataGridViewEquipment";
      this.dataGridViewEquipment.RowHeadersVisible = false;
      gridViewCellStyle2.BackColor = Color.FromArgb(218, 210, 228);
      this.dataGridViewEquipment.RowsDefaultCellStyle = gridViewCellStyle2;
      this.dataGridViewEquipment.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dataGridViewEquipment.ShowEditingIcon = false;
      this.dataGridViewEquipment.Size = new Size(748, 446);
      this.dataGridViewEquipment.TabIndex = 0;
      this.dataGridViewEquipment.CellContentClick += new DataGridViewCellEventHandler(this.dataGridViewEquipment_CellContentClick);
      this.exportContext.AccessibleName = "";
      this.exportContext.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.exportCSVInventoryToolStripMenuItem,
        (ToolStripItem) this.exportJSONInventoryToolStripMenuItem
      });
      this.exportContext.Name = "exportContext";
      this.exportContext.Size = new Size(161, 26);
      this.exportCSVInventoryToolStripMenuItem.Name = "exportCSVInventoryToolStripMenuItem";
      this.exportCSVInventoryToolStripMenuItem.Size = new Size(160, 22);
      this.exportCSVInventoryToolStripMenuItem.Text = "Export Inventory";
      this.exportCSVInventoryToolStripMenuItem.Click += new EventHandler(this.exportCSVInventoryToolStripMenuItem_Click);
      this.exportJSONInventoryToolStripMenuItem.Name = "exportJSONInventoryToolStripMenuItem";
      this.exportJSONInventoryToolStripMenuItem.Size = new Size(160, 22);
      this.exportJSONInventoryToolStripMenuItem.Text = "Export names and levels to JSON";
      this.exportJSONInventoryToolStripMenuItem.Click += new EventHandler(this.exportJSONInventoryToolStripMenuItem_Click);
      this.comboBoxUpgradeMode.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxUpgradeMode.FormattingEnabled = true;
      this.comboBoxUpgradeMode.Items.AddRange(new object[4]
      {
        (object) "at the items' current rarity and level.",
        (object) "at the items' current rarity and maximum level.",
        (object) "at the maximum level combining only existing equipment.",
        (object) "at the items' maximum rarity and level."
      });
      this.comboBoxUpgradeMode.Location = new Point(260, 40);
      this.comboBoxUpgradeMode.Name = "comboBoxUpgradeMode";
      this.comboBoxUpgradeMode.Size = new Size(292, 21);
      this.comboBoxUpgradeMode.TabIndex = 1;
      this.comboBoxUpgradeMode.SelectedIndexChanged += new EventHandler(this.comboBoxUpgradeMode_SelectedIndexChanged);
      this.groupBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
      this.groupBox3.Controls.Add((Control) this.dataGridViewBuddies);
      this.groupBox3.Location = new Point(3, 3);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(396, 606);
      this.groupBox3.TabIndex = 6;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Available characters";
      this.groupBox4.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox4.Controls.Add((Control) this.label6);
      this.groupBox4.Controls.Add((Control) this.comboBoxFilterType);
      this.groupBox4.Controls.Add((Control) this.linkLabelAlgo);
      this.groupBox4.Controls.Add((Control) this.label7);
      this.groupBox4.Controls.Add((Control) this.linkLabelMissing);
      this.groupBox4.Controls.Add((Control) this.comboBoxScoreSelection);
      this.groupBox4.Controls.Add((Control) this.label5);
      this.groupBox4.Controls.Add((Control) this.label4);
      this.groupBox4.Controls.Add((Control) this.label2);
      this.groupBox4.Controls.Add((Control) this.comboBoxSynergy);
      this.groupBox4.Controls.Add((Control) this.label1);
      this.groupBox4.Controls.Add((Control) this.comboBoxViewMode);
      this.groupBox4.Controls.Add((Control) this.label3);
      this.groupBox4.Controls.Add((Control) this.comboBoxUpgradeMode);
      this.groupBox4.Controls.Add((Control) this.dataGridViewEquipment);
      this.groupBox4.Location = new Point(405, 3);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new Size(767, 606);
      this.groupBox4.TabIndex = 7;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Equipment";
      this.label6.AutoSize = true;
      this.label6.Location = new Point(482, 68);
      this.label6.Name = "label6";
      this.label6.Size = new Size(73, 13);
      this.label6.TabIndex = 14;
      this.label6.Text = "Filter by Type:";
      this.comboBoxFilterType.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxFilterType.FormattingEnabled = true;
      this.comboBoxFilterType.Items.AddRange(new object[4]
      {
        (object) "All",
        (object) "Weapon",
        (object) "Armor",
        (object) "Accessory"
      });
      this.comboBoxFilterType.Location = new Point(561, 65);
      this.comboBoxFilterType.Name = "comboBoxFilterType";
      this.comboBoxFilterType.Size = new Size(150, 21);
      this.comboBoxFilterType.TabIndex = 13;
      this.comboBoxFilterType.SelectedIndexChanged += new EventHandler(this.comboBoxFilterType_SelectedIndexChanged);
      this.linkLabelAlgo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.linkLabelAlgo.AutoSize = true;
      this.linkLabelAlgo.Location = new Point(22, 586);
      this.linkLabelAlgo.Name = "linkLabelAlgo";
      this.linkLabelAlgo.Size = new Size(277, 13);
      this.linkLabelAlgo.TabIndex = 12;
      this.linkLabelAlgo.TabStop = true;
      this.linkLabelAlgo.Text = "Click here to read about how the scoring algorithm works ";
      this.linkLabelAlgo.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabelAlgo_LinkClicked_1);
      this.label7.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.label7.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.label7.Location = new Point(22, 558);
      this.label7.Name = "label7";
      this.label7.Size = new Size(697, 28);
      this.label7.TabIndex = 11;
      this.label7.Text = componentResourceManager.GetString("label7.Text");
      this.linkLabelMissing.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
      this.linkLabelMissing.AutoSize = true;
      this.linkLabelMissing.Location = new Point(22, 543);
      this.linkLabelMissing.Name = "linkLabelMissing";
      this.linkLabelMissing.Size = new Size(495, 13);
      this.linkLabelMissing.TabIndex = 9;
      this.linkLabelMissing.TabStop = true;
      this.linkLabelMissing.Text = "Item's whose score shows N/A may be missing stat information in the database.  Click here to add them.";
      this.linkLabelMissing.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabelMissing_LinkClicked);
      this.comboBoxScoreSelection.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxScoreSelection.FormattingEnabled = true;
      this.comboBoxScoreSelection.Items.AddRange(new object[3]
      {
        (object) "using each item's current rarity and level.",
        (object) "using each item's current rarity and maximum level.",
        (object) "using each item's maximum rarity and level."
      });
      this.comboBoxScoreSelection.Location = new Point(94, 65);
      this.comboBoxScoreSelection.Name = "comboBoxScoreSelection";
      this.comboBoxScoreSelection.Size = new Size((int) byte.MaxValue, 21);
      this.comboBoxScoreSelection.TabIndex = 8;
      this.comboBoxScoreSelection.SelectedIndexChanged += new EventHandler(this.comboBoxScoreSelection_SelectedIndexChanged);
      this.label5.AutoSize = true;
      this.label5.Location = new Point(10, 68);
      this.label5.Name = "label5";
      this.label5.Size = new Size(78, 13);
      this.label5.TabIndex = 7;
      this.label5.Text = "Compute score";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(8, 16);
      this.label4.Name = "label4";
      this.label4.Size = new Size(712, 13);
      this.label4.TabIndex = 6;
      this.label4.Text = "Don't see anything here?  The game only sends this information the first time you view your party screen.  You may need to close and re-open the app.";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(675, 43);
      this.label2.Name = "label2";
      this.label2.Size = new Size(79, 13);
      this.label2.TabIndex = 5;
      this.label2.Text = "record synergy.";
      this.comboBoxSynergy.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxSynergy.FormattingEnabled = true;
      this.comboBoxSynergy.Location = new Point(590, 40);
      this.comboBoxSynergy.Name = "comboBoxSynergy";
      this.comboBoxSynergy.Size = new Size(79, 21);
      this.comboBoxSynergy.TabIndex = 4;
      this.comboBoxSynergy.SelectedIndexChanged += new EventHandler(this.comboBoxSynergy_SelectedIndexChanged);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(558, 43);
      this.label1.Name = "label1";
      this.label1.Size = new Size(26, 13);
      this.label1.TabIndex = 3;
      this.label1.Text = "with";
      this.comboBoxViewMode.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxViewMode.FormattingEnabled = true;
      this.comboBoxViewMode.Items.AddRange(new object[1]
      {
        (object) "Available equipment and their stats"
      });
      this.comboBoxViewMode.Location = new Point(46, 40);
      this.comboBoxViewMode.Name = "comboBoxViewMode";
      this.comboBoxViewMode.Size = new Size(208, 21);
      this.comboBoxViewMode.TabIndex = 2;
      this.comboBoxViewMode.SelectedIndexChanged += new EventHandler(this.comboBoxViewMode_SelectedIndexChanged);
      this.label3.AutoSize = true;
      this.label3.Location = new Point(10, 43);
      this.label3.Name = "label3";
      this.label3.Size = new Size(30, 13);
      this.label3.TabIndex = 1;
      this.label3.Text = "View";
      this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewTextBoxColumn1.HeaderText = "Item";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn1.Width = 43;
      this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewTextBoxColumn2.HeaderText = "Category";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.Width = 52;
      this.dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewTextBoxColumn3.HeaderText = "Type";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.ReadOnly = true;
      this.dataGridViewTextBoxColumn3.Width = 74;
      this.dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn4.HeaderText = "Rarity";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.ReadOnly = true;
      this.dataGridViewTextBoxColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn5.HeaderText = "Synergy";
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn5.ReadOnly = true;
      this.dataGridViewTextBoxColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn6.HeaderText = "Level";
      this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      this.dataGridViewTextBoxColumn6.ReadOnly = true;
      this.dataGridViewTextBoxColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn7.HeaderText = "ATK";
      this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
      this.dataGridViewTextBoxColumn7.ReadOnly = true;
      this.dataGridViewTextBoxColumn8.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn8.HeaderText = "MAG";
      this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
      this.dataGridViewTextBoxColumn8.ReadOnly = true;
      this.dataGridViewTextBoxColumn9.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.dataGridViewTextBoxColumn9.HeaderText = "MND";
      this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
      this.dataGridViewTextBoxColumn9.ReadOnly = true;
      this.dataGridViewTextBoxColumn9.Width = 58;
      this.dataGridViewTextBoxColumn10.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.dataGridViewTextBoxColumn10.HeaderText = "DEF";
      this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
      this.dataGridViewTextBoxColumn10.ReadOnly = true;
      this.dataGridViewTextBoxColumn10.Width = 57;
      this.dataGridViewTextBoxColumn11.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.dataGridViewTextBoxColumn11.HeaderText = "RES";
      this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
      this.dataGridViewTextBoxColumn11.ReadOnly = true;
      this.dataGridViewTextBoxColumn11.Width = 58;
      this.dataGridViewTextBoxColumn12.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
      this.dataGridViewTextBoxColumn12.HeaderText = "Score";
      this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
      this.dataGridViewTextBoxColumn12.ReadOnly = true;
      this.dataGridViewTextBoxColumn12.Width = 57;
      this.dataGridViewTextBoxColumn13.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewTextBoxColumn13.HeaderText = "Score";
      this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
      this.dataGridViewTextBoxColumn13.ReadOnly = true;
      this.dataGridViewTextBoxColumn13.Width = 60;
      this.dataGridViewBuddies.AllowUserToAddRows = false;
      this.dataGridViewBuddies.AllowUserToDeleteRows = false;
      this.dataGridViewBuddies.AllowUserToResizeRows = false;
      this.dataGridViewBuddies.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridViewBuddies.Columns.AddRange((DataGridViewColumn) this.dgcCharacterName, (DataGridViewColumn) this.dgcCharacterLevel, (DataGridViewColumn) this.dgcCharacterMaxLevel, (DataGridViewColumn) this.dgcCharacterOptimize, (DataGridViewColumn) this.dgcCharacterOffensiveStat, (DataGridViewColumn) this.dgcCharacterDefensiveStat);
      this.dataGridViewBuddies.Dock = DockStyle.Fill;
      this.dataGridViewBuddies.Location = new Point(3, 16);
      this.dataGridViewBuddies.Name = "dataGridViewBuddies";
      this.dataGridViewBuddies.RowHeadersVisible = false;
      this.dataGridViewBuddies.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      this.dataGridViewBuddies.Size = new Size(390, 587);
      this.dataGridViewBuddies.TabIndex = 0;
      this.dataGridViewBuddies.CellValueChanged += new DataGridViewCellEventHandler(this.dataGridViewBuddies_CellValueChanged);
      this.dgcCharacterName.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dgcCharacterName.HeaderText = "Name";
      this.dgcCharacterName.Name = "dgcCharacterName";
      this.dgcCharacterName.ReadOnly = true;
      this.dgcCharacterName.Width = 60;
      this.dgcCharacterLevel.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcCharacterLevel.HeaderText = "Level";
      this.dgcCharacterLevel.Name = "dgcCharacterLevel";
      this.dgcCharacterLevel.ReadOnly = true;
      this.dgcCharacterMaxLevel.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcCharacterMaxLevel.HeaderText = "Max";
      this.dgcCharacterMaxLevel.Name = "dgcCharacterMaxLevel";
      this.dgcCharacterMaxLevel.ReadOnly = true;
      this.dgcCharacterOptimize.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcCharacterOptimize.HeaderText = "Score";
      this.dgcCharacterOptimize.Name = "dgcCharacterOptimize";
      this.dgcCharacterOptimize.ToolTipText = "When checked, this character will be considered when computing each piece of equipment's score on the right-hand pane";
      this.dgcCharacterOffensiveStat.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcCharacterOffensiveStat.HeaderText = "Off. Stat";
      this.dgcCharacterOffensiveStat.Name = "dgcCharacterOffensiveStat";
      this.dgcCharacterOffensiveStat.ToolTipText = "Determines what stat the scoring algorithm should prioritize for this character on weapons";
      this.dgcCharacterDefensiveStat.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcCharacterDefensiveStat.HeaderText = "Def. Stat";
      this.dgcCharacterDefensiveStat.Name = "dgcCharacterDefensiveStat";
      this.dgcCharacterDefensiveStat.ToolTipText = "Determine what stat the scoring algorithm should prioritize for this character on armor.";
      this.dgcItemID.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dgcItemID.HeaderText = "ID";
      this.dgcItemID.Name = "dgcItemID";
      this.dgcItemID.ReadOnly = true;
      this.dgcItemID.Width = 43;
      this.dgcItem.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dgcItem.HeaderText = "Item";
      this.dgcItem.Name = "dgcItem";
      this.dgcItem.ReadOnly = true;
      this.dgcItem.Width = 52;
      this.dgcCategory.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dgcCategory.HeaderText = "Category";
      this.dgcCategory.Name = "dgcCategory";
      this.dgcCategory.ReadOnly = true;
      this.dgcCategory.Width = 74;
      this.dgcType.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dgcType.HeaderText = "Type";
      this.dgcType.Name = "dgcType";
      this.dgcType.ReadOnly = true;
      this.dgcType.Width = 56;
      this.dgcRarity.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcRarity.HeaderText = "Rarity";
      this.dgcRarity.Name = "dgcRarity";
      this.dgcRarity.ReadOnly = true;
      this.dgcSynergy.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcSynergy.HeaderText = "Synergy";
      this.dgcSynergy.Name = "dgcSynergy";
      this.dgcSynergy.ReadOnly = true;
      this.dgcLevel.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcLevel.HeaderText = "Level";
      this.dgcLevel.Name = "dgcLevel";
      this.dgcLevel.ReadOnly = true;
      this.dgcATK.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcATK.HeaderText = "ATK";
      this.dgcATK.Name = "dgcATK";
      this.dgcATK.ReadOnly = true;
      this.dgcMAG.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcMAG.HeaderText = "MAG";
      this.dgcMAG.Name = "dgcMAG";
      this.dgcMAG.ReadOnly = true;
      this.dgcMND.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcMND.HeaderText = "MND";
      this.dgcMND.Name = "dgcMND";
      this.dgcMND.ReadOnly = true;
      this.dgcDEF.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcDEF.HeaderText = "DEF";
      this.dgcDEF.Name = "dgcDEF";
      this.dgcDEF.ReadOnly = true;
      this.dgcRES.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dgcRES.HeaderText = "RES";
      this.dgcRES.Name = "dgcRES";
      this.dgcRES.ReadOnly = true;
      this.dgcScore.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dgcScore.HeaderText = "Score";
      this.dgcScore.Name = "dgcScore";
      this.dgcScore.ReadOnly = true;
      this.dgcScore.Width = 60;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.groupBox4);
      this.Controls.Add((Control) this.groupBox3);
      this.Name = nameof (FFRKViewInventory);
      this.Size = new Size(1172, 631);
      this.Load += new EventHandler(this.FFRKViewInventory_Load);
      ((ISupportInitialize) this.dataGridViewEquipment).EndInit();
      this.exportContext.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      ((ISupportInitialize) this.dataGridViewBuddies).EndInit();
      this.ResumeLayout(false);
    }

    private void linkLabelAlgo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://ffrki.wordpress.com/2015/05/30/about-the-inventory-analysis-algorithm/");

    private void linkLabelMissing_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      Control parent = this.Parent;
      while (!(parent is FFRKTabInspector))
        parent = parent.Parent;
      FFRKTabInspector ffrkTabInspector = (FFRKTabInspector) parent;
      ffrkTabInspector.DatabaseTab.DatabaseMode = FFRKViewDatabase.DatabaseModeEnum.MissingItems;
      ffrkTabInspector.SelectedPage = FFRKTabInspector.InspectorPage.Database;
    }

    private void RecalculateInventory()
    {
      if (FFRKProxy.Instance == null)
        return;
      DataPartyDetails partyDetails = FFRKProxy.Instance.GameState.PartyDetails;
      if (partyDetails != null)
        this.UpdateEquipmentGrid(partyDetails.Equipments);
    }

    private void RecomputeAllItemStats()
    {
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridViewEquipment.Rows)
      {
        if (row.Tag is DataEquipmentInformation tag1)
        {
          FFRKViewInventory.GridEquipStats displayStats = this.ComputeDisplayStats(tag1);
          this.SetStatsForRow(row, tag1, displayStats);
        }
      }
      this.ResortByCurrentlySortedColumn();
    }

    private void RecomputeAllScores()
    {
      if (this.mAnalyzer == null)
        return;
      this.mAnalyzerSettings.LevelConsideration = this.TranslateLevelConsideration((FFRKViewInventory.ScoreUpgradeModeComboIndex) this.comboBoxScoreSelection.SelectedIndex);
      this.mAnalyzer.Run();
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridViewEquipment.Rows)
      {
        DataEquipmentInformation tag = (DataEquipmentInformation) row.Tag;
        row.Cells[this.dgcScore.Name].Value = (object) new FFRKViewInventory.ScoreColumnValue(this.mAnalyzer.GetScore(tag.InstanceId));
      }
      this.dataGridViewEquipment.InvalidateColumn(this.dgcScore.Index);
      if (this.dataGridViewEquipment.SortedColumn == this.dgcScore)
        this.ResortByCurrentlySortedColumn();
    }

    private void ResortByCurrentlySortedColumn()
    {
      if ((uint) this.dataGridViewEquipment.SortOrder <= 0U)
        return;
      this.dataGridViewEquipment.Sort(this.dataGridViewEquipment.SortedColumn, this.dataGridViewEquipment.SortOrder == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);
    }

    private void SetStatForCell<T>(
      DataGridViewRow row,
      DataGridViewColumn col,
      T actual_value,
      T display_value)
    {
      if (display_value.Equals((object) default (T)) || (object) display_value == null)
      {
        row.Cells[col.Name].Value = (object) null;
      }
      else
      {
        row.Cells[col.Name].Value = (object) display_value;
        DataGridViewCellStyle style = row.Cells[col.Name].Style;
        if (!actual_value.Equals((object) display_value))
        {
          style.ForeColor = Color.White;
          if (style.Font != null)
            style.Font = new Font(style.Font, FontStyle.Bold);
        }
        else
        {
          style.ForeColor = Color.Black;
          if (style.Font != null)
            style.Font = new Font(style.Font, FontStyle.Regular);
        }
      }
    }

    private void SetStatsForRow(
      DataGridViewRow row,
      DataEquipmentInformation actual_stats,
      FFRKViewInventory.GridEquipStats display_stats)
    {
      this.SetStatForCell<FFRKViewInventory.LevelColumnValue>(row, (DataGridViewColumn) this.dgcLevel, new FFRKViewInventory.LevelColumnValue((int) actual_stats.Level, (int) actual_stats.LevelMax), new FFRKViewInventory.LevelColumnValue((int) display_stats.Level, (int) display_stats.MaxLevel));
      this.SetStatForCell<short?>(row, (DataGridViewColumn) this.dgcATK, new short?(actual_stats.Atk), display_stats.Stats.Atk);
      this.SetStatForCell<short?>(row, (DataGridViewColumn) this.dgcMAG, new short?(actual_stats.Mag), display_stats.Stats.Mag);
      this.SetStatForCell<short?>(row, (DataGridViewColumn) this.dgcMND, new short?(actual_stats.Mnd), display_stats.Stats.Mnd);
      this.SetStatForCell<short?>(row, (DataGridViewColumn) this.dgcDEF, new short?(actual_stats.Def), display_stats.Stats.Def);
      this.SetStatForCell<short?>(row, (DataGridViewColumn) this.dgcRES, new short?(actual_stats.Res), display_stats.Stats.Res);
    }

    private bool SetValueIfDirty<T>(object source, ref T dest)
    {
      T y = (T) source;
      bool flag = !EqualityComparer<T>.Default.Equals(dest, y);
      if (flag)
        dest = y;
      return flag;
    }

    private AnalyzerSettings.ItemLevelConsideration TranslateLevelConsideration(
      FFRKViewInventory.ScoreUpgradeModeComboIndex mode)
    {
      switch (mode)
      {
        case FFRKViewInventory.ScoreUpgradeModeComboIndex.CurrentUpgradeCurrentLevel:
          return AnalyzerSettings.ItemLevelConsideration.Current;
        case FFRKViewInventory.ScoreUpgradeModeComboIndex.CurrentUpgradeMaxLevel:
          return AnalyzerSettings.ItemLevelConsideration.CurrentRankMaxLevel;
        default:
          return AnalyzerSettings.ItemLevelConsideration.FullyMaxed;
      }
    }

    private void UpdateEquipmentGrid(DataEquipmentInformation[] EquipList)
    {
      int num1;
      int num2;
      if (this.comboBoxFilterType.SelectedIndex.Equals(0))
      {
        num1 = 0;
        num2 = 99;
      }
      else
      {
        num1 = this.comboBoxFilterType.SelectedIndex;
        num2 = this.comboBoxFilterType.SelectedIndex;
      }
      this.mEquipments = EquipList;
      this.dataGridViewEquipment.Rows.Clear();
      foreach (DataEquipmentInformation equip in EquipList)
      {
        if (equip.Type >= (SchemaConstants.ItemType) num1 && equip.Type <= (SchemaConstants.ItemType) num2 && equip.Category != SchemaConstants.EquipmentCategory.ArmorUpgrade && equip.Category != SchemaConstants.EquipmentCategory.WeaponUpgrade)
        {
          DataGridViewRow row = this.dataGridViewEquipment.Rows[this.dataGridViewEquipment.Rows.Add()];
          row.Tag = (object) equip;
          row.Cells[this.dgcItemID.Name].Value = (object) equip.EquipmentId;
          row.Cells[this.dgcItem.Name].Value = (object) equip.Name;
          row.Cells[this.dgcCategory.Name].Value = (object) equip.Category;
          row.Cells[this.dgcType.Name].Value = (object) equip.Type;
          row.Cells[this.dgcRarity.Name].Value = (object) new FFRKViewInventory.RarityColumnValue((int) equip.BaseRarity, (int) equip.EvolutionNumber);
          row.Cells[this.dgcSynergy.Name].Value = (object) new FFRKViewInventory.SynergyColumnValue(RealmSynergy.FromSeries(equip.SeriesId));
          row.Cells[this.dgcLevel.Name].Value = (object) new FFRKViewInventory.LevelColumnValue((int) equip.Level, (int) equip.LevelMax);
          row.Cells[this.dgcScore.Name].Value = (object) new FFRKViewInventory.ScoreColumnValue(this.mAnalyzer.GetScore(equip.InstanceId));
          FFRKViewInventory.GridEquipStats displayStats = this.ComputeDisplayStats(equip);
          this.SetStatsForRow(row, equip, displayStats);
        }
      }
    }

    private void UpdatePartyGrid(List<DataBuddyInformation> buddies)
    {
      this.mBuddyList.Collection = buddies;
      this.dataGridViewBuddies.Rows.Clear();
      this.dataGridViewBuddies.Rows.Add(buddies.Count);
      int index = 0;
      foreach (DataBuddyInformation buddy in buddies)
      {
        DataGridViewRow row = this.dataGridViewBuddies.Rows[index];
        row.Tag = (object) buddy;
        row.Cells[this.dgcCharacterName.Name].Value = (object) buddy.Name;
        row.Cells[this.dgcCharacterLevel.Name].Value = (object) buddy.Level;
        row.Cells[this.dgcCharacterMaxLevel.Name].Value = (object) buddy.LevelMax;
        AnalyzerSettings.PartyMemberSettings mAnalyzerSetting = this.mAnalyzerSettings[buddy.Name];
        row.Cells[this.dgcCharacterOffensiveStat.Name].Value = (object) mAnalyzerSetting.OffensiveStat;
        row.Cells[this.dgcCharacterDefensiveStat.Name].Value = (object) mAnalyzerSetting.DefensiveStat;
        row.Cells[this.dgcCharacterOptimize.Name].Value = (object) mAnalyzerSetting.Score;
        ++index;
      }
    }

    private class GridEquipStats
    {
      public byte Level;
      public byte MaxLevel;
      public EquipStats Stats = new EquipStats();
    }

    private class LevelColumnValue : IComparable
    {
      private int mCurrentLevel;
      private int mMaxLevel;

      public LevelColumnValue(int Current, int Max)
      {
        this.mCurrentLevel = Current;
        this.mMaxLevel = Max;
      }

      public int CompareTo(object obj)
      {
        FFRKViewInventory.LevelColumnValue levelColumnValue = (FFRKViewInventory.LevelColumnValue) obj;
        int num = this.mCurrentLevel.CompareTo(levelColumnValue.mCurrentLevel);
        return (uint) num > 0U ? num : this.mMaxLevel.CompareTo(levelColumnValue.mMaxLevel);
      }

      public override bool Equals(object obj)
      {
        if (obj == null || obj == DBNull.Value)
          return false;
        FFRKViewInventory.LevelColumnValue levelColumnValue = (FFRKViewInventory.LevelColumnValue) obj;
        return this.mCurrentLevel == levelColumnValue.mCurrentLevel && this.mMaxLevel == levelColumnValue.mMaxLevel;
      }

      public override int GetHashCode() => this.mCurrentLevel + 100 * this.mMaxLevel;

      public override string ToString() => string.Format("{0}/{1}", (object) this.mCurrentLevel, (object) this.mMaxLevel);
    }

    private class RarityColumnValue : IComparable
    {
      private int mBaseRarity;
      private int mUpgrades;

      public RarityColumnValue(int BaseRarity, int Upgrades)
      {
        this.mBaseRarity = BaseRarity;
        this.mUpgrades = Upgrades;
      }

      public int CompareTo(object obj)
      {
        FFRKViewInventory.RarityColumnValue rarityColumnValue = (FFRKViewInventory.RarityColumnValue) obj;
        int num = this.mBaseRarity.CompareTo(rarityColumnValue.mBaseRarity);
        return (uint) num > 0U ? num : this.mUpgrades.CompareTo(rarityColumnValue.mUpgrades);
      }

      public override bool Equals(object obj)
      {
        if (obj == null || obj == DBNull.Value)
          return false;
        FFRKViewInventory.RarityColumnValue rarityColumnValue = (FFRKViewInventory.RarityColumnValue) obj;
        return this.mBaseRarity == rarityColumnValue.mBaseRarity && this.mUpgrades == rarityColumnValue.mUpgrades;
      }

      public override int GetHashCode() => 10 * this.mBaseRarity + this.mUpgrades;

      public override string ToString() => this.mBaseRarity.ToString() + new string('+', this.mUpgrades);
    }

    private class ScoreColumnValue : IComparable
    {
      private double mScore;

      public ScoreColumnValue(double Score) => this.mScore = Score;

      public int CompareTo(object obj) => this.mScore.CompareTo(((FFRKViewInventory.ScoreColumnValue) obj).mScore);

      public override bool Equals(object obj) => obj != null && obj != DBNull.Value && this.mScore == ((FFRKViewInventory.ScoreColumnValue) obj).mScore;

      public override int GetHashCode() => this.mScore.GetHashCode();

      public override string ToString() => double.IsNaN(this.mScore) ? "N/A" : this.mScore.ToString("F");
    }

    private enum ScoreUpgradeModeComboIndex
    {
      CurrentUpgradeCurrentLevel,
      CurrentUpgradeMaxLevel,
      MaxUpgradeMaxLevel,
    }

    private class SynergyColumnValue : IComparable
    {
      private RealmSynergy.SynergyValue mValue;

      public SynergyColumnValue(RealmSynergy.SynergyValue Value) => this.mValue = Value;

      public int CompareTo(object obj) => this.mValue.GameSeries.CompareTo(((FFRKViewInventory.SynergyColumnValue) obj).mValue.GameSeries);

      public override bool Equals(object obj) => obj != null && obj != DBNull.Value && this.mValue == ((FFRKViewInventory.SynergyColumnValue) obj).mValue;

      public override int GetHashCode() => this.mValue.GetHashCode();

      public override string ToString() => this.mValue.Text;
    }

    private class SynergyFormatter
    {
      private RealmSynergy.SynergyValue mSynergy;

      public SynergyFormatter(RealmSynergy.SynergyValue Synergy) => this.mSynergy = Synergy;

      public override string ToString() => this.mSynergy.Realm == RealmSynergy.Value.None ? "No" : this.mSynergy.Realm.ToString();
    }

    private enum ViewFilterTypeComboIndex
    {
      All,
      Weapon,
      Armor,
      Accessory,
    }

    private enum ViewModeComboIndex
    {
      AvailableItems,
      BestItems,
    }

    private enum ViewUpgradeModeComboIndex
    {
      CurrentUpgradeCurrentLevel,
      CurrentUpgradeMaxLevel,
      MaxLevelThroughExistingCombine,
      MaxUpgradeMaxLevel,
    }
  }
}
