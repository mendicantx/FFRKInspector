// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.FFRKViewGacha2
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.DataCache;
using FFRKInspector.DataCache.Banners;
using FFRKInspector.GameData;
using FFRKInspector.Proxy;
using Fiddler;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  public class FFRKViewGacha2 : UserControl
  {
    private Dictionary<int, BasicFeaturedItemInfo> myFeaturedItems;
    private Dictionary<long, BasicRelicDropInfo> myRelicDrops;
    private Dictionary<uint, FilteredRelicDropInfo> myConsolidatedRelicDrops;
    private FFRKDataCacheTable<Key, Data> myBanners;
    private FFRKViewGacha2.MiscDropInfo myMiscInfo = new FFRKViewGacha2.MiscDropInfo();
    private IContainer components = (IContainer) null;
    private ListViewEx2 listViewEx2BannerSelection;
    private ColumnHeader columnBannerID;
    private GroupBox groupBox1;
    private ImageList imageListBanners;
    private ColumnHeader columnServer;
    private ColumnHeader columnBannerImg;
    private GroupBox groupBox2;
    private ListViewEx2 listViewExRecordedRelics;
    private Label label1;
    private Button buttonSearch;
    private Label label2;
    private ComboBox comboBoxPullTypeFilter;
    private ColumnHeader columnItem;
    private ColumnHeader columnRarity;
    private ColumnHeader columnDrawRate;
    private ColumnHeader columnTotal;
    private ColumnHeader columnTotalDraws;
    private ColumnHeader columnReportedRate;

    public FFRKViewGacha2()
    {
      this.InitializeComponent();
      this.comboBoxPullTypeFilter.SelectedIndex = 0;
    }

    private void FFRKViewGacha2_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      if (FFRKProxy.Instance != null)
      {
        FFRKProxy.Instance.OnGachaSeriesList += new FFRKProxy.GachaSeriesListDelegate(this.FFRKProxy_OnGachaSeriesList);
        this.myBanners = FFRKProxy.Instance.Cache.Banners;
        this.PopulateBannerList();
      }
      else
      {
        this.myBanners = (FFRKDataCacheTable<Key, Data>) null;
        this.PopulateBannerList();
      }
    }

    private void FFRKProxy_OnGachaSeriesList()
    {
    }

    private void PopulateBannerList()
    {
      this.listViewEx2BannerSelection.Items.Clear();
      this.myMiscInfo = new FFRKViewGacha2.MiscDropInfo();
      if (this.myBanners == null || this.myBanners.ToList<KeyValuePair<Key, Data>>().Count == 0)
        return;
      foreach (KeyValuePair<Key, Data> banner in this.myBanners)
        this.listViewEx2BannerSelection.Items.Add(new ListViewItem(new string[3]
        {
          banner.Value.BannerId.ToString(),
          banner.Value.isJP ? "JP" : "GL",
          banner.Value.BannerName
        }));
      foreach (ColumnHeader column in this.listViewEx2BannerSelection.Columns)
        column.Width = -2;
    }

    private void buttonSearch_Click(object sender, EventArgs e)
    {
      this.listViewExRecordedRelics.Items.Clear();
      if (this.listViewEx2BannerSelection.SelectedItems.Count != 1 || this.listViewEx2BannerSelection.SelectedItems[0] == null)
        return;
      ListViewItem selectedItem = this.listViewEx2BannerSelection.SelectedItems[0];
      uint BannerID;
      bool isJP;
      try
      {
        BannerID = uint.Parse(selectedItem.SubItems[0].Text);
        isJP = selectedItem.SubItems[1].Text.Equals("JP");
      }
      catch
      {
        FiddlerApplication.Log.LogString("error parsing banner list view object");
        return;
      }
      DbOpLoadFeaturedItems loadFeaturedItems = new DbOpLoadFeaturedItems(BannerID, isJP);
      loadFeaturedItems.OnRequestComplete += new DbOpLoadFeaturedItems.DataReadyCallback(this.RequestFeaturedItems_OnRequestComplete);
    }

    private void RequestFeaturedItems_OnRequestComplete(Dictionary<int, BasicFeaturedItemInfo> items)
    {
      this.myFeaturedItems = items;
      if (this.listViewEx2BannerSelection.SelectedItems.Count != 1 || this.listViewEx2BannerSelection.SelectedItems[0] == null)
        return;
      ListViewItem selectedItem = this.listViewEx2BannerSelection.SelectedItems[0];
      uint BannerID;
      bool isJP;
      try
      {
        BannerID = uint.Parse(selectedItem.SubItems[0].Text);
        isJP = selectedItem.SubItems[1].Text.Equals("JP");
      }
      catch
      {
        FiddlerApplication.Log.LogString("error parsing banner list view object");
        return;
      }
      DbOpFilterDraws dbOpFilterDraws = new DbOpFilterDraws(BannerID, isJP);
      dbOpFilterDraws.OnRequestComplete += new DbOpFilterDraws.DataReadyCallback(this.RequestRelicDraws_OnRequestComplete);
    }

    private void RequestRelicDraws_OnRequestComplete(Dictionary<long, BasicRelicDropInfo> draws)
    {
      this.myRelicDrops = draws;
      this.ConsolidateRelicDrawList(this.myRelicDrops);
      this.RebuildRelicDrawList();
    }

    private void ConsolidateRelicDrawList(Dictionary<long, BasicRelicDropInfo> draws)
    {
      this.myMiscInfo = new FFRKViewGacha2.MiscDropInfo();
      bool flag1 = true;
      if (this.comboBoxPullTypeFilter.SelectedIndex == 1)
        flag1 = false;
      bool flag2 = true;
      if (this.comboBoxPullTypeFilter.SelectedIndex == 0)
        flag2 = false;
      Dictionary<uint, FilteredRelicDropInfo> dictionary1 = new Dictionary<uint, FilteredRelicDropInfo>();
      Dictionary<long, bool> dictionary2 = new Dictionary<long, bool>();
      foreach (BasicRelicDropInfo basicRelicDropInfo in draws.Values)
      {
        BasicRelicDropInfo dropInfo = basicRelicDropInfo;
        if ((dropInfo.ItemTotal != (ushort) 11 || flag1) && (dropInfo.ItemTotal == (ushort) 11 || flag2))
        {
          long key = this.HashTimeAndUser(dropInfo.ServerTime, dropInfo.UserHash);
          if (!dictionary2.ContainsKey(key))
          {
            this.myMiscInfo.totalPulls += (int) dropInfo.ItemTotal;
            dictionary2.Add(key, true);
          }
          switch (dropInfo.Rarity)
          {
            case 1:
              this.myMiscInfo.count1 += (int) dropInfo.DropNum;
              break;
            case 2:
              this.myMiscInfo.count2 += (int) dropInfo.DropNum;
              break;
            case 3:
              this.myMiscInfo.count3 += (int) dropInfo.DropNum;
              break;
            case 4:
              this.myMiscInfo.count4 += (int) dropInfo.DropNum;
              break;
            case 5:
              this.myMiscInfo.count5 += (int) dropInfo.DropNum;
              break;
            case 6:
              this.myMiscInfo.count6 += (int) dropInfo.DropNum;
              break;
            case 7:
              this.myMiscInfo.count7 += (int) dropInfo.DropNum;
              break;
          }
          if (dropInfo.Rarity > (byte) 4)
          {
            if (dictionary1.ContainsKey(dropInfo.ItemID))
              dictionary1[dropInfo.ItemID].DropNum += dropInfo.DropNum;
            else if (this.myFeaturedItems.Values.ToList<BasicFeaturedItemInfo>().Exists((Predicate<BasicFeaturedItemInfo>) (x => (int) x.ItemID == (int) dropInfo.ItemID)))
            {
              FilteredRelicDropInfo filteredRelicDropInfo = new FilteredRelicDropInfo();
              filteredRelicDropInfo.DropNum = dropInfo.DropNum;
              filteredRelicDropInfo.ItemID = dropInfo.ItemID;
              filteredRelicDropInfo.ItemName = dropInfo.ItemName;
              filteredRelicDropInfo.Rarity = dropInfo.Rarity;
              BasicFeaturedItemInfo featuredItemInfo = this.myFeaturedItems.Values.First<BasicFeaturedItemInfo>((Func<BasicFeaturedItemInfo, bool>) (x => (int) x.ItemID == (int) dropInfo.ItemID));
              filteredRelicDropInfo.DisplayOrder = featuredItemInfo.DisplayOrder;
              filteredRelicDropInfo.AssumedRate = featuredItemInfo.Rate;
              dictionary1.Add(dropInfo.ItemID, filteredRelicDropInfo);
            }
            else
            {
              switch (dropInfo.Rarity)
              {
                case 5:
                  this.myMiscInfo.offBanner5count += (int) dropInfo.DropNum;
                  break;
                case 6:
                  this.myMiscInfo.offBanner6count += (int) dropInfo.DropNum;
                  break;
                case 7:
                  this.myMiscInfo.offBanner7count += (int) dropInfo.DropNum;
                  break;
              }
            }
          }
        }
      }
      FiddlerApplication.Log.LogString(dictionary1.Values.Count.ToString());
      this.myConsolidatedRelicDrops = dictionary1;
    }

    private long HashTimeAndUser(ulong time, int user) => (1637L * 5479L + (long) time) * 5479L + (long) user;

    private void RebuildRelicDrawList()
    {
      this.listViewExRecordedRelics.Items.Clear();
      IEnumerable<FilteredRelicDropInfo> filteredRelicDropInfos = (IEnumerable<FilteredRelicDropInfo>) this.myConsolidatedRelicDrops.Values.ToList<FilteredRelicDropInfo>().OrderBy<FilteredRelicDropInfo, int>((Func<FilteredRelicDropInfo, int>) (x => x.DisplayOrder));
      int totalPulls = this.myMiscInfo.totalPulls;
      foreach (FilteredRelicDropInfo filteredRelicDropInfo in filteredRelicDropInfos)
        this.listViewExRecordedRelics.Items.Add(new ListViewItem(new string[6]
        {
          filteredRelicDropInfo.ItemName,
          filteredRelicDropInfo.Rarity.ToString(),
          ((double) filteredRelicDropInfo.DropNum * 100.0 / (double) totalPulls).ToString("N5") + "%",
          filteredRelicDropInfo.DropNum.ToString(),
          totalPulls.ToString(),
          filteredRelicDropInfo.AssumedRate.HasValue ? filteredRelicDropInfo.AssumedRate.Value.ToString("N5") + "%" : "Unrecorded (tap \"Relics\" on the banner screen to record this info"
        }));
      for (int index = 7; index >= 1; --index)
      {
        string str = "";
        int num1 = 0;
        int num2 = index;
        switch (index)
        {
          case 1:
            str = "1* Relic";
            num1 = this.myMiscInfo.count1;
            break;
          case 2:
            str = "2* Relic";
            num1 = this.myMiscInfo.count2;
            break;
          case 3:
            str = "3* Relic";
            num1 = this.myMiscInfo.count3;
            break;
          case 4:
            str = "4* Relic";
            num1 = this.myMiscInfo.count4;
            break;
          case 5:
            str = "5* Off-Banner";
            num1 = this.myMiscInfo.offBanner5count;
            break;
          case 6:
            str = "6* Off-Banner";
            num1 = this.myMiscInfo.offBanner6count;
            break;
          case 7:
            str = "7* Off-Banner";
            num1 = this.myMiscInfo.offBanner7count;
            break;
        }
        if (num1 != 0)
          this.listViewExRecordedRelics.Items.Add(new ListViewItem(new string[6]
          {
            str,
            num2.ToString(),
            ((double) num1 * 100.0 / (double) totalPulls).ToString("N5") + "%",
            num1.ToString(),
            totalPulls.ToString(),
            ""
          }));
      }
      foreach (ColumnHeader column in this.listViewExRecordedRelics.Columns)
        column.Width = -2;
    }

    private void label1_Click(object sender, EventArgs e)
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
      this.components = (IContainer) new Container();
      this.listViewEx2BannerSelection = new ListViewEx2();
      this.columnBannerID = new ColumnHeader();
      this.columnServer = new ColumnHeader();
      this.columnBannerImg = new ColumnHeader();
      this.groupBox1 = new GroupBox();
      this.imageListBanners = new ImageList(this.components);
      this.groupBox2 = new GroupBox();
      this.label2 = new Label();
      this.comboBoxPullTypeFilter = new ComboBox();
      this.buttonSearch = new Button();
      this.listViewExRecordedRelics = new ListViewEx2();
      this.columnItem = new ColumnHeader();
      this.columnRarity = new ColumnHeader();
      this.columnDrawRate = new ColumnHeader();
      this.columnTotal = new ColumnHeader();
      this.columnTotalDraws = new ColumnHeader();
      this.columnReportedRate = new ColumnHeader();
      this.label1 = new Label();
      this.groupBox1.SuspendLayout();
      this.groupBox2.SuspendLayout();
      this.SuspendLayout();
      this.listViewEx2BannerSelection.Columns.AddRange(new ColumnHeader[3]
      {
        this.columnBannerID,
        this.columnServer,
        this.columnBannerImg
      });
      this.listViewEx2BannerSelection.DataBinding = (IListViewBinding) null;
      this.listViewEx2BannerSelection.Dock = DockStyle.Fill;
      this.listViewEx2BannerSelection.FullRowSelect = true;
      this.listViewEx2BannerSelection.HideSelection = false;
      this.listViewEx2BannerSelection.Location = new Point(3, 16);
      this.listViewEx2BannerSelection.MultiSelect = false;
      this.listViewEx2BannerSelection.Name = "listViewEx2BannerSelection";
      this.listViewEx2BannerSelection.SettingsKey = (string) null;
      this.listViewEx2BannerSelection.Size = new Size(563, 184);
      this.listViewEx2BannerSelection.TabIndex = 0;
      this.listViewEx2BannerSelection.UseCompatibleStateImageBehavior = false;
      this.listViewEx2BannerSelection.View = View.Details;
      this.columnBannerID.Text = "Banner ID";
      this.columnServer.Text = "Server";
      this.columnBannerImg.Text = "Banner Name";
      this.columnBannerImg.Width = 80;
      this.groupBox1.Controls.Add((Control) this.listViewEx2BannerSelection);
      this.groupBox1.Location = new Point(3, 41);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(569, 203);
      this.groupBox1.TabIndex = 1;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Banner Selection";
      this.imageListBanners.ColorDepth = ColorDepth.Depth8Bit;
      this.imageListBanners.ImageSize = new Size(16, 16);
      this.imageListBanners.TransparentColor = Color.Transparent;
      this.groupBox2.Controls.Add((Control) this.label2);
      this.groupBox2.Controls.Add((Control) this.comboBoxPullTypeFilter);
      this.groupBox2.Controls.Add((Control) this.buttonSearch);
      this.groupBox2.Controls.Add((Control) this.listViewExRecordedRelics);
      this.groupBox2.Location = new Point(6, 250);
      this.groupBox2.Name = "groupBox2";
      this.groupBox2.Size = new Size(812, 362);
      this.groupBox2.TabIndex = 1;
      this.groupBox2.TabStop = false;
      this.groupBox2.Text = "Recorded Pulls";
      this.label2.AutoSize = true;
      this.label2.Location = new Point(210, 24);
      this.label2.Name = "label2";
      this.label2.Size = new Size(32, 13);
      this.label2.TabIndex = 4;
      this.label2.Text = "Filter:";
      this.comboBoxPullTypeFilter.FormattingEnabled = true;
      this.comboBoxPullTypeFilter.Items.AddRange(new object[3]
      {
        (object) "11-pulls only",
        (object) "Everything except 11-pulls",
        (object) "All pulls"
      });
      this.comboBoxPullTypeFilter.Location = new Point(248, 21);
      this.comboBoxPullTypeFilter.Name = "comboBoxPullTypeFilter";
      this.comboBoxPullTypeFilter.Size = new Size(121, 21);
      this.comboBoxPullTypeFilter.TabIndex = 3;
      this.buttonSearch.Location = new Point(6, 19);
      this.buttonSearch.Name = "buttonSearch";
      this.buttonSearch.Size = new Size(198, 23);
      this.buttonSearch.TabIndex = 1;
      this.buttonSearch.Text = "Retrieve draws on selected banner";
      this.buttonSearch.UseVisualStyleBackColor = true;
      this.buttonSearch.Click += new EventHandler(this.buttonSearch_Click);
      this.listViewExRecordedRelics.Columns.AddRange(new ColumnHeader[6]
      {
        this.columnItem,
        this.columnRarity,
        this.columnDrawRate,
        this.columnTotal,
        this.columnTotalDraws,
        this.columnReportedRate
      });
      this.listViewExRecordedRelics.DataBinding = (IListViewBinding) null;
      this.listViewExRecordedRelics.Dock = DockStyle.Bottom;
      this.listViewExRecordedRelics.Location = new Point(3, 48);
      this.listViewExRecordedRelics.Name = "listViewExRecordedRelics";
      this.listViewExRecordedRelics.SettingsKey = (string) null;
      this.listViewExRecordedRelics.Size = new Size(806, 311);
      this.listViewExRecordedRelics.TabIndex = 0;
      this.listViewExRecordedRelics.UseCompatibleStateImageBehavior = false;
      this.listViewExRecordedRelics.View = View.Details;
      this.columnItem.Text = "Item";
      this.columnRarity.Text = "Rarity";
      this.columnDrawRate.Text = "Recorded Rate";
      this.columnDrawRate.Width = 91;
      this.columnTotal.Text = "Number Drawn";
      this.columnTotal.Width = 99;
      this.columnTotalDraws.Text = "Total Recorded Draws";
      this.columnTotalDraws.Width = 131;
      this.columnReportedRate.Text = "Assumed Base Rate";
      this.columnReportedRate.Width = 113;
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 12f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(18, 9);
      this.label1.Name = "label1";
      this.label1.Size = new Size(167, 20);
      this.label1.TabIndex = 2;
      this.label1.Text = "Recorded Relic Draws";
      this.label1.Click += new EventHandler(this.label1_Click);
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.groupBox2);
      this.Controls.Add((Control) this.groupBox1);
      this.Name = nameof (FFRKViewGacha2);
      this.Size = new Size(1000, 800);
      this.Load += new EventHandler(this.FFRKViewGacha2_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox2.ResumeLayout(false);
      this.groupBox2.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    private class MiscDropInfo
    {
      public int totalPulls;
      public int offBanner5count = 0;
      public int offBanner6count = 0;
      public int offBanner7count = 0;
      public int count1 = 0;
      public int count2 = 0;
      public int count3 = 0;
      public int count4 = 0;
      public int count5 = 0;
      public int count6 = 0;
      public int count7 = 0;
    }
  }
}
