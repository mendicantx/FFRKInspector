// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.FFRKViewActiveDungeon
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.GameData;
using FFRKInspector.GameData.Converters;
using FFRKInspector.Proxy;
using FFRKInspector.UI.ListViewFields;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  internal class FFRKViewActiveDungeon : UserControl
  {
    private IContainer components = (IContainer) null;
    private List<BasicItemDropStats> mAllItems;
    private ListListViewBinding<BasicItemDropStats> mFilteredItems;
    private GroupBox groupBoxDungeon;
    private ListViewEx listViewAllDrops;
    private ListView listViewActiveDungeon;
    private ColumnHeader columnHeaderBattleName;
    private ColumnHeader columnHeaderBattleRounds;
    private ColumnHeader columnHeaderBattleBoss;
    private ColumnHeader columnHeaderStamina;
    private GroupBox groupBoxAllItems;
    private GroupBox groupBoxCurrentDungeon;
    private CheckBox checkBoxRepeatable;
    private Label label1;
    private NumericUpDown numericUpDown1;
    private CheckBox checkBoxFilterSamples;
    private TextBox textBoxNameFilter;
    private Label label2;
    private ListView listViewMasteryCondition;
    private ColumnHeader columnMasteryCond;
    private ColumnHeader columnMasteryCondEnglish;
    private ColumnHeader columnCondBattle;

    public FFRKViewActiveDungeon()
    {
      this.InitializeComponent();
      this.mAllItems = new List<BasicItemDropStats>();
      this.mFilteredItems = new ListListViewBinding<BasicItemDropStats>();
      this.listViewAllDrops.LoadSettings();
      this.listViewAllDrops.AddField((IListViewField) new ItemNameField("Item", FieldWidthStyle.Percent, 24));
      this.listViewAllDrops.AddField((IListViewField) new ItemBattleField("Battle", FieldWidthStyle.Percent, 24));
      this.listViewAllDrops.AddField((IListViewField) new ItemTimesRunField("Times Run"));
      this.listViewAllDrops.AddField((IListViewField) new ItemTotalDropsField("Total Drops"));
      this.listViewAllDrops.AddField((IListViewField) new ItemDropsPerRunField("Drops/Run"));
      this.listViewAllDrops.AddField((IListViewField) new ItemStaminaPerDropField("Stamina/Drop", false));
      this.listViewAllDrops.AddField((IListViewField) new ItemStaminaToReachField("Stamina to Reach"));
      this.listViewAllDrops.AddField((IListViewField) new ItemRepeatableField("Is Repeatable"));
      this.listViewAllDrops.DataBinding = (IListViewBinding) this.mFilteredItems;
    }

    private void FFRKViewCurrentBattle_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      if (FFRKProxy.Instance != null)
      {
        FFRKProxy.Instance.OnBattleEngaged += new FFRKProxy.BattleInitiatedDelegate(this.FFRKProxy_OnBattleEngaged);
        FFRKProxy.Instance.OnListBattles += new FFRKProxy.ListBattlesDelegate(this.FFRKProxy_OnListBattles);
        FFRKProxy.Instance.OnListDungeons += new FFRKProxy.ListDungeonsDelegate(this.FFRKProxy_OnListDungeons);
        FFRKProxy.Instance.OnLeaveDungeon += new FFRKProxy.FFRKDefaultDelegate(this.FFRKProxy_OnLeaveDungeon);
        FFRKProxy.Instance.OnCompleteBattle += new FFRKProxy.BattleResultDelegate(this.FFRKProxy_OnCompleteBattle);
        FFRKProxy.Instance.OnItemCacheRefreshed += new FFRKProxy.FFRKDefaultDelegate(this.FFRKProxy_OnItemCacheRefreshed);
        this.PopulateActiveDungeonListView(FFRKProxy.Instance.GameState.ActiveDungeon);
        this.PopulateMasteryConditionListView(FFRKProxy.Instance.GameState.ActiveDungeon);
        this.listViewAllDrops.VirtualListSize = 0;
        this.BeginPopulateAllDropsListView(FFRKProxy.Instance.GameState.ActiveDungeon);
      }
      else
      {
        this.PopulateActiveDungeonListView((EventListBattles) null);
        this.PopulateMasteryConditionListView((EventListBattles) null);
        this.listViewAllDrops.VirtualListSize = 0;
      }
    }

    private void FFRKProxy_OnItemCacheRefreshed() => this.listViewAllDrops.Invalidate();

    private void BeginPopulateAllDropsListView(EventListBattles dungeon)
    {
      if (dungeon != null)
      {
        DbOpFilterDrops dbOpFilterDrops = new DbOpFilterDrops(FFRKProxy.Instance.Database);
        dbOpFilterDrops.Dungeons.AddValue(dungeon.DungeonSession.DungeonId);
        dbOpFilterDrops.OnRequestComplete += new DbOpFilterDrops.DataReadyCallback(this.RequestDungeonDrops_OnRequestComplete);
        FFRKProxy.Instance.Database.BeginExecuteRequest((IDbRequest) dbOpFilterDrops);
      }
      else
      {
        this.listViewAllDrops.VirtualListSize = 0;
        this.mAllItems.Clear();
        this.mFilteredItems.Collection.Clear();
      }
    }

    private void RequestDungeonDrops_OnRequestComplete(List<BasicItemDropStats> items) => this.BeginInvoke((Action) (() =>
    {
      this.mAllItems = items;
      this.RebuildFilteredDropListAndInvalidate();
    }));

    private void UpdateAllDropsForLastBattle(EventBattleInitiated battle)
    {
      if (battle == null)
      {
        this.listViewAllDrops.VirtualListSize = 0;
        this.mAllItems.Clear();
        this.mFilteredItems.Collection.Clear();
      }
      else
      {
        foreach (BasicItemDropStats basicItemDropStats in this.mAllItems.Where<BasicItemDropStats>((Func<BasicItemDropStats, bool>) (x => (int) x.BattleId == (int) battle.Battle.BattleId)))
        {
          ++basicItemDropStats.TimesRun;
          ++basicItemDropStats.TimesRunWithHistogram;
        }
        lock (FFRKProxy.Instance.Cache.SyncRoot)
        {
          foreach (DropEvent drop1 in battle.Battle.Drops)
          {
            DropEvent drop = drop1;
            if (drop.ItemType != DataEnemyDropItem.DropItemType.Gold && drop.ItemType != DataEnemyDropItem.DropItemType.Materia && drop.ItemType != DataEnemyDropItem.DropItemType.Potion)
            {
              BasicItemDropStats basicItemDropStats = this.mAllItems.Find((Predicate<BasicItemDropStats>) (x => (int) x.BattleId == (int) battle.Battle.BattleId && (int) x.ItemId == (int) drop.ItemId));
              if (basicItemDropStats != null)
              {
                ++basicItemDropStats.TotalDrops;
              }
              else
              {
                EventListBattles activeDungeon = FFRKProxy.Instance.GameState.ActiveDungeon;
                if (activeDungeon != null)
                {
                  DataBattle dataBattle = activeDungeon.Battles.Find(x => (int) x.Id == (int) battle.Battle.BattleId);
                  if (dataBattle != null)
                  {
                    uint num1 = 1;
                    uint num2 = 1;
                    string name = drop.ItemId.ToString();
                    FFRKInspector.DataCache.Items.Data data1;
                    if (FFRKProxy.Instance.Cache.Items.TryGetValue(new FFRKInspector.DataCache.Items.Key()
                    {
                      ItemId = drop.ItemId
                    }, out data1))
                      name = data1.Name;
                    FFRKInspector.DataCache.Battles.Data data2;
                    if (FFRKProxy.Instance.Cache.Battles.TryGetValue(new FFRKInspector.DataCache.Battles.Key()
                    {
                      BattleId = battle.Battle.BattleId
                    }, out data2))
                    {
                      num1 = data2.Samples;
                      num2 = data2.HistoSamples;
                    }
                    this.mAllItems.Add(new BasicItemDropStats()
                    {
                      BattleId = battle.Battle.BattleId,
                      BattleName = dataBattle.Name,
                      BattleStamina = dataBattle.Stamina,
                      ItemId = drop.ItemId,
                      ItemName = name,
                      TimesRun = num1,
                      TotalDrops = 1U
                    });
                  }
                }
              }
            }
          }
        }
        this.RebuildFilteredDropListAndInvalidate();
      }
    }

    private void FFRKProxy_OnCompleteBattle(EventBattleInitiated battle) => this.BeginInvoke((Action) (() => this.UpdateAllDropsForLastBattle(battle)));

    private void FFRKProxy_OnLeaveDungeon() => this.BeginInvoke((Action) (() =>
    {
      this.PopulateActiveDungeonListView((EventListBattles) null);
      this.PopulateMasteryConditionListView((EventListBattles) null);
      this.UpdateAllDropsForLastBattle((EventBattleInitiated) null);
    }));

    private void FFRKProxy_OnListBattles(EventListBattles battles)
    {
      this.BeginPopulateAllDropsListView(battles);
      this.BeginInvoke((Action) (() =>
      {
        this.PopulateActiveDungeonListView(battles);
        this.PopulateMasteryConditionListView(battles);
        this.BeginPopulateAllDropsListView(battles);
      }));
    }

    private void FFRKProxy_OnListDungeons(EventListDungeons dungeons) => this.BeginInvoke((Action) (() =>
    {
      this.PopulateActiveDungeonListView((EventListBattles) null);
      this.PopulateMasteryConditionListView((EventListBattles) null);
      this.BeginPopulateAllDropsListView((EventListBattles) null);
    }));

    private void FFRKProxy_OnBattleEngaged(EventBattleInitiated battle)
    {
    }

    private void PopulateActiveDungeonListView(EventListBattles dungeon)
    {
      this.listViewActiveDungeon.Items.Clear();
      if (dungeon == null)
      {
        this.groupBoxDungeon.Text = "(No Active Dungeon)";
      }
      else
      {
        this.groupBoxDungeon.Text = dungeon.UserDungeon.Name;
        foreach (DataBattle battle in dungeon.Battles)
          this.listViewActiveDungeon.Items.Add(new ListViewItem(new string[4]
          {
            battle.HasBoss ? "BOSS" : "",
            battle.Name,
            battle.RoundNumber.ToString(),
            battle.Stamina.ToString()
          }));
        foreach (ColumnHeader column in this.listViewActiveDungeon.Columns)
          column.Width = -2;
      }
    }

    private void PopulateMasteryConditionListView(EventListBattles dungeon)
    {
      this.listViewMasteryCondition.Items.Clear();
      if (dungeon == null)
        return;
      foreach (DataDungeonCaptures capture in dungeon.UserDungeon.Captures)
      {
        foreach (DataDungeonSpScore dataDungeonSpScore in capture.SpScore)
        {
          string str1 = (string) null;
          foreach (DataBattle battle in dungeon.Battles)
          {
            if ((int) battle.Id == (int) dataDungeonSpScore.BattleID)
            {
              str1 = battle.Name;
              break;
            }
          }
          string str2 = new MedalConditionParser(dataDungeonSpScore.Title.ToString()).translate();
          this.listViewMasteryCondition.Items.Add(new ListViewItem(new string[3]
          {
            str1,
            dataDungeonSpScore.Title,
            str2
          }));
        }
      }
      foreach (ColumnHeader column in this.listViewMasteryCondition.Columns)
        column.Width = -2;
    }

    private void CenterControl(Control parent, Control child)
    {
      int width1 = child.Width;
      int height1 = child.Height;
      int width2 = parent.Width;
      int height2 = parent.Height;
      child.Left = parent.Location.X + (width2 - width1) / 2;
      child.Top = parent.Location.Y + (height2 - height1) / 2;
    }

    private void checkBoxRepeatable_CheckedChanged(object sender, EventArgs e) => this.RebuildFilteredDropListAndInvalidate();

    private void checkBoxFilterSamples_CheckedChanged(object sender, EventArgs e)
    {
      this.numericUpDown1.Enabled = this.checkBoxFilterSamples.Checked;
      this.RebuildFilteredDropListAndInvalidate();
    }

    private void numericUpDown1_ValueChanged(object sender, EventArgs e) => this.RebuildFilteredDropListAndInvalidate();

    private void textBoxNameFilter_TextChanged(object sender, EventArgs e) => this.RebuildFilteredDropListAndInvalidate();

    private void RebuildFilteredDropListAndInvalidate()
    {
      this.mFilteredItems.Collection = this.mAllItems.Where<BasicItemDropStats>((Func<BasicItemDropStats, bool>) (x =>
      {
        if (this.checkBoxFilterSamples.Checked && (Decimal) x.TimesRun < this.numericUpDown1.Value || this.checkBoxRepeatable.Checked && !x.IsBattleRepeatable)
          return false;
        return this.textBoxNameFilter.Text.Length <= 0 || CultureInfo.CurrentCulture.CompareInfo.IndexOf(x.ItemName, this.textBoxNameFilter.Text, CompareOptions.IgnoreCase) != -1;
      })).ToList<BasicItemDropStats>();
      this.listViewAllDrops.VirtualListSize = this.mFilteredItems.Collection.Count;
      this.listViewAllDrops.Invalidate();
      foreach (ColumnHeader column in this.listViewAllDrops.Columns)
        column.Width = -2;
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
      this.groupBoxDungeon = new GroupBox();
      this.listViewActiveDungeon = new ListView();
      this.columnHeaderBattleBoss = new ColumnHeader();
      this.columnHeaderBattleName = new ColumnHeader();
      this.columnHeaderBattleRounds = new ColumnHeader();
      this.columnHeaderStamina = new ColumnHeader();
      this.groupBoxAllItems = new GroupBox();
      this.textBoxNameFilter = new TextBox();
      this.label2 = new Label();
      this.checkBoxRepeatable = new CheckBox();
      this.label1 = new Label();
      this.numericUpDown1 = new NumericUpDown();
      this.checkBoxFilterSamples = new CheckBox();
      this.groupBoxCurrentDungeon = new GroupBox();
      this.listViewMasteryCondition = new ListView();
      this.columnMasteryCond = new ColumnHeader();
      this.columnMasteryCondEnglish = new ColumnHeader();
      this.columnCondBattle = new ColumnHeader();
      this.listViewAllDrops = new ListViewEx();
      this.groupBoxDungeon.SuspendLayout();
      this.groupBoxAllItems.SuspendLayout();
      this.numericUpDown1.BeginInit();
      this.groupBoxCurrentDungeon.SuspendLayout();
      this.SuspendLayout();
      this.groupBoxDungeon.Controls.Add((Control) this.listViewActiveDungeon);
      this.groupBoxDungeon.Location = new Point(3, 3);
      this.groupBoxDungeon.Name = "groupBoxDungeon";
      this.groupBoxDungeon.Size = new Size(427, 213);
      this.groupBoxDungeon.TabIndex = 1;
      this.groupBoxDungeon.TabStop = false;
      this.groupBoxDungeon.Text = "(No Active Dungeon)";
      this.listViewActiveDungeon.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.listViewActiveDungeon.Columns.AddRange(new ColumnHeader[4]
      {
        this.columnHeaderBattleBoss,
        this.columnHeaderBattleName,
        this.columnHeaderBattleRounds,
        this.columnHeaderStamina
      });
      this.listViewActiveDungeon.FullRowSelect = true;
      this.listViewActiveDungeon.Location = new Point(6, 19);
      this.listViewActiveDungeon.Name = "listViewActiveDungeon";
      this.listViewActiveDungeon.Size = new Size(412, 188);
      this.listViewActiveDungeon.TabIndex = 0;
      this.listViewActiveDungeon.UseCompatibleStateImageBehavior = false;
      this.listViewActiveDungeon.View = View.Details;
      this.columnHeaderBattleBoss.Text = "BOSS";
      this.columnHeaderBattleBoss.Width = 57;
      this.columnHeaderBattleName.Text = "Name";
      this.columnHeaderBattleName.Width = 190;
      this.columnHeaderBattleRounds.Text = "Rounds";
      this.columnHeaderStamina.Text = "Stamina";
      this.groupBoxAllItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBoxAllItems.Controls.Add((Control) this.textBoxNameFilter);
      this.groupBoxAllItems.Controls.Add((Control) this.label2);
      this.groupBoxAllItems.Controls.Add((Control) this.checkBoxRepeatable);
      this.groupBoxAllItems.Controls.Add((Control) this.label1);
      this.groupBoxAllItems.Controls.Add((Control) this.numericUpDown1);
      this.groupBoxAllItems.Controls.Add((Control) this.checkBoxFilterSamples);
      this.groupBoxAllItems.Controls.Add((Control) this.listViewAllDrops);
      this.groupBoxAllItems.Location = new Point(0, 225);
      this.groupBoxAllItems.Name = "groupBoxAllItems";
      this.groupBoxAllItems.Size = new Size(1024, 201);
      this.groupBoxAllItems.TabIndex = 10;
      this.groupBoxAllItems.TabStop = false;
      this.groupBoxAllItems.Text = "All items dropped in this dungeon";
      this.textBoxNameFilter.Location = new Point(699, 21);
      this.textBoxNameFilter.Name = "textBoxNameFilter";
      this.textBoxNameFilter.Size = new Size(182, 20);
      this.textBoxNameFilter.TabIndex = 9;
      this.textBoxNameFilter.TextChanged += new EventHandler(this.textBoxNameFilter_TextChanged);
      this.label2.AutoSize = true;
      this.label2.Location = new Point(480, 24);
      this.label2.Name = "label2";
      this.label2.Size = new Size(213, 13);
      this.label2.TabIndex = 8;
      this.label2.Text = "Show only items with a name that contains: ";
      this.checkBoxRepeatable.AutoSize = true;
      this.checkBoxRepeatable.Location = new Point(307, 22);
      this.checkBoxRepeatable.Name = "checkBoxRepeatable";
      this.checkBoxRepeatable.Size = new Size(154, 17);
      this.checkBoxRepeatable.TabIndex = 7;
      this.checkBoxRepeatable.Text = "Show only repeatable runs.";
      this.checkBoxRepeatable.UseVisualStyleBackColor = true;
      this.checkBoxRepeatable.CheckedChanged += new EventHandler(this.checkBoxRepeatable_CheckedChanged);
      this.label1.AutoSize = true;
      this.label1.Location = new Point(262, 23);
      this.label1.Name = "label1";
      this.label1.Size = new Size(30, 13);
      this.label1.TabIndex = 6;
      this.label1.Text = "runs.";
      this.numericUpDown1.Enabled = false;
      this.numericUpDown1.Location = new Point(199, 21);
      this.numericUpDown1.Name = "numericUpDown1";
      this.numericUpDown1.Size = new Size(57, 20);
      this.numericUpDown1.TabIndex = 5;
      this.numericUpDown1.ValueChanged += new EventHandler(this.numericUpDown1_ValueChanged);
      this.checkBoxFilterSamples.AutoSize = true;
      this.checkBoxFilterSamples.Location = new Point(20, 22);
      this.checkBoxFilterSamples.Margin = new Padding(0);
      this.checkBoxFilterSamples.Name = "checkBoxFilterSamples";
      this.checkBoxFilterSamples.Size = new Size(171, 17);
      this.checkBoxFilterSamples.TabIndex = 4;
      this.checkBoxFilterSamples.Text = "Show only battles with at least ";
      this.checkBoxFilterSamples.UseVisualStyleBackColor = true;
      this.checkBoxFilterSamples.CheckedChanged += new EventHandler(this.checkBoxFilterSamples_CheckedChanged);
      this.groupBoxCurrentDungeon.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBoxCurrentDungeon.Controls.Add((Control) this.listViewMasteryCondition);
      this.groupBoxCurrentDungeon.Location = new Point(436, 3);
      this.groupBoxCurrentDungeon.Name = "groupBoxCurrentDungeon";
      this.groupBoxCurrentDungeon.Size = new Size(588, 213);
      this.groupBoxCurrentDungeon.TabIndex = 11;
      this.groupBoxCurrentDungeon.TabStop = false;
      this.groupBoxCurrentDungeon.Text = "Dungeon Information";
      this.listViewMasteryCondition.Columns.AddRange(new ColumnHeader[3]
      {
        this.columnCondBattle,
        this.columnMasteryCond,
        this.columnMasteryCondEnglish
      });
      this.listViewMasteryCondition.Dock = DockStyle.Fill;
      this.listViewMasteryCondition.Location = new Point(3, 16);
      this.listViewMasteryCondition.Name = "listViewMasteryCondition";
      this.listViewMasteryCondition.Size = new Size(582, 194);
      this.listViewMasteryCondition.TabIndex = 0;
      this.listViewMasteryCondition.UseCompatibleStateImageBehavior = false;
      this.listViewMasteryCondition.View = View.Details;
      this.columnMasteryCond.Text = "Mastery Condition";
      this.columnMasteryCond.Width = 296;
      this.columnMasteryCondEnglish.Text = "English";
      this.columnMasteryCondEnglish.Width = 300;
      this.columnCondBattle.Text = "Stage";
      this.columnCondBattle.Width = 136;
      this.listViewAllDrops.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.listViewAllDrops.DataBinding = (IListViewBinding) null;
      this.listViewAllDrops.FullRowSelect = true;
      this.listViewAllDrops.Location = new Point(14, 55);
      this.listViewAllDrops.Name = "listViewAllDrops";
      this.listViewAllDrops.SettingsKey = "FFRKViewActiveDungeon_AllDropsList";
      this.listViewAllDrops.Size = new Size(1004, 140);
      this.listViewAllDrops.TabIndex = 3;
      this.listViewAllDrops.UseCompatibleStateImageBehavior = false;
      this.listViewAllDrops.View = View.Details;
      this.listViewAllDrops.VirtualMode = true;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.groupBoxCurrentDungeon);
      this.Controls.Add((Control) this.groupBoxAllItems);
      this.Controls.Add((Control) this.groupBoxDungeon);
      this.Name = nameof (FFRKViewActiveDungeon);
      this.Size = new Size(1038, 441);
      this.Load += new EventHandler(this.FFRKViewCurrentBattle_Load);
      this.groupBoxDungeon.ResumeLayout(false);
      this.groupBoxAllItems.ResumeLayout(false);
      this.groupBoxAllItems.PerformLayout();
      this.numericUpDown1.EndInit();
      this.groupBoxCurrentDungeon.ResumeLayout(false);
      this.ResumeLayout(false);
    }
  }
}
