// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.FFRKViewGacha
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using FFRKInspector.Proxy;
using FFRKInspector.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  public class FFRKViewGacha : UserControl
  {
    private IContainer components = (IContainer) null;
    private ListViewColumnSorter mSorter;
    private ListView listViewGachaItems;
    private GroupBox groupBox1;
    private ColumnHeader columnHeaderName;
    private ColumnHeader columnHeaderRarity;
    private ColumnHeader columnHeaderType;
    private ColumnHeader columnHeaderSynergy;
    private ColumnHeader columnHeaderProb;
    private ColumnHeader columnHeaderSoulStrike;
    private TableLayoutPanel tableLayoutPanel1;
    private GroupBox groupBox2;
    private TableLayoutPanel tableLayoutPanel2;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label5;
    private Label label4;
    private Label labelFiveStarPct;
    private Label labelFourStarPct;
    private Label labelThreeStarPct;
    private Label labelTwoStarPct;
    private Label labelOneStarPct;
    private Label labelRelics;
    private ComboBox comboBoxGachaSeries;
    private Label labelGachaSeries;

    public FFRKViewGacha() => this.InitializeComponent();

    private void listView1_SizeChanged(object sender, EventArgs e)
    {
      this.labelRelics.Left = this.listViewGachaItems.Left + 10;
      this.labelRelics.Width = this.listViewGachaItems.Width - 20;
    }

    private void FFRKViewGacha_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.mSorter = new ListViewColumnSorter();
      this.mSorter.AddSorter<int>(1);
      this.mSorter.AddSorter<int>(3);
      this.mSorter.AddSorter<float>(4, (IConverter<string, string>) new FFRKViewGacha.PercentRemover());
      this.listViewGachaItems.ListViewItemSorter = (IComparer) this.mSorter;
      FFRKProxy.Instance.OnGachaStats += new FFRKProxy.GachaStatsDelegate(this.FFRKProxy_OnGachaStats);
    }

    private void FFRKProxy_OnGachaStats(DataGachaSeriesItemsForEntryPoints gacha) => this.BeginInvoke((Action) (() => this.DoUpdateGachaInformation(gacha)));

    private void DoUpdateGachaInformation(DataGachaSeriesItemsForEntryPoints gacha)
    {
      this.comboBoxGachaSeries.Items.Clear();
      this.listViewGachaItems.Items.Clear();
      foreach (KeyValuePair<uint, DataGachaSeriesItemsForEntryPoints.ItemsForEntryPoint> gacha1 in gacha.Gachas)
        this.comboBoxGachaSeries.Items.Add((object) new FFRKViewGacha.GachaComboBoxEntry()
        {
          EntryPointId = gacha1.Key,
          EntryPoint = gacha1.Value.EntryPoint,
          SeriesData = gacha1.Value.ItemDetails
        });
      if (this.comboBoxGachaSeries.Items.Count <= 0)
        return;
      this.comboBoxGachaSeries.SelectedIndex = 0;
    }

    private void comboBoxGachaSeries_SelectedIndexChanged(object sender, EventArgs e)
    {
      FFRKViewGacha.GachaComboBoxEntry selectedItem = (FFRKViewGacha.GachaComboBoxEntry) this.comboBoxGachaSeries.SelectedItem;
      this.labelOneStarPct.Text = selectedItem.SeriesData.ProbabilityByRarity.OneStar.Value.ToString("R") + "%";
      this.labelTwoStarPct.Text = selectedItem.SeriesData.ProbabilityByRarity.TwoStar.Value.ToString("R") + "%";
      this.labelThreeStarPct.Text = selectedItem.SeriesData.ProbabilityByRarity.ThreeStar.Value.ToString("R") + "%";
      this.labelFourStarPct.Text = selectedItem.SeriesData.ProbabilityByRarity.FourStar.Value.ToString("R") + "%";
      this.labelFiveStarPct.Text = selectedItem.SeriesData.ProbabilityByRarity.FiveStar.Value.ToString("R") + "%";
      this.labelRelics.Visible = false;
      this.listViewGachaItems.Items.Clear();
      foreach (DataGachaItem dataGachaItem in selectedItem.SeriesData.Items)
      {
        uint number = FFRKInspector.Utility.Utility.RomanNumeralToNumber(Regex.Match(dataGachaItem.ItemName, "\\((.*)\\)").Groups[1].Value);
        this.listViewGachaItems.Items.Add(new ListViewItem(new string[6]
        {
          dataGachaItem.ItemName,
          dataGachaItem.Rarity.ToString(),
          dataGachaItem.Category,
          number.ToString(),
          dataGachaItem.Probability.ToString("R") + "%",
          dataGachaItem.HasSoulBreak ? "YES" : string.Empty
        }));
      }
    }

    private void listViewGachaItems_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      if (e.Column == this.mSorter.SortColumn)
      {
        if (this.mSorter.Order == SortOrder.Ascending)
          this.mSorter.Order = SortOrder.Descending;
        else if (this.mSorter.Order == SortOrder.Descending)
          this.mSorter.Order = SortOrder.Ascending;
      }
      else
      {
        this.mSorter.SortColumn = e.Column;
        this.mSorter.Order = SortOrder.Ascending;
      }
      this.listViewGachaItems.Sort();
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.listViewGachaItems = new ListView();
      this.columnHeaderName = new ColumnHeader();
      this.columnHeaderRarity = new ColumnHeader();
      this.columnHeaderType = new ColumnHeader();
      this.columnHeaderSynergy = new ColumnHeader();
      this.columnHeaderProb = new ColumnHeader();
      this.columnHeaderSoulStrike = new ColumnHeader();
      this.groupBox1 = new GroupBox();
      this.comboBoxGachaSeries = new ComboBox();
      this.labelGachaSeries = new Label();
      this.labelRelics = new Label();
      this.tableLayoutPanel1 = new TableLayoutPanel();
      this.groupBox2 = new GroupBox();
      this.tableLayoutPanel2 = new TableLayoutPanel();
      this.labelFiveStarPct = new Label();
      this.labelFourStarPct = new Label();
      this.labelThreeStarPct = new Label();
      this.labelTwoStarPct = new Label();
      this.label1 = new Label();
      this.label5 = new Label();
      this.label2 = new Label();
      this.label4 = new Label();
      this.label3 = new Label();
      this.labelOneStarPct = new Label();
      this.groupBox1.SuspendLayout();
      this.tableLayoutPanel1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.SuspendLayout();
      this.listViewGachaItems.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.listViewGachaItems.Columns.AddRange(new ColumnHeader[6]
      {
        this.columnHeaderName,
        this.columnHeaderRarity,
        this.columnHeaderType,
        this.columnHeaderSynergy,
        this.columnHeaderProb,
        this.columnHeaderSoulStrike
      });
      this.listViewGachaItems.Location = new Point(15, 46);
      this.listViewGachaItems.Name = "listViewGachaItems";
      this.listViewGachaItems.Size = new Size(622, 386);
      this.listViewGachaItems.TabIndex = 0;
      this.listViewGachaItems.UseCompatibleStateImageBehavior = false;
      this.listViewGachaItems.View = View.Details;
      this.listViewGachaItems.ColumnClick += new ColumnClickEventHandler(this.listViewGachaItems_ColumnClick);
      this.listViewGachaItems.SizeChanged += new EventHandler(this.listView1_SizeChanged);
      this.columnHeaderName.Text = "Name";
      this.columnHeaderName.Width = 136;
      this.columnHeaderRarity.Text = "Rarity";
      this.columnHeaderRarity.Width = 72;
      this.columnHeaderType.Text = "Type";
      this.columnHeaderType.Width = 70;
      this.columnHeaderSynergy.Text = "Synergy";
      this.columnHeaderProb.Text = "Probability";
      this.columnHeaderSoulStrike.Text = "Soul Strike";
      this.columnHeaderSoulStrike.Width = 141;
      this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox1.Controls.Add((Control) this.comboBoxGachaSeries);
      this.groupBox1.Controls.Add((Control) this.labelGachaSeries);
      this.groupBox1.Controls.Add((Control) this.labelRelics);
      this.groupBox1.Controls.Add((Control) this.listViewGachaItems);
      this.groupBox1.Location = new Point((int) byte.MaxValue, 3);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(649, 441);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Items";
      this.comboBoxGachaSeries.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBoxGachaSeries.FormattingEnabled = true;
      this.comboBoxGachaSeries.Location = new Point(88, 19);
      this.comboBoxGachaSeries.Name = "comboBoxGachaSeries";
      this.comboBoxGachaSeries.Size = new Size(218, 21);
      this.comboBoxGachaSeries.TabIndex = 3;
      this.comboBoxGachaSeries.SelectedIndexChanged += new EventHandler(this.comboBoxGachaSeries_SelectedIndexChanged);
      this.labelGachaSeries.AutoSize = true;
      this.labelGachaSeries.Location = new Point(21, 24);
      this.labelGachaSeries.Name = "labelGachaSeries";
      this.labelGachaSeries.Size = new Size(61, 13);
      this.labelGachaSeries.TabIndex = 2;
      this.labelGachaSeries.Text = "Entry Point:";
      this.labelRelics.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelRelics.Location = new Point(15, 193);
      this.labelRelics.Name = "labelRelics";
      this.labelRelics.Size = new Size(622, 49);
      this.labelRelics.TabIndex = 1;
      this.labelRelics.Text = "Tap \"Relics\" followed by a specific relic banner, and then the \"Relics\"  button above the text \"About rarity\" to view information for that banner.";
      this.labelRelics.TextAlign = ContentAlignment.MiddleCenter;
      this.tableLayoutPanel1.ColumnCount = 2;
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 27.7839f));
      this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 72.21609f));
      this.tableLayoutPanel1.Controls.Add((Control) this.groupBox1, 1, 0);
      this.tableLayoutPanel1.Controls.Add((Control) this.groupBox2, 0, 0);
      this.tableLayoutPanel1.Dock = DockStyle.Fill;
      this.tableLayoutPanel1.Location = new Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 2;
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
      this.tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute, 22f));
      this.tableLayoutPanel1.Size = new Size(907, 469);
      this.tableLayoutPanel1.TabIndex = 2;
      this.groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox2.Controls.Add((Control) this.tableLayoutPanel2);
      this.groupBox2.Location = new Point(3, 3);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(246, 441);
      this.groupBox2.TabIndex = 2;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Probability by Rarity";
      this.tableLayoutPanel2.ColumnCount = 2;
      this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57.69231f));
      this.tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 42.30769f));
      this.tableLayoutPanel2.Controls.Add((Control) this.labelFiveStarPct, 1, 4);
      this.tableLayoutPanel2.Controls.Add((Control) this.labelFourStarPct, 1, 3);
      this.tableLayoutPanel2.Controls.Add((Control) this.labelThreeStarPct, 1, 2);
      this.tableLayoutPanel2.Controls.Add((Control) this.labelTwoStarPct, 1, 1);
      this.tableLayoutPanel2.Controls.Add((Control) this.label1, 0, 0);
      this.tableLayoutPanel2.Controls.Add((Control) this.label5, 0, 4);
      this.tableLayoutPanel2.Controls.Add((Control) this.label2, 0, 1);
      this.tableLayoutPanel2.Controls.Add((Control) this.label4, 0, 3);
      this.tableLayoutPanel2.Controls.Add((Control) this.label3, 0, 2);
      this.tableLayoutPanel2.Controls.Add((Control) this.labelOneStarPct, 1, 0);
      this.tableLayoutPanel2.Location = new Point(6, 25);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 5;
      this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 20f));
      this.tableLayoutPanel2.Size = new Size(234, 192);
      this.tableLayoutPanel2.TabIndex = 5;
      this.labelFiveStarPct.Anchor = AnchorStyles.None;
      this.labelFiveStarPct.AutoSize = true;
      this.labelFiveStarPct.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelFiveStarPct.Location = new Point(154, 160);
      this.labelFiveStarPct.Name = "labelFiveStarPct";
      this.labelFiveStarPct.Size = new Size(60, 24);
      this.labelFiveStarPct.TabIndex = 9;
      this.labelFiveStarPct.Text = "0.00%";
      this.labelFourStarPct.Anchor = AnchorStyles.None;
      this.labelFourStarPct.AutoSize = true;
      this.labelFourStarPct.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelFourStarPct.Location = new Point(154, 121);
      this.labelFourStarPct.Name = "labelFourStarPct";
      this.labelFourStarPct.Size = new Size(60, 24);
      this.labelFourStarPct.TabIndex = 8;
      this.labelFourStarPct.Text = "0.00%";
      this.labelThreeStarPct.Anchor = AnchorStyles.None;
      this.labelThreeStarPct.AutoSize = true;
      this.labelThreeStarPct.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelThreeStarPct.Location = new Point(154, 83);
      this.labelThreeStarPct.Name = "labelThreeStarPct";
      this.labelThreeStarPct.Size = new Size(60, 24);
      this.labelThreeStarPct.TabIndex = 7;
      this.labelThreeStarPct.Text = "0.00%";
      this.labelTwoStarPct.Anchor = AnchorStyles.None;
      this.labelTwoStarPct.AutoSize = true;
      this.labelTwoStarPct.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelTwoStarPct.Location = new Point(154, 45);
      this.labelTwoStarPct.Name = "labelTwoStarPct";
      this.labelTwoStarPct.Size = new Size(60, 24);
      this.labelTwoStarPct.TabIndex = 6;
      this.labelTwoStarPct.Text = "0.00%";
      this.label1.Anchor = AnchorStyles.Right;
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(103, 7);
      this.label1.Name = "label1";
      this.label1.Size = new Size(29, 24);
      this.label1.TabIndex = 1;
      this.label1.Text = "★";
      this.label5.Anchor = AnchorStyles.Right;
      this.label5.AutoSize = true;
      this.label5.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label5.Location = new Point(27, 160);
      this.label5.Name = "label5";
      this.label5.Size = new Size(105, 24);
      this.label5.TabIndex = 4;
      this.label5.Text = "★★★★★";
      this.label2.Anchor = AnchorStyles.Right;
      this.label2.AutoSize = true;
      this.label2.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label2.Location = new Point(84, 45);
      this.label2.Name = "label2";
      this.label2.Size = new Size(48, 24);
      this.label2.TabIndex = 2;
      this.label2.Text = "★★";
      this.label4.Anchor = AnchorStyles.Right;
      this.label4.AutoSize = true;
      this.label4.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label4.Location = new Point(46, 121);
      this.label4.Name = "label4";
      this.label4.Size = new Size(86, 24);
      this.label4.TabIndex = 3;
      this.label4.Text = "★★★★";
      this.label3.Anchor = AnchorStyles.Right;
      this.label3.AutoSize = true;
      this.label3.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label3.Location = new Point(65, 83);
      this.label3.Name = "label3";
      this.label3.Size = new Size(67, 24);
      this.label3.TabIndex = 2;
      this.label3.Text = "★★★";
      this.labelOneStarPct.Anchor = AnchorStyles.None;
      this.labelOneStarPct.AutoSize = true;
      this.labelOneStarPct.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.labelOneStarPct.Location = new Point(154, 7);
      this.labelOneStarPct.Name = "labelOneStarPct";
      this.labelOneStarPct.Size = new Size(60, 24);
      this.labelOneStarPct.TabIndex = 5;
      this.labelOneStarPct.Text = "0.00%";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.tableLayoutPanel1);
      this.Name = nameof (FFRKViewGacha);
      this.Size = new Size(907, 469);
      this.Load += new EventHandler(this.FFRKViewGacha_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox1.PerformLayout();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.tableLayoutPanel2.ResumeLayout(false);
      this.tableLayoutPanel2.PerformLayout();
      this.ResumeLayout(false);
    }

    private class PercentRemover : IConverter<string, string>
    {
      public string Convert(string u) => u.EndsWith("%") ? u.Substring(0, u.Length - 1) : u;
    }

    private class GachaComboBoxEntry
    {
      public DataGachaSeriesEntryPoint EntryPoint;
      public uint EntryPointId;
      public DataGachaSeriesItemDetails SeriesData;

      public override string ToString()
      {
        if (this.EntryPoint == null)
          return this.EntryPointId.ToString();
        return this.EntryPoint.PayCost == 0U ? "Free" : string.Format("{0} {1} (EntryPointId = {2})", (object) this.EntryPoint.PayCost, (object) this.EntryPoint.CurrencyType.ToString(), (object) this.EntryPoint.EntryPointId);
      }
    }
  }
}
