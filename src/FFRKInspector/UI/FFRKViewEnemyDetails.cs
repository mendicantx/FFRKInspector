// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.FFRKViewEnemyDetails
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using FFRKInspector.Functionality;
using FFRKInspector.GameData;
using FFRKInspector.GameData.Converters;
using FFRKInspector.Proxy;
using Fiddler;

namespace FFRKInspector.UI
{
    public class FFRKViewEnemyDetails : UserControl
    {
        private Button buttonToggleAbilityDetails;
        private CheckBox checkBoxCastTimes;
        private CheckBox checkBoxEnumerate;
        private CheckBox checkBoxRatesAsFractions;
        private CheckBox checkBoxRawOnly;
        private CheckBox checkBoxTranslate;
        private DataGridViewTextBoxColumn Column1;
        private DataGridViewTextBoxColumn Column10;
        private DataGridViewTextBoxColumn Column11;
        private DataGridViewTextBoxColumn Column12;
        private DataGridViewTextBoxColumn Column13;
        private DataGridViewTextBoxColumn Column14;
        private DataGridViewTextBoxColumn Column15;
        private DataGridViewTextBoxColumn Column16;
        private DataGridViewTextBoxColumn Column17;
        private DataGridViewTextBoxColumn Column18;
        private DataGridViewTextBoxColumn Column19;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewTextBoxColumn Column20;
        private DataGridViewTextBoxColumn Column3;
        private DataGridViewTextBoxColumn Column4;
        private DataGridViewTextBoxColumn Column5;
        private DataGridViewTextBoxColumn Column6;
        private DataGridViewTextBoxColumn Column7;
        private DataGridViewTextBoxColumn Column8;
        private DataGridViewTextBoxColumn Column9;
        private ColumnHeader columnAbilityActionID;
        private ColumnHeader columnAbilityCastTime;
        private ColumnHeader columnAbilityExercise;
        private ColumnHeader columnAbilityID;
        private ColumnHeader columnAbilityName;
        private ColumnHeader columnAbilityTag;
        private ColumnHeader columnAbilityTarMethod;
        private ColumnHeader columnAbilityTarRange;
        private ColumnHeader columnAbilityTarSegment;
        private ColumnHeader columnAbilityUnlock;
        private ColumnHeader columnAbilityWeight;
        private ColumnHeader columnAIArgType;
        private ColumnHeader columnAIArgValue;
        private ColumnHeader columnAITag;
        private ColumnHeader columnArg1;
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
        private ColumnHeader columnArg2;
        private ColumnHeader columnArg20;
        private ColumnHeader columnArg21;
        private ColumnHeader columnArg22;
        private ColumnHeader columnArg23;
        private ColumnHeader columnArg24;
        private ColumnHeader columnArg25;
        private ColumnHeader columnArg26;
        private ColumnHeader columnArg27;
        private ColumnHeader columnArg28;
        private ColumnHeader columnArg29;
        private ColumnHeader columnArg3;
        private ColumnHeader columnArg30;
        private ColumnHeader columnArg4;
        private ColumnHeader columnArg5;
        private ColumnHeader columnArg6;
        private ColumnHeader columnArg7;
        private ColumnHeader columnArg8;
        private ColumnHeader columnArg9;
        private ColumnHeader columnConstraintAbilityTag;
        private ColumnHeader columnConstraintEnemyStatusID;
        private ColumnHeader columnConstraintOptions;
        private ColumnHeader columnConstraintPriority;
        private ColumnHeader columnConstraintType;
        private ColumnHeader columnConstraintValue;
        private ColumnHeader columnCounterEnable;
        private ColumnHeader columnEnemyMultiplicity;
        private ColumnHeader columnEnemyRounds;
        private ColumnHeader columnHeader46;
        private ColumnHeader columnHeader47;
        private ColumnHeader columnHeader48;
        private ColumnHeader columnHeader49;
        private ColumnHeader columnHeader50;
        private ColumnHeader columnMaxDamageThresh;
        private ColumnHeader columnMinDamageThresh;
        private ColumnHeader columnStatusFactor;
        private ColumnHeader columnStatusID;
        private ColumnHeader columnTargetDeath;
        private ComboBox comboBoxEnemySelection;
        private ComboBox comboBoxPhaseSelection;
        private IContainer components;
        private DataGridView dataGridViewBreaks;
        private DataGridView dataGridViewElemental;
        private DataGridView dataGridViewStats;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private GroupBox enemyInfoGroupBox;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private GroupBox groupBox3;
        private GroupBox groupBoxAbilityDetails;
        private GroupBox groupBoxAIArguments;
        private GroupBox groupBoxCounters;
        private GroupBox groupBoxEnemyAbilities;
        private GroupBox groupBoxEnemyAppearances;
        private GroupBox groupBoxEnemyConstraints;
        private GroupBox groupBoxEnemyPhase;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label labelAIID;
        private Label labelChildAIID;
        private Label labelChildID;
        private Label labelChildPosID;
        private Label labelDispName;
        private Label labelEnemyID;
        private Label labelEnemyLevel;
        private Label labelEnemySelection;
        private Label labelEnglishName;
        private Label labelInitialHP;
        private Label labelPhaseSelection;
        private LinkLabel linkLabelAI;
        private LinkLabel linkLabelChildAI;
        private ListView listViewAIArguments;
        private ListView listViewCounters;
        private ListView listViewEnemyAbilities;
        private ListView listViewEnemyAppearances;
        private ListView listViewEnemyConstraints;
        private PictureBox pictureBox1;
        private RichTextBox richTextBoxAbilitySummary;
        private bool showAbilitySummary;
        private Button SummaryButton;
        private TextBox textAIID;
        private TextBox textCastTime;
        private TextBox textChildAIID;
        private TextBox textChildID;
        private TextBox textChildPosID;
        private TextBox textEnglishName;
        private TextBox textEXP;
        private TextBox textID;
        private TextBox textInitHP;
        private TextBox textLevel;
        private TextBox textPhaseDispName;
        private TextBox textPhaseEnglishName;
        private TextBox textPhaseID;
        private TextBox textStatusVuln;
        private ToolTip toolTip1;

        public FFRKViewEnemyDetails()
        {
            showAbilitySummary = false;
            InitializeComponent();
            groupBoxEnemyAbilities.Visible = true;
            groupBoxCounters.Visible = true;
            groupBoxEnemyConstraints.Visible = true;
            richTextBoxAbilitySummary.Visible = false;
            checkBoxRatesAsFractions.Visible = false;
            checkBoxRatesAsFractions.Checked = false;
            checkBoxCastTimes.Visible = false;
            checkBoxRawOnly.Visible = true;
            checkBoxTranslate.Visible = false;
            checkBoxEnumerate.Visible = false;
            buttonToggleAbilityDetails.Text = "Show Enemy Ability Summary";
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
            if (DesignMode)
                return;
            if (FFRKProxy.Instance != null)
            {
                FFRKProxy.Instance.OnBattleEngaged += FFRKProxy_OnBattleEngaged;
                FFRKProxy.Instance.OnListBattles += FFRKProxy_OnListBattles;
                FFRKProxy.Instance.OnListDungeons += FFRKProxy_OnListDungeons;
                FFRKProxy.Instance.OnLeaveDungeon += FFRKProxy_OnLeaveDungeon;
                FFRKProxy.Instance.OnCompleteBattle += FFRKProxy_OnCompleteBattle;
                PopulateEnemySelectionComboBox(FFRKProxy.Instance.GameState.ActiveBattle);
            }
            else
            {
                PopulateEnemySelectionComboBox(null);
            }
        }

        private void PopulateEnemySelectionComboBox(EventBattleInitiated battle)
        {
            comboBoxEnemySelection.SelectedIndex = -1;
            comboBoxEnemySelection.Items.Clear();
            if (battle == null)
                return;
            IEnumerable<BasicEnemyParentInfo> source = battle.Battle.EnemyParents.OrderBy(x => x.ChildPosId);
            if (source.ToList().Count == 0)
                return;
            lock (FFRKProxy.Instance.Cache.SyncRoot)
            {
                foreach (object obj in source)
                    comboBoxEnemySelection.Items.Add(obj);
            }

            if (comboBoxEnemySelection.SelectedIndex != -1)
                return;
            comboBoxEnemySelection.SelectedIndex = 0;
        }

        private void PopulatePhaseSelectionComboBox(IEnumerable<BasicEnemyInfo> myPhases)
        {
            comboBoxPhaseSelection.SelectedIndex = -1;
            comboBoxPhaseSelection.Items.Clear();
            if (myPhases == null || myPhases.ToList().Count == 0)
                return;
            lock (FFRKProxy.Instance.Cache.SyncRoot)
            {
                foreach (object myPhase in myPhases)
                    comboBoxPhaseSelection.Items.Add(myPhase);
            }

            if (comboBoxPhaseSelection.SelectedIndex != -1)
                return;
            comboBoxPhaseSelection.SelectedIndex = 0;
        }

