// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.FFRKViewAbout
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Database;
using FFRKInspector.Properties;
using FFRKInspector.Proxy;
using MySql.Data.MySqlClient;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  public class FFRKViewAbout : UserControl
  {
    private static readonly string mConnectionSuccessMsg = "Connection successful!  Save the settings, and close and restart fiddler for the settings to take effect.";
    private static readonly string mDatabaseTooOldMsg = "The database is for an older version of FFRK Inspector.  Database connectivity will be unavailable with these settings.";
    private static readonly string mDatabaseTooNewMsg = "The database is for a newer version of FFRK Inspector.  You will need to update to a later version to connect to this database.";
    private static readonly string mInvalidConnectionMsg = "Unable to establish a connection with the specified parameters.  Check that they are correct and try again.  Message = {0}";
    private IContainer components = (IContainer) null;
    private Label label1;
    private Label label2;
    private Label label3;
    private GroupBox groupBox1;
    private Label label4;
    private LinkLabel linkLabel1;
    private TextBox textBoxPassword;
    private Label label7;
    private TextBox textBoxHost;
    private Label label6;
    private TextBox textBoxUser;
    private Label label5;
    private Button buttonSaveSettings;
    private TextBox textBoxSchema;
    private Label label8;
    private Button buttonTestSettings;
    private GroupBox groupBox3;
    private Label label9;
    private Label labelConnectionResult;
    private GroupBox groupBox4;
    private Label label10;
    private Button buttonDonate;
    private Label label11;
    private LinkLabel linkLabel2;
    private LinkLabel linkLabel3;
    private Label label12;

    public FFRKViewAbout() => this.InitializeComponent();

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("mailto:cppisking@gmail.com");

    private void buttonTestSettings_Click(object sender, EventArgs e)
    {
    }

    private void buttonSaveSettings_Click(object sender, EventArgs e)
    {
      Settings.Default.DatabaseHost = this.textBoxHost.Text;
      Settings.Default.DatabasePassword = this.textBoxPassword.Text;
      Settings.Default.DatabaseSchema = this.textBoxSchema.Text;
      Settings.Default.DatabaseUser = this.textBoxUser.Text;
      Settings.Default.Save();
      this.labelConnectionResult.Text = "Settings saved.  Close and restart fiddler for the settings to take effect.";
    }

    private void FFRKViewAbout_Load(object sender, EventArgs e)
    {
      this.textBoxHost.Text = Settings.Default.DatabaseHost;
      this.textBoxPassword.Text = Settings.Default.DatabasePassword;
      this.textBoxSchema.Text = Settings.Default.DatabaseSchema;
      this.textBoxUser.Text = Settings.Default.DatabaseUser;
    }

    private void buttonDonate_Click(object sender, EventArgs e)
    {
      string str1 = "cppisking@gmail.com";
      string str2 = WebUtility.HtmlEncode("Donation for FFRK Inspector");
      string str3 = "US";
      string str4 = "USD";
      Process.Start("https://www.paypal.com/cgi-bin/webscr?cmd=_donations&business=" + str1 + "&lc=" + str3 + "&item_name=" + str2 + "&currency_code=" + str4 + "&bn=PP%2dDonationsBF");
    }

    private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://ffrki.wordpress.com/");

    private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://github.com/cppisking/ffrk-inspector/");

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FFRKViewAbout));
      this.label1 = new Label();
      this.label2 = new Label();
      this.label3 = new Label();
      this.groupBox1 = new GroupBox();
      this.label4 = new Label();
      this.linkLabel1 = new LinkLabel();
      this.label5 = new Label();
      this.textBoxUser = new TextBox();
      this.textBoxHost = new TextBox();
      this.label6 = new Label();
      this.label7 = new Label();
      this.textBoxPassword = new TextBox();
      this.buttonSaveSettings = new Button();
      this.textBoxSchema = new TextBox();
      this.label8 = new Label();
      this.buttonTestSettings = new Button();
      this.groupBox3 = new GroupBox();
      this.labelConnectionResult = new Label();
      this.label9 = new Label();
      this.groupBox4 = new GroupBox();
      this.buttonDonate = new Button();
      this.label10 = new Label();
      this.label11 = new Label();
      this.linkLabel2 = new LinkLabel();
      this.linkLabel3 = new LinkLabel();
      this.label12 = new Label();
      this.groupBox1.SuspendLayout();
      this.groupBox3.SuspendLayout();
      this.groupBox4.SuspendLayout();
      this.SuspendLayout();
      this.label1.AutoSize = true;
      this.label1.Font = new Font("Microsoft Sans Serif", 14.25f, FontStyle.Bold | FontStyle.Italic, GraphicsUnit.Point, (byte) 0);
      this.label1.Location = new Point(264, 18);
      this.label1.Name = "label1";
      this.label1.Size = new Size(329, 24);
      this.label1.TabIndex = 0;
      this.label1.Text = "FFRK Inspector (Build 3, 6/8/2015)";
      this.label2.Location = new Point(13, 16);
      this.label2.Name = "label2";
      this.label2.Size = new Size(453, 50);
      this.label2.TabIndex = 1;
      this.label2.Text = "Copyright © 2015 cpp_is_king.  All rights reserved.  Final Fantasy Record Keeper, and all related text, content, and images are registered trademarks or trademarks of DeNA or Square Enix.";
      this.label2.TextAlign = ContentAlignment.MiddleCenter;
      this.label3.Location = new Point(13, 66);
      this.label3.Name = "label3";
      this.label3.Size = new Size(453, 50);
      this.label3.TabIndex = 2;
      this.label3.Text = componentResourceManager.GetString("label3.Text");
      this.label3.TextAlign = ContentAlignment.MiddleCenter;
      this.groupBox1.Controls.Add((Control) this.label3);
      this.groupBox1.Controls.Add((Control) this.label2);
      this.groupBox1.Location = new Point(54, 87);
      this.groupBox1.Name = "groupBox1";
      this.groupBox1.Size = new Size(480, 125);
      this.groupBox1.TabIndex = 3;
      this.groupBox1.TabStop = false;
      this.groupBox1.Text = "Legal Stuff";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(259, 44);
      this.label4.Name = "label4";
      this.label4.Size = new Size(35, 13);
      this.label4.TabIndex = 4;
      this.label4.Text = "Email:";
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new Point(300, 42);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size(110, 13);
      this.linkLabel1.TabIndex = 5;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "cppisking@gmail.com";
      this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      this.label5.AutoSize = true;
      this.label5.Location = new Point(33, 83);
      this.label5.Name = "label5";
      this.label5.Size = new Size(81, 13);
      this.label5.TabIndex = 4;
      this.label5.Text = "Database User:";
      this.textBoxUser.Location = new Point(120, 79);
      this.textBoxUser.Name = "textBoxUser";
      this.textBoxUser.Size = new Size(293, 20);
      this.textBoxUser.TabIndex = 2;
      this.textBoxHost.Location = new Point(120, 53);
      this.textBoxHost.Name = "textBoxHost";
      this.textBoxHost.Size = new Size(293, 20);
      this.textBoxHost.TabIndex = 1;
      this.label6.AutoSize = true;
      this.label6.Location = new Point(34, 56);
      this.label6.Name = "label6";
      this.label6.Size = new Size(81, 13);
      this.label6.TabIndex = 6;
      this.label6.Text = "Database Host:";
      this.label7.AutoSize = true;
      this.label7.Location = new Point(10, 108);
      this.label7.Name = "label7";
      this.label7.Size = new Size(105, 13);
      this.label7.TabIndex = 8;
      this.label7.Text = "Database Password:";
      this.textBoxPassword.Location = new Point(120, 105);
      this.textBoxPassword.Name = "textBoxPassword";
      this.textBoxPassword.Size = new Size(293, 20);
      this.textBoxPassword.TabIndex = 3;
      this.buttonSaveSettings.Location = new Point(293, 157);
      this.buttonSaveSettings.Name = "buttonSaveSettings";
      this.buttonSaveSettings.Size = new Size(120, 32);
      this.buttonSaveSettings.TabIndex = 6;
      this.buttonSaveSettings.Text = "Save";
      this.buttonSaveSettings.UseVisualStyleBackColor = true;
      this.buttonSaveSettings.Click += new EventHandler(this.buttonSaveSettings_Click);
      this.textBoxSchema.Location = new Point(120, 131);
      this.textBoxSchema.Name = "textBoxSchema";
      this.textBoxSchema.Size = new Size(293, 20);
      this.textBoxSchema.TabIndex = 4;
      this.label8.AutoSize = true;
      this.label8.Location = new Point(16, 134);
      this.label8.Name = "label8";
      this.label8.Size = new Size(98, 13);
      this.label8.TabIndex = 11;
      this.label8.Text = "Database Schema:";
      this.buttonTestSettings.Location = new Point(168, 157);
      this.buttonTestSettings.Name = "buttonTestSettings";
      this.buttonTestSettings.Size = new Size(119, 32);
      this.buttonTestSettings.TabIndex = 5;
      this.buttonTestSettings.Text = "Test";
      this.buttonTestSettings.UseVisualStyleBackColor = true;
      this.buttonTestSettings.Click += new EventHandler(this.buttonTestSettings_Click);
      this.groupBox3.Controls.Add((Control) this.labelConnectionResult);
      this.groupBox3.Controls.Add((Control) this.label9);
      this.groupBox3.Controls.Add((Control) this.buttonTestSettings);
      this.groupBox3.Controls.Add((Control) this.textBoxPassword);
      this.groupBox3.Controls.Add((Control) this.textBoxSchema);
      this.groupBox3.Controls.Add((Control) this.label5);
      this.groupBox3.Controls.Add((Control) this.label8);
      this.groupBox3.Controls.Add((Control) this.textBoxUser);
      this.groupBox3.Controls.Add((Control) this.buttonSaveSettings);
      this.groupBox3.Controls.Add((Control) this.label6);
      this.groupBox3.Controls.Add((Control) this.textBoxHost);
      this.groupBox3.Controls.Add((Control) this.label7);
      this.groupBox3.Location = new Point(54, 218);
      this.groupBox3.Name = "groupBox3";
      this.groupBox3.Size = new Size(480, 217);
      this.groupBox3.TabIndex = 7;
      this.groupBox3.TabStop = false;
      this.groupBox3.Text = "Connection";
      this.labelConnectionResult.Location = new Point(6, 192);
      this.labelConnectionResult.Name = "labelConnectionResult";
      this.labelConnectionResult.Size = new Size(407, 22);
      this.labelConnectionResult.TabIndex = 15;
      this.labelConnectionResult.TextAlign = ContentAlignment.MiddleRight;
      this.label9.AutoSize = true;
      this.label9.Location = new Point(17, 20);
      this.label9.Name = "label9";
      this.label9.Size = new Size(348, 13);
      this.label9.TabIndex = 14;
      this.label9.Text = "These settings will take effect the next time you close and restart Fiddler.";
      this.groupBox4.Controls.Add((Control) this.buttonDonate);
      this.groupBox4.Controls.Add((Control) this.label10);
      this.groupBox4.Location = new Point(540, 90);
      this.groupBox4.Name = "groupBox4";
      this.groupBox4.Size = new Size(254, 345);
      this.groupBox4.TabIndex = 8;
      this.groupBox4.TabStop = false;
      this.groupBox4.Text = "Donate";
      this.buttonDonate.AutoSize = true;
      this.buttonDonate.Cursor = Cursors.Hand;
      this.buttonDonate.FlatAppearance.BorderColor = SystemColors.Control;
      this.buttonDonate.FlatAppearance.BorderSize = 0;
      this.buttonDonate.FlatAppearance.MouseDownBackColor = SystemColors.Control;
      this.buttonDonate.FlatAppearance.MouseOverBackColor = SystemColors.Control;
      this.buttonDonate.FlatStyle = FlatStyle.Flat;
      this.buttonDonate.Image = (Image) Resources.paypal_donate_button;
      this.buttonDonate.Location = new Point(46, 286);
      this.buttonDonate.Name = "buttonDonate";
      this.buttonDonate.Size = new Size(168, 34);
      this.buttonDonate.TabIndex = 1;
      this.buttonDonate.UseVisualStyleBackColor = true;
      this.buttonDonate.Click += new EventHandler(this.buttonDonate_Click);
      this.label10.Location = new Point(6, 30);
      this.label10.Name = "label10";
      this.label10.Size = new Size(242, 248);
      this.label10.TabIndex = 0;
      this.label10.Text = componentResourceManager.GetString("label10.Text");
      this.label10.TextAlign = ContentAlignment.TopCenter;
      this.label11.AutoSize = true;
      this.label11.Location = new Point(167, 57);
      this.label11.Name = "label11";
      this.label11.Size = new Size((int) sbyte.MaxValue, 13);
      this.label11.TabIndex = 9;
      this.label11.Text = "Blog, news, and updates:";
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.Location = new Point(300, 55);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new Size(139, 13);
      this.linkLabel2.TabIndex = 10;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "https://ffrki.wordpress.com/";
      this.linkLabel2.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
      this.linkLabel3.AutoSize = true;
      this.linkLabel3.Location = new Point(300, 68);
      this.linkLabel3.Name = "linkLabel3";
      this.linkLabel3.Size = new Size(211, 13);
      this.linkLabel3.TabIndex = 12;
      this.linkLabel3.TabStop = true;
      this.linkLabel3.Text = "https://github.com/cppisking/ffrk-inspector";
      this.linkLabel3.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
      this.label12.AutoSize = true;
      this.label12.Location = new Point(177, 70);
      this.label12.Name = "label12";
      this.label12.Size = new Size(117, 13);
      this.label12.TabIndex = 11;
      this.label12.Text = "Download and support:";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.linkLabel3);
      this.Controls.Add((Control) this.label12);
      this.Controls.Add((Control) this.linkLabel2);
      this.Controls.Add((Control) this.label11);
      this.Controls.Add((Control) this.groupBox4);
      this.Controls.Add((Control) this.groupBox3);
      this.Controls.Add((Control) this.linkLabel1);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.groupBox1);
      this.Name = nameof (FFRKViewAbout);
      this.Size = new Size(818, 483);
      this.Load += new EventHandler(this.FFRKViewAbout_Load);
      this.groupBox1.ResumeLayout(false);
      this.groupBox3.ResumeLayout(false);
      this.groupBox3.PerformLayout();
      this.groupBox4.ResumeLayout(false);
      this.groupBox4.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
