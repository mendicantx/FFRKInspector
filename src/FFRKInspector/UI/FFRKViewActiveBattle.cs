// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.FFRKViewActiveBattle
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.DataCache.Items;
using FFRKInspector.GameData;
using FFRKInspector.Proxy;
using FFRKInspector.UI.ListViewFields;
using Fiddler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  internal class FFRKViewActiveBattle : UserControl
  {
    private IContainer components = (IContainer) null;
    private List<BasicItemDropStats> mAllPrevItems;
    private ListListViewBinding<BasicItemDropStats> mFilteredPrevItems;
    private GroupBox groupBoxEnemyInfo;
    private ListView listViewEnemyInfo;
    private ColumnHeader columnEnemy;
    private ColumnHeader columnMaxHP;
    private GroupBox groupBox1;
    private ListView listViewDropInfo;
    private ColumnHeader columnDropItem;
    private ColumnHeader columnDropRarity;
    private ColumnHeader columnDropRound;
    private ColumnHeader columnDropEnemy;
    private ColumnHeader columnStatusWeakness;
    private ColumnHeader columnFireDef;
    private ColumnHeader columnIceDef;
    private ColumnHeader columnLitDef;
    private ColumnHeader columnEarthDef;
    private ColumnHeader columnWindDef;
    private ColumnHeader columnWaterDef;
    private ColumnHeader columnHolyDef;
    private ColumnHeader columnDarkDef;
    private ColumnHeader columnBioDef;
    private ColumnHeader columnAtk;
    private ColumnHeader columnDef;
    private ColumnHeader columnMag;
    private ColumnHeader columnRes;
    private ColumnHeader columnMnd;
    private ColumnHeader columnSpd;
    private GroupBox groupBox2;
    private ListViewEx listViewPrevDrops;
    private GroupBox abilityJsonBox;
    private TextBox abilityJsonText;

    public FFRKViewActiveBattle()
    {
      this.InitializeComponent();
      this.mAllPrevItems = new List<BasicItemDropStats>();
      this.mFilteredPrevItems = new ListListViewBinding<BasicItemDropStats>();
      this.listViewPrevDrops.LoadSettings();
      this.listViewPrevDrops.AddField((IListViewField) new ItemNameField("Item", FieldWidthStyle.Percent, 24));
      this.listViewPrevDrops.AddField((IListViewField) new ItemDropsPerRunField("Drops/Run"));
      this.listViewPrevDrops.AddField((IListViewField) new ItemTimesRunField("Times Run"));
      this.listViewPrevDrops.AddField((IListViewField) new ItemTotalDropsField("Total Drops"));
      foreach (ColumnHeader column in this.listViewPrevDrops.Columns)
        column.Width = -2;
      this.listViewPrevDrops.DataBinding = (IListViewBinding) this.mFilteredPrevItems;
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
        this.PopulateEnemyInfoListView(FFRKProxy.Instance.GameState.ActiveBattle);
        this.PopulateDropInfoListView(FFRKProxy.Instance.GameState.ActiveBattle);
        this.BeginPopulateAllDropsListView(FFRKProxy.Instance.GameState.ActiveBattle);
        this.PopulateAbilityJsonTextBox(FFRKProxy.Instance.GameState.ActiveBattle);
      }
      else
      {
        this.PopulateEnemyInfoListView((EventBattleInitiated) null);
        this.PopulateDropInfoListView((EventBattleInitiated) null);
        this.BeginPopulateAllDropsListView((EventBattleInitiated) null);
        this.PopulateAbilityJsonTextBox((EventBattleInitiated) null);
      }
    }

    private void BeginPopulateAllDropsListView(EventBattleInitiated battle)
    {
      if (battle != null)
      {
        DbOpFilterDrops dbOpFilterDrops = new DbOpFilterDrops(FFRKProxy.Instance.Database);
        dbOpFilterDrops.Battles.AddValue(battle.Battle.BattleId);
        dbOpFilterDrops.OnRequestComplete += new DbOpFilterDrops.DataReadyCallback(this.RequestBattleDrops_OnRequestComplete);
        FFRKProxy.Instance.Database.BeginExecuteRequest((IDbRequest) dbOpFilterDrops);
      }
      else
      {
        this.listViewPrevDrops.VirtualListSize = 0;
        this.mAllPrevItems.Clear();
        this.mFilteredPrevItems.Collection.Clear();
      }
    }

    private void RequestBattleDrops_OnRequestComplete(List<BasicItemDropStats> items) => this.BeginInvoke((Action) (() =>
    {
      this.mAllPrevItems = items;
      this.RebuildFilteredDropListAndInvalidate();
    }));

    private void RebuildFilteredDropListAndInvalidate()
    {
      this.mFilteredPrevItems.Collection = this.mAllPrevItems.Select<BasicItemDropStats, BasicItemDropStats>((Func<BasicItemDropStats, BasicItemDropStats>) (i => new BasicItemDropStats()
      {
        ItemName = i.ItemName,
        DropsPerRunF = i.DropsPerRun,
        TimesRun = i.TimesRun,
        TotalDrops = i.TotalDrops
      })).ToList<BasicItemDropStats>();
      this.listViewPrevDrops.VirtualListSize = this.mFilteredPrevItems.Collection.Count;
      this.listViewPrevDrops.Invalidate();
      foreach (ColumnHeader column in this.listViewPrevDrops.Columns)
        column.Width = -2;
    }

    private void FFRKProxy_OnItemCacheRefreshed() => this.listViewPrevDrops.Invalidate();

    private void PopulateEnemyInfoListView(EventBattleInitiated battle)
    {
      this.listViewEnemyInfo.Items.Clear();
      if (battle == null)
        return;
      this.listViewEnemyInfo.View = View.Details;
      if (battle.Battle.Enemies.ToList<BasicEnemyInfo>().Count == 0)
        return;
      lock (FFRKProxy.Instance.Cache.SyncRoot)
      {
        foreach (BasicEnemyInfo enemy in battle.Battle.Enemies)
        {
          List<string> list = new List<string>()
          {
            "Poison",
            "Silence",
            "Paralyze",
            "Confuse",
            "Slow",
            "Stop",
            "Blind",
            "Sleep",
            "Petrify",
            "Doom",
            "Instant_KO",
            "Beserk",
            "Stun"
          }.Except<string>((IEnumerable<string>) enemy.EnemyStatusImmunity).ToList<string>();
          this.listViewEnemyInfo.Items.Add(new ListViewItem(new string[18]
          {
            enemy.EnemyName,
            enemy.EnemyMaxHp.ToString("N0"),
            enemy.EnemyAtk.ToString("N0"),
            enemy.EnemyDef.ToString("N0"),
            enemy.EnemyMag.ToString("N0"),
            enemy.EnemyRes.ToString("N0"),
            enemy.EnemyMnd.ToString("N0"),
            enemy.EnemySpd.ToString("N0"),
            enemy.EnemyFireDef,
            enemy.EnemyIceDef,
            enemy.EnemyLitDef,
            enemy.EnemyEarthDef,
            enemy.EnemyWindDef,
            enemy.EnemyWaterDef,
            enemy.EnemyHolyDef,
            enemy.EnemyDarkDef,
            enemy.EnemyBioDef,
            string.Join(", ", list.ToArray())
          }));
        }
      }
      foreach (ColumnHeader column in this.listViewEnemyInfo.Columns)
        column.Width = -2;
    }

    private void PopulateDropInfoListView(EventBattleInitiated battle)
    {
      this.listViewDropInfo.Items.Clear();
      if (battle == null)
        return;
      this.listViewDropInfo.View = View.Details;
      if (battle.Battle.Drops.ToList<DropEvent>().Count == 0)
        return;
      lock (FFRKProxy.Instance.Cache.SyncRoot)
      {
        foreach (DropEvent drop in battle.Battle.Drops)
        {
          Key key = new Key() { ItemId = drop.ItemId };
          Data data = (Data) null;
          string str1 = drop.ItemType != DataEnemyDropItem.DropItemType.Gold ? (drop.ItemType != DataEnemyDropItem.DropItemType.Materia ? (drop.ItemType != DataEnemyDropItem.DropItemType.Potion ? (!FFRKProxy.Instance.Cache.Items.TryGetValue(key, out data) ? drop.ItemId.ToString() : data.Name) : drop.PotionName) : drop.MateriaName) : string.Format("{0} gold", (object) drop.GoldAmount);
          if (drop.ItemId > 40000000U && drop.ItemId <= 40000065U)
          {
            string str2 = "";
            switch (drop.ItemId % 5U)
            {
              case 0:
                str2 += "Major ";
                break;
              case 1:
                str2 += "Minor ";
                break;
              case 2:
                str2 += "Lesser ";
                break;
              case 4:
                str2 += "Greater ";
                break;
            }
            str1 = str2 + Enum.GetName(typeof (SchemaConstants.ItemID), (object) (uint) ((int) ((drop.ItemId - 1U) / 5U) * 5 + 5));
          }
          else if (drop.ItemId >= 40000066U && drop.ItemId <= 40000078U)
            str1 = Enum.GetName(typeof (SchemaConstants.ItemID), (object) drop.ItemId);
          else if (drop.ItemId >= 161000001U && drop.ItemId <= 161000104U)
          {
            try
            {
              str1 = Enum.GetName(typeof (SchemaConstants.MagiciteID), (object) drop.ItemId);
            }
            catch (Exception ex)
            {
              FiddlerApplication.Log.LogString(ex.ToString());
            }
          }
          else if (drop.ItemId == 95001014U)
            str1 = "Gysahl Greens";
          else if (drop.ItemId == 70000001U)
            str1 = "Minor Growth Egg";
          else if (drop.ItemId == 70000002U)
            str1 = "Lesser Growth Egg";
          else if (drop.ItemId == 70000003U)
            str1 = "Growth Egg";
          else if (drop.ItemId == 70000004U)
            str1 = "Greater Growth Egg";
          else if (drop.ItemId == 70000005U)
            str1 = "Major Growth Egg";
          string str3 = str1.Replace('_', ' ');
          if (drop.NumberOfItems > 1U)
            str3 += string.Format(" x{0}", (object) drop.NumberOfItems);
          this.listViewDropInfo.Items.Add(new ListViewItem(new string[4]
          {
            str3,
            drop.ItemId < 161000057U || drop.ItemId > 161000080U ? drop.Rarity.ToString() : "4",
            drop.Round.ToString(),
            drop.EnemyName
          }));
        }
      }
      foreach (ColumnHeader column in this.listViewDropInfo.Columns)
        column.Width = -2;
    }

    private void PopulateAbilityJsonTextBox(EventBattleInitiated battle)
    {
      this.abilityJsonText.Clear();
      if (battle == null)
        return;
      List<DataBuddyInformation> list = battle.Battle.Buddies.ToList<DataBuddyInformation>();
      list.AddRange((IEnumerable<DataBuddyInformation>) battle.Battle.Supporters.ToList<DataBuddyInformation>());
      if (list.Count == 0)
        return;
      lock (FFRKProxy.Instance.Cache.SyncRoot)
      {
        List<string> stringList = new List<string>();
        foreach (DataBuddyInformation buddyInformation in list)
        {
          List<DataAbility> abilities = buddyInformation.Abilities;
          abilities.AddRange((IEnumerable<DataAbility>) buddyInformation.SoulStrikes);
          foreach (DataAbility dataAbility in abilities)
          {
            if (dataAbility.Options.OptionAbilityName != "Attack")
            {
              string[] strArray = new string[50];
              strArray[0] = (dataAbility.Options.OptionAbilityName += "\t");
              strArray[1] = dataAbility.AbilityId.ToString();
              strArray[2] = "\t";
              strArray[3] = dataAbility.ActionId.ToString();
              strArray[4] = "\t";
              strArray[5] = dataAbility.CategoryId.ToString();
              strArray[6] = "\t";
              strArray[7] = dataAbility.ExerciseType.ToString();
              strArray[8] = "\t";
              strArray[9] = dataAbility.Options.AnimId.ToString();
              strArray[10] = "\t";
              strArray[11] = dataAbility.Options.ActiveTargetMethod.ToString();
              strArray[12] = "\t";
              strArray[13] = dataAbility.Options.AliasName.ToString();
              strArray[14] = "\t";
              strArray[15] = dataAbility.Options.Arg1.ToString();
              strArray[16] = "\t";
              strArray[17] = dataAbility.Options.Arg2.ToString();
              strArray[18] = "\t";
              strArray[19] = dataAbility.Options.Arg3.ToString();
              strArray[20] = "\t";
              strArray[21] = dataAbility.Options.Arg4.ToString();
              strArray[22] = "\t";
              strArray[23] = dataAbility.Options.Arg5.ToString();
              strArray[24] = "\t";
              strArray[25] = dataAbility.Options.Arg6.ToString();
              strArray[26] = "\t";
              strArray[27] = dataAbility.Options.Arg7.ToString();
              strArray[28] = "\t";
              strArray[29] = dataAbility.Options.Arg8.ToString();
              strArray[30] = "\t";
              strArray[31] = dataAbility.Options.Arg9.ToString();
              strArray[32] = "\t";
              strArray[33] = dataAbility.Options.Arg10.ToString();
              strArray[34] = "\t";
              strArray[35] = dataAbility.Options.CastTime.ToString();
              strArray[36] = "\t";
              strArray[37] = dataAbility.Options.CounterEnable.ToString();
              strArray[38] = "\t";
              strArray[39] = dataAbility.Options.StatusFactor.ToString();
              strArray[40] = "\t";
              strArray[41] = dataAbility.Options.StatusId.ToString();
              strArray[42] = "\t";
              strArray[43] = dataAbility.Options.TargetDeath.ToString();
              strArray[44] = "\t";
              strArray[45] = dataAbility.Options.TargetMethod.ToString();
              strArray[46] = "\t";
              strArray[47] = dataAbility.Options.TargetRange.ToString();
              strArray[48] = "\t";
              strArray[49] = dataAbility.Options.TargetSegment.ToString();
              string str = string.Concat(strArray);
              stringList.Add(str);
            }
          }
        }
        this.abilityJsonText.Lines = stringList.ToArray();
      }
    }

    private void FFRKProxy_OnCompleteBattle(EventBattleInitiated battle) => this.BeginInvoke((Action) (() =>
    {
      this.PopulateEnemyInfoListView((EventBattleInitiated) null);
      this.PopulateDropInfoListView((EventBattleInitiated) null);
      this.BeginPopulateAllDropsListView((EventBattleInitiated) null);
    }));

    private void FFRKProxy_OnLeaveDungeon() => this.BeginInvoke((Action) (() =>
    {
      this.PopulateEnemyInfoListView((EventBattleInitiated) null);
      this.PopulateDropInfoListView((EventBattleInitiated) null);
      this.BeginPopulateAllDropsListView((EventBattleInitiated) null);
    }));

    private void FFRKProxy_OnListBattles(EventListBattles battles)
    {
    }

    private void FFRKProxy_OnListDungeons(EventListDungeons dungeons) => this.BeginInvoke((Action) (() =>
    {
      this.BeginPopulateAllDropsListView((EventBattleInitiated) null);
      this.PopulateDropInfoListView((EventBattleInitiated) null);
      this.PopulateEnemyInfoListView((EventBattleInitiated) null);
    }));

    private void FFRKProxy_OnBattleEngaged(EventBattleInitiated battle) => this.BeginInvoke((Action) (() =>
    {
      this.BeginPopulateAllDropsListView(battle);
      this.PopulateEnemyInfoListView(battle);
      this.PopulateDropInfoListView(battle);
      this.PopulateAbilityJsonTextBox(battle);
    }));

    private void FFRKViewActiveDungeon2_Load(object sender, EventArgs e)
    {
    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {
    }

    private void label2_Click(object sender, EventArgs e)
    {
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.groupBoxEnemyInfo = new GroupBox();
      this.listViewEnemyInfo = new ListView();
      this.listViewPrevDrops = new ListViewEx();
      this.columnEnemy = new ColumnHeader();
      this.columnMaxHP = new ColumnHeader();
      this.columnAtk = new ColumnHeader();
      this.columnDef = new ColumnHeader();
      this.columnMag = new ColumnHeader();
      this.columnRes = new ColumnHeader();
      this.columnMnd = new ColumnHeader();
      this.columnSpd = new ColumnHeader();
      this.columnFireDef = new ColumnHeader();
      this.columnIceDef = new ColumnHeader();
      this.columnLitDef = new ColumnHeader();
      this.columnEarthDef = new ColumnHeader();
      this.columnWindDef = new ColumnHeader();
      this.columnWaterDef = new ColumnHeader();
      this.columnHolyDef = new ColumnHeader();
      this.columnDarkDef = new ColumnHeader();
      this.columnBioDef = new ColumnHeader();
      this.columnStatusWeakness = new ColumnHeader();
      this.groupBox1 = new GroupBox();
      this.listViewDropInfo = new ListView();
      this.columnDropItem = new ColumnHeader();
      this.columnDropRarity = new ColumnHeader();
      this.columnDropRound = new ColumnHeader();
      this.columnDropEnemy = new ColumnHeader();
      this.groupBox2 = new GroupBox();
      this.abilityJsonBox = new GroupBox();
      this.abilityJsonText = new TextBox();
      this.groupBoxEnemyInfo.SuspendLayout();
      this.groupBox1.SuspendLayout();
      this.abilityJsonBox.SuspendLayout();
      this.SuspendLayout();
      this.groupBoxEnemyInfo.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBoxEnemyInfo.Controls.Add((Control) this.listViewEnemyInfo);
      this.groupBoxEnemyInfo.Location = new Point(3, 8);
      this.groupBoxEnemyInfo.Name = "groupBoxEnemyInfo";
      this.groupBoxEnemyInfo.Size = new Size(1032, 205);
      this.groupBoxEnemyInfo.TabIndex = 0;
      this.groupBoxEnemyInfo.TabStop = false;
      this.groupBoxEnemyInfo.Text = "Enemy Info";
      this.listViewEnemyInfo.Columns.AddRange(new ColumnHeader[18]
      {
        this.columnEnemy,
        this.columnMaxHP,
        this.columnAtk,
        this.columnDef,
        this.columnMag,
        this.columnRes,
        this.columnMnd,
        this.columnSpd,
        this.columnFireDef,
        this.columnIceDef,
        this.columnLitDef,
        this.columnEarthDef,
        this.columnWindDef,
        this.columnWaterDef,
        this.columnHolyDef,
        this.columnDarkDef,
        this.columnBioDef,
        this.columnStatusWeakness
      });
      this.listViewEnemyInfo.Dock = DockStyle.Fill;
      this.listViewEnemyInfo.Location = new Point(3, 16);
      this.listViewEnemyInfo.Name = "listViewEnemyInfo";
      this.listViewEnemyInfo.Size = new Size(1026, 186);
      this.listViewEnemyInfo.TabIndex = 0;
      this.listViewEnemyInfo.UseCompatibleStateImageBehavior = false;
      this.listViewEnemyInfo.View = View.Details;
      this.columnEnemy.Text = "Enemy";
      this.columnEnemy.Width = 211;
      this.columnMaxHP.Text = "Max HP";
      this.columnMaxHP.Width = 52;
      this.columnAtk.Text = "ATK";
      this.columnAtk.Width = 33;
      this.columnDef.Text = "DEF";
      this.columnDef.Width = 33;
      this.columnMag.Text = "MAG";
      this.columnMag.Width = 36;
      this.columnRes.Text = "RES";
      this.columnRes.Width = 36;
      this.columnMnd.Text = "MND";
      this.columnMnd.Width = 37;
      this.columnSpd.Text = "SPD";
      this.columnSpd.Width = 35;
      this.columnFireDef.Text = "Fire";
      this.columnFireDef.Width = 29;
      this.columnIceDef.Text = "Ice";
      this.columnIceDef.Width = 28;
      this.columnLitDef.Text = "Lit.";
      this.columnLitDef.Width = 27;
      this.columnEarthDef.Text = "Earth";
      this.columnEarthDef.Width = 39;
      this.columnWindDef.Text = "Wind";
      this.columnWindDef.Width = 38;
      this.columnWaterDef.Text = "Water";
      this.columnWaterDef.Width = 41;
      this.columnHolyDef.Text = "Holy";
      this.columnHolyDef.Width = 33;
      this.columnDarkDef.Text = "Dark";
      this.columnDarkDef.Width = 36;
      this.columnBioDef.Text = "Bio";
      this.columnBioDef.Width = 27;
      this.columnStatusWeakness.Text = "Status Weaknesses";
      this.columnStatusWeakness.Width = 112;
      this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox1.Controls.Add((Control) this.listViewDropInfo);
      this.groupBox1.Location = new Point(6, 219);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(464, 221);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Drop Info";
      this.groupBox1.Enter += new EventHandler(this.groupBox1_Enter);
      this.listViewDropInfo.Columns.AddRange(new ColumnHeader[4]
      {
        this.columnDropItem,
        this.columnDropRarity,
        this.columnDropRound,
        this.columnDropEnemy
      });
      this.listViewDropInfo.Dock = DockStyle.Fill;
      this.listViewDropInfo.Location = new Point(3, 16);
      this.listViewDropInfo.Name = "listViewDropInfo";
      this.listViewDropInfo.Size = new Size(458, 202);
      this.listViewDropInfo.TabIndex = 0;
      this.listViewDropInfo.UseCompatibleStateImageBehavior = false;
      this.listViewDropInfo.View = View.Details;
      this.columnDropItem.Text = "Item";
      this.columnDropItem.Width = 120;
      this.columnDropRarity.Text = "Rarity";
      this.columnDropRound.Text = "Round";
      this.columnDropEnemy.Text = "Enemy";
      this.columnDropEnemy.Width = 120;
      this.groupBox2.Controls.Add((Control) this.listViewPrevDrops);
      this.groupBox2.Location = new Point(476, 219);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(443, 221);
      this.groupBox2.Text = "Previously Recorded Drops";
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.listViewPrevDrops.DataBinding = (IListViewBinding) null;
      this.listViewPrevDrops.Location = new Point(3, 17);
      this.listViewPrevDrops.Name = "listViewPrevDrops";
      this.listViewPrevDrops.SettingsKey = "FFRKViewActiveDungeon_AllDropsList";
      this.listViewPrevDrops.Size = new Size(434, 203);
      this.listViewPrevDrops.TabIndex = 0;
      this.listViewPrevDrops.UseCompatibleStateImageBehavior = false;
      this.listViewPrevDrops.View = View.Details;
      this.listViewPrevDrops.VirtualMode = true;
      this.abilityJsonBox.Controls.Add((Control) this.abilityJsonText);
      this.abilityJsonBox.Location = new Point(9, 446);
      this.abilityJsonBox.Name = "abilityJsonBox";
      this.abilityJsonBox.Size = new Size(910, 167);
      this.abilityJsonBox.TabIndex = 3;
      this.abilityJsonBox.TabStop = false;
      this.abilityJsonBox.Text = "Ability JSON";
      this.abilityJsonText.Location = new Point(6, 19);
      this.abilityJsonText.Multiline = true;
      this.abilityJsonText.Name = "abilityJsonText";
      this.abilityJsonText.Size = new Size(898, 142);
      this.abilityJsonText.TabIndex = 0;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.abilityJsonBox);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.groupBoxEnemyInfo);
      this.Name = nameof (FFRKViewActiveBattle);
      this.Size = new Size(1038, 671);
      this.Load += new EventHandler(this.FFRKViewCurrentBattle_Load);
      this.groupBoxEnemyInfo.ResumeLayout(false);
      this.groupBox1.ResumeLayout(false);
      this.abilityJsonBox.ResumeLayout(false);
      this.abilityJsonBox.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
