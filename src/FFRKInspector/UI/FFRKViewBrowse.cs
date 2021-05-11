// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.FFRKViewBrowse
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  internal class FFRKViewBrowse : UserControl
  {
    private IContainer components = (IContainer) null;
    private SplitContainer splitContainerBrowser;
    private TreeView treeViewItemBrowser;
    private Panel browseItemDetails;
    private Panel browseDungeonDetails;
    private Panel browseEventDetails;
    private Panel browseBattleDetails;
    private ImageList imageList1;
    private Label label1;

    public FFRKViewBrowse() => this.InitializeComponent();

    private void FFRKViewBrowse_Load(object sender, EventArgs e)
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
      TreeNode treeNode1 = new TreeNode("Items");
      TreeNode treeNode2 = new TreeNode("Worlds");
      TreeNode treeNode3 = new TreeNode("Dungeons");
      TreeNode treeNode4 = new TreeNode("Battles");
      TreeNode treeNode5 = new TreeNode("Events");
      TreeNode treeNode6 = new TreeNode("Characters");
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FFRKViewBrowse));
      this.splitContainerBrowser = new SplitContainer();
      this.treeViewItemBrowser = new TreeView();
      this.imageList1 = new ImageList(this.components);
      this.browseItemDetails = new Panel();
      this.browseDungeonDetails = new Panel();
      this.browseEventDetails = new Panel();
      this.browseBattleDetails = new Panel();
      this.label1 = new Label();
      this.splitContainerBrowser.BeginInit();
      this.splitContainerBrowser.Panel1.SuspendLayout();
      this.splitContainerBrowser.Panel2.SuspendLayout();
      this.splitContainerBrowser.SuspendLayout();
      this.browseItemDetails.SuspendLayout();
      this.browseDungeonDetails.SuspendLayout();
      this.browseEventDetails.SuspendLayout();
      this.browseBattleDetails.SuspendLayout();
      this.SuspendLayout();
      this.splitContainerBrowser.Dock = DockStyle.Fill;
      this.splitContainerBrowser.Location = new Point(0, 0);
      this.splitContainerBrowser.Name = "splitContainerBrowser";
      this.splitContainerBrowser.Panel1.Controls.Add((Control) this.treeViewItemBrowser);
      this.splitContainerBrowser.Panel2.Controls.Add((Control) this.browseItemDetails);
      this.splitContainerBrowser.Size = new Size(624, 375);
      this.splitContainerBrowser.SplitterDistance = 132;
      this.splitContainerBrowser.TabIndex = 1;
      this.treeViewItemBrowser.Dock = DockStyle.Fill;
      this.treeViewItemBrowser.ImageIndex = 0;
      this.treeViewItemBrowser.ImageList = this.imageList1;
      this.treeViewItemBrowser.Location = new Point(0, 0);
      this.treeViewItemBrowser.Name = "treeViewItemBrowser";
      treeNode1.Name = "NodeItems";
      treeNode1.Text = "Items";
      treeNode2.Name = "NodeWorlds";
      treeNode2.Text = "Worlds";
      treeNode3.Name = "NodeDunegons";
      treeNode3.Text = "Dungeons";
      treeNode4.Name = "NodeBattles";
      treeNode4.Text = "Battles";
      treeNode5.Name = "Events";
      treeNode5.Text = "Events";
      treeNode6.Name = "NodeCharacters";
      treeNode6.Text = "Characters";
      this.treeViewItemBrowser.Nodes.AddRange(new TreeNode[6]
      {
        treeNode1,
        treeNode2,
        treeNode3,
        treeNode4,
        treeNode5,
        treeNode6
      });
      this.treeViewItemBrowser.SelectedImageIndex = 0;
      this.treeViewItemBrowser.Size = new Size(132, 375);
      this.treeViewItemBrowser.TabIndex = 0;
      this.imageList1.ImageStream = (ImageListStreamer) componentResourceManager.GetObject("imageList1.ImageStream");
      this.imageList1.TransparentColor = Color.Transparent;
      this.imageList1.Images.SetKeyName(0, "");
      this.browseItemDetails.Controls.Add((Control) this.browseDungeonDetails);
      this.browseItemDetails.Dock = DockStyle.Fill;
      this.browseItemDetails.Location = new Point(0, 0);
      this.browseItemDetails.Name = "browseItemDetails";
      this.browseItemDetails.Size = new Size(488, 375);
      this.browseItemDetails.TabIndex = 0;
      this.browseItemDetails.Visible = false;
      this.browseDungeonDetails.Controls.Add((Control) this.browseEventDetails);
      this.browseDungeonDetails.Dock = DockStyle.Fill;
      this.browseDungeonDetails.Location = new Point(0, 0);
      this.browseDungeonDetails.Name = "browseDungeonDetails";
      this.browseDungeonDetails.Size = new Size(488, 375);
      this.browseDungeonDetails.TabIndex = 0;
      this.browseDungeonDetails.Visible = false;
      this.browseEventDetails.Controls.Add((Control) this.browseBattleDetails);
      this.browseEventDetails.Dock = DockStyle.Fill;
      this.browseEventDetails.Location = new Point(0, 0);
      this.browseEventDetails.Name = "browseEventDetails";
      this.browseEventDetails.Size = new Size(488, 375);
      this.browseEventDetails.TabIndex = 0;
      this.browseEventDetails.Visible = false;
      this.browseBattleDetails.Controls.Add((Control) this.label1);
      this.browseBattleDetails.Dock = DockStyle.Fill;
      this.browseBattleDetails.Location = new Point(0, 0);
      this.browseBattleDetails.Name = "browseBattleDetails";
      this.browseBattleDetails.Size = new Size(488, 375);
      this.browseBattleDetails.TabIndex = 0;
      this.label1.AutoSize = true;
      this.label1.Location = new Point(143, 151);
      this.label1.Name = "label1";
      this.label1.Size = new Size(229, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "This page doesn't work yet!  It's coming though";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.splitContainerBrowser);
      this.Name = nameof (FFRKViewBrowse);
      this.Size = new Size(624, 375);
      this.Load += new EventHandler(this.FFRKViewBrowse_Load);
      this.splitContainerBrowser.Panel1.ResumeLayout(false);
      this.splitContainerBrowser.Panel2.ResumeLayout(false);
      this.splitContainerBrowser.EndInit();
      this.splitContainerBrowser.ResumeLayout(false);
      this.browseItemDetails.ResumeLayout(false);
      this.browseDungeonDetails.ResumeLayout(false);
      this.browseEventDetails.ResumeLayout(false);
      this.browseBattleDetails.ResumeLayout(false);
      this.browseBattleDetails.PerformLayout();
      this.ResumeLayout(false);
    }
  }
}
