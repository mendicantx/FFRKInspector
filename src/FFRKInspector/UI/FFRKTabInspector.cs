// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.FFRKTabInspector
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.Proxy;
using FFRKInspector.UI.DatabaseUI;
using Fiddler;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  internal class FFRKTabInspector : UserControl
  {
    private IContainer components = (IContainer) null;
    private TabPage[] mDeveloperTabs;
    private TabControl tabControlFFRKInspector;
    private TabPage tabPageSearch;
    private TabPage tabPageDungeon;
    private TabPage tabPageInventory;
    private TabPage tabPageAbout;
    private StatusStrip statusStrip1;
    private ToolStripStatusLabel toolStripStatusLabel1;
    private ToolStripStatusLabel toolStripStatusLabelConnection;
    private TabPage tabPageDebug;
    private FFRKViewDebugging ffrkViewDebugging1;
    private TabPage tabPageGacha;
    private FFRKViewGacha ffrkViewGacha1;
    private FFRKViewAbout ffrkViewAbout1;
    private FFRKViewActiveDungeon ffrkViewActiveDungeon;
    private FFRKViewItemSearch ffrkViewItemSearch1;
    private TabPage tabPageEditEquipment;
    private FFRKViewDatabase ffrkViewEditItemStats1;
    private TabPage tabPageBattle;
    private FFRKViewActiveBattle ffrkViewActiveBattle;
    private TabPage tabPageEnemyDetails;
    private FFRKViewEnemyDetails ffrkViewEnemyDetails;
    private TabPage tabPageViewGachaVer2;
    private FFRKViewGacha2 ffrkViewGacha2;
    private FFRKViewInventory ffrkViewInventory1;

    public FFRKTabInspector.InspectorPage SelectedPage
    {
      get => this.tabControlFFRKInspector.SelectedTab == null ? FFRKTabInspector.InspectorPage.CurrentDungeon : (FFRKTabInspector.InspectorPage) this.tabControlFFRKInspector.SelectedTab.Tag;
      set
      {
        try
        {
          this.tabControlFFRKInspector.SelectTab(this.tabControlFFRKInspector.TabPages.Cast<TabPage>().First<TabPage>((Func<TabPage, bool>) (x => (FFRKTabInspector.InspectorPage) x.Tag == value)));
        }
        catch (Exception ex)
        {
          FiddlerApplication.Log.LogString(ex.ToString());
        }
      }
    }

    public FFRKViewDatabase DatabaseTab => (FFRKViewDatabase) this.tabPageEditEquipment.Controls[0];

    public FFRKTabInspector()
    {
      this.InitializeComponent();
      this.tabPageAbout.Tag = (object) FFRKTabInspector.InspectorPage.About;
      this.tabPageDungeon.Tag = (object) FFRKTabInspector.InspectorPage.CurrentDungeon;
      this.tabPageBattle.Tag = (object) FFRKTabInspector.InspectorPage.CurrentBattle;
      this.tabPageDebug.Tag = (object) FFRKTabInspector.InspectorPage.Debugging;
      this.tabPageEditEquipment.Tag = (object) FFRKTabInspector.InspectorPage.Database;
      this.tabPageGacha.Tag = (object) FFRKTabInspector.InspectorPage.Gacha;
      this.tabPageInventory.Tag = (object) FFRKTabInspector.InspectorPage.Inventory;
      this.tabPageSearch.Tag = (object) FFRKTabInspector.InspectorPage.ItemSearch;
      this.tabPageEnemyDetails.Tag = (object) FFRKTabInspector.InspectorPage.EnemyDetails;
    }

    private void FFRKTabInspectorView_Load(object sender, EventArgs e)
    {
      this.tabControlFFRKInspector.SelectedIndexChanged += new EventHandler(this.tabControlFFRKInspector_SelectedIndexChanged);
      if (FFRKProxy.Instance == null)
        return;
      FFRKProxy.Instance.Database.OnConnectionStateChanged += new FFRKMySqlInstance.ConnectionStateChangedDelegate(this.Database_OnConnectionStateChanged);
    }

    private void tabControlFFRKInspector_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    private void FFRKTabInspector_HandleDestroyed(object sender, EventArgs e)
    {
    }

    private void Database_OnConnectionStateChanged(FFRKMySqlInstance.ConnectionState NewState)
    {
      if (!this.IsHandleCreated)
        return;
      this.BeginInvoke((Action) (() => this.toolStripStatusLabelConnection.Text = NewState.ToString()));
    }

    private void tabPageInventory_Click(object sender, EventArgs e)
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
      this.tabControlFFRKInspector = new TabControl();
      this.tabPageDungeon = new TabPage();
      this.ffrkViewActiveDungeon = new FFRKViewActiveDungeon();
      this.tabPageBattle = new TabPage();
      this.ffrkViewActiveBattle = new FFRKViewActiveBattle();
      this.tabPageEnemyDetails = new TabPage();
      this.ffrkViewEnemyDetails = new FFRKViewEnemyDetails();
      this.tabPageViewGachaVer2 = new TabPage();
      this.ffrkViewGacha2 = new FFRKViewGacha2();
      this.tabPageSearch = new TabPage();
      this.ffrkViewItemSearch1 = new FFRKViewItemSearch();
      this.tabPageInventory = new TabPage();
      this.ffrkViewInventory1 = new FFRKViewInventory();
      this.tabPageEditEquipment = new TabPage();
      this.ffrkViewEditItemStats1 = new FFRKViewDatabase();
      this.tabPageGacha = new TabPage();
      this.ffrkViewGacha1 = new FFRKViewGacha();
      this.tabPageAbout = new TabPage();
      this.ffrkViewAbout1 = new FFRKViewAbout();
      this.tabPageDebug = new TabPage();
      this.ffrkViewDebugging1 = new FFRKViewDebugging();
      this.statusStrip1 = new StatusStrip();
      this.toolStripStatusLabel1 = new ToolStripStatusLabel();
      this.toolStripStatusLabelConnection = new ToolStripStatusLabel();
      this.tabControlFFRKInspector.SuspendLayout();
      this.tabPageDungeon.SuspendLayout();
      this.tabPageBattle.SuspendLayout();
      this.tabPageEnemyDetails.SuspendLayout();
      this.tabPageViewGachaVer2.SuspendLayout();
      this.tabPageSearch.SuspendLayout();
      this.tabPageInventory.SuspendLayout();
      this.tabPageEditEquipment.SuspendLayout();
      this.tabPageGacha.SuspendLayout();
      this.tabPageAbout.SuspendLayout();
      this.tabPageDebug.SuspendLayout();
      this.statusStrip1.SuspendLayout();
      this.SuspendLayout();
      this.tabControlFFRKInspector.Controls.Add((Control) this.tabPageDungeon);
      this.tabControlFFRKInspector.Controls.Add((Control) this.tabPageBattle);
      this.tabControlFFRKInspector.Controls.Add((Control) this.tabPageEnemyDetails);
      this.tabControlFFRKInspector.Controls.Add((Control) this.tabPageSearch);
      this.tabControlFFRKInspector.Controls.Add((Control) this.tabPageInventory);
      this.tabControlFFRKInspector.Controls.Add((Control) this.tabPageEditEquipment);
      this.tabControlFFRKInspector.Controls.Add((Control) this.tabPageViewGachaVer2);
      this.tabControlFFRKInspector.Controls.Add((Control) this.tabPageAbout);
      this.tabControlFFRKInspector.Controls.Add((Control) this.tabPageDebug);
      this.tabControlFFRKInspector.Dock = DockStyle.Fill;
      this.tabControlFFRKInspector.Location = new Point(0, 0);
      this.tabControlFFRKInspector.Name = "tabControlFFRKInspector";
      this.tabControlFFRKInspector.SelectedIndex = 0;
      this.tabControlFFRKInspector.Size = new Size(991, 800);
      this.tabControlFFRKInspector.TabIndex = 0;
      this.tabPageDungeon.Controls.Add((Control) this.ffrkViewActiveDungeon);
      this.tabPageDungeon.Location = new Point(4, 22);
      this.tabPageDungeon.Name = "tabPageDungeon";
      this.tabPageDungeon.Padding = new Padding(3);
      this.tabPageDungeon.Size = new Size(983, 590);
      this.tabPageDungeon.TabIndex = 1;
      this.tabPageDungeon.Text = "Current Dungeon";
      this.tabPageDungeon.UseVisualStyleBackColor = true;
      this.ffrkViewActiveDungeon.Dock = DockStyle.Fill;
      this.ffrkViewActiveDungeon.Location = new Point(3, 3);
      this.ffrkViewActiveDungeon.Name = "ffrkViewActiveDungeon";
      this.ffrkViewActiveDungeon.Size = new Size(977, 800);
      this.ffrkViewActiveDungeon.TabIndex = 0;
      this.tabPageBattle.Controls.Add((Control) this.ffrkViewActiveBattle);
      this.tabPageBattle.Location = new Point(4, 22);
      this.tabPageBattle.Name = "tabPageBattle";
      this.tabPageBattle.Size = new Size(983, 800);
      this.tabPageBattle.TabIndex = 10;
      this.tabPageBattle.Text = "Current Battle";
      this.tabPageBattle.UseVisualStyleBackColor = true;
      this.tabPageEnemyDetails.Controls.Add((Control) this.ffrkViewEnemyDetails);
      this.tabPageEnemyDetails.Location = new Point(4, 22);
      this.tabPageEnemyDetails.Name = "tabPageEnemyDetails";
      this.tabPageEnemyDetails.Size = new Size(1000, 800);
      this.tabPageEnemyDetails.TabIndex = 0;
      this.tabPageEnemyDetails.Text = "Enemy Details";
      this.tabPageEnemyDetails.UseVisualStyleBackColor = true;
      this.ffrkViewEnemyDetails.Location = new Point(-4, 0);
      this.ffrkViewEnemyDetails.Name = "ffrkViewActiveBattle";
      this.ffrkViewEnemyDetails.Size = new Size(1000, 800);
      this.ffrkViewEnemyDetails.TabIndex = 1;
      this.tabPageViewGachaVer2.Controls.Add((Control) this.ffrkViewGacha2);
      this.tabPageViewGachaVer2.Location = new Point(4, 22);
      this.tabPageViewGachaVer2.Name = "tabPageGacha2";
      this.tabPageViewGachaVer2.TabIndex = 0;
      this.tabPageViewGachaVer2.Text = "Gacha";
      this.tabPageViewGachaVer2.UseVisualStyleBackColor = true;
      this.ffrkViewGacha2.Location = new Point(-4, 0);
      this.ffrkViewGacha2.Name = "ffrkViewGacha2";
      this.ffrkViewGacha2.Size = new Size(1000, 800);
      this.ffrkViewGacha2.TabIndex = 1;
      this.ffrkViewActiveBattle.Location = new Point(-4, 0);
      this.ffrkViewActiveBattle.Name = "ffrkViewActiveBattle";
      this.ffrkViewActiveBattle.Size = new Size(984, 800);
      this.ffrkViewActiveBattle.TabIndex = 0;
      this.tabPageSearch.Controls.Add((Control) this.ffrkViewItemSearch1);
      this.tabPageSearch.Location = new Point(4, 22);
      this.tabPageSearch.Name = "tabPageSearch";
      this.tabPageSearch.Padding = new Padding(3);
      this.tabPageSearch.Size = new Size(983, 590);
      this.tabPageSearch.TabIndex = 0;
      this.tabPageSearch.Text = "Item Search";
      this.tabPageSearch.UseVisualStyleBackColor = true;
      this.ffrkViewItemSearch1.Dock = DockStyle.Fill;
      this.ffrkViewItemSearch1.Location = new Point(3, 3);
      this.ffrkViewItemSearch1.Name = "ffrkViewItemSearch1";
      this.ffrkViewItemSearch1.Size = new Size(977, 584);
      this.ffrkViewItemSearch1.TabIndex = 0;
      this.tabPageInventory.Controls.Add((Control) this.ffrkViewInventory1);
      this.tabPageInventory.Location = new Point(4, 22);
      this.tabPageInventory.Name = "tabPageInventory";
      this.tabPageInventory.Size = new Size(983, 590);
      this.tabPageInventory.TabIndex = 2;
      this.tabPageInventory.Text = "Inventory";
      this.tabPageInventory.UseVisualStyleBackColor = true;
      this.tabPageInventory.Click += new EventHandler(this.tabPageInventory_Click);
      this.ffrkViewInventory1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.ffrkViewInventory1.Location = new Point(-4, 0);
      this.ffrkViewInventory1.Name = "ffrkViewInventory1";
      this.ffrkViewInventory1.Size = new Size(984, 594);
      this.ffrkViewInventory1.TabIndex = 0;
      this.tabPageEditEquipment.Controls.Add((Control) this.ffrkViewEditItemStats1);
      this.tabPageEditEquipment.Location = new Point(4, 22);
      this.tabPageEditEquipment.Name = "tabPageEditEquipment";
      this.tabPageEditEquipment.Size = new Size(983, 590);
      this.tabPageEditEquipment.TabIndex = 9;
      this.tabPageEditEquipment.Text = "Edit Database";
      this.tabPageEditEquipment.UseVisualStyleBackColor = true;
      this.ffrkViewEditItemStats1.DatabaseMode = FFRKViewDatabase.DatabaseModeEnum.EquipmentAndStats;
      this.ffrkViewEditItemStats1.Dock = DockStyle.Fill;
      this.ffrkViewEditItemStats1.Location = new Point(0, 0);
      this.ffrkViewEditItemStats1.Name = "ffrkViewEditItemStats1";
      this.ffrkViewEditItemStats1.Size = new Size(983, 590);
      this.ffrkViewEditItemStats1.TabIndex = 0;
      this.tabPageGacha.Controls.Add((Control) this.ffrkViewGacha1);
      this.tabPageGacha.Location = new Point(4, 22);
      this.tabPageGacha.Name = "tabPageGacha";
      this.tabPageGacha.Size = new Size(983, 590);
      this.tabPageGacha.TabIndex = 8;
      this.tabPageGacha.Text = "Gacha";
      this.tabPageGacha.UseVisualStyleBackColor = true;
      this.ffrkViewGacha1.Dock = DockStyle.Fill;
      this.ffrkViewGacha1.Location = new Point(0, 0);
      this.ffrkViewGacha1.Name = "ffrkViewGacha1";
      this.ffrkViewGacha1.Size = new Size(983, 590);
      this.ffrkViewGacha1.TabIndex = 0;
      this.tabPageAbout.Controls.Add((Control) this.ffrkViewAbout1);
      this.tabPageAbout.Location = new Point(4, 22);
      this.tabPageAbout.Name = "tabPageAbout";
      this.tabPageAbout.Size = new Size(983, 590);
      this.tabPageAbout.TabIndex = 5;
      this.tabPageAbout.Text = "About";
      this.tabPageAbout.UseVisualStyleBackColor = true;
      this.ffrkViewAbout1.Dock = DockStyle.Fill;
      this.ffrkViewAbout1.Location = new Point(0, 0);
      this.ffrkViewAbout1.Name = "ffrkViewAbout1";
      this.ffrkViewAbout1.Size = new Size(983, 590);
      this.ffrkViewAbout1.TabIndex = 0;
      this.tabPageDebug.Controls.Add((Control) this.ffrkViewDebugging1);
      this.tabPageDebug.Location = new Point(4, 22);
      this.tabPageDebug.Name = "tabPageDebug";
      this.tabPageDebug.Size = new Size(983, 590);
      this.tabPageDebug.TabIndex = 7;
      this.tabPageDebug.Text = "Debugging";
      this.tabPageDebug.UseVisualStyleBackColor = true;
      this.ffrkViewDebugging1.Dock = DockStyle.Fill;
      this.ffrkViewDebugging1.Location = new Point(0, 0);
      this.ffrkViewDebugging1.Name = "ffrkViewDebugging1";
      this.ffrkViewDebugging1.Size = new Size(983, 590);
      this.ffrkViewDebugging1.TabIndex = 0;
      this.statusStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.toolStripStatusLabel1,
        (ToolStripItem) this.toolStripStatusLabelConnection
      });
      this.statusStrip1.Location = new Point(0, 594);
      this.statusStrip1.Name = "statusStrip1";
      this.statusStrip1.Size = new Size(991, 22);
      this.statusStrip1.TabIndex = 1;
      this.statusStrip1.Text = "statusStrip1";
      this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
      this.toolStripStatusLabel1.Size = new Size(45, 17);
      this.toolStripStatusLabel1.Text = "Status: ";
      this.toolStripStatusLabelConnection.Name = "toolStripStatusLabelConnection";
      this.toolStripStatusLabelConnection.Size = new Size(79, 17);
      this.toolStripStatusLabelConnection.Text = "Disconnected";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.statusStrip1);
      this.Controls.Add((Control) this.tabControlFFRKInspector);
      this.Name = nameof (FFRKTabInspector);
      this.Size = new Size(991, 616);
      this.Load += new EventHandler(this.FFRKTabInspectorView_Load);
      this.tabControlFFRKInspector.ResumeLayout(false);
      this.tabPageDungeon.ResumeLayout(false);
      this.tabPageBattle.ResumeLayout(false);
      this.tabPageSearch.ResumeLayout(false);
      this.tabPageInventory.ResumeLayout(false);
      this.tabPageEditEquipment.ResumeLayout(false);
      this.tabPageGacha.ResumeLayout(false);
      this.tabPageAbout.ResumeLayout(false);
      this.tabPageDebug.ResumeLayout(false);
      this.statusStrip1.ResumeLayout(false);
      this.statusStrip1.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }

    public enum InspectorPage
    {
      CurrentDungeon,
      CurrentBattle,
      EnemyDetails,
      ItemSearch,
      Inventory,
      Database,
      Gacha,
      About,
      Debugging,
    }
  }
}
