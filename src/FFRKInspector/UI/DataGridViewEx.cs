// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.DataGridViewEx
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  internal class DataGridViewEx : DataGridView
  {
    public DataGridViewEx()
    {
      this.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(this.DataGridViewEx_EditingControlShowing);
      this.CurrentCellDirtyStateChanged += new EventHandler(this.DataGridViewEx_CurrentCellDirtyStateChanged);
    }

    private void DataGridViewEx_CurrentCellDirtyStateChanged(object sender, EventArgs e)
    {
      if (!this.IsCurrentCellDirty || !(this.CurrentCell is DataGridViewCheckBoxCell) && !(this.CurrentCell is DataGridViewComboBoxCell))
        return;
      this.CommitEdit(DataGridViewDataErrorContexts.Commit);
    }

    private void DataGridViewEx_EditingControlShowing(
      object sender,
      DataGridViewEditingControlShowingEventArgs e)
    {
      DataGridViewCell cellTemplate = this.CurrentCell.OwningColumn.CellTemplate;
      if (cellTemplate == null)
        return;
      IDataGridViewAutoCompleteSource autoCompleteSource = cellTemplate as IDataGridViewAutoCompleteSource;
      if (!(e.Control is TextBox control))
        return;
      if (autoCompleteSource == null)
      {
        control.AutoCompleteMode = AutoCompleteMode.None;
        control.AutoCompleteSource = AutoCompleteSource.None;
        control.AutoCompleteCustomSource = (AutoCompleteStringCollection) null;
      }
      else
      {
        control.AutoCompleteMode = AutoCompleteMode.Append;
        control.AutoCompleteSource = AutoCompleteSource.CustomSource;
        control.AutoCompleteCustomSource = autoCompleteSource.AutoCompleteSource;
      }
    }

    protected override bool ProcessDialogKey(Keys keyData) => base.ProcessDialogKey(keyData);

    protected override bool ProcessDataGridViewKey(KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Left:
        case Keys.Right:
          if (this.IsCurrentCellInEditMode && e.Control)
          {
            this.OnKeyDown(e);
            return true;
          }
          break;
      }
      return base.ProcessDataGridViewKey(e);
    }
  }
}
