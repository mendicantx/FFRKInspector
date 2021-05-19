// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.DatabaseUI.ItemStatsPanel
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.equipmentStatsDataSetTableAdapters;
using FFRKInspector.GameData;
using FFRKInspector.Proxy;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace FFRKInspector.UI.DatabaseUI
{
  public class ItemStatsPanel : UserControl, FFRKDataBoundPanel
  {
    private static readonly int kBaseStatsColumnZero = 5;
    private static readonly int kMaxStatsColumnZero = 12;
    private IContainer components = (IContainer) null;
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
    private DataGridViewEx dataGridView1;
    private BindingSource equipmentstatsBindingSource;
    private equipmentStatsDataSet equipmentStatsDataSet;
    private equipment_statsTableAdapter equipment_statsTableAdapter;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
    private DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
    private DataGridViewTextBoxColumn name;
    private DataGridViewTextBoxColumn rarity;
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

    public ItemStatsPanel()
    {
      this.InitializeComponent();
      this.type.CellTemplate = (DataGridViewCell) new EnumDataViewGridCell<SchemaConstants.ItemType>();
      this.subtype.CellTemplate = (DataGridViewCell) new EnumDataViewGridCell<SchemaConstants.EquipmentCategory>();
    }

    private void EditExistingItemsPanel_Load(object sender, EventArgs e)
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

    public void Reload() => this.equipment_statsTableAdapter.Fill(this.equipmentStatsDataSet.equipment_stats);

    public void Commit()
    {
      int num1 = this.equipment_statsTableAdapter.Update(this.equipmentStatsDataSet.equipment_stats);
      if (num1 <= 0)
        return;
      int num2 = (int) MessageBox.Show(string.Format("{0} items successfully updated.", (object) num1));
    }

    private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Tab:
          if (this.dataGridView1.CurrentCell != null)
          {
            DataGridViewCell currentCell = this.dataGridView1.CurrentCell;
            if (e.Shift)
              this.dataGridView1.CurrentCell = this.FindPreviousNonEmptyCell(this.dataGridView1.CurrentCell);
            else
              this.dataGridView1.CurrentCell = this.FindNextNonEmptyCell(this.dataGridView1.CurrentCell);
            e.Handled = true;
            break;
          }
          break;
        case Keys.Left:
          if (e.Control && this.dataGridView1.CurrentCell != null)
          {
            int columnIndex = this.dataGridView1.CurrentCell.ColumnIndex;
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[columnIndex > ItemStatsPanel.kBaseStatsColumnZero ? (columnIndex > ItemStatsPanel.kMaxStatsColumnZero ? ItemStatsPanel.kMaxStatsColumnZero : ItemStatsPanel.kBaseStatsColumnZero) : 0];
            e.Handled = true;
            break;
          }
          break;
        case Keys.Right:
          if (e.Control && this.dataGridView1.CurrentCell != null)
          {
            int columnIndex = this.dataGridView1.CurrentCell.ColumnIndex;
            this.dataGridView1.CurrentCell = this.dataGridView1.Rows[this.dataGridView1.CurrentCell.RowIndex].Cells[columnIndex < ItemStatsPanel.kMaxStatsColumnZero ? (columnIndex < ItemStatsPanel.kBaseStatsColumnZero ? ItemStatsPanel.kBaseStatsColumnZero : ItemStatsPanel.kMaxStatsColumnZero) : this.dataGridView1.Columns.Count - 1];
            e.Handled = false;
            e.SuppressKeyPress = true;
            break;
          }
          break;
        case Keys.Delete:
          foreach (DataGridViewCell selectedCell in (BaseCollection) this.dataGridView1.SelectedCells)
            selectedCell.Value = (object) DBNull.Value;
          e.Handled = true;
          break;
        case Keys.C:
          if (e.Control)
          {
            Clipboard.SetDataObject((object) this.dataGridView1.GetClipboardContent());
            e.Handled = true;
            break;
          }
          break;
        case Keys.V:
          if (e.Control)
          {
            this.PasteAtCurrentLocation((Clipboard.GetDataObject() as DataObject).GetText().Split('\n'));
            e.Handled = true;
            break;
          }
          break;
        case Keys.X:
          if (e.Control)
          {
            Clipboard.SetDataObject((object) this.dataGridView1.GetClipboardContent());
            foreach (DataGridViewCell selectedCell in (BaseCollection) this.dataGridView1.SelectedCells)
              selectedCell.Value = (object) DBNull.Value;
            e.Handled = true;
            break;
          }
          break;
      }
      if (!e.Handled)
        return;
      e.SuppressKeyPress = true;
    }

    private DataGridViewCell FindNextNonEmptyCell(DataGridViewCell cell)
    {
      int rowIndex = cell.RowIndex;
      int index1 = cell.ColumnIndex + 1;
      int count = this.dataGridView1.Columns.Count;
      DataGridViewCell dataGridViewCell = (DataGridViewCell) null;
      for (int index2 = rowIndex; index2 < this.dataGridView1.Rows.Count; ++index2)
      {
        for (; index1 < this.dataGridView1.Columns.Count; ++index1)
        {
          dataGridViewCell = this.dataGridView1.Rows[index2].Cells[index1];
          if (dataGridViewCell.Value != DBNull.Value)
            return dataGridViewCell;
        }
        index1 = 0;
      }
      return dataGridViewCell;
    }

    private DataGridViewCell FindPreviousNonEmptyCell(DataGridViewCell cell)
    {
      int rowIndex = cell.RowIndex;
      int index1 = cell.ColumnIndex - 1;
      int count = this.dataGridView1.Columns.Count;
      DataGridViewCell dataGridViewCell = (DataGridViewCell) null;
      for (int index2 = rowIndex; index2 >= 0; --index2)
      {
        for (; index1 >= 0; --index1)
        {
          dataGridViewCell = this.dataGridView1.Rows[index2].Cells[index1];
          if (dataGridViewCell.Value != DBNull.Value)
            return dataGridViewCell;
        }
        index1 = count - 1;
      }
      return dataGridViewCell;
    }

    private DataGridViewCell FindSymmetricCell(DataGridViewCell cell)
    {
      int columnIndex = cell.ColumnIndex;
      if (columnIndex < ItemStatsPanel.kBaseStatsColumnZero)
        return (DataGridViewCell) null;
      return columnIndex < ItemStatsPanel.kMaxStatsColumnZero ? this.dataGridView1.Rows[cell.RowIndex].Cells[columnIndex + ItemStatsPanel.kMaxStatsColumnZero - ItemStatsPanel.kBaseStatsColumnZero] : this.dataGridView1.Rows[cell.RowIndex].Cells[columnIndex - (ItemStatsPanel.kMaxStatsColumnZero - ItemStatsPanel.kBaseStatsColumnZero)];
    }

    private void PasteAtCurrentLocation(string[] lines)
    {
      int rowIndex = this.dataGridView1.CurrentCell.RowIndex;
      int columnIndex = this.dataGridView1.CurrentCell.ColumnIndex;
      foreach (string line in lines)
      {
        char[] chArray = new char[1]{ '\r' };
        string[] strArray = line.TrimEnd(chArray).Split('\t');
        int index = columnIndex;
        DataGridViewRow row = this.dataGridView1.Rows[rowIndex];
        foreach (string str in strArray)
        {
          if (index < row.Cells.Count)
          {
            DataGridViewCell cell = row.Cells[index];
            cell.Value = !(str == string.Empty) ? Convert.ChangeType((object) str, cell.ValueType) : (object) DBNull.Value;
            ++index;
          }
          else
            break;
        }
        ++rowIndex;
      }
    }

    private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
    {
    }

    private void dataGridView1_CellParsing(object sender, DataGridViewCellParsingEventArgs e)
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
      DataGridViewCellStyle gridViewCellStyle1 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle2 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle3 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle4 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle5 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle6 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle7 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle8 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle9 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle10 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle11 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle12 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle13 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle14 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle15 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle16 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle17 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle18 = new DataGridViewCellStyle();
      DataGridViewCellStyle gridViewCellStyle19 = new DataGridViewCellStyle();
      this.dataGridView1 = new DataGridViewEx();
      this.equipmentstatsBindingSource = new BindingSource(this.components);
      this.equipmentStatsDataSet = new equipmentStatsDataSet();
      this.equipment_statsTableAdapter = new equipment_statsTableAdapter();
      this.dataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
      this.name = new DataGridViewTextBoxColumn();
      this.rarity = new DataGridViewTextBoxColumn();
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
      ((ISupportInitialize) this.dataGridView1).BeginInit();
      ((ISupportInitialize) this.equipmentstatsBindingSource).BeginInit();
      this.equipmentStatsDataSet.BeginInit();
      this.SuspendLayout();
      this.dataGridView1.AllowUserToAddRows = false;
      this.dataGridView1.AllowUserToDeleteRows = false;
      this.dataGridView1.AutoGenerateColumns = false;
      this.dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
      this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dataGridView1.Columns.AddRange((DataGridViewColumn) this.dataGridViewTextBoxColumn1, (DataGridViewColumn) this.name, (DataGridViewColumn) this.rarity, (DataGridViewColumn) this.type, (DataGridViewColumn) this.subtype, (DataGridViewColumn) this.dataGridViewTextBoxColumn3, (DataGridViewColumn) this.dataGridViewTextBoxColumn4, (DataGridViewColumn) this.dataGridViewTextBoxColumn5, (DataGridViewColumn) this.dataGridViewTextBoxColumn6, (DataGridViewColumn) this.dataGridViewTextBoxColumn7, (DataGridViewColumn) this.dataGridViewTextBoxColumn8, (DataGridViewColumn) this.dataGridViewTextBoxColumn9, (DataGridViewColumn) this.dataGridViewTextBoxColumn10, (DataGridViewColumn) this.dataGridViewTextBoxColumn11, (DataGridViewColumn) this.dataGridViewTextBoxColumn12, (DataGridViewColumn) this.dataGridViewTextBoxColumn13, (DataGridViewColumn) this.dataGridViewTextBoxColumn14, (DataGridViewColumn) this.dataGridViewTextBoxColumn15, (DataGridViewColumn) this.dataGridViewTextBoxColumn16);
      this.dataGridView1.DataSource = (object) this.equipmentstatsBindingSource;
      this.dataGridView1.Dock = DockStyle.Fill;
      this.dataGridView1.Location = new Point(0, 0);
      this.dataGridView1.Name = "dataGridView1";
      this.dataGridView1.Size = new Size(965, 500);
      this.dataGridView1.TabIndex = 0;
      this.dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
      this.dataGridView1.CellParsing += new DataGridViewCellParsingEventHandler(this.dataGridView1_CellParsing);
      this.dataGridView1.KeyDown += new KeyEventHandler(this.dataGridView1_KeyDown);
      this.equipmentstatsBindingSource.DataMember = "equipment_stats";
      this.equipmentstatsBindingSource.DataSource = (object) this.equipmentStatsDataSet;
      this.equipmentStatsDataSet.DataSetName = "equipmentStatsDataSet";
      this.equipmentStatsDataSet.SchemaSerializationMode = SchemaSerializationMode.IncludeSchema;
      this.equipment_statsTableAdapter.ClearBeforeFill = true;
      this.dataGridViewTextBoxColumn1.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.dataGridViewTextBoxColumn1.DataPropertyName = "equipment_id";
      gridViewCellStyle1.BackColor = Color.FromArgb(228, 109, 10);
      this.dataGridViewTextBoxColumn1.DefaultCellStyle = gridViewCellStyle1;
      this.dataGridViewTextBoxColumn1.HeaderText = "equipment_id";
      this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
      this.dataGridViewTextBoxColumn1.ReadOnly = true;
      this.dataGridViewTextBoxColumn1.Width = 95;
      this.name.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.name.DataPropertyName = "name";
      gridViewCellStyle2.BackColor = Color.FromArgb(228, 109, 10);
      this.name.DefaultCellStyle = gridViewCellStyle2;
      this.name.HeaderText = "name";
      this.name.Name = "name";
      this.name.Width = 58;
      this.rarity.DataPropertyName = "rarity";
      gridViewCellStyle3.BackColor = Color.FromArgb(228, 109, 10);
      this.rarity.DefaultCellStyle = gridViewCellStyle3;
      this.rarity.HeaderText = "rarity";
      this.rarity.Name = "rarity";
      this.type.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.type.DataPropertyName = "type";
      gridViewCellStyle4.BackColor = Color.FromArgb(228, 109, 10);
      this.type.DefaultCellStyle = gridViewCellStyle4;
      this.type.HeaderText = "type";
      this.type.Name = "type";
      this.type.Resizable = DataGridViewTriState.True;
      this.type.Width = 52;
      this.subtype.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
      this.subtype.DataPropertyName = "subtype";
      gridViewCellStyle5.BackColor = Color.FromArgb(228, 109, 10);
      this.subtype.DefaultCellStyle = gridViewCellStyle5;
      this.subtype.DividerWidth = 3;
      this.subtype.HeaderText = "subtype";
      this.subtype.Name = "subtype";
      this.subtype.Resizable = DataGridViewTriState.True;
      this.subtype.Width = 72;
      this.dataGridViewTextBoxColumn3.DataPropertyName = "base_atk";
      gridViewCellStyle6.BackColor = Color.FromArgb(250, 192, 144);
      this.dataGridViewTextBoxColumn3.DefaultCellStyle = gridViewCellStyle6;
      this.dataGridViewTextBoxColumn3.HeaderText = "base_atk";
      this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
      this.dataGridViewTextBoxColumn4.DataPropertyName = "base_mag";
      gridViewCellStyle7.BackColor = Color.FromArgb(250, 192, 144);
      this.dataGridViewTextBoxColumn4.DefaultCellStyle = gridViewCellStyle7;
      this.dataGridViewTextBoxColumn4.HeaderText = "base_mag";
      this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
      this.dataGridViewTextBoxColumn5.DataPropertyName = "base_acc";
      gridViewCellStyle8.BackColor = Color.FromArgb(250, 192, 144);
      this.dataGridViewTextBoxColumn5.DefaultCellStyle = gridViewCellStyle8;
      this.dataGridViewTextBoxColumn5.HeaderText = "base_acc";
      this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
      this.dataGridViewTextBoxColumn6.DataPropertyName = "base_def";
      gridViewCellStyle9.BackColor = Color.FromArgb(250, 192, 144);
      this.dataGridViewTextBoxColumn6.DefaultCellStyle = gridViewCellStyle9;
      this.dataGridViewTextBoxColumn6.HeaderText = "base_def";
      this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
      this.dataGridViewTextBoxColumn7.DataPropertyName = "base_res";
      gridViewCellStyle10.BackColor = Color.FromArgb(250, 192, 144);
      this.dataGridViewTextBoxColumn7.DefaultCellStyle = gridViewCellStyle10;
      this.dataGridViewTextBoxColumn7.HeaderText = "base_res";
      this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
      this.dataGridViewTextBoxColumn8.DataPropertyName = "base_eva";
      gridViewCellStyle11.BackColor = Color.FromArgb(250, 192, 144);
      this.dataGridViewTextBoxColumn8.DefaultCellStyle = gridViewCellStyle11;
      this.dataGridViewTextBoxColumn8.HeaderText = "base_eva";
      this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
      this.dataGridViewTextBoxColumn9.DataPropertyName = "base_mnd";
      gridViewCellStyle12.BackColor = Color.FromArgb(250, 192, 144);
      this.dataGridViewTextBoxColumn9.DefaultCellStyle = gridViewCellStyle12;
      this.dataGridViewTextBoxColumn9.DividerWidth = 3;
      this.dataGridViewTextBoxColumn9.HeaderText = "base_mnd";
      this.dataGridViewTextBoxColumn9.Name = "dataGridViewTextBoxColumn9";
      this.dataGridViewTextBoxColumn10.DataPropertyName = "max_atk";
      gridViewCellStyle13.BackColor = Color.FromArgb(252, 213, 180);
      this.dataGridViewTextBoxColumn10.DefaultCellStyle = gridViewCellStyle13;
      this.dataGridViewTextBoxColumn10.HeaderText = "max_atk";
      this.dataGridViewTextBoxColumn10.Name = "dataGridViewTextBoxColumn10";
      this.dataGridViewTextBoxColumn11.DataPropertyName = "max_mag";
      gridViewCellStyle14.BackColor = Color.FromArgb(252, 213, 180);
      this.dataGridViewTextBoxColumn11.DefaultCellStyle = gridViewCellStyle14;
      this.dataGridViewTextBoxColumn11.HeaderText = "max_mag";
      this.dataGridViewTextBoxColumn11.Name = "dataGridViewTextBoxColumn11";
      this.dataGridViewTextBoxColumn12.DataPropertyName = "max_acc";
      gridViewCellStyle15.BackColor = Color.FromArgb(252, 213, 180);
      this.dataGridViewTextBoxColumn12.DefaultCellStyle = gridViewCellStyle15;
      this.dataGridViewTextBoxColumn12.HeaderText = "max_acc";
      this.dataGridViewTextBoxColumn12.Name = "dataGridViewTextBoxColumn12";
      this.dataGridViewTextBoxColumn13.DataPropertyName = "max_def";
      gridViewCellStyle16.BackColor = Color.FromArgb(252, 213, 180);
      this.dataGridViewTextBoxColumn13.DefaultCellStyle = gridViewCellStyle16;
      this.dataGridViewTextBoxColumn13.HeaderText = "max_def";
      this.dataGridViewTextBoxColumn13.Name = "dataGridViewTextBoxColumn13";
      this.dataGridViewTextBoxColumn14.DataPropertyName = "max_res";
      gridViewCellStyle17.BackColor = Color.FromArgb(252, 213, 180);
      this.dataGridViewTextBoxColumn14.DefaultCellStyle = gridViewCellStyle17;
      this.dataGridViewTextBoxColumn14.HeaderText = "max_res";
      this.dataGridViewTextBoxColumn14.Name = "dataGridViewTextBoxColumn14";
      this.dataGridViewTextBoxColumn15.DataPropertyName = "max_eva";
      gridViewCellStyle18.BackColor = Color.FromArgb(252, 213, 180);
      this.dataGridViewTextBoxColumn15.DefaultCellStyle = gridViewCellStyle18;
      this.dataGridViewTextBoxColumn15.HeaderText = "max_eva";
      this.dataGridViewTextBoxColumn15.Name = "dataGridViewTextBoxColumn15";
      this.dataGridViewTextBoxColumn16.DataPropertyName = "max_mnd";
      gridViewCellStyle19.BackColor = Color.FromArgb(252, 213, 180);
      this.dataGridViewTextBoxColumn16.DefaultCellStyle = gridViewCellStyle19;
      this.dataGridViewTextBoxColumn16.HeaderText = "max_mnd";
      this.dataGridViewTextBoxColumn16.Name = "dataGridViewTextBoxColumn16";
      this.AutoScaleDimensions = new SizeF(6f, 13f);
      this.AutoScaleMode = AutoScaleMode.Font;
      this.Controls.Add((Control) this.dataGridView1);
      this.Name = "EditExistingItemsPanel";
      this.Size = new Size(965, 500);
      this.Load += new EventHandler(this.EditExistingItemsPanel_Load);
      ((ISupportInitialize) this.dataGridView1).EndInit();
      ((ISupportInitialize) this.equipmentstatsBindingSource).EndInit();
      this.equipmentStatsDataSet.EndInit();
      this.ResumeLayout(false);
    }
  }
}
