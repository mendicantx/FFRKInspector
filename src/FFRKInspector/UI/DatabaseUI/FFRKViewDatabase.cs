// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.DatabaseUI.FFRKViewDatabase
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Fiddler;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace FFRKInspector.UI.DatabaseUI
{
  public class FFRKViewDatabase : UserControl
  {
    private string sDatabaseLoadError = "Unable to load items from the database.  Check that you are using the latest version of FFRKInspector.  Functionality on this page will be disabled.";
    private string sPrivilegeError = "You do not have the appropriate privileges to submit changes to the requested fields.  Please submit missing and incorrect item information through the missing items panel";
    private IContainer components = (IContainer) null;
    private List<UserControl> mPanels;
    private UserControl mSelectedPanel;
    private Button buttonCommit;
    private Button buttonReload;
    private ComboBox comboBox1;
    private GroupBox groupBox1;

    public FFRKViewDatabase.DatabaseModeEnum DatabaseMode
    {
      get => (FFRKViewDatabase.DatabaseModeEnum) this.comboBox1.SelectedIndex;
      set => this.comboBox1.SelectedIndex = (int) value;
    }

    public FFRKViewDatabase()
    {
      this.InitializeComponent();
      this.mPanels = new List<UserControl>();
      ItemStatsPanel itemStatsPanel = new ItemStatsPanel();
      itemStatsPanel.Dock = DockStyle.Fill;
      itemStatsPanel.Location = new Point(0, 0);
      this.groupBox1.Controls.Add((Control) itemStatsPanel);
      this.mPanels.Add((UserControl) itemStatsPanel);
      this.comboBox1.Items.Add((object) "View equipment and stats");
      MissingItemsPanel missingItemsPanel = new MissingItemsPanel();
      missingItemsPanel.Dock = DockStyle.Fill;
      missingItemsPanel.Location = new Point(0, 0);
      this.groupBox1.Controls.Add((Control) missingItemsPanel);
      this.mPanels.Add((UserControl) missingItemsPanel);
      this.comboBox1.Items.Add((object) "Add missing items or submit fixes for incorrect items");
      this.comboBox1.SelectedIndex = 0;
    }

    private void FFRKViewEditItemStats_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      try
      {
        foreach (FFRKDataBoundPanel mPanel in this.mPanels)
        {
          mPanel.InitializeConnection();
          mPanel.Reload();
        }
      }
      catch (Exception ex)
      {
        FiddlerApplication.Log.LogString(ex.ToString());
        int num = (int) MessageBox.Show(this.sDatabaseLoadError);
        this.groupBox1.Controls.Clear();
        this.mPanels.Clear();
        this.mSelectedPanel = (UserControl) null;
      }
    }

    private void buttonCommit_Click(object sender, EventArgs e)
    {
      if (this.mSelectedPanel == null)
        return;
      try
      {
        ((FFRKDataBoundPanel) this.mSelectedPanel).Commit();
      }
      catch (MySqlException ex)
      {
        if (ex.Number == 1142)
        {
          int num = (int) MessageBox.Show(this.sPrivilegeError);
        }
        else
          throw;
      }
    }

    private void buttonReload_Click(object sender, EventArgs e)
    {
      if (this.mSelectedPanel == null)
        return;
      ((FFRKDataBoundPanel) this.mSelectedPanel).Reload();
    }

    private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.mSelectedPanel = this.comboBox1.SelectedIndex < this.mPanels.Count ? this.mPanels[this.comboBox1.SelectedIndex] : (UserControl) null;
      foreach (UserControl mPanel in this.mPanels)
        mPanel.Visible = mPanel == this.mSelectedPanel;
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.buttonCommit = new Button();
      this.buttonReload = new Button();
      this.comboBox1 = new ComboBox();
      this.groupBox1 = new GroupBox();
      this.SuspendLayout();
      this.buttonCommit.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonCommit.Location = new Point(637, 451);
      this.buttonCommit.Name = "buttonCommit";
      this.buttonCommit.Size = new Size(151, 46);
      this.buttonCommit.TabIndex = 1;
      this.buttonCommit.Text = "Commit";
      this.buttonCommit.UseVisualStyleBackColor = true;
      this.buttonCommit.Click += new EventHandler(this.buttonCommit_Click);
      this.buttonReload.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
      this.buttonReload.Location = new Point(794, 451);
      this.buttonReload.Name = "buttonReload";
      this.buttonReload.Size = new Size(151, 46);
      this.buttonReload.TabIndex = 2;
      this.buttonReload.Text = "Reload";
      this.buttonReload.UseVisualStyleBackColor = true;
      this.buttonReload.Click += new EventHandler(this.buttonReload_Click);
      this.comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
      this.comboBox1.FormattingEnabled = true;
      this.comboBox1.Location = new Point(12, 21);
      this.comboBox1.Name = "comboBox1";
      this.comboBox1.Size = new Size(392, 21);
      this.comboBox1.TabIndex = 4;
      this.comboBox1.SelectedIndexChanged += new EventHandler(this.comboBox1_SelectedIndexChanged);
      this.groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.groupBox1.Location = new Point(12, 48);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(933, 397);
      this.groupBox1.TabIndex = 5;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Items";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.groupBox1);
      this.Controls.Add((Control) this.comboBox1);
      this.Controls.Add((Control) this.buttonReload);
      this.Controls.Add((Control) this.buttonCommit);
      this.Name = "FFRKViewEditItemStats";
      this.Size = new Size(968, 513);
      this.Load += new EventHandler(this.FFRKViewEditItemStats_Load);
      this.ResumeLayout(false);
    }

    public enum DatabaseModeEnum
    {
      EquipmentAndStats,
      MissingItems,
    }
  }
}