        private void PopulateEnemyInfoGroupBox(EventBattleInitiated battle)
        {
            textID.Text = "";
            textChildID.Text = "";
            textAIID.Text = "";
            textChildAIID.Text = "";
            textLevel.Text = "";
            textChildPosID.Text = "";
            textInitHP.Text = "";
            textEnglishName.Text = "";
            pictureBox1.Image = null;
            richTextBoxAbilitySummary.Text = "";
            linkLabelAI.Links.Clear();
            linkLabelChildAI.Links.Clear();
            listViewAIArguments.Items.Clear();
            listViewEnemyAppearances.Items.Clear();
            if (battle == null || comboBoxEnemySelection.Items.Count == 0)
                return;
            var selectedItem = (BasicEnemyParentInfo) comboBoxEnemySelection.SelectedItem;
            if (selectedItem == null)
                return;
            lock (FFRKProxy.Instance.Cache.SyncRoot)
            {
                textChildID.Text = selectedItem.EnemyId.ToString();
                textID.Text = selectedItem.Id.ToString();
                textAIID.Text = selectedItem.ParentAiId.ToString();
                textChildAIID.Text = selectedItem.AiId.ToString();
                textLevel.Text = selectedItem.Level.ToString();
                textInitHP.Text = selectedItem.EnemyInitHp.ToString("N0");
                textChildPosID.Text = selectedItem.ChildPosId.ToString();
                textEnglishName.Text = new Translator().Translate(selectedItem.ToString());
                if (selectedItem.ParentAiId > 0UL)
                    linkLabelAI.Links.Add(0, linkLabelAI.Text.Length,
                        string.Format("https://dff.sp.mbga.jp/dff/static/js/direct/battle/ai/conf/{0}.js",
                            selectedItem.ParentAiId));
                if (selectedItem.AiId > 0UL)
                    linkLabelChildAI.Links.Add(0, linkLabelChildAI.Text.Length,
                        string.Format("https://dff.sp.mbga.jp/dff/static/js/direct/battle/ai/conf/{0}.js",
                            selectedItem.AiId));
                var flag = false;
                try
                {
                    using (var response = WebRequest
                        .Create(string.Format("https://dff.sp.mbga.jp/dff/static/lang/image/enemy/{0}.png",
                            selectedItem.EnemyId)).GetResponse())
                    {
                        using (var responseStream = response.GetResponseStream())
                        {
                            pictureBox1.Image = Image.FromStream(responseStream);
                        }
                    }
                }
                catch (WebException ex)
                {
                    flag = true;
                }

                if (flag)
                    try
                    {
                        using (var response = WebRequest
                            .Create(string.Format(
                                "https://dff.sp.mbga.jp/dff/static/lang/ab/character/enemy/img_{0}.png",
                                selectedItem.BackupImgId)).GetResponse())
                        {
                            using (var responseStream = response.GetResponseStream())
                            {
                                pictureBox1.Image = Image.FromStream(responseStream);
                            }
                        }
                    }
                    catch (WebException ex)
                    {
                        using (var response = WebRequest.Create("https://i.imgur.com/PR9Uric.png").GetResponse())
                        {
                            using (var responseStream = response.GetResponseStream())
                            {
                                pictureBox1.Image = Image.FromStream(responseStream);
                            }
                        }
                    }

                foreach (var dataAiArgs in selectedItem.AiArgs.OrderBy(x => x.Tag).ToList())
                {
                    var name = Enum.GetName(typeof(SchemaConstants.AiArgType), dataAiArgs.ArgType);
                    listViewAIArguments.Items.Add(new ListViewItem(new string[3]
                    {
                        dataAiArgs.Tag,
                        dataAiArgs.ArgValue,
                        name
                    }));
                }

                foreach (ColumnHeader column in listViewAIArguments.Columns)
                    column.Width = -2;
                for (uint index = 0; index < selectedItem.Appearances.Length; ++index)
                    if (selectedItem.Appearances[(int) index] > 0U)
                        listViewEnemyAppearances.Items.Add(new ListViewItem(new string[2]
                        {
                            (index + 1U).ToString(),
                            selectedItem.Appearances[(int) index].ToString()
                        }));
                foreach (ColumnHeader column in listViewEnemyAppearances.Columns)
                    column.Width = -2;
            }
        }

        private void PopulatePhaseInfoGroupBox(BasicEnemyInfo enemy)
        {
            textPhaseDispName.Text = "";
            textPhaseEnglishName.Text = "";
            textPhaseID.Text = "";
            textEXP.Text = "";
            textCastTime.Text = "";
            textStatusVuln.Text = "";
            listViewEnemyConstraints.Items.Clear();
            listViewEnemyAbilities.Items.Clear();
            listViewCounters.Items.Clear();
            richTextBoxAbilitySummary.Text = "";
            foreach (DataGridViewRow row in dataGridViewElemental.Rows)
                if (!row.IsNewRow)
                    dataGridViewElemental.Rows.Remove(row);
            foreach (DataGridViewRow row in dataGridViewBreaks.Rows)
                if (!row.IsNewRow)
                    dataGridViewBreaks.Rows.Remove(row);
            foreach (DataGridViewRow row in dataGridViewStats.Rows)
                if (!row.IsNewRow)
                    dataGridViewStats.Rows.Remove(row);
            if (enemy == null || FFRKProxy.Instance.GameState.ActiveBattle == null ||
                comboBoxEnemySelection.SelectedItem == null)
                return;
            var activeBattle = FFRKProxy.Instance.GameState.ActiveBattle;
            var selectedItem = (BasicEnemyParentInfo) comboBoxEnemySelection.SelectedItem;
            textPhaseDispName.Text = enemy.EnemyName;
            textPhaseEnglishName.Text = new Translator().Translate(enemy.EnemyName);
            textPhaseID.Text = enemy.EnemyId.ToString();
            textEXP.Text = enemy.EnemyExp.ToString("N0");
            textCastTime.Text = enemy.EnemyCastTime;
            textStatusVuln.Text = string.Join(", ", new List<string>
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
            }.Except(enemy.EnemyStatusImmunity).ToList().ToArray()).Replace('_', ' ');
            if (textStatusVuln.Text == "")
                textStatusVuln.Text = "None";
            dataGridViewElemental.Rows.Add(enemy.EnemyFireDef, enemy.EnemyIceDef, enemy.EnemyLitDef,
                enemy.EnemyEarthDef, enemy.EnemyWindDef, enemy.EnemyWaterDef, enemy.EnemyHolyDef, enemy.EnemyDarkDef,
                enemy.EnemyBioDef);
            for (var index = 0; index < dataGridViewElemental.ColumnCount; ++index)
            {
                var str = dataGridViewElemental[index, 0].Value.ToString();
                int result;
                if (int.TryParse(str.Substring(0, str.IndexOf('%')), out result) && result > 100)
                    dataGridViewElemental.Rows[0].Cells[index].Style.ForeColor = Color.Red;
            }

            dataGridViewBreaks.Rows.Add(enemy.EnemyAtkBrkDef, enemy.EnemyDefBrkDef, enemy.EnemyMagBrkDef,
                enemy.EnemyResBrkDef, enemy.EnemyMndBrkDef, enemy.EnemySpdBrkDef);
            dataGridViewStats.Rows.Add(enemy.EnemyLv, enemy.EnemyMaxHp.ToString("N0"), enemy.EnemyAtk.ToString("N0"),
                enemy.EnemyDef.ToString("N0"), enemy.EnemyMag.ToString("N0"), enemy.EnemyRes.ToString("N0"),
                enemy.EnemyMnd, enemy.EnemySpd, enemy.EnemyAcc, enemy.EnemyEva, enemy.EnemyCrit);
            var flag1 = checkBoxRawOnly.Checked;
            foreach (var constraint in selectedItem.Constraints)
                if (constraint.EnemyStatusId == 0U || (int) constraint.EnemyStatusId == (int) enemy.EnemyId)
                    listViewEnemyConstraints.Items.Add(new ListViewItem(new string[6]
                    {
                        constraint.AbilityTag,
                        constraint.ConstraintValue,
                        flag1
                            ? constraint.ConstraintType.ToString()
                            : Enum.GetName(typeof(SchemaConstants.EnemyConstraintType), constraint.ConstraintType),
                        constraint.Priority.ToString(),
                        constraint.EnemyStatusId.ToString(),
                        constraint.Options
                    }));
            foreach (ColumnHeader column in listViewEnemyConstraints.Columns)
                column.Width = -2;
            foreach (var enemyAbility in enemy.EnemyAbilities)
            {
                var ability = activeBattle.Battle.getAbility(enemyAbility.AbilityId);
                var options = ability.Options;
                listViewEnemyAbilities.Items.Add(new ListViewItem(new string[47]
                {
                    ability.name,
                    enemyAbility.AbilityId.ToString(),
                    enemyAbility.Tag,
                    enemyAbility.Weight.ToString(),
                    enemyAbility.UnlockTurn.ToString(),
                    flag1
                        ? ability.ExerciseType.ToString()
                        : Enum.GetName(typeof(SchemaConstants.ExerciseAbbr), ability.ExerciseType),
                    options.CastTime.ToString(),
                    flag1
                        ? options.TargetRange.ToString()
                        : Enum.GetName(typeof(SchemaConstants.TargetRange), options.TargetRange),
                    flag1
                        ? options.TargetMethod.ToString()
                        : Enum.GetName(typeof(SchemaConstants.TargetMethod), options.TargetMethod),
                    flag1
                        ? options.TargetSegment.ToString()
                        : Enum.GetName(typeof(SchemaConstants.TargetSegment), options.TargetSegment),
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
                    flag1
                        ? options.TargetDeath.ToString()
                        : Enum.GetName(typeof(SchemaConstants.TargetDeath), options.TargetDeath),
                    options.CounterEnable.ToString(),
                    flag1
                        ? options.MinDamageThreshold.ToString()
                        : Enum.GetName(typeof(SchemaConstants.DamageThresholdType), options.MinDamageThreshold),
                    flag1
                        ? options.MaxDamageThreshold.ToString()
                        : Enum.GetName(typeof(SchemaConstants.DamageThresholdType), options.MaxDamageThreshold)
                }));
            }

            foreach (var enemyCounter in enemy.EnemyCounters)
            {
                listViewCounters.Items.Add(new ListViewItem(new string[5]
                {
                    activeBattle.Battle.getAbility(enemyCounter.AbilityId).name,
                    enemyCounter.AbilityId.ToString(),
                    enemyCounter.Rate.ToString(),
                    enemyCounter.CondValue.ToString(),
                    flag1
                        ? enemyCounter.CondType.ToString()
                        : Enum.GetName(typeof(SchemaConstants.CounterConditionType), enemyCounter.CondType)
                }));
                var flag2 = false;
                foreach (ListViewItem listViewItem in listViewEnemyAbilities.Items)
                    if (int.Parse(listViewItem.SubItems[1].Text) == enemyCounter.AbilityId)
                    {
                        flag2 = true;
                        break;
                    }

                if (!flag2)
                {
                    var ability = activeBattle.Battle.getAbility(enemyCounter.AbilityId);
                    var options = ability.Options;
                    listViewEnemyAbilities.Items.Add(new ListViewItem(new string[47]
                    {
                        ability.name,
                        enemyCounter.AbilityId.ToString(),
                        "",
                        "0",
                        "0",
                        flag1
                            ? ability.ExerciseType.ToString()
                            : Enum.GetName(typeof(SchemaConstants.ExerciseAbbr), ability.ExerciseType),
                        options.CastTime.ToString(),
                        flag1
                            ? options.TargetRange.ToString()
                            : Enum.GetName(typeof(SchemaConstants.TargetRange), options.TargetRange),
                        flag1
                            ? options.TargetMethod.ToString()
                            : Enum.GetName(typeof(SchemaConstants.TargetMethod), options.TargetMethod),
                        flag1
                            ? options.TargetSegment.ToString()
                            : Enum.GetName(typeof(SchemaConstants.TargetSegment), options.TargetSegment),
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
                        flag1
                            ? options.TargetDeath.ToString()
                            : Enum.GetName(typeof(SchemaConstants.TargetDeath), options.TargetDeath),
                        options.CounterEnable.ToString(),
                        flag1
                            ? options.MinDamageThreshold.ToString()
                            : Enum.GetName(typeof(SchemaConstants.DamageThresholdType), options.MinDamageThreshold),
                        flag1
                            ? options.MaxDamageThreshold.ToString()
                            : Enum.GetName(typeof(SchemaConstants.DamageThresholdType), options.MaxDamageThreshold)
                    }));
                }
            }

