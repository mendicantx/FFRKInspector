// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.DatabaseUI.MissingItemsPanel
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using FFRKInspector.missingItemsDataSetTableAdapters;
using FFRKInspector.Proxy;
using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace FFRKInspector.UI.DatabaseUI
{
  public class MissingItemsPanel : UserControl, FFRKDataBoundPanel
  {
    private IContainer components = (IContainer) null;
    private DataGridViewEx dataGridView1;
    private DataGridViewTextBoxColumn equipmentidDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn baseatkDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn basemagDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn baseaccDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn basedefDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn baseresDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn baseevaDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn basemndDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn maxatkDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn maxmagDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn maxaccDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn maxdefDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn maxresDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn maxevaDataGridViewTextBoxColumn;
    private DataGridViewTextBoxColumn maxmndDataGridViewTextBoxColumn;
    private BindingSource missingitemsBindingSource;
    private missingItemsDataSet missingItemsDataSet;
    private missing_itemsTableAdapter missing_itemsTableAdapter;
    private Label label1;
    private Label label2;
    private LinkLabel linkLabel1;
    private Label label3;
    private Label label4;
    private Label label5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn rarity;
    private DataGridViewTextBoxColumn series;
    private DataGridViewTextBoxColumn type;
    private DataGridViewTextBoxColumn subtype;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn9;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn10;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn11;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn12;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn13;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn14;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn15;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn16;

    public MissingItemsPanel()
    {
      this.InitializeComponent();
      this.type.CellTemplate = (DataGridViewCell) new EnumDataViewGridCell<SchemaConstants.ItemType>();
      this.subtype.CellTemplate = (DataGridViewCell) new EnumDataViewGridCell<SchemaConstants.EquipmentCategory>();
      this.series.CellTemplate = (DataGridViewCell) new SeriesDataGridViewCell();
    }

    private void MissingItemsPanel_Load(object sender, EventArgs e)
    {
      if (this.DesignMode)
        return;
      this.InitializeConnection();
    }

    public void InitializeConnection()
    {
      if (FFRKProxy.Instance == null)
        return;
    }

    public void Reload() => this.missing_itemsTableAdapter.Fill(this.missingItemsDataSet.missing_items);

    public void Commit()
    {
      int num1 = this.missing_itemsTableAdapter.Update(this.missingItemsDataSet.missing_items);
      if (num1 == 0)
      {
        int num2 = (int) MessageBox.Show("There are no changes to commit");
      }
      else
      {
        int num3 = (int) MessageBox.Show(string.Format("Updated {0} entries.", (object) num1));
      }
    }

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) => Process.Start("https://ffrkstrategy.gamematome.jp");

    protected override void Dispose(bool disposing)
    {
      if (disposing && this.components != null)
        this.components.Dispose();
      base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (MissingItemsPanel));
      this.dataGridView1 = new DataGridViewEx();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn2 = new DataGridViewTextBoxColumn();
      this.rarity = new DataGridViewTextBoxColumn();
      this.series = new DataGridViewTextBoxColumn();
      this.type = new DataGridViewTextBoxColumn();
      this.subtype = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn3 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn4 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn5 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn6 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn7 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn8 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn9 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn10 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn11 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn12 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn13 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn14 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn15 = new DataGridViewTextBoxColumn();
      this.dataGridViewTextBoxColumn16 = new DataGridViewTextBoxColumn();
      this.missingitemsBindingSource = new BindingSource(this.components);
      this.missingItemsDataSet = new missingItemsDataSet();
      this.missing_itemsTableAdapter = new missing_itemsTableAdapter();
      this.label1 = new Label();
      this.label2 = new Label();
      this.linkLabel1 = new LinkLabel();
      this.label3 = new Label();
      this.label4 = new Label();
      this.label5 = new Label();
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      ((ISupportInitialize) this.missingitemsBindingSource).BeginInit();
      this.missingItemsDataSet.BeginInit();
      this.SuspendLayout();
      this.dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
      this.dataGridView1.AutoGenerateColumns = false;
      this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn1, (DataGridViewColumn) this.dataGridViewTextBoxColumn2, (DataGridViewColumn) this.rarity, (DataGridViewColumn) this.series, (DataGridViewColumn) this.type, (DataGridViewColumn) this.subtype, (DataGridViewColumn) this.dataGridViewTextBoxColumn3, (DataGridViewColumn) this.dataGridViewTextBoxColumn4, (DataGridViewColumn) this.dataGridViewTextBoxColumn5, (DataGridViewColumn) this.dataGridViewTextBoxColumn6, (DataGridViewColumn) this.dataGridViewTextBoxColumn7, (DataGridViewColumn) this.dataGridViewTextBoxColumn8, (DataGridViewColumn) this.dataGridViewTextBoxColumn9, (DataGridViewColumn) this.dataGridViewTextBoxColumn10, (DataGridViewColumn) this.dataGridViewTextBoxColumn11, (DataGridViewColumn) this.dataGridViewTextBoxColumn12, (DataGridViewColumn) this.dataGridViewTextBoxColumn13, (DataGridViewColumn) this.dataGridViewTextBoxColumn14, (DataGridViewColumn) this.dataGridViewTextBoxColumn15, (DataGridViewColumn) this.dataGridViewTextBoxColumn16);
      this.dataGridView1.DataSource = (object) this.missingitemsBindingSource;
      this.dataGridView1.Location = new Point(0, 128);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new Size(798, 324);
      this.dataGridView1.TabIndex = 0;
      this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewTextBoxColumn1.DataPropertyName = "equipment_id";
      this.dataGridViewTextBoxColumn1.HeaderText = "equipment_id";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.Width = 95;
      this.dataGridViewTextBoxColumn2.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewTextBoxColumn2.DataPropertyName = "name";
      this.dataGridViewTextBoxColumn2.HeaderText = "name";
      this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
      this.dataGridViewTextBoxColumn2.Width = 58;
      this.rarity.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.rarity.DataPropertyName = "rarity";
      this.rarity.HeaderText = "rarity";
      this.rarity.Name = "rarity";
      this.rarity.Width = 54;
      this.series.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.series.DataPropertyName = "series";
      this.series.HeaderText = "series";
      this.series.Name = "series";
      this.series.Width = 59;
      this.type.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.type.DataPropertyName = "type";
      this.type.HeaderText = "type";
      this.type.Name = "type";
      this.type.Resizable = DataGridViewTriState.True;
      this.type.Width = 52;
      this.subtype.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.subtype.DataPropertyName = "subtype";
      this.subtype.DividerWidth = 3;
      this.subtype.HeaderText = "subtype";
      this.subtype.Name = "subtype";
      this.subtype.Resizable = DataGridViewTriState.True;
      this.subtype.Width = 72;
      this.dataGridViewTextBoxColumn3.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn3.DataPropertyName = "base_atk";
      this.dataGridViewTextBoxColumn3.HeaderText = "base_atk";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn4.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn4.DataPropertyName = "base_mag";
      this.dataGridViewTextBoxColumn4.HeaderText = "base_mag";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn5.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn5.DataPropertyName = "base_acc";
      this.dataGridViewTextBoxColumn5.HeaderText = "base_acc";
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn6.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn6.DataPropertyName = "base_def";
      this.dataGridViewTextBoxColumn6.HeaderText = "base_def";
      this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      this.dataGridViewTextBoxColumn7.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn7.DataPropertyName = "base_res";
      this.dataGridViewTextBoxColumn7.HeaderText = "base_res";
      this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
      this.dataGridViewTextBoxColumn8.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn8.DataPropertyName = "base_eva";
      this.dataGridViewTextBoxColumn8.HeaderText = "base_eva";
      this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
      this.dataGridViewTextBoxColumn9.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn9.DataPropertyName = "base_mnd";
      this.dataGridViewTextBoxColumn9.DividerWidth = 3;
      this.dataGridViewTextBoxColumn9.HeaderText = "base_mnd";
      this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
      this.dataGridViewTextBoxColumn10.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn10.DataPropertyName = "max_atk";
      this.dataGridViewTextBoxColumn10.HeaderText = "max_atk";
      this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
      this.dataGridViewTextBoxColumn11.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn11.DataPropertyName = "max_mag";
      this.dataGridViewTextBoxColumn11.HeaderText = "max_mag";
      this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
      this.dataGridViewTextBoxColumn12.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn12.DataPropertyName = "max_acc";
      this.dataGridViewTextBoxColumn12.HeaderText = "max_acc";
      this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
      this.dataGridViewTextBoxColumn13.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn13.DataPropertyName = "max_def";
      this.dataGridViewTextBoxColumn13.HeaderText = "max_def";
      this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
      this.dataGridViewTextBoxColumn14.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn14.DataPropertyName = "max_res";
      this.dataGridViewTextBoxColumn14.HeaderText = "max_res";
      this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
      this.dataGridViewTextBoxColumn15.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn15.DataPropertyName = "max_eva";
      this.dataGridViewTextBoxColumn15.HeaderText = "max_eva";
      this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
      this.dataGridViewTextBoxColumn16.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
      this.dataGridViewTextBoxColumn16.DataPropertyName = "max_mnd";
      this.dataGridViewTextBoxColumn16.HeaderText = "max_mnd";
      this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
      this.missingitemsBindingSource.DataMember = "missing_items";
      this.missingitemsBindingSource.DataSource = (object) this.missingItemsDataSet;
      this.missingItemsDataSet.DataSetName = "missingItemsDataSet";
      this.missingItemsDataSet.EnforceConstraints = false;
      this.missingItemsDataSet.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.missing_itemsTableAdapter.ClearBeforeFill = true;
      this.label1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
      this.label1.Location = new Point(3, 10);
      this.label1.Name = "label1";
      this.label1.Size = new Size(792, 68);
      this.label1.TabIndex = 1;
      this.label1.Text = componentResourceManager.GetString("label1.Text");
      this.label2.AutoSize = true;
      this.label2.Location = new Point(3, 76);
      this.label2.Name = "label2";
      this.label2.Size = new Size(231, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "* Base and max stats can be gathered from the ";
      this.linkLabel1.Location = new Point(250, 75);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new Size((int) byte.MaxValue, 13);
      this.linkLabel1.TabIndex = 3;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "Final Fantasy Record Keeper Official Strategy Site";
      this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
      this.label3.AutoSize = true;
      this.label3.Location = new Point(3, 91);
      this.label3.Name = "label3";
      this.label3.Size = new Size(106, 13);
      this.label3.TabIndex = 4;
      this.label3.Text = "* Series = I, II, II, etc.";
      this.label4.AutoSize = true;
      this.label4.Location = new Point(3, 106);
      this.label4.Name = "label4";
      this.label4.Size = new Size(486, 13);
      this.label4.TabIndex = 5;
      this.label4.Text = "* Type = Weapon, Armor, Accessory, Subtype = Dagger, Bow, Instrument, LightArmor, Helm, Hat, etc.";
      this.label5.AutoSize = true;
      this.label5.Location = new Point(501, 76);
      this.label5.Name = "label5";
      this.label5.Size = new Size(214, 13);
      this.label5.TabIndex = 6;
      this.label5.Text = ".  You can leave the stats blank if you want.";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.label5);
      this.Controls.Add((Control) this.label4);
      this.Controls.Add((Control) this.label3);
      this.Controls.Add((Control) this.linkLabel1);
      this.Controls.Add((Control) this.label2);
      this.Controls.Add((Control) this.label1);
      this.Controls.Add((Control) this.dataGridView1);
      this.Name = nameof (MissingItemsPanel);
      this.Size = new Size(798, 452);
      this.Load += new EventHandler(this.MissingItemsPanel_Load);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      ((ISupportInitialize) this.missingitemsBindingSource).EndInit();
      this.missingItemsDataSet.EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();
    }
  }
}
