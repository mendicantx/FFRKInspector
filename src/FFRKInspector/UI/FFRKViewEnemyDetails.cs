// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.FFRKViewEnemyDetails
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using FFRKInspector.GameData.Converters;
using FFRKInspector.Proxy;
using Fiddler;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  public class FFRKViewEnemyDetails : UserControl
  {
    private bool showAbilitySummary;
    private IContainer components = (IContainer) null;
    private GroupBox enemyInfoGroupBox;
    private Label labelEnemySelection;
    private ComboBox comboBoxEnemySelection;
    private Label labelEnemyID;
    private PictureBox pictureBox1;
    private GroupBox groupBoxAIArguments;
    private ListView listViewAIArguments;
    private ColumnHeader columnAITag;
    private ColumnHeader columnAIArgValue;
    private ColumnHeader columnAIArgType;
    private Button SummaryButton;
    private GroupBox groupBoxEnemyPhase;
    private GroupBox groupBoxEnemyAppearances;
    private ListView listViewEnemyAppearances;
    private ColumnHeader columnEnemyRounds;
    private ColumnHeader columnEnemyMultiplicity;
    private Label labelChildAIID;
    private Label labelEnemyLevel;
    private TextBox textLevel;
    private TextBox textID;
    private TextBox textChildAIID;
    private TextBox textEnglishName;
    private Label labelEnglishName;
    private ToolTip toolTip1;
    private TextBox textInitHP;
    private Label labelInitialHP;
    private TextBox textAIID;
    private Label labelAIID;
    private LinkLabel linkLabelChildAI;
    private LinkLabel linkLabelAI;
    private TextBox textChildID;
    private Label labelChildID;
    private Label labelPhaseSelection;
    private ComboBox comboBoxPhaseSelection;
    private TextBox textStatusVuln;
    private GroupBox groupBox1;
    private DataGridView dataGridViewElemental;
    private GroupBox groupBoxEnemyConstraints;
    private ListView listViewEnemyConstraints;
    private ColumnHeader columnConstraintAbilityTag;
    private ColumnHeader columnConstraintValue;
    private ColumnHeader columnConstraintType;
    private ColumnHeader columnConstraintPriority;
    private ColumnHeader columnConstraintEnemyStatusID;
    private ColumnHeader columnConstraintOptions;
    private GroupBox groupBox2;
    private DataGridView dataGridViewStats;
    private GroupBox groupBox3;
    private DataGridView dataGridViewBreaks;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private DataGridViewTextBoxColumn Column17;
    private DataGridViewTextBoxColumn Column10;
    private DataGridViewTextBoxColumn Column11;
    private DataGridViewTextBoxColumn Column12;
    private DataGridViewTextBoxColumn Column13;
    private DataGridViewTextBoxColumn Column14;
    private DataGridViewTextBoxColumn Column15;
    private DataGridViewTextBoxColumn Column16;
    private DataGridViewTextBoxColumn Column18;
    private DataGridViewTextBoxColumn Column20;
    private DataGridViewTextBoxColumn Column19;
    private DataGridViewTextBoxColumn Column1;
    private DataGridViewTextBoxColumn Column2;
    private DataGridViewTextBoxColumn Column3;
    private DataGridViewTextBoxColumn Column4;
    private DataGridViewTextBoxColumn Column5;
    private DataGridViewTextBoxColumn Column6;
    private DataGridViewTextBoxColumn Column7;
    private DataGridViewTextBoxColumn Column8;
    private DataGridViewTextBoxColumn Column9;
    private Label label4;
    private Label label3;
    private TextBox textEXP;
    private TextBox textPhaseID;
    private TextBox textPhaseEnglishName;
    private TextBox textPhaseDispName;
    private Label label2;
    private Label label1;
    private Label labelDispName;
    private RichTextBox richTextBoxAbilitySummary;
    private Label label5;
    private TextBox textCastTime;
    private Button buttonToggleAbilityDetails;
    private GroupBox groupBoxAbilityDetails;
    private GroupBox groupBoxEnemyAbilities;
    private ListView listViewEnemyAbilities;
    private ColumnHeader columnAbilityName;
    private ColumnHeader columnAbilityWeight;
    private ColumnHeader columnAbilityTag;
    private ColumnHeader columnAbilityUnlock;
    private ColumnHeader columnAbilityID;
    private ColumnHeader columnAbilityActionID;
    private ColumnHeader columnAbilityExercise;
    private ColumnHeader columnAbilityCastTime;
    private ColumnHeader columnArg1;
    private ColumnHeader columnArg2;
    private ColumnHeader columnArg3;
    private ColumnHeader columnArg4;
    private ColumnHeader columnArg5;
    private ColumnHeader columnArg6;
    private ColumnHeader columnArg7;
    private ColumnHeader columnArg8;
    private ColumnHeader columnArg9;
    private ColumnHeader columnArg10;
    private ColumnHeader columnArg11;
    private ColumnHeader columnArg12;
    private ColumnHeader columnArg13;
    private ColumnHeader columnArg14;
    private ColumnHeader columnArg15;
    private ColumnHeader columnArg16;
    private ColumnHeader columnArg17;
    private ColumnHeader columnArg18;
    private ColumnHeader columnArg19;
    private ColumnHeader columnArg20;
    private ColumnHeader columnArg21;
    private ColumnHeader columnArg22;
    private ColumnHeader columnArg23;
    private ColumnHeader columnArg24;
    private ColumnHeader columnArg25;
    private ColumnHeader columnStatusFactor;
    private ColumnHeader columnStatusID;
    private ColumnHeader columnAbilityTarRange;
    private ColumnHeader columnAbilityTarMethod;
    private ColumnHeader columnAbilityTarSegment;
    private ColumnHeader columnTargetDeath;
    private ColumnHeader columnCounterEnable;
    private ColumnHeader columnArg26;
    private ColumnHeader columnArg27;
    private ColumnHeader columnArg28;
    private ColumnHeader columnArg29;
    private ColumnHeader columnArg30;
    private GroupBox groupBoxCounters;
    private ListView listViewCounters;
    private ColumnHeader columnHeader46;
    private ColumnHeader columnHeader47;
    private ColumnHeader columnHeader48;
    private ColumnHeader columnHeader49;
    private ColumnHeader columnHeader50;
    private CheckBox checkBoxRatesAsFractions;
    private CheckBox checkBoxCastTimes;
    private CheckBox checkBoxRawOnly;
    private ColumnHeader columnMinDamageThresh;
    private ColumnHeader columnMaxDamageThresh;
    private CheckBox checkBoxTranslate;
    private CheckBox checkBoxEnumerate;
    private TextBox textChildPosID;
    private Label labelChildPosID;

    public FFRKViewEnemyDetails()
    {
      this.showAbilitySummary = false;
      this.InitializeComponent();
      this.groupBoxEnemyAbilities.Visible = true;
      this.groupBoxCounters.Visible = true;
      this.groupBoxEnemyConstraints.Visible = true;
      this.richTextBoxAbilitySummary.Visible = false;
      this.checkBoxRatesAsFractions.Visible = false;
      this.checkBoxRatesAsFractions.Checked = false;
      this.checkBoxCastTimes.Visible = false;
      this.checkBoxRawOnly.Visible = true;
      this.checkBoxTranslate.Visible = false;
      this.checkBoxEnumerate.Visible = false;
      this.buttonToggleAbilityDetails.Text = "Show Enemy Ability Summary";
    }

    private void label2_Click(object sender, EventArgs e)
    {
    }

    private void groupBox1_Enter(object sender, EventArgs e)
    {
    }

    private void button2_Click(object sender, EventArgs e)
    {
    }

    private void FFRKViewEnemyDetails_Load(object sender, EventArgs e)
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
        this.PopulateEnemySelectionComboBox(FFRKProxy.Instance.GameState.ActiveBattle);
      }
      else
        this.PopulateEnemySelectionComboBox((EventBattleInitiated) null);
    }

    private void PopulateEnemySelectionComboBox(EventBattleInitiated battle)
    {
      this.comboBoxEnemySelection.SelectedIndex = -1;
      this.comboBoxEnemySelection.Items.Clear();
      if (battle == null)
        return;
      IEnumerable<BasicEnemyParentInfo> source = (IEnumerable<BasicEnemyParentInfo>) battle.Battle.EnemyParents.OrderBy<BasicEnemyParentInfo, uint>((Func<BasicEnemyParentInfo, uint>) (x => x.ChildPosId));
      if (source.ToList<BasicEnemyParentInfo>().Count == 0)
        return;
      lock (FFRKProxy.Instance.Cache.SyncRoot)
      {
        foreach (object obj in source)
          this.comboBoxEnemySelection.Items.Add(obj);
      }
      if (this.comboBoxEnemySelection.SelectedIndex != -1)
        return;
      this.comboBoxEnemySelection.SelectedIndex = 0;
    }

    private void PopulatePhaseSelectionComboBox(IEnumerable<BasicEnemyInfo> myPhases)
    {
      this.comboBoxPhaseSelection.SelectedIndex = -1;
      this.comboBoxPhaseSelection.Items.Clear();
      if (myPhases == null || myPhases.ToList<BasicEnemyInfo>().Count == 0)
        return;
      lock (FFRKProxy.Instance.Cache.SyncRoot)
      {
        foreach (object myPhase in myPhases)
          this.comboBoxPhaseSelection.Items.Add(myPhase);
      }
      if (this.comboBoxPhaseSelection.SelectedIndex != -1)
        return;
      this.comboBoxPhaseSelection.SelectedIndex = 0;
    }

    private void PopulateEnemyInfoGroupBox(EventBattleInitiated battle)
    {
      this.textID.Text = "";
      this.textChildID.Text = "";
      this.textAIID.Text = "";
      this.textChildAIID.Text = "";
      this.textLevel.Text = "";
      this.textChildPosID.Text = "";
      this.textInitHP.Text = "";
      this.textEnglishName.Text = "";
      this.pictureBox1.Image = (Image) null;
      this.richTextBoxAbilitySummary.Text = "";
      this.linkLabelAI.Links.Clear();
      this.linkLabelChildAI.Links.Clear();
      this.listViewAIArguments.Items.Clear();
      this.listViewEnemyAppearances.Items.Clear();
      if (battle == null || this.comboBoxEnemySelection.Items.Count == 0)
        return;
      BasicEnemyParentInfo selectedItem = (BasicEnemyParentInfo) this.comboBoxEnemySelection.SelectedItem;
      if (selectedItem == null)
        return;
      lock (FFRKProxy.Instance.Cache.SyncRoot)
      {
        this.textChildID.Text = selectedItem.EnemyId.ToString();
        this.textID.Text = selectedItem.Id.ToString();
        this.textAIID.Text = selectedItem.ParentAiId.ToString();
        this.textChildAIID.Text = selectedItem.AiId.ToString();
        this.textLevel.Text = selectedItem.Level.ToString();
        this.textInitHP.Text = selectedItem.EnemyInitHp.ToString("N0");
        this.textChildPosID.Text = selectedItem.ChildPosId.ToString();
        this.textEnglishName.Text = new Translator().Translate(selectedItem.ToString());
        if (selectedItem.ParentAiId > 0UL)
          this.linkLabelAI.Links.Add(0, this.linkLabelAI.Text.Length, (object) string.Format("https://dff.sp.mbga.jp/dff/static/js/direct/battle/ai/conf/{0}.js", (object) selectedItem.ParentAiId));
        if (selectedItem.AiId > 0UL)
          this.linkLabelChildAI.Links.Add(0, this.linkLabelChildAI.Text.Length, (object) string.Format("https://dff.sp.mbga.jp/dff/static/js/direct/battle/ai/conf/{0}.js", (object) selectedItem.AiId));
        bool flag = false;
        try
        {
          using (WebResponse response = WebRequest.Create(string.Format("https://dff.sp.mbga.jp/dff/static/lang/image/enemy/{0}.png", (object) selectedItem.EnemyId)).GetResponse())
          {
            using (Stream responseStream = response.GetResponseStream())
              this.pictureBox1.Image = Image.FromStream(responseStream);
          }
        }
        catch (WebException ex)
        {
          flag = true;
        }
        if (flag)
        {
          try
          {
            using (WebResponse response = WebRequest.Create(string.Format("https://dff.sp.mbga.jp/dff/static/lang/ab/character/enemy/img_{0}.png", (object) selectedItem.BackupImgId)).GetResponse())
            {
              using (Stream responseStream = response.GetResponseStream())
                this.pictureBox1.Image = Image.FromStream(responseStream);
            }
          }
          catch (WebException ex)
          {
            using (WebResponse response = WebRequest.Create("https://i.imgur.com/PR9Uric.png").GetResponse())
            {
              using (Stream responseStream = response.GetResponseStream())
                this.pictureBox1.Image = Image.FromStream(responseStream);
            }
          }
        }
        foreach (DataAIArgs dataAiArgs in selectedItem.AiArgs.OrderBy<DataAIArgs, string>((Func<DataAIArgs, string>) (x => x.Tag)).ToList<DataAIArgs>())
        {
          string name = System.Enum.GetName(typeof (SchemaConstants.AiArgType), (object) dataAiArgs.ArgType);
          this.listViewAIArguments.Items.Add(new ListViewItem(new string[3]
          {
            dataAiArgs.Tag,
            dataAiArgs.ArgValue.ToString(),
            name
          }));
        }
        foreach (ColumnHeader column in this.listViewAIArguments.Columns)
          column.Width = -2;
        for (uint index = 0; (long) index < (long) selectedItem.Appearances.Length; ++index)
        {
          if (selectedItem.Appearances[(int) index] > 0U)
            this.listViewEnemyAppearances.Items.Add(new ListViewItem(new string[2]
            {
              (index + 1U).ToString(),
              selectedItem.Appearances[(int) index].ToString()
            }));
        }
        foreach (ColumnHeader column in this.listViewEnemyAppearances.Columns)
          column.Width = -2;
      }
    }

    private void PopulatePhaseInfoGroupBox(BasicEnemyInfo enemy)
    {
      this.textPhaseDispName.Text = "";
      this.textPhaseEnglishName.Text = "";
      this.textPhaseID.Text = "";
      this.textEXP.Text = "";
      this.textCastTime.Text = "";
      this.textStatusVuln.Text = "";
      this.listViewEnemyConstraints.Items.Clear();
      this.listViewEnemyAbilities.Items.Clear();
      this.listViewCounters.Items.Clear();
      this.richTextBoxAbilitySummary.Text = "";
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridViewElemental.Rows)
      {
        if (!row.IsNewRow)
          this.dataGridViewElemental.Rows.Remove(row);
      }
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridViewBreaks.Rows)
      {
        if (!row.IsNewRow)
          this.dataGridViewBreaks.Rows.Remove(row);
      }
      foreach (DataGridViewRow row in (IEnumerable) this.dataGridViewStats.Rows)
      {
        if (!row.IsNewRow)
          this.dataGridViewStats.Rows.Remove(row);
      }
      if (enemy == null || FFRKProxy.Instance.GameState.ActiveBattle == null || this.comboBoxEnemySelection.SelectedItem == null)
        return;
      EventBattleInitiated activeBattle = FFRKProxy.Instance.GameState.ActiveBattle;
      BasicEnemyParentInfo selectedItem = (BasicEnemyParentInfo) this.comboBoxEnemySelection.SelectedItem;
      this.textPhaseDispName.Text = enemy.EnemyName;
      this.textPhaseEnglishName.Text = new Translator().Translate(enemy.EnemyName);
      this.textPhaseID.Text = enemy.EnemyId.ToString();
      this.textEXP.Text = enemy.EnemyExp.ToString("N0");
      this.textCastTime.Text = enemy.EnemyCastTime;
      this.textStatusVuln.Text = string.Join(", ", new List<string>()
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
      }.Except<string>((IEnumerable<string>) enemy.EnemyStatusImmunity).ToList<string>().ToArray()).Replace('_', ' ');
      if (this.textStatusVuln.Text == "")
        this.textStatusVuln.Text = "None";
      this.dataGridViewElemental.Rows.Add((object) enemy.EnemyFireDef, (object) enemy.EnemyIceDef, (object) enemy.EnemyLitDef, (object) enemy.EnemyEarthDef, (object) enemy.EnemyWindDef, (object) enemy.EnemyWaterDef, (object) enemy.EnemyHolyDef, (object) enemy.EnemyDarkDef, (object) enemy.EnemyBioDef);
      for (int index = 0; index < this.dataGridViewElemental.ColumnCount; ++index)
      {
        string str = this.dataGridViewElemental[index, 0].Value.ToString();
        int result;
        if (int.TryParse(str.Substring(0, str.IndexOf('%')), out result) && result > 100)
          this.dataGridViewElemental.Rows[0].Cells[index].Style.ForeColor = Color.Red;
      }
      this.dataGridViewBreaks.Rows.Add((object) enemy.EnemyAtkBrkDef, (object) enemy.EnemyDefBrkDef, (object) enemy.EnemyMagBrkDef, (object) enemy.EnemyResBrkDef, (object) enemy.EnemyMndBrkDef, (object) enemy.EnemySpdBrkDef);
      this.dataGridViewStats.Rows.Add((object) enemy.EnemyLv, (object) enemy.EnemyMaxHp.ToString("N0"), (object) enemy.EnemyAtk.ToString("N0"), (object) enemy.EnemyDef.ToString("N0"), (object) enemy.EnemyMag.ToString("N0"), (object) enemy.EnemyRes.ToString("N0"), (object) enemy.EnemyMnd, (object) enemy.EnemySpd, (object) enemy.EnemyAcc, (object) enemy.EnemyEva, (object) enemy.EnemyCrit);
      bool flag1 = this.checkBoxRawOnly.Checked;
      foreach (DataEnemyConstraints constraint in selectedItem.Constraints)
      {
        if (constraint.EnemyStatusId == 0U || (int) constraint.EnemyStatusId == (int) enemy.EnemyId)
          this.listViewEnemyConstraints.Items.Add(new ListViewItem(new string[6]
          {
            constraint.AbilityTag,
            constraint.ConstraintValue.ToString(),
            flag1 ? constraint.ConstraintType.ToString() : System.Enum.GetName(typeof (SchemaConstants.EnemyConstraintType), (object) constraint.ConstraintType),
            constraint.Priority.ToString(),
            constraint.EnemyStatusId.ToString(),
            constraint.Options
          }));
      }
      foreach (ColumnHeader column in this.listViewEnemyConstraints.Columns)
        column.Width = -2;
      foreach (DataEnemyParamAbilities enemyAbility in enemy.EnemyAbilities)
      {
        DataEnemyAbility ability = activeBattle.Battle.getAbility(enemyAbility.AbilityId);
        DataEnemyAbilityOptions options = ability.Options;
        this.listViewEnemyAbilities.Items.Add(new ListViewItem(new string[47]
        {
          ability.name,
          enemyAbility.AbilityId.ToString(),
          enemyAbility.Tag,
          enemyAbility.Weight.ToString(),
          enemyAbility.UnlockTurn.ToString(),
          flag1 ? ability.ExerciseType.ToString() : System.Enum.GetName(typeof (SchemaConstants.ExerciseAbbr), (object) ability.ExerciseType),
          options.CastTime.ToString(),
          flag1 ? options.TargetRange.ToString() : System.Enum.GetName(typeof (SchemaConstants.TargetRange), (object) options.TargetRange),
          flag1 ? options.TargetMethod.ToString() : System.Enum.GetName(typeof (SchemaConstants.TargetMethod), (object) options.TargetMethod),
          flag1 ? options.TargetSegment.ToString() : System.Enum.GetName(typeof (SchemaConstants.TargetSegment), (object) options.TargetSegment),
          ability.ActionId.ToString(),
          options.Arg1.ToString(),
          options.Arg2.ToString(),
          options.Arg3.ToString(),
          options.Arg4.ToString(),
          options.Arg5.ToString(),
          options.Arg6.ToString(),
          options.Arg7.ToString(),
          options.Arg8.ToString(),
          options.Arg9.ToString(),
          options.Arg10.ToString(),
          options.Arg11.ToString(),
          options.Arg12.ToString(),
          options.Arg13.ToString(),
          options.Arg14.ToString(),
          options.Arg15.ToString(),
          options.Arg16.ToString(),
          options.Arg17.ToString(),
          options.Arg18.ToString(),
          options.Arg19.ToString(),
          options.Arg20.ToString(),
          options.Arg21.ToString(),
          options.Arg22.ToString(),
          options.Arg23.ToString(),
          options.Arg24.ToString(),
          options.Arg25.ToString(),
          options.Arg26.ToString(),
          options.Arg27.ToString(),
          options.Arg28.ToString(),
          options.Arg29.ToString(),
          options.Arg30.ToString(),
          options.StatusAilmentsFactor.ToString(),
          options.StatusAilmentsId.ToString(),
          flag1 ? options.TargetDeath.ToString() : System.Enum.GetName(typeof (SchemaConstants.TargetDeath), (object) options.TargetDeath),
          options.CounterEnable.ToString(),
          flag1 ? options.MinDamageThreshold.ToString() : System.Enum.GetName(typeof (SchemaConstants.DamageThresholdType), (object) options.MinDamageThreshold),
          flag1 ? options.MaxDamageThreshold.ToString() : System.Enum.GetName(typeof (SchemaConstants.DamageThresholdType), (object) options.MaxDamageThreshold)
        }));
      }
      foreach (DataEnemyParamCounters enemyCounter in enemy.EnemyCounters)
      {
        this.listViewCounters.Items.Add(new ListViewItem(new string[5]
        {
          activeBattle.Battle.getAbility(enemyCounter.AbilityId).name,
          enemyCounter.AbilityId.ToString(),
          enemyCounter.Rate.ToString(),
          enemyCounter.CondValue.ToString(),
          flag1 ? enemyCounter.CondType.ToString() : System.Enum.GetName(typeof (SchemaConstants.CounterConditionType), (object) enemyCounter.CondType)
        }));
        bool flag2 = false;
        foreach (ListViewItem listViewItem in this.listViewEnemyAbilities.Items)
        {
          if ((long) int.Parse(listViewItem.SubItems[1].Text) == (long) enemyCounter.AbilityId)
          {
            flag2 = true;
            break;
          }
        }
        if (!flag2)
        {
          DataEnemyAbility ability = activeBattle.Battle.getAbility(enemyCounter.AbilityId);
          DataEnemyAbilityOptions options = ability.Options;
          this.listViewEnemyAbilities.Items.Add(new ListViewItem(new string[47]
          {
            ability.name,
            enemyCounter.AbilityId.ToString(),
            "",
            "0",
            "0",
            flag1 ? ability.ExerciseType.ToString() : System.Enum.GetName(typeof (SchemaConstants.ExerciseAbbr), (object) ability.ExerciseType),
            options.CastTime.ToString(),
            flag1 ? options.TargetRange.ToString() : System.Enum.GetName(typeof (SchemaConstants.TargetRange), (object) options.TargetRange),
            flag1 ? options.TargetMethod.ToString() : System.Enum.GetName(typeof (SchemaConstants.TargetMethod), (object) options.TargetMethod),
            flag1 ? options.TargetSegment.ToString() : System.Enum.GetName(typeof (SchemaConstants.TargetSegment), (object) options.TargetSegment),
            ability.ActionId.ToString(),
            options.Arg1.ToString(),
            options.Arg2.ToString(),
            options.Arg3.ToString(),
            options.Arg4.ToString(),
            options.Arg5.ToString(),
            options.Arg6.ToString(),
            options.Arg7.ToString(),
            options.Arg8.ToString(),
            options.Arg9.ToString(),
            options.Arg10.ToString(),
            options.Arg11.ToString(),
            options.Arg12.ToString(),
            options.Arg13.ToString(),
            options.Arg14.ToString(),
            options.Arg15.ToString(),
            options.Arg16.ToString(),
            options.Arg17.ToString(),
            options.Arg18.ToString(),
            options.Arg19.ToString(),
            options.Arg20.ToString(),
            options.Arg21.ToString(),
            options.Arg22.ToString(),
            options.Arg23.ToString(),
            options.Arg24.ToString(),
            options.Arg25.ToString(),
            options.Arg26.ToString(),
            options.Arg27.ToString(),
            options.Arg28.ToString(),
            options.Arg29.ToString(),
            options.Arg30.ToString(),
            options.StatusAilmentsFactor.ToString(),
            options.StatusAilmentsId.ToString(),
            flag1 ? options.TargetDeath.ToString() : System.Enum.GetName(typeof (SchemaConstants.TargetDeath), (object) options.TargetDeath),
            options.CounterEnable.ToString(),
            flag1 ? options.MinDamageThreshold.ToString() : System.Enum.GetName(typeof (SchemaConstants.DamageThresholdType), (object) options.MinDamageThreshold),
            flag1 ? options.MaxDamageThreshold.ToString() : System.Enum.GetName(typeof (SchemaConstants.DamageThresholdType), (object) options.MaxDamageThreshold)
          }));
        }
      }
      foreach (ColumnHeader column in this.listViewEnemyAbilities.Columns)
        column.Width = -2;
      foreach (ColumnHeader column in this.listViewCounters.Columns)
        column.Width = -2;
      this.PopulateAbilitySummary(enemy, selectedItem, activeBattle);
    }

    private void PopulateAbilitySummary(
      BasicEnemyInfo enemy,
      BasicEnemyParentInfo myEnemyParent,
      EventBattleInitiated battle)
    {
      this.richTextBoxAbilitySummary.Text = "";
      foreach (char ch in this.AbilitySummaryHelper(enemy, myEnemyParent, battle))
      {
        this.richTextBoxAbilitySummary.SelectionFont = this.richTextBoxAbilitySummary.Font;
        this.richTextBoxAbilitySummary.AppendText(ch.ToString());
      }
    }

    private string AbilitySummaryHelper(
      BasicEnemyInfo enemy,
      BasicEnemyParentInfo myEnemyParent,
      EventBattleInitiated battle)
    {
      if (enemy == null || myEnemyParent == null || battle == null)
        return "";
      bool single = myEnemyParent.Appearances[0] == 1U && this.comboBoxEnemySelection.Items.Count == 1;
      uint totalWeight = 0;
      foreach (DataEnemyParamAbilities enemyAbility in enemy.EnemyAbilities)
        totalWeight += enemyAbility.Weight;
      List<string> source = new List<string>();
      EnemyAbilityParser enemyAbilityParser = new EnemyAbilityParser(totalWeight, battle, single);
      CheckState checkState = this.checkBoxCastTimes.CheckState;
      bool flag1;
      if (checkState.Equals((object) CheckState.Indeterminate))
      {
        flag1 = enemy.EnemyCastTime.Equals("Variable");
      }
      else
      {
        checkState = this.checkBoxCastTimes.CheckState;
        flag1 = checkState.Equals((object) CheckState.Checked);
      }
      checkState = this.checkBoxEnumerate.CheckState;
      bool flag2;
      if (checkState.Equals((object) CheckState.Indeterminate))
      {
        int num = 0;
        foreach (DataEnemyConstraints constraint in myEnemyParent.Constraints)
        {
          if (constraint.ConstraintType == 1001U && (int) constraint.EnemyStatusId == (int) enemy.EnemyId)
            ++num;
        }
        flag2 = num >= 10;
      }
      else
      {
        checkState = this.checkBoxEnumerate.CheckState;
        flag2 = checkState.Equals((object) CheckState.Checked);
      }
      EnemyAbilityParserOptions parseOpt = new EnemyAbilityParserOptions()
      {
        displayFractions = this.checkBoxRatesAsFractions.Checked,
        displayCastTimes = flag1,
        translateAbilityNames = this.checkBoxTranslate.Checked
      };
      IOrderedEnumerable<DataEnemyParamAbilities> orderedEnumerable = enemy.getAbilities(myEnemyParent.Constraints).OrderBy<DataEnemyParamAbilities, int>((Func<DataEnemyParamAbilities, int>) (x =>
      {
        int val2 = int.MaxValue;
        foreach (DataEnemyConstraints constraint in myEnemyParent.Constraints)
        {
          if (constraint.ConstraintType == 1001U && constraint.AbilityTag.Equals(x.Tag))
          {
            int val1 = int.Parse(constraint.ConstraintValue);
            if (constraint.EnemyStatusId == 0U)
              val1 -= 10000;
            else if ((int) constraint.EnemyStatusId != (int) enemy.EnemyId)
              continue;
            val2 = Math.Min(val1, val2);
          }
        }
        return val2;
      })).OrderBy<DataEnemyParamAbilities, uint>((Func<DataEnemyParamAbilities, uint>) (x => x.Weight)).OrderBy<DataEnemyParamAbilities, uint>((Func<DataEnemyParamAbilities, uint>) (x => x.Weight != 0U ? x.UnlockTurn : 0U));
      if (flag2)
      {
        int val1 = 0;
        foreach (DataEnemyConstraints constraint in myEnemyParent.Constraints)
        {
          if (constraint.ConstraintType == 1001U && (int) constraint.EnemyStatusId == (int) enemy.EnemyId)
            val1 = Math.Max(val1, int.Parse(constraint.ConstraintValue));
        }
        for (int enumeratedTurn = 1; enumeratedTurn <= val1; ++enumeratedTurn)
        {
          foreach (DataEnemyConstraints constraint1 in myEnemyParent.Constraints)
          {
            if (constraint1.ConstraintType == 1001U && (int) constraint1.EnemyStatusId == (int) enemy.EnemyId && int.Parse(constraint1.ConstraintValue) == enumeratedTurn)
            {
              foreach (DataEnemyParamAbilities paramAbility in (IEnumerable<DataEnemyParamAbilities>) orderedEnumerable)
              {
                if (constraint1.AbilityTag.Equals(paramAbility.Tag))
                {
                  int num = 0;
                  bool flag3 = true;
                  if (paramAbility.Weight > 0U)
                    flag3 = false;
                  foreach (DataEnemyConstraints constraint2 in myEnemyParent.Constraints)
                  {
                    if (flag3 && constraint2.ConstraintType == 1001U && (constraint2.AbilityTag.Equals(paramAbility.Tag) && (int) constraint2.EnemyStatusId == (int) enemy.EnemyId) && int.Parse(constraint2.ConstraintValue) > enumeratedTurn)
                      flag3 = false;
                    if (flag3 && constraint2.ConstraintType >= 1003U && (constraint2.ConstraintType <= 1005U && constraint2.AbilityTag.Equals(paramAbility.Tag)) && (int) constraint1.EnemyStatusId == (int) enemy.EnemyId)
                      flag3 = false;
                    if (flag3 && constraint2.ConstraintType == 1002U && constraint2.AbilityTag.Equals(paramAbility.Tag) && (int) constraint2.EnemyStatusId == (int) enemy.EnemyId)
                      num = num == 0 ? int.Parse(constraint2.ConstraintValue) : Math.Min(int.Parse(constraint2.ConstraintValue), num);
                  }
                  if (num > 0 & flag3)
                    source.Add(enemyAbilityParser.parseAbility(paramAbility, myEnemyParent.Constraints, enemy, parseOpt, false, enumeratedTurn, num));
                  else
                    source.Add(enemyAbilityParser.parseAbility(paramAbility, myEnemyParent.Constraints, enemy, parseOpt, false, enumeratedTurn));
                }
              }
            }
          }
        }
        foreach (DataEnemyParamAbilities paramAbility in (IEnumerable<DataEnemyParamAbilities>) orderedEnumerable)
        {
          if (paramAbility.Weight > 0U)
          {
            source.Add(enemyAbilityParser.parseAbility(paramAbility, myEnemyParent.Constraints, enemy, parseOpt, false, 0));
          }
          else
          {
            bool flag3 = false;
            bool flag4 = false;
            foreach (DataEnemyConstraints constraint in myEnemyParent.Constraints)
            {
              if (constraint.AbilityTag.Equals(paramAbility.Tag))
              {
                if ((int) constraint.EnemyStatusId == (int) enemy.EnemyId && constraint.ConstraintType == 1001U)
                  flag3 = true;
                if (constraint.EnemyStatusId == 0U || (int) constraint.EnemyStatusId == (int) enemy.EnemyId && constraint.ConstraintType != 1001U && constraint.ConstraintType != 1002U)
                  flag4 = true;
              }
            }
            if (!flag3 | flag4)
              source.Add(enemyAbilityParser.parseAbility(paramAbility, myEnemyParent.Constraints, enemy, parseOpt, false, 0));
          }
        }
      }
      else
      {
        foreach (DataEnemyParamAbilities paramAbility in (IEnumerable<DataEnemyParamAbilities>) orderedEnumerable)
          source.Add(enemyAbilityParser.parseAbility(paramAbility, myEnemyParent.Constraints, enemy, parseOpt));
      }
      List<string> list = source.OrderBy<string, bool>((Func<string, bool>) (x => x[0] != 'T')).ThenBy<string, bool>((Func<string, bool>) (x => x[0] != 'S')).ToList<string>();
      foreach (DataEnemyParamCounters counter in (IEnumerable<DataEnemyParamCounters>) enemy.EnemyCounters.OrderBy<DataEnemyParamCounters, uint>((Func<DataEnemyParamCounters, uint>) (x => x.Rate)))
        list.Add(enemyAbilityParser.parseCounter(counter, parseOpt));
      return string.Join("  \n", (IEnumerable<string>) list);
    }

    private void CopySelectedItemsToClipboard(ListView myListView, string[] columnNames)
    {
      List<string> stringList1 = new List<string>();
      foreach (ListViewItem selectedItem in myListView.SelectedItems)
      {
        int count = selectedItem.SubItems.Count;
        List<string> stringList2 = new List<string>();
        for (int index = 0; index < count; ++index)
        {
          string columnName = columnNames[index];
          string text = selectedItem.SubItems[index].Text;
          stringList2.Add(string.Format("{0}: {1}", (object) columnName, (object) text));
        }
        stringList1.Add(string.Join(", ", (IEnumerable<string>) stringList2));
      }
      Clipboard.SetText(string.Join("  \n", (IEnumerable<string>) stringList1));
    }

    private void FFRKProxy_OnCompleteBattle(EventBattleInitiated battle) => this.BeginInvoke((Action) (() => this.PopulateEnemySelectionComboBox((EventBattleInitiated) null)));

    private void FFRKProxy_OnLeaveDungeon() => this.BeginInvoke((Action) (() => this.PopulateEnemySelectionComboBox((EventBattleInitiated) null)));

    private void FFRKProxy_OnListBattles(EventListBattles battles)
    {
    }

    private void FFRKProxy_OnListDungeons(EventListDungeons dungeons) => this.BeginInvoke((Action) (() => this.PopulateEnemySelectionComboBox((EventBattleInitiated) null)));

    private void FFRKProxy_OnBattleEngaged(EventBattleInitiated battle) => this.BeginInvoke((Action) (() => this.PopulateEnemySelectionComboBox(battle)));

    private void comboBoxEnemySelection_DropDownClosed(object sender, EventArgs e) => this.BeginInvoke((Action) (() => this.comboBoxEnemySelection.Select(0, 0)));

    private void comboBoxEnemySelection_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.PopulateEnemyInfoGroupBox(FFRKProxy.Instance.GameState.ActiveBattle);
      if (this.comboBoxEnemySelection.SelectedIndex == -1 || this.comboBoxEnemySelection.Items.Count == 0)
        this.PopulatePhaseSelectionComboBox((IEnumerable<BasicEnemyInfo>) null);
      else
        this.PopulatePhaseSelectionComboBox(((BasicEnemyParentInfo) this.comboBoxEnemySelection.SelectedItem).Phases);
    }

    private void linkLabelAI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start(e.Link.LinkData.ToString());

    private void linkLabelChildAI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start(e.Link.LinkData.ToString());

    private void comboBoxPhaseSelection_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.comboBoxPhaseSelection.SelectedIndex == -1)
        this.PopulatePhaseInfoGroupBox((BasicEnemyInfo) null);
      else
        this.PopulatePhaseInfoGroupBox((BasicEnemyInfo) this.comboBoxPhaseSelection.SelectedItem);
    }

    private void comboBoxPhaseSelection_DropDownClosed(object sender, EventArgs e) => this.BeginInvoke((Action) (() => this.comboBoxPhaseSelection.Select(0, 0)));

    private void dataGridViewElemental_SelectionChanged(object sender, EventArgs e) => this.dataGridViewElemental.ClearSelection();

    private void dataGridViewBreaks_SelectionChanged(object sender, EventArgs e)
    {
    }

    private void dataGridViewStats_SelectionChanged(object sender, EventArgs e)
    {
    }

    private void buttonToggleAbilityDetails_Click(object sender, EventArgs e)
    {
      if (this.showAbilitySummary)
      {
        this.showAbilitySummary = false;
        this.richTextBoxAbilitySummary.Visible = false;
        this.checkBoxRatesAsFractions.Visible = false;
        this.checkBoxCastTimes.Visible = false;
        this.checkBoxTranslate.Visible = false;
        this.checkBoxEnumerate.Visible = false;
        this.checkBoxRawOnly.Visible = true;
        this.groupBoxEnemyAbilities.Visible = true;
        this.groupBoxCounters.Visible = true;
        this.groupBoxEnemyConstraints.Visible = true;
        this.buttonToggleAbilityDetails.Text = "Show Enemy Ability Summary";
      }
      else
      {
        this.showAbilitySummary = true;
        this.groupBoxEnemyAbilities.Visible = false;
        this.groupBoxCounters.Visible = false;
        this.groupBoxEnemyConstraints.Visible = false;
        this.richTextBoxAbilitySummary.Visible = true;
        this.checkBoxRawOnly.Visible = false;
        this.checkBoxCastTimes.Visible = true;
        this.checkBoxRatesAsFractions.Visible = true;
        this.checkBoxTranslate.Visible = true;
        this.checkBoxEnumerate.Visible = true;
        this.buttonToggleAbilityDetails.Text = "Show Enemy Ability Data";
      }
      this.Focus();
    }

    private void checkBoxCastTimes_Click(object sender, EventArgs e)
    {
      if (this.comboBoxEnemySelection.SelectedIndex == -1 || this.comboBoxEnemySelection.Items.Count == 0 || (this.comboBoxPhaseSelection.SelectedIndex == -1 || this.comboBoxPhaseSelection.Items.Count == 0) || FFRKProxy.Instance == null)
        this.PopulateAbilitySummary((BasicEnemyInfo) null, (BasicEnemyParentInfo) null, (EventBattleInitiated) null);
      else
        this.PopulateAbilitySummary((BasicEnemyInfo) this.comboBoxPhaseSelection.SelectedItem, (BasicEnemyParentInfo) this.comboBoxEnemySelection.SelectedItem, FFRKProxy.Instance.GameState.ActiveBattle);
    }

    private void checkBoxRatesAsFractions_Click(object sender, EventArgs e)
    {
      if (this.comboBoxEnemySelection.SelectedIndex == -1 || this.comboBoxEnemySelection.Items.Count == 0 || (this.comboBoxPhaseSelection.SelectedIndex == -1 || this.comboBoxPhaseSelection.Items.Count == 0) || FFRKProxy.Instance == null)
        this.PopulateAbilitySummary((BasicEnemyInfo) null, (BasicEnemyParentInfo) null, (EventBattleInitiated) null);
      else
        this.PopulateAbilitySummary((BasicEnemyInfo) this.comboBoxPhaseSelection.SelectedItem, (BasicEnemyParentInfo) this.comboBoxEnemySelection.SelectedItem, FFRKProxy.Instance.GameState.ActiveBattle);
    }

    private void checkBoxRawOnly_Click(object sender, EventArgs e)
    {
      if (this.comboBoxPhaseSelection.SelectedIndex == -1 || this.comboBoxPhaseSelection.Items.Count == 0)
        this.PopulatePhaseInfoGroupBox((BasicEnemyInfo) null);
      else
        this.PopulatePhaseInfoGroupBox((BasicEnemyInfo) this.comboBoxPhaseSelection.SelectedItem);
    }

    private void listViewAIArguments_KeyUp(object sender, KeyEventArgs e)
    {
      if (sender != this.listViewAIArguments || (!e.Control || e.KeyCode != Keys.C))
        return;
      this.CopySelectedItemsToClipboard(this.listViewAIArguments, new string[3]
      {
        "Tag",
        "Value",
        "Type"
      });
    }

    private void checkBoxTranslate_Click(object sender, EventArgs e)
    {
      if (this.comboBoxEnemySelection.SelectedIndex == -1 || this.comboBoxEnemySelection.Items.Count == 0 || (this.comboBoxPhaseSelection.SelectedIndex == -1 || this.comboBoxPhaseSelection.Items.Count == 0) || FFRKProxy.Instance == null)
        this.PopulateAbilitySummary((BasicEnemyInfo) null, (BasicEnemyParentInfo) null, (EventBattleInitiated) null);
      else
        this.PopulateAbilitySummary((BasicEnemyInfo) this.comboBoxPhaseSelection.SelectedItem, (BasicEnemyParentInfo) this.comboBoxEnemySelection.SelectedItem, FFRKProxy.Instance.GameState.ActiveBattle);
    }

    private void SummaryButton_Click(object sender, EventArgs e)
    {
      StringBuilder stringBuilder1 = new StringBuilder();
      if (FFRKProxy.Instance.GameState.ActiveDungeon != null)
      {
        EventListBattles activeDungeon = FFRKProxy.Instance.GameState.ActiveDungeon;
        uint battleId = FFRKProxy.Instance.GameState.ActiveBattle.Battle.BattleId;
        DataBattle dataBattle = (DataBattle) null;
        foreach (DataBattle battle in activeDungeon.Battles)
        {
          if ((int) battle.Id == (int) battleId)
            dataBattle = battle;
        }
        stringBuilder1.Append(string.Format("*****\n\n##{0}  \n\n", (object) dataBattle.Name));
        StringBuilder stringBuilder2 = new StringBuilder();
        foreach (DataDungeonCaptures capture in activeDungeon.UserDungeon.Captures)
        {
          foreach (DataDungeonSpScore dataDungeonSpScore in capture.SpScore)
          {
            if ((int) battleId == (int) dataDungeonSpScore.BattleID)
              stringBuilder2.Append(string.Format("{0}. ", (object) new MedalConditionParser(dataDungeonSpScore.Title.ToString()).translate(false)));
          }
        }
        if (stringBuilder2.Length > 0)
          stringBuilder1.Append("**Medal Conditions:** " + (object) stringBuilder2 + "\n\n");
        DataDungeonPrizes prizes = activeDungeon.UserDungeon.Prizes;
        StringBuilder stringBuilder3 = new StringBuilder();
        if (prizes.ClearRewards != null)
        {
          foreach (DataDungeonPrizeItem clearReward in prizes.ClearRewards)
            stringBuilder3.Append(string.Format("* {0}  \n", (object) this.getItemName(clearReward.Id, clearReward.Quantity, clearReward.Name)));
          if (stringBuilder3.Length > 0)
            stringBuilder1.Append(string.Format("**Clear Reward{0}:**  \n\n{1}\n", stringBuilder3.Length != 1 ? (object) "s" : (object) "", (object) stringBuilder3));
        }
        StringBuilder stringBuilder4 = new StringBuilder();
        if (prizes.MasteryRewards != null)
        {
          foreach (DataDungeonPrizeItem masteryReward in prizes.MasteryRewards)
            stringBuilder4.Append(string.Format("* {0}  \n", (object) this.getItemName(masteryReward.Id, masteryReward.Quantity, masteryReward.Name)));
          if (stringBuilder4.Length > 0)
            stringBuilder1.Append(string.Format("**Mastery Reward{0}:**  \n\n{1}\n", stringBuilder4.Length != 1 ? (object) "s" : (object) "", (object) stringBuilder4));
        }
        StringBuilder stringBuilder5 = new StringBuilder();
        if (prizes.FirstClearRewards != null)
        {
          foreach (DataDungeonPrizeItem firstClearReward in prizes.FirstClearRewards)
            stringBuilder5.Append(string.Format("* {0}  \n", (object) this.getItemName(firstClearReward.Id, firstClearReward.Quantity, firstClearReward.Name)));
          if (stringBuilder5.Length > 0)
            stringBuilder1.Append(string.Format("**First Clear Reward{0}:**  \n\n{1}\n", stringBuilder5.Length != 1 ? (object) "s" : (object) "", (object) stringBuilder5));
        }
      }
      List<BasicEnemyParentInfo> basicEnemyParentInfoList = new List<BasicEnemyParentInfo>();
      foreach (object obj in this.comboBoxEnemySelection.Items)
        basicEnemyParentInfoList.Add((BasicEnemyParentInfo) obj);
      bool flag1 = true;
      string str1 = this.StatusVulnerabilities(basicEnemyParentInfoList[0].Phases.First<BasicEnemyInfo>().EnemyStatusImmunity);
      foreach (BasicEnemyParentInfo basicEnemyParentInfo in basicEnemyParentInfoList)
      {
        foreach (BasicEnemyInfo phase in basicEnemyParentInfo.Phases)
        {
          if (!str1.Equals(this.StatusVulnerabilities(phase.EnemyStatusImmunity)))
            flag1 = false;
        }
      }
      for (int index1 = 0; index1 < basicEnemyParentInfoList.Count; ++index1)
      {
        BasicEnemyParentInfo basicEnemyParentInfo = basicEnemyParentInfoList[index1];
        List<BasicEnemyInfo> basicEnemyInfoList = new List<BasicEnemyInfo>();
        foreach (BasicEnemyInfo phase in basicEnemyParentInfo.Phases)
          basicEnemyInfoList.Add(phase);
        bool flag2 = basicEnemyParentInfoList.Count > 1;
        bool flag3 = basicEnemyInfoList.Count > 1;
        if (!flag2)
          stringBuilder1.Append(string.Format("**{0}**\n\n", (object) basicEnemyParentInfo.EnemyName));
        else if (index1 == 0)
          stringBuilder1.Append("**Stats:**\n\n");
        if (index1 == 0)
        {
          if (flag2)
            stringBuilder1.Append("Enemy | ");
          else if (flag3)
            stringBuilder1.Append("Phase | ");
          stringBuilder1.Append("LV | HP | ATK | DEF | MAG | RES | MND | SPD |");
          if (!flag1)
            stringBuilder1.Append(" Status Vuln. |");
          stringBuilder1.Append("\n");
          if (flag2 | flag3)
            stringBuilder1.Append(":--|");
          stringBuilder1.Append(":--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|");
          if (!flag1)
            stringBuilder1.Append(":--|");
          stringBuilder1.Append("\n");
        }
        else
          stringBuilder1.Append("| \n");
        for (int index2 = 0; index2 < basicEnemyInfoList.Count; ++index2)
        {
          BasicEnemyInfo basicEnemyInfo = basicEnemyInfoList[index2];
          if (flag2)
          {
            stringBuilder1.Append(basicEnemyParentInfo.EnemyName);
            if (flag3)
              stringBuilder1.Append(" - ");
            else
              stringBuilder1.Append(" |");
          }
          if (flag3)
          {
            if (basicEnemyInfoList.Count <= 3)
            {
              if (index2 == 0)
                stringBuilder1.Append("Default |");
              if (index2 == 1)
                stringBuilder1.Append("Weak |");
              if (index2 == 2)
                stringBuilder1.Append("Very Weak |");
            }
            else
              stringBuilder1.Append(string.Format("Phase {0} |", (object) (index2 + 1)));
          }
          if (index2 == 0)
          {
            stringBuilder1.Append(string.Format(" {0} | {1} | {2} | {3} | ", (object) basicEnemyInfo.EnemyLv, (object) basicEnemyInfo.EnemyMaxHp.ToString("N0"), (object) basicEnemyInfo.EnemyAtk.ToString("N0"), (object) basicEnemyInfo.EnemyDef.ToString("N0")) + string.Format("{0} | {1} | {2} | {3} |", (object) basicEnemyInfo.EnemyMag.ToString("N0"), (object) basicEnemyInfo.EnemyRes.ToString("N0"), (object) basicEnemyInfo.EnemyMnd.ToString("N0"), (object) basicEnemyInfo.EnemySpd.ToString("N0")));
            if (!flag1)
              stringBuilder1.Append(string.Format(" {0} |", (object) this.StatusVulnerabilities(basicEnemyInfo.EnemyStatusImmunity)));
            stringBuilder1.Append("\n");
          }
          else
          {
            stringBuilder1.Append(" | | ");
            stringBuilder1.Append((int) basicEnemyInfo.EnemyAtk == (int) basicEnemyInfoList[index2 - 1].EnemyAtk ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyAtk.ToString("N0")) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyAtk.ToString("N0")));
            stringBuilder1.Append((int) basicEnemyInfo.EnemyDef == (int) basicEnemyInfoList[index2 - 1].EnemyDef ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyDef.ToString("N0")) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyDef.ToString("N0")));
            stringBuilder1.Append((int) basicEnemyInfo.EnemyMag == (int) basicEnemyInfoList[index2 - 1].EnemyMag ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyMag.ToString("N0")) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyMag.ToString("N0")));
            stringBuilder1.Append((int) basicEnemyInfo.EnemyRes == (int) basicEnemyInfoList[index2 - 1].EnemyRes ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyRes.ToString("N0")) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyRes.ToString("N0")));
            stringBuilder1.Append((int) basicEnemyInfo.EnemyMnd == (int) basicEnemyInfoList[index2 - 1].EnemyMnd ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyMnd.ToString("N0")) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyMnd.ToString("N0")));
            stringBuilder1.Append((int) basicEnemyInfo.EnemySpd == (int) basicEnemyInfoList[index2 - 1].EnemySpd ? string.Format("{0} |", (object) basicEnemyInfo.EnemySpd.ToString("N0")) : string.Format("**{0}** |", (object) basicEnemyInfo.EnemySpd.ToString("N0")));
            if (!flag1)
              stringBuilder1.Append(string.Format(" {0} |", (object) this.StatusVulnerabilities(basicEnemyInfo.EnemyStatusImmunity)));
            stringBuilder1.Append("\n");
          }
        }
      }
      stringBuilder1.Append("\n**Elemental Damage Taken:**  \n\n");
      for (int index1 = 0; index1 < basicEnemyParentInfoList.Count; ++index1)
      {
        BasicEnemyParentInfo basicEnemyParentInfo1 = basicEnemyParentInfoList[index1];
        List<BasicEnemyInfo> basicEnemyInfoList = new List<BasicEnemyInfo>();
        foreach (BasicEnemyInfo phase in basicEnemyParentInfo1.Phases)
          basicEnemyInfoList.Add(phase);
        bool flag2 = basicEnemyParentInfoList.Count > 1;
        bool flag3 = basicEnemyInfoList.Count > 1;
        if (index1 == 0)
        {
          bool flag4 = true;
          BasicEnemyInfo basicEnemyInfo = basicEnemyParentInfoList[0].Phases.First<BasicEnemyInfo>();
          foreach (BasicEnemyParentInfo basicEnemyParentInfo2 in basicEnemyParentInfoList)
          {
            foreach (BasicEnemyInfo phase in basicEnemyParentInfo2.Phases)
            {
              if (!basicEnemyInfo.EnemyFireDef.Equals(phase.EnemyFireDef) || !basicEnemyInfo.EnemyIceDef.Equals(phase.EnemyIceDef) || (!basicEnemyInfo.EnemyLitDef.Equals(phase.EnemyLitDef) || !basicEnemyInfo.EnemyEarthDef.Equals(phase.EnemyEarthDef)) || (!basicEnemyInfo.EnemyWindDef.Equals(phase.EnemyWindDef) || !basicEnemyInfo.EnemyWaterDef.Equals(phase.EnemyWaterDef) || (!basicEnemyInfo.EnemyHolyDef.Equals(phase.EnemyHolyDef) || !basicEnemyInfo.EnemyDarkDef.Equals(phase.EnemyDarkDef))) || !basicEnemyInfo.EnemyBioDef.Equals(phase.EnemyBioDef))
                flag4 = false;
            }
          }
          if (flag4)
          {
            bool flag5 = true;
            if (basicEnemyParentInfoList.Count == 1 && basicEnemyParentInfoList[0].Phases.Count<BasicEnemyInfo>() == 1)
              flag5 = false;
            if (flag5)
              stringBuilder1.Append(" | ");
            stringBuilder1.Append("Fire | Ice | Lightning | Earth | Wind | Water | Holy | Dark | Bio \n");
            if (flag5)
              stringBuilder1.Append(":--|");
            stringBuilder1.Append(":--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:\n");
            if (flag5)
            {
              if (basicEnemyParentInfoList.Count == 1)
                stringBuilder1.Append("All Phases | ");
              else
                stringBuilder1.Append("All Enemies | ");
            }
            stringBuilder1.Append(int.Parse(basicEnemyInfo.EnemyFireDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyFireDef) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyFireDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo.EnemyIceDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyIceDef) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyIceDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo.EnemyLitDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyLitDef) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyLitDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo.EnemyEarthDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyEarthDef) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyEarthDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo.EnemyWindDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyWindDef) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyWindDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo.EnemyWaterDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyWaterDef) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyWaterDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo.EnemyHolyDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyHolyDef) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyHolyDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo.EnemyDarkDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo.EnemyDarkDef) : string.Format("**{0}** | ", (object) basicEnemyInfo.EnemyDarkDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo.EnemyBioDef.Replace("%", "")) <= 100 ? string.Format("{0} \n", (object) basicEnemyInfo.EnemyBioDef) : string.Format("**{0}** \n", (object) basicEnemyInfo.EnemyBioDef));
            break;
          }
          stringBuilder1.Append(string.Format("{0}", flag2 ? (object) "Enemy" : (object) "Phase"));
          stringBuilder1.Append(" | Fire | Ice | Lightning | Earth | Wind | Water | Holy | Dark | Bio \n");
          stringBuilder1.Append(":--|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:|:--:\n");
        }
        else
          stringBuilder1.Append("| \n");
        bool flag6 = true;
        BasicEnemyInfo basicEnemyInfo1 = basicEnemyParentInfo1.Phases.First<BasicEnemyInfo>();
        foreach (BasicEnemyInfo phase in basicEnemyParentInfo1.Phases)
        {
          if (!basicEnemyInfo1.EnemyFireDef.Equals(phase.EnemyFireDef) || !basicEnemyInfo1.EnemyIceDef.Equals(phase.EnemyIceDef) || (!basicEnemyInfo1.EnemyLitDef.Equals(phase.EnemyLitDef) || !basicEnemyInfo1.EnemyEarthDef.Equals(phase.EnemyEarthDef)) || (!basicEnemyInfo1.EnemyWindDef.Equals(phase.EnemyWindDef) || !basicEnemyInfo1.EnemyWaterDef.Equals(phase.EnemyWaterDef) || (!basicEnemyInfo1.EnemyHolyDef.Equals(phase.EnemyHolyDef) || !basicEnemyInfo1.EnemyDarkDef.Equals(phase.EnemyDarkDef))) || !basicEnemyInfo1.EnemyBioDef.Equals(phase.EnemyBioDef))
            flag6 = false;
        }
        if (flag6)
        {
          bool flag4 = basicEnemyParentInfo1.Phases.Count<BasicEnemyInfo>() > 1;
          stringBuilder1.Append(string.Format("{0}{1} | ", (object) basicEnemyParentInfo1.EnemyName, flag4 ? (object) " (all phases)" : (object) ""));
          stringBuilder1.Append(int.Parse(basicEnemyInfo1.EnemyFireDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo1.EnemyFireDef) : string.Format("**{0}** | ", (object) basicEnemyInfo1.EnemyFireDef));
          stringBuilder1.Append(int.Parse(basicEnemyInfo1.EnemyIceDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo1.EnemyIceDef) : string.Format("**{0}** | ", (object) basicEnemyInfo1.EnemyIceDef));
          stringBuilder1.Append(int.Parse(basicEnemyInfo1.EnemyLitDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo1.EnemyLitDef) : string.Format("**{0}** | ", (object) basicEnemyInfo1.EnemyLitDef));
          stringBuilder1.Append(int.Parse(basicEnemyInfo1.EnemyEarthDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo1.EnemyEarthDef) : string.Format("**{0}** | ", (object) basicEnemyInfo1.EnemyEarthDef));
          stringBuilder1.Append(int.Parse(basicEnemyInfo1.EnemyWindDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo1.EnemyWindDef) : string.Format("**{0}** | ", (object) basicEnemyInfo1.EnemyWindDef));
          stringBuilder1.Append(int.Parse(basicEnemyInfo1.EnemyWaterDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo1.EnemyWaterDef) : string.Format("**{0}** | ", (object) basicEnemyInfo1.EnemyWaterDef));
          stringBuilder1.Append(int.Parse(basicEnemyInfo1.EnemyHolyDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo1.EnemyHolyDef) : string.Format("**{0}** | ", (object) basicEnemyInfo1.EnemyHolyDef));
          stringBuilder1.Append(int.Parse(basicEnemyInfo1.EnemyDarkDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo1.EnemyDarkDef) : string.Format("**{0}** | ", (object) basicEnemyInfo1.EnemyDarkDef));
          stringBuilder1.Append(int.Parse(basicEnemyInfo1.EnemyBioDef.Replace("%", "")) <= 100 ? string.Format("{0} \n", (object) basicEnemyInfo1.EnemyBioDef) : string.Format("**{0}** \n", (object) basicEnemyInfo1.EnemyBioDef));
        }
        else
        {
          for (int index2 = 0; index2 < basicEnemyInfoList.Count; ++index2)
          {
            BasicEnemyInfo basicEnemyInfo2 = basicEnemyInfoList[index2];
            if (flag2)
            {
              stringBuilder1.Append(basicEnemyParentInfo1.EnemyName);
              if (flag3)
                stringBuilder1.Append(" - ");
              else
                stringBuilder1.Append(" |");
            }
            if (flag3)
            {
              if (basicEnemyInfoList.Count <= 3)
              {
                if (index2 == 0)
                  stringBuilder1.Append("Default |");
                if (index2 == 1)
                  stringBuilder1.Append("Weak |");
                if (index2 == 2)
                  stringBuilder1.Append("Very Weak |");
              }
              else
                stringBuilder1.Append(string.Format("Phase {0} |", (object) (index2 + 1)));
            }
            stringBuilder1.Append(int.Parse(basicEnemyInfo2.EnemyFireDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo2.EnemyFireDef) : string.Format("**{0}** | ", (object) basicEnemyInfo2.EnemyFireDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo2.EnemyIceDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo2.EnemyIceDef) : string.Format("**{0}** | ", (object) basicEnemyInfo2.EnemyIceDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo2.EnemyLitDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo2.EnemyLitDef) : string.Format("**{0}** | ", (object) basicEnemyInfo2.EnemyLitDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo2.EnemyEarthDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo2.EnemyEarthDef) : string.Format("**{0}** | ", (object) basicEnemyInfo2.EnemyEarthDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo2.EnemyWindDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo2.EnemyWindDef) : string.Format("**{0}** | ", (object) basicEnemyInfo2.EnemyWindDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo2.EnemyWaterDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo2.EnemyWaterDef) : string.Format("**{0}** | ", (object) basicEnemyInfo2.EnemyWaterDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo2.EnemyHolyDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo2.EnemyHolyDef) : string.Format("**{0}** | ", (object) basicEnemyInfo2.EnemyHolyDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo2.EnemyDarkDef.Replace("%", "")) <= 100 ? string.Format("{0} | ", (object) basicEnemyInfo2.EnemyDarkDef) : string.Format("**{0}** | ", (object) basicEnemyInfo2.EnemyDarkDef));
            stringBuilder1.Append(int.Parse(basicEnemyInfo2.EnemyBioDef.Replace("%", "")) <= 100 ? string.Format("{0} \n", (object) basicEnemyInfo2.EnemyBioDef) : string.Format("**{0}** \n", (object) basicEnemyInfo2.EnemyBioDef));
          }
        }
      }
      stringBuilder1.Append("\n**Break Effectiveness:**  ");
      bool flag7 = true;
      string enemyAtkBrkDef = basicEnemyParentInfoList[0].Phases.First<BasicEnemyInfo>().EnemyAtkBrkDef;
      foreach (BasicEnemyParentInfo basicEnemyParentInfo in basicEnemyParentInfoList)
      {
        foreach (BasicEnemyInfo phase in basicEnemyParentInfo.Phases)
        {
          if (!enemyAtkBrkDef.Equals(phase.EnemyAtkBrkDef) || !enemyAtkBrkDef.Equals(phase.EnemyDefBrkDef) || (!enemyAtkBrkDef.Equals(phase.EnemyMagBrkDef) || !enemyAtkBrkDef.Equals(phase.EnemyResBrkDef)) || !enemyAtkBrkDef.Equals(phase.EnemyMndBrkDef))
            flag7 = false;
        }
      }
      if (flag7)
      {
        stringBuilder1.Append(string.Format("{0} (all)  \n", (object) enemyAtkBrkDef));
      }
      else
      {
        for (int index1 = 0; index1 < basicEnemyParentInfoList.Count; ++index1)
        {
          BasicEnemyParentInfo basicEnemyParentInfo = basicEnemyParentInfoList[index1];
          List<BasicEnemyInfo> basicEnemyInfoList = new List<BasicEnemyInfo>();
          foreach (BasicEnemyInfo phase in basicEnemyParentInfo.Phases)
            basicEnemyInfoList.Add(phase);
          bool flag2 = basicEnemyParentInfoList.Count > 1;
          bool flag3 = basicEnemyInfoList.Count > 1;
          if (index1 == 0)
          {
            stringBuilder1.Append(string.Format("\n\n{0}", flag2 ? (object) "Enemy" : (object) "Phase"));
            stringBuilder1.Append(" | ATK | DEF | MAG| RES | MND | \n");
            stringBuilder1.Append(":--|:--:|:--:|:--:|:--:|:--:|\n");
          }
          else
            stringBuilder1.Append("| \n");
          bool flag4 = true;
          BasicEnemyInfo basicEnemyInfo1 = basicEnemyParentInfo.Phases.First<BasicEnemyInfo>();
          foreach (BasicEnemyInfo phase in basicEnemyParentInfo.Phases)
          {
            if (!basicEnemyInfo1.EnemyAtkBrkDef.Equals(phase.EnemyAtkBrkDef) || !basicEnemyInfo1.EnemyDefBrkDef.Equals(phase.EnemyDefBrkDef) || (!basicEnemyInfo1.EnemyMagBrkDef.Equals(phase.EnemyMagBrkDef) || !basicEnemyInfo1.EnemyResBrkDef.Equals(phase.EnemyResBrkDef)) || !basicEnemyInfo1.EnemyMndBrkDef.Equals(phase.EnemyMndBrkDef))
              flag4 = false;
          }
          if (flag4)
          {
            bool flag5 = basicEnemyParentInfo.Phases.Count<BasicEnemyInfo>() > 1;
            stringBuilder1.Append(string.Format("{0}{1} | ", (object) basicEnemyParentInfo.EnemyName, flag5 ? (object) " (all phases)" : (object) ""));
            stringBuilder1.Append(string.Format("{0} | {1} | {2} | ", (object) basicEnemyInfo1.EnemyAtkBrkDef, (object) basicEnemyInfo1.EnemyDefBrkDef, (object) basicEnemyInfo1.EnemyMagBrkDef) + string.Format("{0} | {1} \n", (object) basicEnemyInfo1.EnemyResBrkDef, (object) basicEnemyInfo1.EnemyMndBrkDef));
          }
          else
          {
            for (int index2 = 0; index2 < basicEnemyInfoList.Count; ++index2)
            {
              BasicEnemyInfo basicEnemyInfo2 = basicEnemyInfoList[index2];
              if (flag2)
              {
                stringBuilder1.Append(basicEnemyParentInfo.EnemyName);
                if (flag3)
                  stringBuilder1.Append(" - ");
                else
                  stringBuilder1.Append(" |");
              }
              if (flag3)
              {
                if (basicEnemyInfoList.Count <= 3)
                {
                  if (index2 == 0)
                    stringBuilder1.Append("Default |");
                  if (index2 == 1)
                    stringBuilder1.Append("Weak |");
                  if (index2 == 2)
                    stringBuilder1.Append("Very Weak |");
                }
                else
                  stringBuilder1.Append(string.Format("Phase {0} |", (object) (index2 + 1)));
              }
              stringBuilder1.Append(string.Format(" {0} | {1} | {2} | ", (object) basicEnemyInfoList[index2].EnemyAtkBrkDef, (object) basicEnemyInfoList[index2].EnemyDefBrkDef, (object) basicEnemyInfoList[index2].EnemyMagBrkDef) + string.Format("{0} | {1} \n", (object) basicEnemyInfoList[index2].EnemyResBrkDef, (object) basicEnemyInfoList[index2].EnemyMndBrkDef));
            }
          }
        }
      }
      if (flag1)
        stringBuilder1.Append(string.Format("**Status Vulnerabilities{0}**: {1}  \n", basicEnemyParentInfoList.Count == 1 ? (object) "" : (object) " (all)", (object) str1));
      if (basicEnemyParentInfoList.Count == 1 && basicEnemyParentInfoList[0].Phases.Count<BasicEnemyInfo>() == 1)
        stringBuilder1.Append("\n##Moveset\n\n");
      else
        stringBuilder1.Append("\n##Movesets\n\n");
      bool flag8 = true;
      string enemyCastTime = basicEnemyParentInfoList[0].Phases.First<BasicEnemyInfo>().EnemyCastTime;
      foreach (BasicEnemyParentInfo basicEnemyParentInfo in basicEnemyParentInfoList)
      {
        foreach (BasicEnemyInfo phase in basicEnemyParentInfo.Phases)
        {
          if (phase.EnemyCastTime.Equals("Variable") || !phase.EnemyCastTime.Equals(enemyCastTime))
            flag8 = false;
        }
      }
      if (flag8)
      {
        string str2 = enemyCastTime.Replace("sec", "seconds");
        if (str2.Equals("1 seconds"))
          str2 = "1 second";
        stringBuilder1.Append(string.Format("All (non-interrupt) enemy abilities in this fight have a cast time of {0}.\n\n", (object) str2));
      }
      for (int index1 = 0; index1 < basicEnemyParentInfoList.Count; ++index1)
      {
        BasicEnemyParentInfo myEnemyParent = basicEnemyParentInfoList[index1];
        List<BasicEnemyInfo> basicEnemyInfoList = new List<BasicEnemyInfo>();
        foreach (BasicEnemyInfo phase in myEnemyParent.Phases)
          basicEnemyInfoList.Add(phase);
        for (int index2 = 0; index2 < basicEnemyInfoList.Count; ++index2)
        {
          BasicEnemyInfo enemy = basicEnemyInfoList[index2];
          string enemyName = myEnemyParent.EnemyName;
          if (basicEnemyInfoList.Count == 2 || basicEnemyInfoList.Count == 3)
          {
            if (index2 == 0)
              enemyName += " - Default";
            if (index2 == 1)
              enemyName += " - Weak";
            if (index2 == 2)
              enemyName += " - Very Weak";
          }
          else if (basicEnemyInfoList.Count >= 4)
            enemyName += string.Format(" - Phase {0}", (object) (index2 + 1));
          if (basicEnemyInfoList.Count >= 2)
          {
            bool flag2 = false;
            bool flag3 = false;
            string str2 = "hp_rate_to_phase_" + (object) basicEnemyInfoList.Count;
            string str3 = "hp_rate_to_phase_" + (object) (basicEnemyInfoList.Count + 1);
            if (basicEnemyInfoList.Count == 6)
              str2 = "hp_rate_to_phase_5";
            foreach (DataAIArgs aiArg in myEnemyParent.AiArgs)
            {
              if (aiArg.Tag.Equals(str2) || basicEnemyInfoList.Count == 2 && aiArg.Tag.Equals("hp_rate_to_weak") || basicEnemyInfoList.Count == 3 && aiArg.Tag.Equals("hp_rate_to_very_weak"))
                flag2 = true;
              else if (aiArg.Tag.Equals(str3) || basicEnemyInfoList.Count == 1 && aiArg.Tag.Equals("hp_rate_to_weak") || basicEnemyInfoList.Count == 2 && aiArg.Tag.Equals("hp_rate_to_very_weak"))
                flag3 = true;
            }
            if (flag2 && !flag3)
            {
              DataAIArgs dataAiArgs1 = (DataAIArgs) null;
              DataAIArgs dataAiArgs2 = (DataAIArgs) null;
              string str4 = "hp_rate_to_phase_" + (object) (index2 + 1);
              string str5 = "hp_rate_to_phase_" + (object) (index2 + 2);
              int result1 = -1;
              int result2 = -1;
              foreach (DataAIArgs aiArg in myEnemyParent.AiArgs)
              {
                if (aiArg.Tag.Equals(str4) || index2 == 1 && aiArg.Tag.Equals("hp_rate_to_weak") || index2 == 2 && aiArg.Tag.Equals("hp_rate_to_very_weak"))
                  dataAiArgs1 = aiArg;
                else if (aiArg.Tag.Equals(str5) || index2 == 0 && aiArg.Tag.Equals("hp_rate_to_weak") || index2 == 1 && aiArg.Tag.Equals("hp_rate_to_very_weak"))
                  dataAiArgs2 = aiArg;
              }
              if (dataAiArgs1 != null && int.TryParse(dataAiArgs1.ArgValue, out result1))
              {
                if (index2 == basicEnemyInfoList.Count - 1)
                  enemyName += string.Format(" ({0}% - 0% HP)", (object) result1);
                else if (index2 < basicEnemyInfoList.Count - 1 && dataAiArgs2 != null && int.TryParse(dataAiArgs2.ArgValue, out result2))
                  enemyName += string.Format(" ({0}% - {1}% HP)", (object) result1, (object) (result2 + 1));
                else if (index2 < basicEnemyInfoList.Count - 1 && dataAiArgs2 == null)
                  enemyName += string.Format(" ({0}% - 0% HP)", (object) dataAiArgs1.ArgValue);
              }
              else if (index2 == 0 && dataAiArgs2 != null && int.TryParse(dataAiArgs2.ArgValue, out result2))
                enemyName += string.Format(" (100% - {0}% HP)", (object) (result2 + 1));
            }
          }
          stringBuilder1.Append(string.Format("**{0}:**  \n\n", (object) enemyName));
          if (!flag8 && !enemy.EnemyCastTime.Equals("Variable"))
            stringBuilder1.Append(string.Format("(These abilities have a cast time of {0})\n\n", (object) enemy.EnemyCastTime));
          string str6 = this.AbilitySummaryHelper(enemy, myEnemyParent, FFRKProxy.Instance.GameState.ActiveBattle).Replace("\n", "\n* ").Replace("not yet implemented", "**not yet implemented**").Replace("not parsed", "**not parsed**").Replace(" 【", "【");
          if (!str6.Equals(""))
            stringBuilder1.Append(string.Format("* {0}\n\n", (object) str6));
        }
      }
      Clipboard.SetText(stringBuilder1.ToString());
    }

    public string getItemName(uint ItemId, uint num, string defaultName)
    {
      string str1 = "";
      if (ItemId > 40000000U && ItemId <= 40000065U)
      {
        string str2 = "";
        switch (ItemId % 5U)
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
        str1 = str2 + System.Enum.GetName(typeof (SchemaConstants.ItemID), (object) (uint) ((int) ((ItemId - 1U) / 5U) * 5 + 5));
      }
      else if (ItemId >= 40000066U && ItemId <= 40000078U)
        str1 = System.Enum.GetName(typeof (SchemaConstants.ItemID), (object) ItemId);
      else if (ItemId >= 161000001U && ItemId <= 161001000U)
      {
        try
        {
          str1 = System.Enum.GetName(typeof (SchemaConstants.MagiciteID), (object) ItemId);
        }
        catch (Exception ex)
        {
          FiddlerApplication.Log.LogString(ex.ToString());
        }
      }
      else
        str1 = !SchemaConstants.miscItemNames.ContainsKey(ItemId) ? defaultName : SchemaConstants.miscItemNames[ItemId];
      string str3 = str1.Replace('_', ' ');
      if (num > 1U)
        str3 += string.Format(" x{0}", (object) num);
      return str3;
    }

    private string StatusVulnerabilities(List<string> input)
    {
      string str = string.Join(", ", new List<string>()
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
      }.Except<string>((IEnumerable<string>) input).ToList<string>().ToArray()).Replace('_', ' ');
      if (str.Equals(""))
        str = "None";
      return str;
    }

    private void checkBoxEnumerate_Click(object sender, EventArgs e)
    {
      if (this.comboBoxEnemySelection.SelectedIndex == -1 || this.comboBoxEnemySelection.Items.Count == 0 || (this.comboBoxPhaseSelection.SelectedIndex == -1 || this.comboBoxPhaseSelection.Items.Count == 0) || FFRKProxy.Instance == null)
        this.PopulateAbilitySummary((BasicEnemyInfo) null, (BasicEnemyParentInfo) null, (EventBattleInitiated) null);
      else
        this.PopulateAbilitySummary((BasicEnemyInfo) this.comboBoxPhaseSelection.SelectedItem, (BasicEnemyParentInfo) this.comboBoxEnemySelection.SelectedItem, FFRKProxy.Instance.GameState.ActiveBattle);
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
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FFRKViewEnemyDetails));
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle5 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle6 = new DataGridViewCellStyle();
      this.enemyInfoGroupBox = new GroupBox();
      this.textChildPosID = new TextBox();
      this.labelChildPosID = new Label();
      this.textChildID = new TextBox();
      this.labelChildID = new Label();
      this.linkLabelAI = new LinkLabel();
      this.textAIID = new TextBox();
      this.labelAIID = new Label();
      this.linkLabelChildAI = new LinkLabel();
      this.textInitHP = new TextBox();
      this.SummaryButton = new Button();
      this.textEnglishName = new TextBox();
      this.labelInitialHP = new Label();
      this.labelEnglishName = new Label();
      this.textID = new TextBox();
      this.groupBoxEnemyAppearances = new GroupBox();
      this.listViewEnemyAppearances = new ListView();
      this.columnEnemyRounds = new ColumnHeader();
      this.columnEnemyMultiplicity = new ColumnHeader();
      this.textChildAIID = new TextBox();
      this.textLevel = new TextBox();
      this.labelChildAIID = new Label();
      this.labelEnemyLevel = new Label();
      this.pictureBox1 = new PictureBox();
      this.groupBoxAIArguments = new GroupBox();
      this.listViewAIArguments = new ListView();
      this.columnAITag = new ColumnHeader();
      this.columnAIArgValue = new ColumnHeader();
      this.columnAIArgType = new ColumnHeader();
      this.labelEnemyID = new Label();
      this.labelEnemySelection = new Label();
      this.comboBoxEnemySelection = new ComboBox();
      this.groupBoxEnemyPhase = new GroupBox();
      this.checkBoxEnumerate = new CheckBox();
      this.checkBoxTranslate = new CheckBox();
      this.checkBoxRawOnly = new CheckBox();
      this.checkBoxCastTimes = new CheckBox();
      this.checkBoxRatesAsFractions = new CheckBox();
      this.buttonToggleAbilityDetails = new Button();
      this.groupBoxAbilityDetails = new GroupBox();
      this.richTextBoxAbilitySummary = new RichTextBox();
      this.groupBoxCounters = new GroupBox();
      this.listViewCounters = new ListView();
      this.columnHeader50 = new ColumnHeader();
      this.columnHeader46 = new ColumnHeader();
      this.columnHeader47 = new ColumnHeader();
      this.columnHeader48 = new ColumnHeader();
      this.columnHeader49 = new ColumnHeader();
      this.groupBoxEnemyAbilities = new GroupBox();
      this.listViewEnemyAbilities = new ListView();
      this.columnAbilityName = new ColumnHeader();
      this.columnAbilityID = new ColumnHeader();
      this.columnAbilityTag = new ColumnHeader();
      this.columnAbilityWeight = new ColumnHeader();
      this.columnAbilityUnlock = new ColumnHeader();
      this.columnAbilityExercise = new ColumnHeader();
      this.columnAbilityCastTime = new ColumnHeader();
      this.columnAbilityTarRange = new ColumnHeader();
      this.columnAbilityTarMethod = new ColumnHeader();
      this.columnAbilityTarSegment = new ColumnHeader();
      this.columnAbilityActionID = new ColumnHeader();
      this.columnArg1 = new ColumnHeader();
      this.columnArg2 = new ColumnHeader();
      this.columnArg3 = new ColumnHeader();
      this.columnArg4 = new ColumnHeader();
      this.columnArg5 = new ColumnHeader();
      this.columnArg6 = new ColumnHeader();
      this.columnArg7 = new ColumnHeader();
      this.columnArg8 = new ColumnHeader();
      this.columnArg9 = new ColumnHeader();
      this.columnArg10 = new ColumnHeader();
      this.columnArg11 = new ColumnHeader();
      this.columnArg12 = new ColumnHeader();
      this.columnArg13 = new ColumnHeader();
      this.columnArg14 = new ColumnHeader();
      this.columnArg15 = new ColumnHeader();
      this.columnArg16 = new ColumnHeader();
      this.columnArg17 = new ColumnHeader();
      this.columnArg18 = new ColumnHeader();
      this.columnArg19 = new ColumnHeader();
      this.columnArg20 = new ColumnHeader();
      this.columnArg21 = new ColumnHeader();
      this.columnArg22 = new ColumnHeader();
      this.columnArg23 = new ColumnHeader();
      this.columnArg24 = new ColumnHeader();
      this.columnArg25 = new ColumnHeader();
      this.columnArg26 = new ColumnHeader();
      this.columnArg27 = new ColumnHeader();
      this.columnArg28 = new ColumnHeader();
      this.columnArg29 = new ColumnHeader();
      this.columnArg30 = new ColumnHeader();
      this.columnStatusFactor = new ColumnHeader();
      this.columnStatusID = new ColumnHeader();
      this.columnTargetDeath = new ColumnHeader();
      this.columnCounterEnable = new ColumnHeader();
      this.columnMinDamageThresh = new ColumnHeader();
      this.columnMaxDamageThresh = new ColumnHeader();
      this.groupBoxEnemyConstraints = new GroupBox();
      this.listViewEnemyConstraints = new ListView();
      this.columnConstraintAbilityTag = new ColumnHeader();
      this.columnConstraintValue = new ColumnHeader();
      this.columnConstraintType = new ColumnHeader();
      this.columnConstraintPriority = new ColumnHeader();
      this.columnConstraintEnemyStatusID = new ColumnHeader();
      this.columnConstraintOptions = new ColumnHeader();
      this.label5 = new Label();
      this.textCastTime = new TextBox();
      this.label4 = new Label();
      this.groupBox3 = new GroupBox();
      this.dataGridViewBreaks = new DataGridView();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      this.label3 = new Label();
      this.textEXP = new TextBox();
      this.textPhaseID = new TextBox();
      this.textPhaseEnglishName = new TextBox();
      this.textPhaseDispName = new TextBox();
      this.label2 = new Label();
      this.label1 = new Label();
      this.labelDispName = new Label();
      this.groupBox2 = new GroupBox();
      this.dataGridViewStats = new DataGridView();
      this.Column17 = new DataGridViewTextBoxColumn();
      this.Column10 = new DataGridViewTextBoxColumn();
      this.Column11 = new DataGridViewTextBoxColumn();
      this.Column12 = new DataGridViewTextBoxColumn();
      this.Column13 = new DataGridViewTextBoxColumn();
      this.Column14 = new DataGridViewTextBoxColumn();
      this.Column15 = new DataGridViewTextBoxColumn();
      this.Column16 = new DataGridViewTextBoxColumn();
      this.Column18 = new DataGridViewTextBoxColumn();
      this.Column20 = new DataGridViewTextBoxColumn();
      this.Column19 = new DataGridViewTextBoxColumn();
      this.groupBox1 = new GroupBox();
      this.dataGridViewElemental = new DataGridView();
      this.Column1 = new DataGridViewTextBoxColumn();
      this.Column2 = new DataGridViewTextBoxColumn();
      this.Column3 = new DataGridViewTextBoxColumn();
      this.Column4 = new DataGridViewTextBoxColumn();
      this.Column5 = new DataGridViewTextBoxColumn();
      this.Column6 = new DataGridViewTextBoxColumn();
      this.Column7 = new DataGridViewTextBoxColumn();
      this.Column8 = new DataGridViewTextBoxColumn();
      this.Column9 = new DataGridViewTextBoxColumn();
      this.textStatusVuln = new TextBox();
      this.comboBoxPhaseSelection = new ComboBox();
      this.labelPhaseSelection = new Label();
      this.toolTip1 = new ToolTip(this.components);
      this.enemyInfoGroupBox.SuspendLayout();
      this.groupBoxEnemyAppearances.SuspendLayout();
      ((ISupportInitialize) this.pictureBox1).BeginInit();
      this.groupBoxAIArguments.SuspendLayout();
      this.groupBoxEnemyPhase.SuspendLayout();
      this.groupBoxAbilityDetails.SuspendLayout();
      this.groupBoxCounters.SuspendLayout();
      this.groupBoxEnemyAbilities.SuspendLayout();
      this.groupBoxEnemyConstraints.SuspendLayout();
      this.groupBox3.SuspendLayout();
      ((ISupportInitialize) this.dataGridViewBreaks).BeginInit();
      this.groupBox2.SuspendLayout();
      ((ISupportInitialize) this.dataGridViewStats).BeginInit();
      this.groupBox1.SuspendLayout();
      ((ISupportInitialize) this.dataGridViewElemental).BeginInit();
      this.SuspendLayout();
      this.enemyInfoGroupBox.Controls.Add((Control) this.textChildPosID);
      this.enemyInfoGroupBox.Controls.Add((Control) this.labelChildPosID);
      this.enemyInfoGroupBox.Controls.Add((Control) this.textChildID);
      this.enemyInfoGroupBox.Controls.Add((Control) this.labelChildID);
      this.enemyInfoGroupBox.Controls.Add((Control) this.linkLabelAI);
      this.enemyInfoGroupBox.Controls.Add((Control) this.textAIID);
      this.enemyInfoGroupBox.Controls.Add((Control) this.labelAIID);
      this.enemyInfoGroupBox.Controls.Add((Control) this.linkLabelChildAI);
      this.enemyInfoGroupBox.Controls.Add((Control) this.textInitHP);
      this.enemyInfoGroupBox.Controls.Add((Control) this.SummaryButton);
      this.enemyInfoGroupBox.Controls.Add((Control) this.textEnglishName);
      this.enemyInfoGroupBox.Controls.Add((Control) this.labelInitialHP);
      this.enemyInfoGroupBox.Controls.Add((Control) this.labelEnglishName);
      this.enemyInfoGroupBox.Controls.Add((Control) this.textID);
      this.enemyInfoGroupBox.Controls.Add((Control) this.groupBoxEnemyAppearances);
      this.enemyInfoGroupBox.Controls.Add((Control) this.textChildAIID);
      this.enemyInfoGroupBox.Controls.Add((Control) this.textLevel);
      this.enemyInfoGroupBox.Controls.Add((Control) this.labelChildAIID);
      this.enemyInfoGroupBox.Controls.Add((Control) this.labelEnemyLevel);
      this.enemyInfoGroupBox.Controls.Add((Control) this.pictureBox1);
      this.enemyInfoGroupBox.Controls.Add((Control) this.groupBoxAIArguments);
      this.enemyInfoGroupBox.Controls.Add((Control) this.labelEnemyID);
      this.enemyInfoGroupBox.Controls.Add((Control) this.labelEnemySelection);
      this.enemyInfoGroupBox.Controls.Add((Control) this.comboBoxEnemySelection);
      this.enemyInfoGroupBox.Location = new Point(6, 6);
      this.enemyInfoGroupBox.Name = "enemyInfoGroupBox";
      this.enemyInfoGroupBox.Size = new Size(829, 254);
      this.enemyInfoGroupBox.TabIndex = 0;
      this.enemyInfoGroupBox.TabStop = false;
      this.enemyInfoGroupBox.Text = "Enemy Info";
      this.textChildPosID.BackColor = Color.White;
      this.textChildPosID.BorderStyle = BorderStyle.None;
      this.textChildPosID.Location = new Point(569, 41);
      this.textChildPosID.Name = "textChildPosID";
      this.textChildPosID.ReadOnly = true;
      this.textChildPosID.Size = new Size(93, 13);
      this.textChildPosID.TabIndex = 23;
      this.labelChildPosID.AutoSize = true;
      this.labelChildPosID.Location = new Point(490, 41);
      this.labelChildPosID.Name = "labelChildPosID";
      this.labelChildPosID.Size = new Size(68, 13);
      this.labelChildPosID.TabIndex = 22;
      this.labelChildPosID.Text = "Child Pos ID:";
      this.textChildID.BackColor = Color.White;
      this.textChildID.BorderStyle = BorderStyle.None;
      this.textChildID.Location = new Point(343, 79);
      this.textChildID.Name = "textChildID";
      this.textChildID.ReadOnly = true;
      this.textChildID.Size = new Size(93, 13);
      this.textChildID.TabIndex = 21;
      this.labelChildID.AutoSize = true;
      this.labelChildID.Location = new Point(262, 79);
      this.labelChildID.Name = "labelChildID";
      this.labelChildID.Size = new Size(56, 13);
      this.labelChildID.TabIndex = 20;
      this.labelChildID.Text = "Enemy ID:";
      this.linkLabelAI.AutoSize = true;
      this.linkLabelAI.LinkColor = Color.Navy;
      this.linkLabelAI.Location = new Point(671, 60);
      this.linkLabelAI.Name = "linkLabelAI";
      this.linkLabelAI.Size = new Size(95, 13);
      this.linkLabelAI.TabIndex = 18;
      this.linkLabelAI.TabStop = true;
      this.linkLabelAI.Text = "View AI in Browser";
      this.linkLabelAI.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabelAI_LinkClicked);
      this.textAIID.BackColor = Color.White;
      this.textAIID.BorderStyle = BorderStyle.None;
      this.textAIID.Location = new Point(569, 60);
      this.textAIID.Name = "textAIID";
      this.textAIID.ReadOnly = true;
      this.textAIID.Size = new Size(93, 13);
      this.textAIID.TabIndex = 19;
      this.labelAIID.AutoSize = true;
      this.labelAIID.Location = new Point(490, 60);
      this.labelAIID.Name = "labelAIID";
      this.labelAIID.Size = new Size(34, 13);
      this.labelAIID.TabIndex = 18;
      this.labelAIID.Text = "AI ID:";
      this.linkLabelChildAI.AutoSize = true;
      this.linkLabelChildAI.LinkColor = Color.Navy;
      this.linkLabelChildAI.Location = new Point(671, 79);
      this.linkLabelChildAI.Name = "linkLabelChildAI";
      this.linkLabelChildAI.Size = new Size(121, 13);
      this.linkLabelChildAI.TabIndex = 17;
      this.linkLabelChildAI.TabStop = true;
      this.linkLabelChildAI.Text = "View Child AI in Browser";
      this.linkLabelChildAI.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabelChildAI_LinkClicked);
      this.textInitHP.BackColor = Color.White;
      this.textInitHP.BorderStyle = BorderStyle.None;
      this.textInitHP.Location = new Point(343, 60);
      this.textInitHP.Name = "textInitHP";
      this.textInitHP.ReadOnly = true;
      this.textInitHP.Size = new Size(121, 13);
      this.textInitHP.TabIndex = 16;
      this.SummaryButton.Anchor = AnchorStyles.Left;
      this.SummaryButton.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, (byte) 0);
      this.SummaryButton.Location = new Point(671, 14);
      this.SummaryButton.Name = "SummaryButton";
      this.SummaryButton.Padding = new Padding(23, 0, 23, 0);
      this.SummaryButton.Size = new Size(151, 43);
      this.SummaryButton.TabIndex = 0;
      this.SummaryButton.Text = "Copy Summary to Clipboard";
      this.toolTip1.SetToolTip((Control) this.SummaryButton, componentResourceManager.GetString("SummaryButton.ToolTip"));
      this.SummaryButton.UseVisualStyleBackColor = true;
      this.SummaryButton.Click += new EventHandler(this.SummaryButton_Click);
      this.textEnglishName.BackColor = Color.White;
      this.textEnglishName.BorderStyle = BorderStyle.None;
      this.textEnglishName.Location = new Point(343, 22);
      this.textEnglishName.Name = "textEnglishName";
      this.textEnglishName.ReadOnly = true;
      this.textEnglishName.Size = new Size(194, 13);
      this.textEnglishName.TabIndex = 14;
      this.labelInitialHP.AutoSize = true;
      this.labelInitialHP.Location = new Point(262, 60);
      this.labelInitialHP.Name = "labelInitialHP";
      this.labelInitialHP.Size = new Size(52, 13);
      this.labelInitialHP.TabIndex = 15;
      this.labelInitialHP.Text = "Initial HP:";
      this.labelEnglishName.AutoSize = true;
      this.labelEnglishName.Location = new Point(262, 22);
      this.labelEnglishName.Name = "labelEnglishName";
      this.labelEnglishName.Size = new Size(75, 13);
      this.labelEnglishName.TabIndex = 14;
      this.labelEnglishName.Text = "English Name:";
      this.toolTip1.SetToolTip((Control) this.labelEnglishName, "Translated using Google Translate");
      this.textID.BackColor = Color.White;
      this.textID.BorderStyle = BorderStyle.None;
      this.textID.Location = new Point(572, 22);
      this.textID.Name = "textID";
      this.textID.ReadOnly = true;
      this.textID.Size = new Size(93, 13);
      this.textID.TabIndex = 12;
      this.textID.Visible = false;
      this.groupBoxEnemyAppearances.Controls.Add((Control) this.listViewEnemyAppearances);
      this.groupBoxEnemyAppearances.Location = new Point(671, 95);
      this.groupBoxEnemyAppearances.Name = "groupBoxEnemyAppearances";
      this.groupBoxEnemyAppearances.Size = new Size(151, 153);
      this.groupBoxEnemyAppearances.TabIndex = 6;
      this.groupBoxEnemyAppearances.TabStop = false;
      this.groupBoxEnemyAppearances.Text = "Appearances";
      this.listViewEnemyAppearances.Columns.AddRange(new ColumnHeader[2]
      {
        this.columnEnemyRounds,
        this.columnEnemyMultiplicity
      });
      this.listViewEnemyAppearances.Dock = DockStyle.Fill;
      this.listViewEnemyAppearances.Location = new Point(3, 16);
      this.listViewEnemyAppearances.Name = "listViewEnemyAppearances";
      this.listViewEnemyAppearances.Size = new Size(145, 134);
      this.listViewEnemyAppearances.TabIndex = 0;
      this.listViewEnemyAppearances.UseCompatibleStateImageBehavior = false;
      this.listViewEnemyAppearances.View = View.Details;
      this.columnEnemyRounds.Text = "Wave";
      this.columnEnemyRounds.Width = 42;
      this.columnEnemyMultiplicity.Text = "Quantity";
      this.columnEnemyMultiplicity.Width = 80;
      this.textChildAIID.BackColor = Color.White;
      this.textChildAIID.BorderStyle = BorderStyle.None;
      this.textChildAIID.Location = new Point(569, 79);
      this.textChildAIID.Name = "textChildAIID";
      this.textChildAIID.ReadOnly = true;
      this.textChildAIID.Size = new Size(93, 13);
      this.textChildAIID.TabIndex = 11;
      this.textLevel.BackColor = Color.White;
      this.textLevel.BorderStyle = BorderStyle.None;
      this.textLevel.Location = new Point(343, 41);
      this.textLevel.Name = "textLevel";
      this.textLevel.ReadOnly = true;
      this.textLevel.Size = new Size(121, 13);
      this.textLevel.TabIndex = 10;
      this.labelChildAIID.AutoSize = true;
      this.labelChildAIID.Location = new Point(490, 79);
      this.labelChildAIID.Name = "labelChildAIID";
      this.labelChildAIID.Size = new Size(60, 13);
      this.labelChildAIID.TabIndex = 1;
      this.labelChildAIID.Text = "Child AI ID:";
      this.labelEnemyLevel.AutoSize = true;
      this.labelEnemyLevel.Location = new Point(262, 41);
      this.labelEnemyLevel.Name = "labelEnemyLevel";
      this.labelEnemyLevel.Size = new Size(36, 13);
      this.labelEnemyLevel.TabIndex = 0;
      this.labelEnemyLevel.Text = "Level:";
      this.pictureBox1.AccessibleRole = AccessibleRole.Cursor;
      this.pictureBox1.Anchor = AnchorStyles.None;
      this.pictureBox1.BorderStyle = BorderStyle.FixedSingle;
      this.pictureBox1.Location = new Point(6, 47);
      this.pictureBox1.Name = "pictureBox1";
      this.pictureBox1.Size = new Size(250, 200);
      this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
      this.pictureBox1.TabIndex = 1;
      this.pictureBox1.TabStop = false;
      this.groupBoxAIArguments.Controls.Add((Control) this.listViewAIArguments);
      this.groupBoxAIArguments.Location = new Point(265, 95);
      this.groupBoxAIArguments.Name = "groupBoxAIArguments";
      this.groupBoxAIArguments.Size = new Size(400, 153);
      this.groupBoxAIArguments.TabIndex = 4;
      this.groupBoxAIArguments.TabStop = false;
      this.groupBoxAIArguments.Text = "AI Arguments";
      this.groupBoxAIArguments.Enter += new EventHandler(this.groupBox1_Enter);
      this.listViewAIArguments.Columns.AddRange(new ColumnHeader[3]
      {
        this.columnAITag,
        this.columnAIArgValue,
        this.columnAIArgType
      });
      this.listViewAIArguments.Dock = DockStyle.Fill;
      this.listViewAIArguments.Location = new Point(3, 16);
      this.listViewAIArguments.Name = "listViewAIArguments";
      this.listViewAIArguments.Size = new Size(394, 134);
      this.listViewAIArguments.TabIndex = 0;
      this.listViewAIArguments.UseCompatibleStateImageBehavior = false;
      this.listViewAIArguments.View = View.Details;
      this.listViewAIArguments.KeyUp += new KeyEventHandler(this.listViewAIArguments_KeyUp);
      this.columnAITag.Text = "Tag";
      this.columnAITag.Width = 168;
      this.columnAIArgValue.Text = "Value";
      this.columnAIArgValue.Width = 41;
      this.columnAIArgType.Text = "Type";
      this.columnAIArgType.Width = 37;
      this.labelEnemyID.AutoSize = true;
      this.labelEnemyID.Location = new Point(506, 22);
      this.labelEnemyID.Name = "labelEnemyID";
      this.labelEnemyID.Size = new Size(56, 13);
      this.labelEnemyID.TabIndex = 2;
      this.labelEnemyID.Text = "Enemy ID:";
      this.labelEnemyID.Visible = false;
      this.labelEnemyID.Click += new EventHandler(this.label2_Click);
      this.labelEnemySelection.AutoSize = true;
      this.labelEnemySelection.Location = new Point(6, 22);
      this.labelEnemySelection.Name = "labelEnemySelection";
      this.labelEnemySelection.Size = new Size(42, 13);
      this.labelEnemySelection.TabIndex = 1;
      this.labelEnemySelection.Text = "Enemy:";
      this.comboBoxEnemySelection.FormattingEnabled = true;
      this.comboBoxEnemySelection.Location = new Point(49, 19);
      this.comboBoxEnemySelection.Name = "comboBoxEnemySelection";
      this.comboBoxEnemySelection.Size = new Size(207, 21);
      this.comboBoxEnemySelection.TabIndex = 0;
      this.comboBoxEnemySelection.SelectedIndexChanged += new EventHandler(this.comboBoxEnemySelection_SelectedIndexChanged);
      this.comboBoxEnemySelection.DropDownClosed += new EventHandler(this.comboBoxEnemySelection_DropDownClosed);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.checkBoxEnumerate);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.checkBoxTranslate);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.checkBoxRawOnly);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.checkBoxCastTimes);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.checkBoxRatesAsFractions);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.buttonToggleAbilityDetails);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.groupBoxAbilityDetails);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.label5);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.textCastTime);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.label4);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.groupBox3);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.label3);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.textEXP);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.textPhaseID);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.textPhaseEnglishName);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.textPhaseDispName);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.label2);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.label1);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.labelDispName);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.groupBox2);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.groupBox1);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.textStatusVuln);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.comboBoxPhaseSelection);
      this.groupBoxEnemyPhase.Controls.Add((Control) this.labelPhaseSelection);
      this.groupBoxEnemyPhase.Location = new Point(6, 266);
      this.groupBoxEnemyPhase.Name = "groupBoxEnemyPhase";
      this.groupBoxEnemyPhase.Size = new Size(991, 531);
      this.groupBoxEnemyPhase.TabIndex = 1;
      this.groupBoxEnemyPhase.TabStop = false;
      this.groupBoxEnemyPhase.Text = "Phase/Form Info";
      this.checkBoxEnumerate.AutoSize = true;
      this.checkBoxEnumerate.Checked = true;
      this.checkBoxEnumerate.CheckState = CheckState.Checked;
      this.checkBoxEnumerate.Location = new Point(499, 168);
      this.checkBoxEnumerate.Name = "checkBoxEnumerate";
      this.checkBoxEnumerate.Size = new Size(147, 17);
      this.checkBoxEnumerate.TabIndex = 39;
      this.checkBoxEnumerate.Text = "Enumerate forced actions";
      this.checkBoxEnumerate.ThreeState = true;
      this.toolTip1.SetToolTip((Control) this.checkBoxEnumerate, "Lists each turn where an action is forced by constraint type 1001 (FORCE_BY_TURN).\r\nIf left indeterminate, actions will be enumerated if there are at least 10 forced actions for this phase.");
      this.checkBoxEnumerate.UseVisualStyleBackColor = true;
      this.checkBoxEnumerate.Click += new EventHandler(this.checkBoxEnumerate_Click);
      this.checkBoxTranslate.AutoSize = true;
      this.checkBoxTranslate.Checked = true;
      this.checkBoxTranslate.CheckState = CheckState.Checked;
      this.checkBoxTranslate.Location = new Point(652, 168);
      this.checkBoxTranslate.Name = "checkBoxTranslate";
      this.checkBoxTranslate.Size = new Size(133, 17);
      this.checkBoxTranslate.TabIndex = 38;
      this.checkBoxTranslate.Text = "Translate ability names";
      this.checkBoxTranslate.UseVisualStyleBackColor = true;
      this.checkBoxTranslate.Click += new EventHandler(this.checkBoxTranslate_Click);
      this.checkBoxRawOnly.AutoSize = true;
      this.checkBoxRawOnly.Location = new Point(190, 168);
      this.checkBoxRawOnly.Name = "checkBoxRawOnly";
      this.checkBoxRawOnly.Size = new Size(129, 17);
      this.checkBoxRawOnly.TabIndex = 37;
      this.checkBoxRawOnly.Text = "Show raw values only";
      this.checkBoxRawOnly.UseVisualStyleBackColor = true;
      this.checkBoxRawOnly.Visible = false;
      this.checkBoxRawOnly.Click += new EventHandler(this.checkBoxRawOnly_Click);
      this.checkBoxCastTimes.AutoSize = true;
      this.checkBoxCastTimes.Checked = true;
      this.checkBoxCastTimes.CheckState = CheckState.Indeterminate;
      this.checkBoxCastTimes.Location = new Point(361, 168);
      this.checkBoxCastTimes.Name = "checkBoxCastTimes";
      this.checkBoxCastTimes.Size = new Size(132, 17);
      this.checkBoxCastTimes.TabIndex = 36;
      this.checkBoxCastTimes.Text = "Show ability cast times";
      this.checkBoxCastTimes.ThreeState = true;
      this.toolTip1.SetToolTip((Control) this.checkBoxCastTimes, "If left indeterminate, cast times will be displayed only if allowed by cast_time_type.");
      this.checkBoxCastTimes.UseVisualStyleBackColor = true;
      this.checkBoxCastTimes.Click += new EventHandler(this.checkBoxCastTimes_Click);
      this.checkBoxRatesAsFractions.AutoSize = true;
      this.checkBoxRatesAsFractions.Location = new Point(190, 168);
      this.checkBoxRatesAsFractions.Name = "checkBoxRatesAsFractions";
      this.checkBoxRatesAsFractions.Size = new Size(165, 17);
      this.checkBoxRatesAsFractions.TabIndex = 1;
      this.checkBoxRatesAsFractions.Text = "Show ability rates as fractions";
      this.checkBoxRatesAsFractions.UseVisualStyleBackColor = true;
      this.checkBoxRatesAsFractions.Click += new EventHandler(this.checkBoxRatesAsFractions_Click);
      this.buttonToggleAbilityDetails.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.buttonToggleAbilityDetails.Location = new Point(6, 164);
      this.buttonToggleAbilityDetails.Name = "buttonToggleAbilityDetails";
      this.buttonToggleAbilityDetails.Size = new Size(173, 23);
      this.buttonToggleAbilityDetails.TabIndex = 27;
      this.buttonToggleAbilityDetails.Text = "View Enemy Ability Summary";
      this.buttonToggleAbilityDetails.UseVisualStyleBackColor = true;
      this.buttonToggleAbilityDetails.Click += new EventHandler(this.buttonToggleAbilityDetails_Click);
      this.groupBoxAbilityDetails.Controls.Add((Control) this.richTextBoxAbilitySummary);
      this.groupBoxAbilityDetails.Controls.Add((Control) this.groupBoxCounters);
      this.groupBoxAbilityDetails.Controls.Add((Control) this.groupBoxEnemyAbilities);
      this.groupBoxAbilityDetails.Controls.Add((Control) this.groupBoxEnemyConstraints);
      this.groupBoxAbilityDetails.Location = new Point(6, 191);
      this.groupBoxAbilityDetails.Name = "groupBoxAbilityDetails";
      this.groupBoxAbilityDetails.Size = new Size(976, 334);
      this.groupBoxAbilityDetails.TabIndex = 2;
      this.groupBoxAbilityDetails.TabStop = false;
      this.groupBoxAbilityDetails.Text = "Enemy Ability Details";
      this.richTextBoxAbilitySummary.Font = new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.richTextBoxAbilitySummary.Location = new Point(6, 18);
      this.richTextBoxAbilitySummary.Name = "richTextBoxAbilitySummary";
      this.richTextBoxAbilitySummary.Size = new Size(964, 309);
      this.richTextBoxAbilitySummary.TabIndex = 26;
      this.richTextBoxAbilitySummary.Text = "";
      this.richTextBoxAbilitySummary.Visible = false;
      this.groupBoxCounters.Controls.Add((Control) this.listViewCounters);
      this.groupBoxCounters.Location = new Point(540, 190);
      this.groupBoxCounters.Name = "groupBoxCounters";
      this.groupBoxCounters.Size = new Size(430, 138);
      this.groupBoxCounters.TabIndex = 27;
      this.groupBoxCounters.TabStop = false;
      this.groupBoxCounters.Text = "Counters";
      this.listViewCounters.Columns.AddRange(new ColumnHeader[5]
      {
        this.columnHeader50,
        this.columnHeader46,
        this.columnHeader47,
        this.columnHeader48,
        this.columnHeader49
      });
      this.listViewCounters.Dock = DockStyle.Fill;
      this.listViewCounters.Location = new Point(3, 16);
      this.listViewCounters.Name = "listViewCounters";
      this.listViewCounters.Size = new Size(424, 119);
      this.listViewCounters.TabIndex = 1;
      this.listViewCounters.UseCompatibleStateImageBehavior = false;
      this.listViewCounters.View = View.Details;
      this.columnHeader50.Text = "Name";
      this.columnHeader46.Text = "Ability ID";
      this.columnHeader46.Width = 80;
      this.columnHeader47.Text = "Rate";
      this.columnHeader47.Width = 50;
      this.columnHeader48.Text = "Condition Value";
      this.columnHeader48.Width = 92;
      this.columnHeader49.Text = "Condition Type";
      this.columnHeader49.Width = 94;
      this.groupBoxEnemyAbilities.Controls.Add((Control) this.listViewEnemyAbilities);
      this.groupBoxEnemyAbilities.Location = new Point(6, 19);
      this.groupBoxEnemyAbilities.Name = "groupBoxEnemyAbilities";
      this.groupBoxEnemyAbilities.Size = new Size(964, 165);
      this.groupBoxEnemyAbilities.TabIndex = 6;
      this.groupBoxEnemyAbilities.TabStop = false;
      this.groupBoxEnemyAbilities.Text = "Abilities";
      this.listViewEnemyAbilities.Columns.AddRange(new ColumnHeader[47]
      {
        this.columnAbilityName,
        this.columnAbilityID,
        this.columnAbilityTag,
        this.columnAbilityWeight,
        this.columnAbilityUnlock,
        this.columnAbilityExercise,
        this.columnAbilityCastTime,
        this.columnAbilityTarRange,
        this.columnAbilityTarMethod,
        this.columnAbilityTarSegment,
        this.columnAbilityActionID,
        this.columnArg1,
        this.columnArg2,
        this.columnArg3,
        this.columnArg4,
        this.columnArg5,
        this.columnArg6,
        this.columnArg7,
        this.columnArg8,
        this.columnArg9,
        this.columnArg10,
        this.columnArg11,
        this.columnArg12,
        this.columnArg13,
        this.columnArg14,
        this.columnArg15,
        this.columnArg16,
        this.columnArg17,
        this.columnArg18,
        this.columnArg19,
        this.columnArg20,
        this.columnArg21,
        this.columnArg22,
        this.columnArg23,
        this.columnArg24,
        this.columnArg25,
        this.columnArg26,
        this.columnArg27,
        this.columnArg28,
        this.columnArg29,
        this.columnArg30,
        this.columnStatusFactor,
        this.columnStatusID,
        this.columnTargetDeath,
        this.columnCounterEnable,
        this.columnMinDamageThresh,
        this.columnMaxDamageThresh
      });
      this.listViewEnemyAbilities.Dock = DockStyle.Fill;
      this.listViewEnemyAbilities.Location = new Point(3, 16);
      this.listViewEnemyAbilities.Name = "listViewEnemyAbilities";
      this.listViewEnemyAbilities.Size = new Size(958, 146);
      this.listViewEnemyAbilities.TabIndex = 0;
      this.listViewEnemyAbilities.UseCompatibleStateImageBehavior = false;
      this.listViewEnemyAbilities.View = View.Details;
      this.columnAbilityName.Text = "Name";
      this.columnAbilityName.Width = 80;
      this.columnAbilityID.Text = "Ability ID";
      this.columnAbilityID.Width = 55;
      this.columnAbilityTag.Text = "Tag";
      this.columnAbilityTag.Width = 54;
      this.columnAbilityWeight.Text = "Weight";
      this.columnAbilityWeight.Width = 50;
      this.columnAbilityUnlock.Text = "Unlock Turn";
      this.columnAbilityUnlock.Width = 72;
      this.columnAbilityExercise.Text = "Exercise Type";
      this.columnAbilityExercise.Width = 80;
      this.columnAbilityCastTime.Text = "Cast Time";
      this.columnAbilityTarRange.Text = "Target Range";
      this.columnAbilityTarRange.Width = 78;
      this.columnAbilityTarMethod.Text = "Target Method";
      this.columnAbilityTarMethod.Width = 83;
      this.columnAbilityTarSegment.Text = "Target Segment";
      this.columnAbilityTarSegment.Width = 89;
      this.columnAbilityActionID.Text = "Action ID";
      this.columnAbilityActionID.Width = 57;
      this.columnArg1.Text = "Arg1";
      this.columnArg1.Width = 36;
      this.columnArg2.Text = "Arg2";
      this.columnArg2.Width = 37;
      this.columnArg3.Text = "Arg3";
      this.columnArg3.Width = 38;
      this.columnArg4.Text = "Arg4";
      this.columnArg4.Width = 39;
      this.columnArg5.Text = "Arg5";
      this.columnArg5.Width = 36;
      this.columnArg6.Text = "Arg6";
      this.columnArg6.Width = 37;
      this.columnArg7.Text = "Arg7";
      this.columnArg7.Width = 35;
      this.columnArg8.Text = "Arg8";
      this.columnArg8.Width = 35;
      this.columnArg9.Text = "Arg9";
      this.columnArg9.Width = 35;
      this.columnArg10.Text = "Arg10";
      this.columnArg10.Width = 40;
      this.columnArg11.Text = "Arg11";
      this.columnArg11.Width = 40;
      this.columnArg12.Text = "Arg12";
      this.columnArg12.Width = 40;
      this.columnArg13.Text = "Arg13";
      this.columnArg13.Width = 43;
      this.columnArg14.Text = "Arg14";
      this.columnArg15.Text = "Arg15";
      this.columnArg16.Text = "Arg16";
      this.columnArg17.Text = "Arg17";
      this.columnArg18.Text = "Arg18";
      this.columnArg19.Text = "Arg19";
      this.columnArg20.Text = "Arg20";
      this.columnArg21.Text = "Arg21";
      this.columnArg22.Text = "Arg22";
      this.columnArg23.Text = "Arg23";
      this.columnArg24.Text = "Arg24";
      this.columnArg25.Text = "Arg25";
      this.columnArg26.Text = "Arg26";
      this.columnArg27.Text = "Arg27";
      this.columnArg28.Text = "Arg28";
      this.columnArg29.Text = "Arg29";
      this.columnArg30.Text = "Arg30";
      this.columnStatusFactor.Text = "Status Factor";
      this.columnStatusID.Text = "Status ID";
      this.columnTargetDeath.Text = "Target Death";
      this.columnCounterEnable.Text = "Counter Enable";
      this.columnMinDamageThresh.Text = "Min Damage Threshold Type";
      this.columnMaxDamageThresh.Text = "Max Damage Threshold Type";
      this.groupBoxEnemyConstraints.Controls.Add((Control) this.listViewEnemyConstraints);
      this.groupBoxEnemyConstraints.Location = new Point(6, 190);
      this.groupBoxEnemyConstraints.Name = "groupBoxEnemyConstraints";
      this.groupBoxEnemyConstraints.Size = new Size(528, 138);
      this.groupBoxEnemyConstraints.TabIndex = 5;
      this.groupBoxEnemyConstraints.TabStop = false;
      this.groupBoxEnemyConstraints.Text = "Constraints";
      this.listViewEnemyConstraints.Columns.AddRange(new ColumnHeader[6]
      {
        this.columnConstraintAbilityTag,
        this.columnConstraintValue,
        this.columnConstraintType,
        this.columnConstraintPriority,
        this.columnConstraintEnemyStatusID,
        this.columnConstraintOptions
      });
      this.listViewEnemyConstraints.Dock = DockStyle.Fill;
      this.listViewEnemyConstraints.Location = new Point(3, 16);
      this.listViewEnemyConstraints.Name = "listViewEnemyConstraints";
      this.listViewEnemyConstraints.Size = new Size(522, 119);
      this.listViewEnemyConstraints.TabIndex = 0;
      this.listViewEnemyConstraints.UseCompatibleStateImageBehavior = false;
      this.listViewEnemyConstraints.View = View.Details;
      this.columnConstraintAbilityTag.Text = "Ability Tag";
      this.columnConstraintAbilityTag.Width = 80;
      this.columnConstraintValue.Text = "Value";
      this.columnConstraintValue.Width = 50;
      this.columnConstraintType.Text = "Type";
      this.columnConstraintType.Width = 54;
      this.columnConstraintPriority.Text = "Priority";
      this.columnConstraintPriority.Width = 48;
      this.columnConstraintEnemyStatusID.Text = "Enemy Status ID";
      this.columnConstraintEnemyStatusID.Width = 96;
      this.columnConstraintOptions.Text = "Options";
      this.label5.AutoSize = true;
      this.label5.Location = new Point(6, 124);
      this.label5.Name = "label5";
      this.label5.Size = new Size(57, 13);
      this.label5.TabIndex = 35;
      this.label5.Text = "Cast Time:";
      this.textCastTime.BackColor = Color.White;
      this.textCastTime.BorderStyle = BorderStyle.None;
      this.textCastTime.Location = new Point(87, 124);
      this.textCastTime.Name = "textCastTime";
      this.textCastTime.ReadOnly = true;
      this.textCastTime.Size = new Size(169, 13);
      this.textCastTime.TabIndex = 34;
      this.label4.AutoSize = true;
      this.label4.Location = new Point(6, 143);
      this.label4.Name = "label4";
      this.label4.Size = new Size(67, 13);
      this.label4.TabIndex = 33;
      this.label4.Text = "Status Vuln.:";
      this.groupBox3.Controls.Add((Control) this.dataGridViewBreaks);
      this.groupBox3.Location = new Point(698, 76);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(287, 64);
      this.groupBox3.TabIndex = 24;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Break Effectiveness";
      this.dataGridViewBreaks.AllowUserToResizeColumns = false;
      this.dataGridViewBreaks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridViewBreaks.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gridViewCellStyle1.BackColor = SystemColors.Control;
      gridViewCellStyle1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      gridViewCellStyle1.ForeColor = SystemColors.WindowText;
      gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
      this.dataGridViewBreaks.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
      this.dataGridViewBreaks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dataGridViewBreaks.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn1, (DataGridViewColumn) this.dataGridViewTextBoxColumn2, (DataGridViewColumn) this.dataGridViewTextBoxColumn3, (DataGridViewColumn) this.dataGridViewTextBoxColumn4, (DataGridViewColumn) this.dataGridViewTextBoxColumn5, (DataGridViewColumn) this.dataGridViewTextBoxColumn6);
      gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gridViewCellStyle2.BackColor = SystemColors.Window;
      gridViewCellStyle2.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      gridViewCellStyle2.ForeColor = SystemColors.ControlText;
      gridViewCellStyle2.SelectionBackColor = SystemColors.Window;
      gridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
      gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
      this.dataGridViewBreaks.DefaultCellStyle = gridViewCellStyle2;
      this.dataGridViewBreaks.Dock = DockStyle.Fill;
      this.dataGridViewBreaks.Location = new Point(3, 16);
      this.dataGridViewBreaks.Name = "dataGridViewBreaks";
      this.dataGridViewBreaks.ReadOnly = true;
      this.dataGridViewBreaks.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
      this.dataGridViewBreaks.RowHeadersVisible = false;
      this.dataGridViewBreaks.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dataGridViewBreaks.ScrollBars = ScrollBars.None;
      this.dataGridViewBreaks.Size = new Size(281, 45);
      this.dataGridViewBreaks.TabIndex = 1;
      this.dataGridViewBreaks.SelectionChanged += new EventHandler(this.dataGridViewBreaks_SelectionChanged);
      this.dataGridViewTextBoxColumn1.HeaderText = "ATK";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.dataGridViewTextBoxColumn2.HeaderText = "DEF";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.ReadOnly = true;
      this.dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.dataGridViewTextBoxColumn3.HeaderText = "MAG";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn3.ReadOnly = true;
      this.dataGridViewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.dataGridViewTextBoxColumn4.HeaderText = "RES";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn4.ReadOnly = true;
      this.dataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.dataGridViewTextBoxColumn5.HeaderText = "MND";
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn5.ReadOnly = true;
      this.dataGridViewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.dataGridViewTextBoxColumn6.HeaderText = "SPD";
      this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      this.dataGridViewTextBoxColumn6.ReadOnly = true;
      this.dataGridViewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.label3.AutoSize = true;
      this.label3.Location = new Point(6, 105);
      this.label3.Name = "label3";
      this.label3.Size = new Size(31, 13);
      this.label3.TabIndex = 32;
      this.label3.Text = "EXP:";
      this.textEXP.BackColor = Color.White;
      this.textEXP.BorderStyle = BorderStyle.None;
      this.textEXP.Location = new Point(87, 105);
      this.textEXP.Name = "textEXP";
      this.textEXP.ReadOnly = true;
      this.textEXP.Size = new Size(169, 13);
      this.textEXP.TabIndex = 31;
      this.textPhaseID.BackColor = Color.White;
      this.textPhaseID.BorderStyle = BorderStyle.None;
      this.textPhaseID.Location = new Point(87, 86);
      this.textPhaseID.Name = "textPhaseID";
      this.textPhaseID.ReadOnly = true;
      this.textPhaseID.Size = new Size(169, 13);
      this.textPhaseID.TabIndex = 30;
      this.textPhaseEnglishName.BackColor = Color.White;
      this.textPhaseEnglishName.BorderStyle = BorderStyle.None;
      this.textPhaseEnglishName.Location = new Point(87, 67);
      this.textPhaseEnglishName.Name = "textPhaseEnglishName";
      this.textPhaseEnglishName.ReadOnly = true;
      this.textPhaseEnglishName.Size = new Size(169, 13);
      this.textPhaseEnglishName.TabIndex = 29;
      this.textPhaseDispName.BackColor = Color.White;
      this.textPhaseDispName.BorderStyle = BorderStyle.None;
      this.textPhaseDispName.Location = new Point(87, 48);
      this.textPhaseDispName.Name = "textPhaseDispName";
      this.textPhaseDispName.ReadOnly = true;
      this.textPhaseDispName.Size = new Size(169, 13);
      this.textPhaseDispName.TabIndex = 22;
      this.label2.AutoSize = true;
      this.label2.Location = new Point(6, 67);
      this.label2.Name = "label2";
      this.label2.Size = new Size(75, 13);
      this.label2.TabIndex = 28;
      this.label2.Text = "English Name:";
      this.label1.AutoSize = true;
      this.label1.Location = new Point(6, 86);
      this.label1.Name = "label1";
      this.label1.Size = new Size(21, 13);
      this.label1.TabIndex = 27;
      this.label1.Text = "ID:";
      this.labelDispName.AutoSize = true;
      this.labelDispName.Location = new Point(6, 48);
      this.labelDispName.Name = "labelDispName";
      this.labelDispName.Size = new Size(75, 13);
      this.labelDispName.TabIndex = 22;
      this.labelDispName.Text = "Display Name:";
      this.groupBox2.Controls.Add((Control) this.dataGridViewStats);
      this.groupBox2.Location = new Point(265, 12);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(720, 64);
      this.groupBox2.TabIndex = 24;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Stats";
      this.dataGridViewStats.AllowUserToResizeColumns = false;
      this.dataGridViewStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridViewStats.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gridViewCellStyle3.BackColor = SystemColors.Control;
      gridViewCellStyle3.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      gridViewCellStyle3.ForeColor = SystemColors.WindowText;
      gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
      this.dataGridViewStats.ColumnHeadersDefaultCellStyle = gridViewCellStyle3;
      this.dataGridViewStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dataGridViewStats.Columns.AddRange((DataGridViewColumn) this.Column17, (DataGridViewColumn) this.Column10, (DataGridViewColumn) this.Column11, (DataGridViewColumn) this.Column12, (DataGridViewColumn) this.Column13, (DataGridViewColumn) this.Column14, (DataGridViewColumn) this.Column15, (DataGridViewColumn) this.Column16, (DataGridViewColumn) this.Column18, (DataGridViewColumn) this.Column20, (DataGridViewColumn) this.Column19);
      gridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gridViewCellStyle4.BackColor = SystemColors.Window;
      gridViewCellStyle4.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      gridViewCellStyle4.ForeColor = SystemColors.ControlText;
      gridViewCellStyle4.SelectionBackColor = SystemColors.Window;
      gridViewCellStyle4.SelectionForeColor = SystemColors.ControlText;
      gridViewCellStyle4.WrapMode = DataGridViewTriState.False;
      this.dataGridViewStats.DefaultCellStyle = gridViewCellStyle4;
      this.dataGridViewStats.Dock = DockStyle.Fill;
      this.dataGridViewStats.Location = new Point(3, 16);
      this.dataGridViewStats.Name = "dataGridViewStats";
      this.dataGridViewStats.ReadOnly = true;
      this.dataGridViewStats.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
      this.dataGridViewStats.RowHeadersVisible = false;
      this.dataGridViewStats.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dataGridViewStats.ScrollBars = ScrollBars.None;
      this.dataGridViewStats.Size = new Size(714, 45);
      this.dataGridViewStats.TabIndex = 1;
      this.dataGridViewStats.SelectionChanged += new EventHandler(this.dataGridViewStats_SelectionChanged);
      this.Column17.HeaderText = "Level";
      this.Column17.Name = "Column17";
      this.Column17.ReadOnly = true;
      this.Column17.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column10.HeaderText = "Max HP";
      this.Column10.Name = "Column10";
      this.Column10.ReadOnly = true;
      this.Column10.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column11.HeaderText = "Attack";
      this.Column11.Name = "Column11";
      this.Column11.ReadOnly = true;
      this.Column11.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column12.HeaderText = "Defense";
      this.Column12.Name = "Column12";
      this.Column12.ReadOnly = true;
      this.Column12.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column13.HeaderText = "Magic";
      this.Column13.Name = "Column13";
      this.Column13.ReadOnly = true;
      this.Column13.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column14.HeaderText = "Resistance";
      this.Column14.Name = "Column14";
      this.Column14.ReadOnly = true;
      this.Column14.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column15.HeaderText = "Mind";
      this.Column15.Name = "Column15";
      this.Column15.ReadOnly = true;
      this.Column15.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column16.HeaderText = "Speed";
      this.Column16.Name = "Column16";
      this.Column16.ReadOnly = true;
      this.Column16.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column18.HeaderText = "Accuracy";
      this.Column18.Name = "Column18";
      this.Column18.ReadOnly = true;
      this.Column18.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column20.HeaderText = "Evade";
      this.Column20.Name = "Column20";
      this.Column20.ReadOnly = true;
      this.Column20.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column19.HeaderText = "Crit";
      this.Column19.Name = "Column19";
      this.Column19.ReadOnly = true;
      this.Column19.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.groupBox1.Controls.Add((Control) this.dataGridViewElemental);
      this.groupBox1.Location = new Point(265, 76);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(427, 64);
      this.groupBox1.TabIndex = 23;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Elemental Damage Taken";
      this.dataGridViewElemental.AllowUserToResizeColumns = false;
      this.dataGridViewElemental.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridViewElemental.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
      gridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gridViewCellStyle5.BackColor = SystemColors.Control;
      gridViewCellStyle5.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      gridViewCellStyle5.ForeColor = SystemColors.WindowText;
      gridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
      gridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
      gridViewCellStyle5.WrapMode = DataGridViewTriState.True;
      this.dataGridViewElemental.ColumnHeadersDefaultCellStyle = gridViewCellStyle5;
      this.dataGridViewElemental.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
      this.dataGridViewElemental.Columns.AddRange((DataGridViewColumn) this.Column1, (DataGridViewColumn) this.Column2, (DataGridViewColumn) this.Column3, (DataGridViewColumn) this.Column4, (DataGridViewColumn) this.Column5, (DataGridViewColumn) this.Column6, (DataGridViewColumn) this.Column7, (DataGridViewColumn) this.Column8, (DataGridViewColumn) this.Column9);
      gridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
      gridViewCellStyle6.BackColor = SystemColors.Window;
      gridViewCellStyle6.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 128);
      gridViewCellStyle6.ForeColor = SystemColors.ControlText;
      gridViewCellStyle6.SelectionBackColor = SystemColors.Window;
      gridViewCellStyle6.SelectionForeColor = SystemColors.ControlText;
      gridViewCellStyle6.WrapMode = DataGridViewTriState.False;
      this.dataGridViewElemental.DefaultCellStyle = gridViewCellStyle6;
      this.dataGridViewElemental.Dock = DockStyle.Fill;
      this.dataGridViewElemental.Location = new Point(3, 16);
      this.dataGridViewElemental.Name = "dataGridViewElemental";
      this.dataGridViewElemental.ReadOnly = true;
      this.dataGridViewElemental.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
      this.dataGridViewElemental.RowHeadersVisible = false;
      this.dataGridViewElemental.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
      this.dataGridViewElemental.ScrollBars = ScrollBars.None;
      this.dataGridViewElemental.Size = new Size(421, 45);
      this.dataGridViewElemental.TabIndex = 1;
      this.dataGridViewElemental.SelectionChanged += new EventHandler(this.dataGridViewElemental_SelectionChanged);
      this.Column1.DataPropertyName = "EnemyFireDef";
      this.Column1.HeaderText = "Fire";
      this.Column1.Name = "Column1";
      this.Column1.ReadOnly = true;
      this.Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column2.DataPropertyName = "EnemyIceDef";
      this.Column2.HeaderText = "Ice";
      this.Column2.Name = "Column2";
      this.Column2.ReadOnly = true;
      this.Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column3.DataPropertyName = "EnemyLitDef";
      this.Column3.HeaderText = "Lit.";
      this.Column3.Name = "Column3";
      this.Column3.ReadOnly = true;
      this.Column3.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column4.DataPropertyName = "EnemyEarthDef";
      this.Column4.HeaderText = "Earth";
      this.Column4.Name = "Column4";
      this.Column4.ReadOnly = true;
      this.Column4.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column5.DataPropertyName = "EnemyWindDef";
      this.Column5.HeaderText = "Wind";
      this.Column5.Name = "Column5";
      this.Column5.ReadOnly = true;
      this.Column5.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column6.DataPropertyName = "EnemyWaterDef";
      this.Column6.HeaderText = "Water";
      this.Column6.Name = "Column6";
      this.Column6.ReadOnly = true;
      this.Column6.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column7.DataPropertyName = "EnemyHolyDef";
      this.Column7.HeaderText = "Holy";
      this.Column7.Name = "Column7";
      this.Column7.ReadOnly = true;
      this.Column7.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column8.DataPropertyName = "EnemyDarkDef";
      this.Column8.HeaderText = "Dark";
      this.Column8.Name = "Column8";
      this.Column8.ReadOnly = true;
      this.Column8.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.Column9.DataPropertyName = "EnemyBioDef";
      this.Column9.HeaderText = "Bio";
      this.Column9.Name = "Column9";
      this.Column9.ReadOnly = true;
      this.Column9.SortMode = DataGridViewColumnSortMode.NotSortable;
      this.textStatusVuln.BackColor = Color.White;
      this.textStatusVuln.BorderStyle = BorderStyle.None;
      this.textStatusVuln.Location = new Point(87, 143);
      this.textStatusVuln.Name = "textStatusVuln";
      this.textStatusVuln.ReadOnly = true;
      this.textStatusVuln.Size = new Size(895, 13);
      this.textStatusVuln.TabIndex = 22;
      this.comboBoxPhaseSelection.FormattingEnabled = true;
      this.comboBoxPhaseSelection.Location = new Point(52, 19);
      this.comboBoxPhaseSelection.Name = "comboBoxPhaseSelection";
      this.comboBoxPhaseSelection.Size = new Size(204, 21);
      this.comboBoxPhaseSelection.TabIndex = 22;
      this.comboBoxPhaseSelection.SelectedIndexChanged += new EventHandler(this.comboBoxPhaseSelection_SelectedIndexChanged);
      this.comboBoxPhaseSelection.DropDownClosed += new EventHandler(this.comboBoxPhaseSelection_DropDownClosed);
      this.labelPhaseSelection.AutoSize = true;
      this.labelPhaseSelection.Location = new Point(6, 22);
      this.labelPhaseSelection.Name = "labelPhaseSelection";
      this.labelPhaseSelection.Size = new Size(40, 13);
      this.labelPhaseSelection.TabIndex = 22;
      this.labelPhaseSelection.Text = "Phase:";
      this.toolTip1.AutomaticDelay = 300;
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.groupBoxEnemyPhase);
      this.Controls.Add((Control) this.enemyInfoGroupBox);
      this.Name = nameof (FFRKViewEnemyDetails);
      this.Size = new Size(1000, 800);
      this.Load += new EventHandler(this.FFRKViewEnemyDetails_Load);
      this.enemyInfoGroupBox.ResumeLayout(false);
      this.enemyInfoGroupBox.PerformLayout();
      this.groupBoxEnemyAppearances.ResumeLayout(false);
      ((ISupportInitialize) this.pictureBox1).EndInit();
      this.groupBoxAIArguments.ResumeLayout(false);
      this.groupBoxEnemyPhase.ResumeLayout(false);
      this.groupBoxEnemyPhase.PerformLayout();
      this.groupBoxAbilityDetails.ResumeLayout(false);
      this.groupBoxCounters.ResumeLayout(false);
      this.groupBoxEnemyAbilities.ResumeLayout(false);
      this.groupBoxEnemyConstraints.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      ((ISupportInitialize) this.dataGridViewBreaks).EndInit();
      this.groupBox2.ResumeLayout(false);
      ((ISupportInitialize) this.dataGridViewStats).EndInit();
      this.groupBox1.ResumeLayout(false);
      ((ISupportInitialize) this.dataGridViewElemental).EndInit();
      this.ResumeLayout(false);
    }
  }
}