            foreach (ColumnHeader column in listViewEnemyAbilities.Columns)
                column.Width = -2;
            foreach (ColumnHeader column in listViewCounters.Columns)
                column.Width = -2;
            PopulateAbilitySummary(enemy, selectedItem, activeBattle);
        }

        private void PopulateAbilitySummary(
            BasicEnemyInfo enemy,
            BasicEnemyParentInfo myEnemyParent,
            EventBattleInitiated battle)
        {
            var abilitySummaryHelper = new AbilitySummaryHelper();
            richTextBoxAbilitySummary.Text = "";

            var abilitySummary = abilitySummaryHelper.GetAbilitySummary(enemy, myEnemyParent, battle, comboBoxEnemySelection, checkBoxCastTimes, checkBoxEnumerate, checkBoxRatesAsFractions, checkBoxTranslate);
            foreach (var ch in abilitySummary)
            {
                richTextBoxAbilitySummary.SelectionFont = richTextBoxAbilitySummary.Font;
                richTextBoxAbilitySummary.AppendText(ch.ToString());
            }
        }

        

        private void CopySelectedItemsToClipboard(ListView myListView, string[] columnNames)
        {
            var stringList1 = new List<string>();
            foreach (ListViewItem selectedItem in myListView.SelectedItems)
            {
                var count = selectedItem.SubItems.Count;
                var stringList2 = new List<string>();
                for (var index = 0; index < count; ++index)
                {
                    var columnName = columnNames[index];
                    var text = selectedItem.SubItems[index].Text;
                    stringList2.Add(string.Format("{0}: {1}", columnName, text));
                }

                stringList1.Add(string.Join(", ", stringList2));
            }

            Clipboard.SetText(string.Join("  \n", stringList1));
        }

        private void FFRKProxy_OnCompleteBattle(EventBattleInitiated battle)
        {
            BeginInvoke((Action) (() => PopulateEnemySelectionComboBox(null)));
        }

        private void FFRKProxy_OnLeaveDungeon()
        {
            BeginInvoke((Action) (() => PopulateEnemySelectionComboBox(null)));
        }

        private void FFRKProxy_OnListBattles(EventListBattles battles)
        {
        }

        private void FFRKProxy_OnListDungeons(EventListDungeons dungeons)
        {
            BeginInvoke((Action) (() => PopulateEnemySelectionComboBox(null)));
        }

        private void FFRKProxy_OnBattleEngaged(EventBattleInitiated battle)
        {
            BeginInvoke((Action) (() => PopulateEnemySelectionComboBox(battle)));
        }

        private void comboBoxEnemySelection_DropDownClosed(object sender, EventArgs e)
        {
            BeginInvoke((Action) (() => comboBoxEnemySelection.Select(0, 0)));
        }

        private void comboBoxEnemySelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateEnemyInfoGroupBox(FFRKProxy.Instance.GameState.ActiveBattle);
            if (comboBoxEnemySelection.SelectedIndex == -1 || comboBoxEnemySelection.Items.Count == 0)
                PopulatePhaseSelectionComboBox(null);
            else
                PopulatePhaseSelectionComboBox(((BasicEnemyParentInfo) comboBoxEnemySelection.SelectedItem).Phases);
        }

        private void linkLabelAI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        private void linkLabelChildAI_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start(e.Link.LinkData.ToString());
        }

        private void comboBoxPhaseSelection_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPhaseSelection.SelectedIndex == -1)
                PopulatePhaseInfoGroupBox(null);
            else
                PopulatePhaseInfoGroupBox((BasicEnemyInfo) comboBoxPhaseSelection.SelectedItem);
        }

        private void comboBoxPhaseSelection_DropDownClosed(object sender, EventArgs e)
        {
            BeginInvoke((Action) (() => comboBoxPhaseSelection.Select(0, 0)));
        }

        private void dataGridViewElemental_SelectionChanged(object sender, EventArgs e)
        {
            dataGridViewElemental.ClearSelection();
        }

        private void dataGridViewBreaks_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void dataGridViewStats_SelectionChanged(object sender, EventArgs e)
        {
        }

        private void buttonToggleAbilityDetails_Click(object sender, EventArgs e)
        {
            if (showAbilitySummary)
            {
                showAbilitySummary = false;
                richTextBoxAbilitySummary.Visible = false;
                checkBoxRatesAsFractions.Visible = false;
                checkBoxCastTimes.Visible = false;
                checkBoxTranslate.Visible = false;
                checkBoxEnumerate.Visible = false;
                checkBoxRawOnly.Visible = true;
                groupBoxEnemyAbilities.Visible = true;
                groupBoxCounters.Visible = true;
                groupBoxEnemyConstraints.Visible = true;
                buttonToggleAbilityDetails.Text = "Show Enemy Ability Summary";
            }
            else
            {
                showAbilitySummary = true;
                groupBoxEnemyAbilities.Visible = false;
                groupBoxCounters.Visible = false;
                groupBoxEnemyConstraints.Visible = false;
                richTextBoxAbilitySummary.Visible = true;
                checkBoxRawOnly.Visible = false;
                checkBoxCastTimes.Visible = true;
                checkBoxRatesAsFractions.Visible = true;
                checkBoxTranslate.Visible = true;
                checkBoxEnumerate.Visible = true;
                buttonToggleAbilityDetails.Text = "Show Enemy Ability Data";
            }

            Focus();
        }

        private void checkBoxCastTimes_Click(object sender, EventArgs e)
        {
            if (comboBoxEnemySelection.SelectedIndex == -1 || comboBoxEnemySelection.Items.Count == 0 ||
                comboBoxPhaseSelection.SelectedIndex == -1 || comboBoxPhaseSelection.Items.Count == 0 ||
                FFRKProxy.Instance == null)
                PopulateAbilitySummary(null, null, null);
            else
                PopulateAbilitySummary((BasicEnemyInfo) comboBoxPhaseSelection.SelectedItem,
                    (BasicEnemyParentInfo) comboBoxEnemySelection.SelectedItem,
                    FFRKProxy.Instance.GameState.ActiveBattle);
        }

        private void checkBoxRatesAsFractions_Click(object sender, EventArgs e)
        {
            if (comboBoxEnemySelection.SelectedIndex == -1 || comboBoxEnemySelection.Items.Count == 0 ||
                comboBoxPhaseSelection.SelectedIndex == -1 || comboBoxPhaseSelection.Items.Count == 0 ||
                FFRKProxy.Instance == null)
                PopulateAbilitySummary(null, null, null);
            else
                PopulateAbilitySummary((BasicEnemyInfo) comboBoxPhaseSelection.SelectedItem,
                    (BasicEnemyParentInfo) comboBoxEnemySelection.SelectedItem,
                    FFRKProxy.Instance.GameState.ActiveBattle);
        }

        private void checkBoxRawOnly_Click(object sender, EventArgs e)
        {
            if (comboBoxPhaseSelection.SelectedIndex == -1 || comboBoxPhaseSelection.Items.Count == 0)
                PopulatePhaseInfoGroupBox(null);
            else
                PopulatePhaseInfoGroupBox((BasicEnemyInfo) comboBoxPhaseSelection.SelectedItem);
        }

        private void listViewAIArguments_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender != listViewAIArguments || !e.Control || e.KeyCode != Keys.C)
                return;
            CopySelectedItemsToClipboard(listViewAIArguments, new string[3]
            {
                "Tag",
                "Value",
                "Type"
            });
        }

        private void checkBoxTranslate_Click(object sender, EventArgs e)
        {
            if (comboBoxEnemySelection.SelectedIndex == -1 || comboBoxEnemySelection.Items.Count == 0 ||
                comboBoxPhaseSelection.SelectedIndex == -1 || comboBoxPhaseSelection.Items.Count == 0 ||
                FFRKProxy.Instance == null)
                PopulateAbilitySummary(null, null, null);
            else
                PopulateAbilitySummary((BasicEnemyInfo) comboBoxPhaseSelection.SelectedItem,
                    (BasicEnemyParentInfo) comboBoxEnemySelection.SelectedItem,
                    FFRKProxy.Instance.GameState.ActiveBattle);
        }

        private void SummaryButton_Click(object sender, EventArgs e)
        {
            var summaryGenerator = new SummaryInformationGenerator();
            Clipboard.SetText(summaryGenerator.GetSummaryInformation(comboBoxEnemySelection, checkBoxCastTimes, checkBoxEnumerate, checkBoxRatesAsFractions, checkBoxTranslate).ToString());
        }

        

       

        private void checkBoxEnumerate_Click(object sender, EventArgs e)
        {
            if (comboBoxEnemySelection.SelectedIndex == -1 || comboBoxEnemySelection.Items.Count == 0 ||
                comboBoxPhaseSelection.SelectedIndex == -1 || comboBoxPhaseSelection.Items.Count == 0 ||
                FFRKProxy.Instance == null)
                PopulateAbilitySummary(null, null, null);
            else
                PopulateAbilitySummary((BasicEnemyInfo) comboBoxPhaseSelection.SelectedItem,
                    (BasicEnemyParentInfo) comboBoxEnemySelection.SelectedItem,
                    FFRKProxy.Instance.GameState.ActiveBattle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            var componentResourceManager = new ComponentResourceManager(typeof(FFRKViewEnemyDetails));
            var gridViewCellStyle1 = new DataGridViewCellStyle();
            var gridViewCellStyle2 = new DataGridViewCellStyle();
            var gridViewCellStyle3 = new DataGridViewCellStyle();
            var gridViewCellStyle4 = new DataGridViewCellStyle();
            var gridViewCellStyle5 = new DataGridViewCellStyle();
            var gridViewCellStyle6 = new DataGridViewCellStyle();
            enemyInfoGroupBox = new GroupBox();
            textChildPosID = new TextBox();
            labelChildPosID = new Label();
            textChildID = new TextBox();
            labelChildID = new Label();
            linkLabelAI = new LinkLabel();
            textAIID = new TextBox();
            labelAIID = new Label();
            linkLabelChildAI = new LinkLabel();
            textInitHP = new TextBox();
            SummaryButton = new Button();
            textEnglishName = new TextBox();
            labelInitialHP = new Label();
            labelEnglishName = new Label();
            textID = new TextBox();
            groupBoxEnemyAppearances = new GroupBox();
            listViewEnemyAppearances = new ListView();
            columnEnemyRounds = new ColumnHeader();
            columnEnemyMultiplicity = new ColumnHeader();
            textChildAIID = new TextBox();
            textLevel = new TextBox();
            labelChildAIID = new Label();
            labelEnemyLevel = new Label();
            pictureBox1 = new PictureBox();
            groupBoxAIArguments = new GroupBox();
            listViewAIArguments = new ListView();
            columnAITag = new ColumnHeader();
            columnAIArgValue = new ColumnHeader();
            columnAIArgType = new ColumnHeader();
            labelEnemyID = new Label();
            labelEnemySelection = new Label();
            comboBoxEnemySelection = new ComboBox();
            groupBoxEnemyPhase = new GroupBox();
            checkBoxEnumerate = new CheckBox();
            checkBoxTranslate = new CheckBox();
            checkBoxRawOnly = new CheckBox();
            checkBoxCastTimes = new CheckBox();
            checkBoxRatesAsFractions = new CheckBox();
            buttonToggleAbilityDetails = new Button();
            groupBoxAbilityDetails = new GroupBox();
            richTextBoxAbilitySummary = new RichTextBox();
            groupBoxCounters = new GroupBox();
            listViewCounters = new ListView();
            columnHeader50 = new ColumnHeader();
            columnHeader46 = new ColumnHeader();
            columnHeader47 = new ColumnHeader();
            columnHeader48 = new ColumnHeader();
            columnHeader49 = new ColumnHeader();
            groupBoxEnemyAbilities = new GroupBox();
            listViewEnemyAbilities = new ListView();
            columnAbilityName = new ColumnHeader();
            columnAbilityID = new ColumnHeader();
            columnAbilityTag = new ColumnHeader();
            columnAbilityWeight = new ColumnHeader();
            columnAbilityUnlock = new ColumnHeader();
            columnAbilityExercise = new ColumnHeader();
            columnAbilityCastTime = new ColumnHeader();
            columnAbilityTarRange = new ColumnHeader();
            columnAbilityTarMethod = new ColumnHeader();
            columnAbilityTarSegment = new ColumnHeader();
            columnAbilityActionID = new ColumnHeader();
            columnArg1 = new ColumnHeader();
            columnArg2 = new ColumnHeader();
            columnArg3 = new ColumnHeader();
            columnArg4 = new ColumnHeader();
            columnArg5 = new ColumnHeader();
            columnArg6 = new ColumnHeader();
            columnArg7 = new ColumnHeader();
            columnArg8 = new ColumnHeader();
            columnArg9 = new ColumnHeader();
            columnArg10 = new ColumnHeader();
            columnArg11 = new ColumnHeader();
            columnArg12 = new ColumnHeader();
            columnArg13 = new ColumnHeader();
            columnArg14 = new ColumnHeader();
            columnArg15 = new ColumnHeader();
            columnArg16 = new ColumnHeader();
            columnArg17 = new ColumnHeader();
            columnArg18 = new ColumnHeader();
            columnArg19 = new ColumnHeader();
            columnArg20 = new ColumnHeader();
            columnArg21 = new ColumnHeader();
            columnArg22 = new ColumnHeader();
            columnArg23 = new ColumnHeader();
            columnArg24 = new ColumnHeader();
            columnArg25 = new ColumnHeader();
            columnArg26 = new ColumnHeader();
            columnArg27 = new ColumnHeader();
            columnArg28 = new ColumnHeader();
            columnArg29 = new ColumnHeader();
            columnArg30 = new ColumnHeader();
            columnStatusFactor = new ColumnHeader();
            columnStatusID = new ColumnHeader();
            columnTargetDeath = new ColumnHeader();
            columnCounterEnable = new ColumnHeader();
            columnMinDamageThresh = new ColumnHeader();
            columnMaxDamageThresh = new ColumnHeader();
            groupBoxEnemyConstraints = new GroupBox();
            listViewEnemyConstraints = new ListView();
            columnConstraintAbilityTag = new ColumnHeader();
            columnConstraintValue = new ColumnHeader();
            columnConstraintType = new ColumnHeader();
            columnConstraintPriority = new ColumnHeader();
            columnConstraintEnemyStatusID = new ColumnHeader();
            columnConstraintOptions = new ColumnHeader();
            label5 = new Label();
            textCastTime = new TextBox();
            label4 = new Label();
            groupBox3 = new GroupBox();
            dataGridViewBreaks = new DataGridView();
            dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
            label3 = new Label();
            textEXP = new TextBox();
            textPhaseID = new TextBox();
            textPhaseEnglishName = new TextBox();
            textPhaseDispName = new TextBox();
            label2 = new Label();
            label1 = new Label();
            labelDispName = new Label();
            groupBox2 = new GroupBox();
            dataGridViewStats = new DataGridView();
            Column17 = new DataGridViewTextBoxColumn();
            Column10 = new DataGridViewTextBoxColumn();
            Column11 = new DataGridViewTextBoxColumn();
            Column12 = new DataGridViewTextBoxColumn();
            Column13 = new DataGridViewTextBoxColumn();
            Column14 = new DataGridViewTextBoxColumn();
            Column15 = new DataGridViewTextBoxColumn();
            Column16 = new DataGridViewTextBoxColumn();
            Column18 = new DataGridViewTextBoxColumn();
            Column20 = new DataGridViewTextBoxColumn();
            Column19 = new DataGridViewTextBoxColumn();
            groupBox1 = new GroupBox();
            dataGridViewElemental = new DataGridView();
            Column1 = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column3 = new DataGridViewTextBoxColumn();
            Column4 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewTextBoxColumn();
            Column6 = new DataGridViewTextBoxColumn();
            Column7 = new DataGridViewTextBoxColumn();
            Column8 = new DataGridViewTextBoxColumn();
            Column9 = new DataGridViewTextBoxColumn();
            textStatusVuln = new TextBox();
            comboBoxPhaseSelection = new ComboBox();
            labelPhaseSelection = new Label();
            toolTip1 = new ToolTip(components);
            enemyInfoGroupBox.SuspendLayout();
            groupBoxEnemyAppearances.SuspendLayout();
            ((ISupportInitialize) pictureBox1).BeginInit();
            groupBoxAIArguments.SuspendLayout();
            groupBoxEnemyPhase.SuspendLayout();
            groupBoxAbilityDetails.SuspendLayout();
            groupBoxCounters.SuspendLayout();
            groupBoxEnemyAbilities.SuspendLayout();
            groupBoxEnemyConstraints.SuspendLayout();
            groupBox3.SuspendLayout();
            ((ISupportInitialize) dataGridViewBreaks).BeginInit();
            groupBox2.SuspendLayout();
            ((ISupportInitialize) dataGridViewStats).BeginInit();
            groupBox1.SuspendLayout();
            ((ISupportInitialize) dataGridViewElemental).BeginInit();
            SuspendLayout();
            enemyInfoGroupBox.Controls.Add(textChildPosID);
            enemyInfoGroupBox.Controls.Add(labelChildPosID);
            enemyInfoGroupBox.Controls.Add(textChildID);
            enemyInfoGroupBox.Controls.Add(labelChildID);
            enemyInfoGroupBox.Controls.Add(linkLabelAI);
            enemyInfoGroupBox.Controls.Add(textAIID);
            enemyInfoGroupBox.Controls.Add(labelAIID);
            enemyInfoGroupBox.Controls.Add(linkLabelChildAI);
            enemyInfoGroupBox.Controls.Add(textInitHP);
            enemyInfoGroupBox.Controls.Add(SummaryButton);
            enemyInfoGroupBox.Controls.Add(textEnglishName);
            enemyInfoGroupBox.Controls.Add(labelInitialHP);
            enemyInfoGroupBox.Controls.Add(labelEnglishName);
            enemyInfoGroupBox.Controls.Add(textID);
            enemyInfoGroupBox.Controls.Add(groupBoxEnemyAppearances);
            enemyInfoGroupBox.Controls.Add(textChildAIID);
            enemyInfoGroupBox.Controls.Add(textLevel);
            enemyInfoGroupBox.Controls.Add(labelChildAIID);
            enemyInfoGroupBox.Controls.Add(labelEnemyLevel);
            enemyInfoGroupBox.Controls.Add(pictureBox1);
            enemyInfoGroupBox.Controls.Add(groupBoxAIArguments);
            enemyInfoGroupBox.Controls.Add(labelEnemyID);
            enemyInfoGroupBox.Controls.Add(labelEnemySelection);
            enemyInfoGroupBox.Controls.Add(comboBoxEnemySelection);
            enemyInfoGroupBox.Location = new Point(6, 6);
            enemyInfoGroupBox.Name = "enemyInfoGroupBox";
            enemyInfoGroupBox.Size = new Size(829, 254);
            enemyInfoGroupBox.TabIndex = 0;
            enemyInfoGroupBox.TabStop = false;
            enemyInfoGroupBox.Text = "Enemy Info";
            textChildPosID.BackColor = Color.White;
            textChildPosID.BorderStyle = BorderStyle.None;
            textChildPosID.Location = new Point(569, 41);
            textChildPosID.Name = "textChildPosID";
            textChildPosID.ReadOnly = true;
            textChildPosID.Size = new Size(93, 13);
            textChildPosID.TabIndex = 23;
            labelChildPosID.AutoSize = true;
            labelChildPosID.Location = new Point(490, 41);
            labelChildPosID.Name = "labelChildPosID";
            labelChildPosID.Size = new Size(68, 13);
            labelChildPosID.TabIndex = 22;
            labelChildPosID.Text = "Child Pos ID:";
            textChildID.BackColor = Color.White;
            textChildID.BorderStyle = BorderStyle.None;
            textChildID.Location = new Point(343, 79);
            textChildID.Name = "textChildID";
            textChildID.ReadOnly = true;
            textChildID.Size = new Size(93, 13);
            textChildID.TabIndex = 21;
            labelChildID.AutoSize = true;
            labelChildID.Location = new Point(262, 79);
            labelChildID.Name = "labelChildID";
            labelChildID.Size = new Size(56, 13);
            labelChildID.TabIndex = 20;
            labelChildID.Text = "Enemy ID:";
            linkLabelAI.AutoSize = true;
            linkLabelAI.LinkColor = Color.Navy;
            linkLabelAI.Location = new Point(671, 60);
            linkLabelAI.Name = "linkLabelAI";
            linkLabelAI.Size = new Size(95, 13);
            linkLabelAI.TabIndex = 18;
            linkLabelAI.TabStop = true;
            linkLabelAI.Text = "View AI in Browser";
            linkLabelAI.LinkClicked += linkLabelAI_LinkClicked;
            textAIID.BackColor = Color.White;
            textAIID.BorderStyle = BorderStyle.None;
            textAIID.Location = new Point(569, 60);
            textAIID.Name = "textAIID";
            textAIID.ReadOnly = true;
            textAIID.Size = new Size(93, 13);
            textAIID.TabIndex = 19;
            labelAIID.AutoSize = true;
            labelAIID.Location = new Point(490, 60);
            labelAIID.Name = "labelAIID";
            labelAIID.Size = new Size(34, 13);
            labelAIID.TabIndex = 18;
            labelAIID.Text = "AI ID:";
            linkLabelChildAI.AutoSize = true;
            linkLabelChildAI.LinkColor = Color.Navy;
            linkLabelChildAI.Location = new Point(671, 79);
            linkLabelChildAI.Name = "linkLabelChildAI";
            linkLabelChildAI.Size = new Size(121, 13);
            linkLabelChildAI.TabIndex = 17;
            linkLabelChildAI.TabStop = true;
            linkLabelChildAI.Text = "View Child AI in Browser";
            linkLabelChildAI.LinkClicked += linkLabelChildAI_LinkClicked;
            textInitHP.BackColor = Color.White;
            textInitHP.BorderStyle = BorderStyle.None;
            textInitHP.Location = new Point(343, 60);
            textInitHP.Name = "textInitHP";
            textInitHP.ReadOnly = true;
            textInitHP.Size = new Size(121, 13);
            textInitHP.TabIndex = 16;
            SummaryButton.Anchor = AnchorStyles.Left;
            SummaryButton.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
            SummaryButton.Location = new Point(671, 14);
            SummaryButton.Name = "SummaryButton";
            SummaryButton.Padding = new Padding(23, 0, 23, 0);
            SummaryButton.Size = new Size(151, 43);
            SummaryButton.TabIndex = 0;
            SummaryButton.Text = "Copy Summary to Clipboard";
            toolTip1.SetToolTip(SummaryButton, componentResourceManager.GetString("SummaryButton.ToolTip"));
            SummaryButton.UseVisualStyleBackColor = true;
            SummaryButton.Click += SummaryButton_Click;
            textEnglishName.BackColor = Color.White;
            textEnglishName.BorderStyle = BorderStyle.None;
            textEnglishName.Location = new Point(343, 22);
            textEnglishName.Name = "textEnglishName";
            textEnglishName.ReadOnly = true;
            textEnglishName.Size = new Size(194, 13);
            textEnglishName.TabIndex = 14;
            labelInitialHP.AutoSize = true;
            labelInitialHP.Location = new Point(262, 60);
            labelInitialHP.Name = "labelInitialHP";
            labelInitialHP.Size = new Size(52, 13);
            labelInitialHP.TabIndex = 15;
            labelInitialHP.Text = "Initial HP:";
            labelEnglishName.AutoSize = true;
            labelEnglishName.Location = new Point(262, 22);
            labelEnglishName.Name = "labelEnglishName";
            labelEnglishName.Size = new Size(75, 13);
            labelEnglishName.TabIndex = 14;
            labelEnglishName.Text = "English Name:";
            toolTip1.SetToolTip(labelEnglishName, "Translated using Google Translate");
            textID.BackColor = Color.White;
            textID.BorderStyle = BorderStyle.None;
            textID.Location = new Point(572, 22);
            textID.Name = "textID";
            textID.ReadOnly = true;
            textID.Size = new Size(93, 13);
            textID.TabIndex = 12;
            textID.Visible = false;
            groupBoxEnemyAppearances.Controls.Add(listViewEnemyAppearances);
            groupBoxEnemyAppearances.Location = new Point(671, 95);
            groupBoxEnemyAppearances.Name = "groupBoxEnemyAppearances";
            groupBoxEnemyAppearances.Size = new Size(151, 153);
            groupBoxEnemyAppearances.TabIndex = 6;
            groupBoxEnemyAppearances.TabStop = false;
            groupBoxEnemyAppearances.Text = "Appearances";
            listViewEnemyAppearances.Columns.AddRange(new ColumnHeader[2]
            {
                columnEnemyRounds,
                columnEnemyMultiplicity
            });
            listViewEnemyAppearances.Dock = DockStyle.Fill;
            listViewEnemyAppearances.Location = new Point(3, 16);
            listViewEnemyAppearances.Name = "listViewEnemyAppearances";
            listViewEnemyAppearances.Size = new Size(145, 134);
            listViewEnemyAppearances.TabIndex = 0;
            listViewEnemyAppearances.UseCompatibleStateImageBehavior = false;
            listViewEnemyAppearances.View = View.Details;
            columnEnemyRounds.Text = "Wave";
            columnEnemyRounds.Width = 42;
            columnEnemyMultiplicity.Text = "Quantity";
            columnEnemyMultiplicity.Width = 80;
            textChildAIID.BackColor = Color.White;
            textChildAIID.BorderStyle = BorderStyle.None;
            textChildAIID.Location = new Point(569, 79);
            textChildAIID.Name = "textChildAIID";
            textChildAIID.ReadOnly = true;
            textChildAIID.Size = new Size(93, 13);
            textChildAIID.TabIndex = 11;
            textLevel.BackColor = Color.White;
            textLevel.BorderStyle = BorderStyle.None;
            textLevel.Location = new Point(343, 41);
            textLevel.Name = "textLevel";
            textLevel.ReadOnly = true;
            textLevel.Size = new Size(121, 13);
            textLevel.TabIndex = 10;
            labelChildAIID.AutoSize = true;
            labelChildAIID.Location = new Point(490, 79);
            labelChildAIID.Name = "labelChildAIID";
            labelChildAIID.Size = new Size(60, 13);
            labelChildAIID.TabIndex = 1;
            labelChildAIID.Text = "Child AI ID:";
            labelEnemyLevel.AutoSize = true;
            labelEnemyLevel.Location = new Point(262, 41);
            labelEnemyLevel.Name = "labelEnemyLevel";
            labelEnemyLevel.Size = new Size(36, 13);
            labelEnemyLevel.TabIndex = 0;
            labelEnemyLevel.Text = "Level:";
            pictureBox1.AccessibleRole = AccessibleRole.Cursor;
            pictureBox1.Anchor = AnchorStyles.None;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Location = new Point(6, 47);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(250, 200);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            groupBoxAIArguments.Controls.Add(listViewAIArguments);
            groupBoxAIArguments.Location = new Point(265, 95);
            groupBoxAIArguments.Name = "groupBoxAIArguments";
            groupBoxAIArguments.Size = new Size(400, 153);
            groupBoxAIArguments.TabIndex = 4;
            groupBoxAIArguments.TabStop = false;
            groupBoxAIArguments.Text = "AI Arguments";
            groupBoxAIArguments.Enter += groupBox1_Enter;
            listViewAIArguments.Columns.AddRange(new ColumnHeader[3]
            {
                columnAITag,
                columnAIArgValue,
                columnAIArgType
            });
            listViewAIArguments.Dock = DockStyle.Fill;
            listViewAIArguments.Location = new Point(3, 16);
            listViewAIArguments.Name = "listViewAIArguments";
            listViewAIArguments.Size = new Size(394, 134);
            listViewAIArguments.TabIndex = 0;
            listViewAIArguments.UseCompatibleStateImageBehavior = false;
            listViewAIArguments.View = View.Details;
            listViewAIArguments.KeyUp += listViewAIArguments_KeyUp;
            columnAITag.Text = "Tag";
            columnAITag.Width = 168;
            columnAIArgValue.Text = "Value";
            columnAIArgValue.Width = 41;
            columnAIArgType.Text = "Type";
            columnAIArgType.Width = 37;
            labelEnemyID.AutoSize = true;
            labelEnemyID.Location = new Point(506, 22);
            labelEnemyID.Name = "labelEnemyID";
            labelEnemyID.Size = new Size(56, 13);
            labelEnemyID.TabIndex = 2;
            labelEnemyID.Text = "Enemy ID:";
            labelEnemyID.Visible = false;
            labelEnemyID.Click += label2_Click;
            labelEnemySelection.AutoSize = true;
            labelEnemySelection.Location = new Point(6, 22);
            labelEnemySelection.Name = "labelEnemySelection";
            labelEnemySelection.Size = new Size(42, 13);
            labelEnemySelection.TabIndex = 1;
            labelEnemySelection.Text = "Enemy:";
            comboBoxEnemySelection.FormattingEnabled = true;
            comboBoxEnemySelection.Location = new Point(49, 19);
            comboBoxEnemySelection.Name = "comboBoxEnemySelection";
            comboBoxEnemySelection.Size = new Size(207, 21);
            comboBoxEnemySelection.TabIndex = 0;
            comboBoxEnemySelection.SelectedIndexChanged += comboBoxEnemySelection_SelectedIndexChanged;
            comboBoxEnemySelection.DropDownClosed += comboBoxEnemySelection_DropDownClosed;
            groupBoxEnemyPhase.Controls.Add(checkBoxEnumerate);
            groupBoxEnemyPhase.Controls.Add(checkBoxTranslate);
            groupBoxEnemyPhase.Controls.Add(checkBoxRawOnly);
            groupBoxEnemyPhase.Controls.Add(checkBoxCastTimes);
            groupBoxEnemyPhase.Controls.Add(checkBoxRatesAsFractions);
            groupBoxEnemyPhase.Controls.Add(buttonToggleAbilityDetails);
            groupBoxEnemyPhase.Controls.Add(groupBoxAbilityDetails);
            groupBoxEnemyPhase.Controls.Add(label5);
            groupBoxEnemyPhase.Controls.Add(textCastTime);
            groupBoxEnemyPhase.Controls.Add(label4);
            groupBoxEnemyPhase.Controls.Add(groupBox3);
            groupBoxEnemyPhase.Controls.Add(label3);
            groupBoxEnemyPhase.Controls.Add(textEXP);
            groupBoxEnemyPhase.Controls.Add(textPhaseID);
            groupBoxEnemyPhase.Controls.Add(textPhaseEnglishName);
            groupBoxEnemyPhase.Controls.Add(textPhaseDispName);
            groupBoxEnemyPhase.Controls.Add(label2);
            groupBoxEnemyPhase.Controls.Add(label1);
            groupBoxEnemyPhase.Controls.Add(labelDispName);
            groupBoxEnemyPhase.Controls.Add(groupBox2);
            groupBoxEnemyPhase.Controls.Add(groupBox1);
            groupBoxEnemyPhase.Controls.Add(textStatusVuln);
            groupBoxEnemyPhase.Controls.Add(comboBoxPhaseSelection);
            groupBoxEnemyPhase.Controls.Add(labelPhaseSelection);
            groupBoxEnemyPhase.Location = new Point(6, 266);
            groupBoxEnemyPhase.Name = "groupBoxEnemyPhase";
            groupBoxEnemyPhase.Size = new Size(991, 531);
            groupBoxEnemyPhase.TabIndex = 1;
            groupBoxEnemyPhase.TabStop = false;
            groupBoxEnemyPhase.Text = "Phase/Form Info";
            checkBoxEnumerate.AutoSize = true;
            checkBoxEnumerate.Checked = true;
            checkBoxEnumerate.CheckState = CheckState.Checked;
            checkBoxEnumerate.Location = new Point(499, 168);
            checkBoxEnumerate.Name = "checkBoxEnumerate";
            checkBoxEnumerate.Size = new Size(147, 17);
            checkBoxEnumerate.TabIndex = 39;
            checkBoxEnumerate.Text = "Enumerate forced actions";
            checkBoxEnumerate.ThreeState = true;
            toolTip1.SetToolTip(checkBoxEnumerate,
                "Lists each turn where an action is forced by constraint type 1001 (FORCE_BY_TURN).\r\nIf left indeterminate, actions will be enumerated if there are at least 10 forced actions for this phase.");
            checkBoxEnumerate.UseVisualStyleBackColor = true;
            checkBoxEnumerate.Click += checkBoxEnumerate_Click;
            checkBoxTranslate.AutoSize = true;
            checkBoxTranslate.Checked = true;
            checkBoxTranslate.CheckState = CheckState.Checked;
            checkBoxTranslate.Location = new Point(652, 168);
            checkBoxTranslate.Name = "checkBoxTranslate";
            checkBoxTranslate.Size = new Size(133, 17);
            checkBoxTranslate.TabIndex = 38;
            checkBoxTranslate.Text = "Translate ability names";
            checkBoxTranslate.UseVisualStyleBackColor = true;
            checkBoxTranslate.Click += checkBoxTranslate_Click;
            checkBoxRawOnly.AutoSize = true;
            checkBoxRawOnly.Location = new Point(190, 168);
            checkBoxRawOnly.Name = "checkBoxRawOnly";
            checkBoxRawOnly.Size = new Size(129, 17);
            checkBoxRawOnly.TabIndex = 37;
            checkBoxRawOnly.Text = "Show raw values only";
            checkBoxRawOnly.UseVisualStyleBackColor = true;
            checkBoxRawOnly.Visible = false;
            checkBoxRawOnly.Click += checkBoxRawOnly_Click;
            checkBoxCastTimes.AutoSize = true;
            checkBoxCastTimes.Checked = true;
            checkBoxCastTimes.CheckState = CheckState.Indeterminate;
            checkBoxCastTimes.Location = new Point(361, 168);
            checkBoxCastTimes.Name = "checkBoxCastTimes";
            checkBoxCastTimes.Size = new Size(132, 17);
            checkBoxCastTimes.TabIndex = 36;
            checkBoxCastTimes.Text = "Show ability cast times";
            checkBoxCastTimes.ThreeState = true;
            toolTip1.SetToolTip(checkBoxCastTimes,
                "If left indeterminate, cast times will be displayed only if allowed by cast_time_type.");
            checkBoxCastTimes.UseVisualStyleBackColor = true;
            checkBoxCastTimes.Click += checkBoxCastTimes_Click;
            checkBoxRatesAsFractions.AutoSize = true;
            checkBoxRatesAsFractions.Location = new Point(190, 168);
            checkBoxRatesAsFractions.Name = "checkBoxRatesAsFractions";
            checkBoxRatesAsFractions.Size = new Size(165, 17);
            checkBoxRatesAsFractions.TabIndex = 1;
            checkBoxRatesAsFractions.Text = "Show ability rates as fractions";
            checkBoxRatesAsFractions.UseVisualStyleBackColor = true;
            checkBoxRatesAsFractions.Click += checkBoxRatesAsFractions_Click;
            buttonToggleAbilityDetails.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
            buttonToggleAbilityDetails.Location = new Point(6, 164);
            buttonToggleAbilityDetails.Name = "buttonToggleAbilityDetails";
            buttonToggleAbilityDetails.Size = new Size(173, 23);
            buttonToggleAbilityDetails.TabIndex = 27;
            buttonToggleAbilityDetails.Text = "View Enemy Ability Summary";
            buttonToggleAbilityDetails.UseVisualStyleBackColor = true;
            buttonToggleAbilityDetails.Click += buttonToggleAbilityDetails_Click;
            groupBoxAbilityDetails.Controls.Add(richTextBoxAbilitySummary);
            groupBoxAbilityDetails.Controls.Add(groupBoxCounters);
            groupBoxAbilityDetails.Controls.Add(groupBoxEnemyAbilities);
            groupBoxAbilityDetails.Controls.Add(groupBoxEnemyConstraints);
            groupBoxAbilityDetails.Location = new Point(6, 191);
            groupBoxAbilityDetails.Name = "groupBoxAbilityDetails";
            groupBoxAbilityDetails.Size = new Size(976, 334);
            groupBoxAbilityDetails.TabIndex = 2;
            groupBoxAbilityDetails.TabStop = false;
            groupBoxAbilityDetails.Text = "Enemy Ability Details";
            richTextBoxAbilitySummary.Font =
                new Font("Microsoft Sans Serif", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
            richTextBoxAbilitySummary.Location = new Point(6, 18);
            richTextBoxAbilitySummary.Name = "richTextBoxAbilitySummary";
            richTextBoxAbilitySummary.Size = new Size(964, 309);
            richTextBoxAbilitySummary.TabIndex = 26;
            richTextBoxAbilitySummary.Text = "";
            richTextBoxAbilitySummary.Visible = false;
            groupBoxCounters.Controls.Add(listViewCounters);
            groupBoxCounters.Location = new Point(540, 190);
            groupBoxCounters.Name = "groupBoxCounters";
            groupBoxCounters.Size = new Size(430, 138);
            groupBoxCounters.TabIndex = 27;
            groupBoxCounters.TabStop = false;
            groupBoxCounters.Text = "Counters";
            listViewCounters.Columns.AddRange(new ColumnHeader[5]
            {
                columnHeader50,
                columnHeader46,
                columnHeader47,
                columnHeader48,
                columnHeader49
            });
            listViewCounters.Dock = DockStyle.Fill;
            listViewCounters.Location = new Point(3, 16);
            listViewCounters.Name = "listViewCounters";
            listViewCounters.Size = new Size(424, 119);
            listViewCounters.TabIndex = 1;
            listViewCounters.UseCompatibleStateImageBehavior = false;
            listViewCounters.View = View.Details;
            columnHeader50.Text = "Name";
            columnHeader46.Text = "Ability ID";
            columnHeader46.Width = 80;
            columnHeader47.Text = "Rate";
            columnHeader47.Width = 50;
            columnHeader48.Text = "Condition Value";
            columnHeader48.Width = 92;
            columnHeader49.Text = "Condition Type";
            columnHeader49.Width = 94;
            groupBoxEnemyAbilities.Controls.Add(listViewEnemyAbilities);
            groupBoxEnemyAbilities.Location = new Point(6, 19);
            groupBoxEnemyAbilities.Name = "groupBoxEnemyAbilities";
            groupBoxEnemyAbilities.Size = new Size(964, 165);
            groupBoxEnemyAbilities.TabIndex = 6;
            groupBoxEnemyAbilities.TabStop = false;
            groupBoxEnemyAbilities.Text = "Abilities";
            listViewEnemyAbilities.Columns.AddRange(new ColumnHeader[47]
            {
                columnAbilityName,
                columnAbilityID,
                columnAbilityTag,
                columnAbilityWeight,
                columnAbilityUnlock,
                columnAbilityExercise,
                columnAbilityCastTime,
                columnAbilityTarRange,
                columnAbilityTarMethod,
                columnAbilityTarSegment,
                columnAbilityActionID,
                columnArg1,
                columnArg2,
                columnArg3,
                columnArg4,
                columnArg5,
                columnArg6,
                columnArg7,
                columnArg8,
                columnArg9,
                columnArg10,
                columnArg11,
                columnArg12,
                columnArg13,
                columnArg14,
                columnArg15,
                columnArg16,
                columnArg17,
                columnArg18,
                columnArg19,
                columnArg20,
                columnArg21,
                columnArg22,
                columnArg23,
                columnArg24,
                columnArg25,
                columnArg26,
                columnArg27,
                columnArg28,
                columnArg29,
                columnArg30,
                columnStatusFactor,
                columnStatusID,
                columnTargetDeath,
                columnCounterEnable,
                columnMinDamageThresh,
                columnMaxDamageThresh
            });
            listViewEnemyAbilities.Dock = DockStyle.Fill;
            listViewEnemyAbilities.Location = new Point(3, 16);
            listViewEnemyAbilities.Name = "listViewEnemyAbilities";
            listViewEnemyAbilities.Size = new Size(958, 146);
            listViewEnemyAbilities.TabIndex = 0;
            listViewEnemyAbilities.UseCompatibleStateImageBehavior = false;
            listViewEnemyAbilities.View = View.Details;
            columnAbilityName.Text = "Name";
            columnAbilityName.Width = 80;
            columnAbilityID.Text = "Ability ID";
            columnAbilityID.Width = 55;
            columnAbilityTag.Text = "Tag";
            columnAbilityTag.Width = 54;
            columnAbilityWeight.Text = "Weight";
            columnAbilityWeight.Width = 50;
            columnAbilityUnlock.Text = "Unlock Turn";
            columnAbilityUnlock.Width = 72;
            columnAbilityExercise.Text = "Exercise Type";
            columnAbilityExercise.Width = 80;
            columnAbilityCastTime.Text = "Cast Time";
            columnAbilityTarRange.Text = "Target Range";
            columnAbilityTarRange.Width = 78;
            columnAbilityTarMethod.Text = "Target Method";
            columnAbilityTarMethod.Width = 83;
            columnAbilityTarSegment.Text = "Target Segment";
            columnAbilityTarSegment.Width = 89;
            columnAbilityActionID.Text = "Action ID";
            columnAbilityActionID.Width = 57;
            columnArg1.Text = "Arg1";
            columnArg1.Width = 36;
            columnArg2.Text = "Arg2";
            columnArg2.Width = 37;
            columnArg3.Text = "Arg3";
            columnArg3.Width = 38;
            columnArg4.Text = "Arg4";
            columnArg4.Width = 39;
            columnArg5.Text = "Arg5";
            columnArg5.Width = 36;
            columnArg6.Text = "Arg6";
            columnArg6.Width = 37;
            columnArg7.Text = "Arg7";
            columnArg7.Width = 35;
            columnArg8.Text = "Arg8";
            columnArg8.Width = 35;
            columnArg9.Text = "Arg9";
            columnArg9.Width = 35;
            columnArg10.Text = "Arg10";
            columnArg10.Width = 40;
            columnArg11.Text = "Arg11";
            columnArg11.Width = 40;
            columnArg12.Text = "Arg12";
            columnArg12.Width = 40;
            columnArg13.Text = "Arg13";
            columnArg13.Width = 43;
            columnArg14.Text = "Arg14";
            columnArg15.Text = "Arg15";
            columnArg16.Text = "Arg16";
            columnArg17.Text = "Arg17";
            columnArg18.Text = "Arg18";
            columnArg19.Text = "Arg19";
            columnArg20.Text = "Arg20";
            columnArg21.Text = "Arg21";
            columnArg22.Text = "Arg22";
            columnArg23.Text = "Arg23";
            columnArg24.Text = "Arg24";
            columnArg25.Text = "Arg25";
            columnArg26.Text = "Arg26";
            columnArg27.Text = "Arg27";
            columnArg28.Text = "Arg28";
            columnArg29.Text = "Arg29";
            columnArg30.Text = "Arg30";
            columnStatusFactor.Text = "Status Factor";
            columnStatusID.Text = "Status ID";
            columnTargetDeath.Text = "Target Death";
            columnCounterEnable.Text = "Counter Enable";
            columnMinDamageThresh.Text = "Min Damage Threshold Type";
            columnMaxDamageThresh.Text = "Max Damage Threshold Type";
            groupBoxEnemyConstraints.Controls.Add(listViewEnemyConstraints);
            groupBoxEnemyConstraints.Location = new Point(6, 190);
            groupBoxEnemyConstraints.Name = "groupBoxEnemyConstraints";
            groupBoxEnemyConstraints.Size = new Size(528, 138);
            groupBoxEnemyConstraints.TabIndex = 5;
            groupBoxEnemyConstraints.TabStop = false;
            groupBoxEnemyConstraints.Text = "Constraints";
            listViewEnemyConstraints.Columns.AddRange(new ColumnHeader[6]
            {
                columnConstraintAbilityTag,
                columnConstraintValue,
                columnConstraintType,
                columnConstraintPriority,
                columnConstraintEnemyStatusID,
                columnConstraintOptions
            });
            listViewEnemyConstraints.Dock = DockStyle.Fill;
            listViewEnemyConstraints.Location = new Point(3, 16);
            listViewEnemyConstraints.Name = "listViewEnemyConstraints";
            listViewEnemyConstraints.Size = new Size(522, 119);
            listViewEnemyConstraints.TabIndex = 0;
            listViewEnemyConstraints.UseCompatibleStateImageBehavior = false;
            listViewEnemyConstraints.View = View.Details;
            columnConstraintAbilityTag.Text = "Ability Tag";
            columnConstraintAbilityTag.Width = 80;
            columnConstraintValue.Text = "Value";
            columnConstraintValue.Width = 50;
            columnConstraintType.Text = "Type";
            columnConstraintType.Width = 54;
            columnConstraintPriority.Text = "Priority";
            columnConstraintPriority.Width = 48;
            columnConstraintEnemyStatusID.Text = "Enemy Status ID";
            columnConstraintEnemyStatusID.Width = 96;
            columnConstraintOptions.Text = "Options";
            label5.AutoSize = true;
            label5.Location = new Point(6, 124);
            label5.Name = "label5";
            label5.Size = new Size(57, 13);
            label5.TabIndex = 35;
            label5.Text = "Cast Time:";
            textCastTime.BackColor = Color.White;
            textCastTime.BorderStyle = BorderStyle.None;
            textCastTime.Location = new Point(87, 124);
            textCastTime.Name = "textCastTime";
            textCastTime.ReadOnly = true;
            textCastTime.Size = new Size(169, 13);
            textCastTime.TabIndex = 34;
            label4.AutoSize = true;
            label4.Location = new Point(6, 143);
            label4.Name = "label4";
            label4.Size = new Size(67, 13);
            label4.TabIndex = 33;
            label4.Text = "Status Vuln.:";
            groupBox3.Controls.Add(dataGridViewBreaks);
            groupBox3.Location = new Point(698, 76);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(287, 64);
            groupBox3.TabIndex = 24;
            groupBox3.TabStop = false;
            groupBox3.Text = "Break Effectiveness";
            dataGridViewBreaks.AllowUserToResizeColumns = false;
            dataGridViewBreaks.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewBreaks.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridViewCellStyle1.BackColor = SystemColors.Control;
            gridViewCellStyle1.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 128);
            gridViewCellStyle1.ForeColor = SystemColors.WindowText;
            gridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            dataGridViewBreaks.ColumnHeadersDefaultCellStyle = gridViewCellStyle1;
            dataGridViewBreaks.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewBreaks.Columns.AddRange(dataGridViewTextBoxColumn1, dataGridViewTextBoxColumn2,
                dataGridViewTextBoxColumn3, dataGridViewTextBoxColumn4, dataGridViewTextBoxColumn5,
                dataGridViewTextBoxColumn6);
            gridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridViewCellStyle2.BackColor = SystemColors.Window;
            gridViewCellStyle2.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 128);
            gridViewCellStyle2.ForeColor = SystemColors.ControlText;
            gridViewCellStyle2.SelectionBackColor = SystemColors.Window;
            gridViewCellStyle2.SelectionForeColor = SystemColors.ControlText;
            gridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            dataGridViewBreaks.DefaultCellStyle = gridViewCellStyle2;
            dataGridViewBreaks.Dock = DockStyle.Fill;
            dataGridViewBreaks.Location = new Point(3, 16);
            dataGridViewBreaks.Name = "dataGridViewBreaks";
            dataGridViewBreaks.ReadOnly = true;
            dataGridViewBreaks.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewBreaks.RowHeadersVisible = false;
            dataGridViewBreaks.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewBreaks.ScrollBars = ScrollBars.None;
            dataGridViewBreaks.Size = new Size(281, 45);
            dataGridViewBreaks.TabIndex = 1;
            dataGridViewBreaks.SelectionChanged += dataGridViewBreaks_SelectionChanged;
            dataGridViewTextBoxColumn1.HeaderText = "ATK";
            dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            dataGridViewTextBoxColumn1.ReadOnly = true;
            dataGridViewTextBoxColumn1.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewTextBoxColumn2.HeaderText = "DEF";
            dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            dataGridViewTextBoxColumn2.ReadOnly = true;
            dataGridViewTextBoxColumn2.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewTextBoxColumn3.HeaderText = "MAG";
            dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            dataGridViewTextBoxColumn3.ReadOnly = true;
            dataGridViewTextBoxColumn3.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewTextBoxColumn4.HeaderText = "RES";
            dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            dataGridViewTextBoxColumn4.ReadOnly = true;
            dataGridViewTextBoxColumn4.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewTextBoxColumn5.HeaderText = "MND";
            dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            dataGridViewTextBoxColumn5.ReadOnly = true;
            dataGridViewTextBoxColumn5.SortMode = DataGridViewColumnSortMode.NotSortable;
            dataGridViewTextBoxColumn6.HeaderText = "SPD";
            dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            dataGridViewTextBoxColumn6.ReadOnly = true;
            dataGridViewTextBoxColumn6.SortMode = DataGridViewColumnSortMode.NotSortable;
            label3.AutoSize = true;
            label3.Location = new Point(6, 105);
            label3.Name = "label3";
            label3.Size = new Size(31, 13);
            label3.TabIndex = 32;
            label3.Text = "EXP:";
            textEXP.BackColor = Color.White;
            textEXP.BorderStyle = BorderStyle.None;
            textEXP.Location = new Point(87, 105);
            textEXP.Name = "textEXP";
            textEXP.ReadOnly = true;
            textEXP.Size = new Size(169, 13);
            textEXP.TabIndex = 31;
            textPhaseID.BackColor = Color.White;
            textPhaseID.BorderStyle = BorderStyle.None;
            textPhaseID.Location = new Point(87, 86);
            textPhaseID.Name = "textPhaseID";
            textPhaseID.ReadOnly = true;
            textPhaseID.Size = new Size(169, 13);
            textPhaseID.TabIndex = 30;
            textPhaseEnglishName.BackColor = Color.White;
            textPhaseEnglishName.BorderStyle = BorderStyle.None;
            textPhaseEnglishName.Location = new Point(87, 67);
            textPhaseEnglishName.Name = "textPhaseEnglishName";
            textPhaseEnglishName.ReadOnly = true;
            textPhaseEnglishName.Size = new Size(169, 13);
            textPhaseEnglishName.TabIndex = 29;
            textPhaseDispName.BackColor = Color.White;
            textPhaseDispName.BorderStyle = BorderStyle.None;
            textPhaseDispName.Location = new Point(87, 48);
            textPhaseDispName.Name = "textPhaseDispName";
            textPhaseDispName.ReadOnly = true;
            textPhaseDispName.Size = new Size(169, 13);
            textPhaseDispName.TabIndex = 22;
            label2.AutoSize = true;
            label2.Location = new Point(6, 67);
            label2.Name = "label2";
            label2.Size = new Size(75, 13);
            label2.TabIndex = 28;
            label2.Text = "English Name:";
            label1.AutoSize = true;
            label1.Location = new Point(6, 86);
            label1.Name = "label1";
            label1.Size = new Size(21, 13);
            label1.TabIndex = 27;
            label1.Text = "ID:";
            labelDispName.AutoSize = true;
            labelDispName.Location = new Point(6, 48);
            labelDispName.Name = "labelDispName";
            labelDispName.Size = new Size(75, 13);
            labelDispName.TabIndex = 22;
            labelDispName.Text = "Display Name:";
            groupBox2.Controls.Add(dataGridViewStats);
            groupBox2.Location = new Point(265, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(720, 64);
            groupBox2.TabIndex = 24;
            groupBox2.TabStop = false;
            groupBox2.Text = "Stats";
            dataGridViewStats.AllowUserToResizeColumns = false;
            dataGridViewStats.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewStats.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridViewCellStyle3.BackColor = SystemColors.Control;
            gridViewCellStyle3.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 128);
            gridViewCellStyle3.ForeColor = SystemColors.WindowText;
            gridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle3.WrapMode = DataGridViewTriState.True;
            dataGridViewStats.ColumnHeadersDefaultCellStyle = gridViewCellStyle3;
            dataGridViewStats.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewStats.Columns.AddRange(Column17, Column10, Column11, Column12, Column13, Column14, Column15,
                Column16, Column18, Column20, Column19);
            gridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridViewCellStyle4.BackColor = SystemColors.Window;
            gridViewCellStyle4.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 128);
            gridViewCellStyle4.ForeColor = SystemColors.ControlText;
            gridViewCellStyle4.SelectionBackColor = SystemColors.Window;
            gridViewCellStyle4.SelectionForeColor = SystemColors.ControlText;
            gridViewCellStyle4.WrapMode = DataGridViewTriState.False;
            dataGridViewStats.DefaultCellStyle = gridViewCellStyle4;
            dataGridViewStats.Dock = DockStyle.Fill;
            dataGridViewStats.Location = new Point(3, 16);
            dataGridViewStats.Name = "dataGridViewStats";
            dataGridViewStats.ReadOnly = true;
            dataGridViewStats.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewStats.RowHeadersVisible = false;
            dataGridViewStats.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewStats.ScrollBars = ScrollBars.None;
            dataGridViewStats.Size = new Size(714, 45);
            dataGridViewStats.TabIndex = 1;
            dataGridViewStats.SelectionChanged += dataGridViewStats_SelectionChanged;
            Column17.HeaderText = "Level";
            Column17.Name = "Column17";
            Column17.ReadOnly = true;
            Column17.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column10.HeaderText = "Max HP";
            Column10.Name = "Column10";
            Column10.ReadOnly = true;
            Column10.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column11.HeaderText = "Attack";
            Column11.Name = "Column11";
            Column11.ReadOnly = true;
            Column11.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column12.HeaderText = "Defense";
            Column12.Name = "Column12";
            Column12.ReadOnly = true;
            Column12.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column13.HeaderText = "Magic";
            Column13.Name = "Column13";
            Column13.ReadOnly = true;
            Column13.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column14.HeaderText = "Resistance";
            Column14.Name = "Column14";
            Column14.ReadOnly = true;
            Column14.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column15.HeaderText = "Mind";
            Column15.Name = "Column15";
            Column15.ReadOnly = true;
            Column15.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column16.HeaderText = "Speed";
            Column16.Name = "Column16";
            Column16.ReadOnly = true;
            Column16.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column18.HeaderText = "Accuracy";
            Column18.Name = "Column18";
            Column18.ReadOnly = true;
            Column18.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column20.HeaderText = "Evade";
            Column20.Name = "Column20";
            Column20.ReadOnly = true;
            Column20.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column19.HeaderText = "Crit";
            Column19.Name = "Column19";
            Column19.ReadOnly = true;
            Column19.SortMode = DataGridViewColumnSortMode.NotSortable;
            groupBox1.Controls.Add(dataGridViewElemental);
            groupBox1.Location = new Point(265, 76);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(427, 64);
            groupBox1.TabIndex = 23;
            groupBox1.TabStop = false;
            groupBox1.Text = "Elemental Damage Taken";
            dataGridViewElemental.AllowUserToResizeColumns = false;
            dataGridViewElemental.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewElemental.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridViewCellStyle5.BackColor = SystemColors.Control;
            gridViewCellStyle5.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 128);
            gridViewCellStyle5.ForeColor = SystemColors.WindowText;
            gridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            gridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            gridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dataGridViewElemental.ColumnHeadersDefaultCellStyle = gridViewCellStyle5;
            dataGridViewElemental.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewElemental.Columns.AddRange(Column1, Column2, Column3, Column4, Column5, Column6, Column7,
                Column8, Column9);
            gridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleCenter;
            gridViewCellStyle6.BackColor = SystemColors.Window;
            gridViewCellStyle6.Font =
                new Font("Microsoft Sans Serif", 8.25f, FontStyle.Regular, GraphicsUnit.Point, 128);
            gridViewCellStyle6.ForeColor = SystemColors.ControlText;
            gridViewCellStyle6.SelectionBackColor = SystemColors.Window;
            gridViewCellStyle6.SelectionForeColor = SystemColors.ControlText;
            gridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dataGridViewElemental.DefaultCellStyle = gridViewCellStyle6;
            dataGridViewElemental.Dock = DockStyle.Fill;
            dataGridViewElemental.Location = new Point(3, 16);
            dataGridViewElemental.Name = "dataGridViewElemental";
            dataGridViewElemental.ReadOnly = true;
            dataGridViewElemental.RowHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewElemental.RowHeadersVisible = false;
            dataGridViewElemental.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            dataGridViewElemental.ScrollBars = ScrollBars.None;
            dataGridViewElemental.Size = new Size(421, 45);
            dataGridViewElemental.TabIndex = 1;
            dataGridViewElemental.SelectionChanged += dataGridViewElemental_SelectionChanged;
            Column1.DataPropertyName = "EnemyFireDef";
            Column1.HeaderText = "Fire";
            Column1.Name = "Column1";
            Column1.ReadOnly = true;
            Column1.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column2.DataPropertyName = "EnemyIceDef";
            Column2.HeaderText = "Ice";
            Column2.Name = "Column2";
            Column2.ReadOnly = true;
            Column2.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column3.DataPropertyName = "EnemyLitDef";
            Column3.HeaderText = "Lit.";
            Column3.Name = "Column3";
            Column3.ReadOnly = true;
            Column3.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column4.DataPropertyName = "EnemyEarthDef";
            Column4.HeaderText = "Earth";
            Column4.Name = "Column4";
            Column4.ReadOnly = true;
            Column4.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column5.DataPropertyName = "EnemyWindDef";
            Column5.HeaderText = "Wind";
            Column5.Name = "Column5";
            Column5.ReadOnly = true;
            Column5.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column6.DataPropertyName = "EnemyWaterDef";
            Column6.HeaderText = "Water";
            Column6.Name = "Column6";
            Column6.ReadOnly = true;
            Column6.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column7.DataPropertyName = "EnemyHolyDef";
            Column7.HeaderText = "Holy";
            Column7.Name = "Column7";
            Column7.ReadOnly = true;
            Column7.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column8.DataPropertyName = "EnemyDarkDef";
            Column8.HeaderText = "Dark";
            Column8.Name = "Column8";
            Column8.ReadOnly = true;
            Column8.SortMode = DataGridViewColumnSortMode.NotSortable;
            Column9.DataPropertyName = "EnemyBioDef";
            Column9.HeaderText = "Bio";
            Column9.Name = "Column9";
            Column9.ReadOnly = true;
            Column9.SortMode = DataGridViewColumnSortMode.NotSortable;
            textStatusVuln.BackColor = Color.White;
            textStatusVuln.BorderStyle = BorderStyle.None;
            textStatusVuln.Location = new Point(87, 143);
            textStatusVuln.Name = "textStatusVuln";
            textStatusVuln.ReadOnly = true;
            textStatusVuln.Size = new Size(895, 13);
            textStatusVuln.TabIndex = 22;
            comboBoxPhaseSelection.FormattingEnabled = true;
            comboBoxPhaseSelection.Location = new Point(52, 19);
            comboBoxPhaseSelection.Name = "comboBoxPhaseSelection";
            comboBoxPhaseSelection.Size = new Size(204, 21);
            comboBoxPhaseSelection.TabIndex = 22;
            comboBoxPhaseSelection.SelectedIndexChanged += comboBoxPhaseSelection_SelectedIndexChanged;
            comboBoxPhaseSelection.DropDownClosed += comboBoxPhaseSelection_DropDownClosed;
            labelPhaseSelection.AutoSize = true;
            labelPhaseSelection.Location = new Point(6, 22);
            labelPhaseSelection.Name = "labelPhaseSelection";
            labelPhaseSelection.Size = new Size(40, 13);
            labelPhaseSelection.TabIndex = 22;
            labelPhaseSelection.Text = "Phase:";
            toolTip1.AutomaticDelay = 300;
            AutoScaleDimensions = new SizeF(6f, 13f);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBoxEnemyPhase);
            Controls.Add(enemyInfoGroupBox);
            Name = nameof(FFRKViewEnemyDetails);
            Size = new Size(1000, 800);
            Load += FFRKViewEnemyDetails_Load;
            enemyInfoGroupBox.ResumeLayout(false);
            enemyInfoGroupBox.PerformLayout();
            groupBoxEnemyAppearances.ResumeLayout(false);
            ((ISupportInitialize) pictureBox1).EndInit();
            groupBoxAIArguments.ResumeLayout(false);
            groupBoxEnemyPhase.ResumeLayout(false);
            groupBoxEnemyPhase.PerformLayout();
            groupBoxAbilityDetails.ResumeLayout(false);
            groupBoxCounters.ResumeLayout(false);
            groupBoxEnemyAbilities.ResumeLayout(false);
            groupBoxEnemyConstraints.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            ((ISupportInitialize) dataGridViewBreaks).EndInit();
            groupBox2.ResumeLayout(false);
            ((ISupportInitialize) dataGridViewStats).EndInit();
            groupBox1.ResumeLayout(false);
            ((ISupportInitialize) dataGridViewElemental).EndInit();
            ResumeLayout(false);
        }
    }
}