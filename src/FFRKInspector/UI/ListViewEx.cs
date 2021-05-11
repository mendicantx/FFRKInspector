// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.ListViewEx
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Config;
using FFRKInspector.Proxy;
using FFRKInspector.UI.ListViewFields;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  internal class ListViewEx : ListView
  {
    private Rectangle mHeaderRect;
    private List<ListViewEx.ListViewFieldAdapter> mFields;
    private ContextMenuStrip mHeaderContextMenuStrip;
    private SortOrder mCurrentSortOrder;
    private ColumnHeader mCurrentSortColumn;
    private IListViewBinding mBinding;
    private ListViewSettings mSettings;
    private ContextMenuStrip contextMenuStrip1;
    private IContainer components;
    private ToolStripMenuItem toolStripMenuItemExportAll;
    private ToolStripMenuItem toolStripMenuItemExportSelected;
    private string mSettingsKey;

    public IListViewBinding DataBinding
    {
      get => this.mBinding;
      set => this.mBinding = value;
    }

    [Category("Behavior")]
    [Browsable(true)]
    public string SettingsKey
    {
      get => this.mSettingsKey;
      set => this.mSettingsKey = value;
    }

    public ListViewEx()
    {
      this.mCurrentSortOrder = SortOrder.None;
      this.mCurrentSortColumn = (ColumnHeader) null;
      this.mFields = new List<ListViewEx.ListViewFieldAdapter>();
      this.InitializeComponent();
      this.HandleCreated += new EventHandler(this.ListViewEx_HandleCreated);
      this.RetrieveVirtualItem += new RetrieveVirtualItemEventHandler(this.ListViewEx_RetrieveVirtualItem);
      this.ColumnClick += new ColumnClickEventHandler(this.ListViewEx_ColumnClick);
      this.ColumnWidthChanged += new ColumnWidthChangedEventHandler(this.ListViewEx_ColumnWidthChanged);
      this.View = View.Details;
    }

    [DllImport("user32.dll")]
    private static extern int EnumChildWindows(
      IntPtr HwndParent,
      ListViewEx.EnumWinCallback Callback,
      IntPtr LParam);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr Hwnd, out ListViewEx.RECT Rect);

    public void LoadSettings()
    {
      if (this.mSettingsKey == null || FFRKProxy.Instance == null)
        return;
      AppSettings appSettings = FFRKProxy.Instance.AppSettings;
      if (appSettings == null || appSettings.ListViews.TryGetValue(this.mSettingsKey, out this.mSettings))
        return;
      this.mSettings = new ListViewSettings();
      FFRKProxy.Instance.AppSettings.ListViews.Add(this.mSettingsKey, this.mSettings);
    }

    public void AddField(IListViewField Field)
    {
      ListViewEx.ListViewFieldAdapter viewFieldAdapter = new ListViewEx.ListViewFieldAdapter()
      {
        Field = Field,
        Column = new ColumnHeader()
      };
      viewFieldAdapter.Column.Text = Field.DisplayName;
      viewFieldAdapter.Column.Name = Field.GetType().Name;
      int Magnitude = Field.InitialWidth;
      FieldWidthStyle Style = Field.InitialWidthStyle;
      ListViewColumnSettings viewColumnSettings = (ListViewColumnSettings) null;
      if (this.mSettings != null)
      {
        viewColumnSettings = this.mSettings.GetColumnSettings(viewFieldAdapter.Column, Field.InitialWidthStyle, Field.InitialWidth);
        Magnitude = viewColumnSettings.Width;
        Style = viewColumnSettings.WidthStyle;
      }
      viewFieldAdapter.Column.Width = this.ComputeColumnWidth(Style, Magnitude);
      this.mFields.Add(viewFieldAdapter);
      if (viewColumnSettings != null && !viewColumnSettings.Visible)
        return;
      viewFieldAdapter.Column.DisplayIndex = this.mFields.Count + 1;
      this.Columns.Add(viewFieldAdapter.Column);
    }

    private void ListViewEx_ColumnWidthChanged(object sender, ColumnWidthChangedEventArgs e)
    {
      ColumnHeader column1 = this.Columns[e.ColumnIndex];
      if (this.mSettings == null)
        return;
      ListViewColumnSettings column2 = this.mSettings.Columns[column1.Name];
      column2.WidthStyle = FieldWidthStyle.Absolute;
      column2.Width = column1.Width;
    }

    private void ListViewEx_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      ColumnHeader clicked_column = this.Columns[e.Column];
      if (clicked_column == this.mCurrentSortColumn)
      {
        if (this.mCurrentSortOrder == SortOrder.Ascending)
          this.mCurrentSortOrder = SortOrder.Descending;
        else if (this.mCurrentSortOrder == SortOrder.Descending)
          this.mCurrentSortOrder = SortOrder.Ascending;
      }
      else
      {
        this.mCurrentSortColumn = clicked_column;
        this.mCurrentSortOrder = SortOrder.Ascending;
      }
      ListViewEx.ListViewFieldAdapter Field = this.mFields.Find((Predicate<ListViewEx.ListViewFieldAdapter>) (x => x.Column == clicked_column));
      switch (this.mCurrentSortOrder)
      {
        case SortOrder.Ascending:
          this.mBinding.SortData(new Comparison<object>(Field.Field.Compare));
          this.Invalidate();
          break;
        case SortOrder.Descending:
          this.mBinding.SortData((Comparison<object>) ((x, y) => -Field.Field.Compare(x, y)));
          this.Invalidate();
          break;
      }
    }

    private int ComputeColumnWidth(FieldWidthStyle Style, int Magnitude)
    {
      switch (Style)
      {
        case FieldWidthStyle.Percent:
          return (int) ((double) this.Width * ((double) Magnitude / 100.0));
        case FieldWidthStyle.Absolute:
          return Magnitude;
        case FieldWidthStyle.AutoSize:
          return -2;
        default:
          return Magnitude;
      }
    }

    private void ListViewEx_HandleCreated(object sender, EventArgs e)
    {
      this.mHeaderContextMenuStrip = new ContextMenuStrip();
      foreach (ColumnHeader columnHeader in this.mFields.Select<ListViewEx.ListViewFieldAdapter, ColumnHeader>((Func<ListViewEx.ListViewFieldAdapter, ColumnHeader>) (x => x.Column)))
      {
        ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem(columnHeader.Text);
        ListViewColumnSettings viewColumnSettings = this.mSettings == null ? (ListViewColumnSettings) null : this.mSettings.Columns[columnHeader.Name];
        toolStripMenuItem.Checked = viewColumnSettings == null || viewColumnSettings.Visible;
        toolStripMenuItem.CheckOnClick = true;
        toolStripMenuItem.Tag = (object) new ListViewEx.ToolStripItemTag()
        {
          Header = columnHeader,
          LastIndex = columnHeader.DisplayIndex,
          LastWidth = (this.mSettings == null ? columnHeader.Width : this.ComputeColumnWidth(viewColumnSettings.WidthStyle, viewColumnSettings.Width))
        };
        toolStripMenuItem.CheckStateChanged += new EventHandler(this.item_CheckStateChanged);
        this.mHeaderContextMenuStrip.Items.Add((ToolStripItem) toolStripMenuItem);
      }
    }

    private void ListViewEx_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
    {
      object obj = this.mBinding.RetrieveItem(e.ItemIndex);
      List<string> stringList = new List<string>();
      foreach (ListViewEx.ListViewFieldAdapter mField in this.mFields)
      {
        if (mField.Column.DisplayIndex != -1)
        {
          string str = mField.Field.Format(obj);
          stringList.Add(str);
        }
      }
      e.Item = new ListViewItem(stringList.ToArray());
    }

    private void item_CheckStateChanged(object sender, EventArgs e)
    {
      ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem) sender;
      ListViewEx.ToolStripItemTag tag = (ListViewEx.ToolStripItemTag) toolStripMenuItem.Tag;
      ColumnHeader column_to_change = tag.Header;
      this.mFields.Find((Predicate<ListViewEx.ListViewFieldAdapter>) (x => x.Column == column_to_change));
      if (toolStripMenuItem.CheckState == CheckState.Checked)
      {
        int forInsertingColumn = this.DetermineIndexForInsertingColumn(tag.Header);
        this.Columns.Insert(forInsertingColumn, tag.Header);
        tag.Header.DisplayIndex = forInsertingColumn;
        tag.Header.Width = tag.LastWidth;
        if (this.mSettings == null)
          return;
        this.mSettings.Columns[column_to_change.Name].Visible = true;
      }
      else
      {
        tag.LastWidth = tag.Header.Width;
        this.Columns.Remove(tag.Header);
        if (this.mSettings != null)
          this.mSettings.Columns[column_to_change.Name].Visible = false;
      }
    }

    private int DetermineIndexForInsertingColumn(ColumnHeader Header) => this.mFields.TakeWhile<ListViewEx.ListViewFieldAdapter>((Func<ListViewEx.ListViewFieldAdapter, bool>) (x => x.Column != Header)).Count<ListViewEx.ListViewFieldAdapter>((Func<ListViewEx.ListViewFieldAdapter, bool>) (x => x.Column.DisplayIndex != -1));

    private bool EnumWindowCallback(IntPtr Hwnd, IntPtr LParam)
    {
      ListViewEx.RECT Rect;
      this.mHeaderRect = ListViewEx.GetWindowRect(Hwnd, out Rect) ? new Rectangle(Rect.Left, Rect.Top, Rect.Right - Rect.Left + 1, Rect.Bottom - Rect.Top + 1) : Rectangle.Empty;
      return false;
    }

    private void InitializeComponent()
    {
      this.components = (IContainer) new Container();
      this.contextMenuStrip1 = new ContextMenuStrip(this.components);
      this.toolStripMenuItemExportAll = new ToolStripMenuItem();
      this.toolStripMenuItemExportSelected = new ToolStripMenuItem();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      this.contextMenuStrip1.Items.AddRange(new ToolStripItem[2]
      {
        (ToolStripItem) this.toolStripMenuItemExportAll,
        (ToolStripItem) this.toolStripMenuItemExportSelected
      });
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new Size(186, 48);
      this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
      this.contextMenuStrip1.ItemClicked += new ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
      this.toolStripMenuItemExportAll.Name = "toolStripMenuItemExportAll";
      this.toolStripMenuItemExportAll.Size = new Size(185, 22);
      this.toolStripMenuItemExportAll.Text = "Export All Rows";
      this.toolStripMenuItemExportSelected.Name = "toolStripMenuItemExportSelected";
      this.toolStripMenuItemExportSelected.Size = new Size(185, 22);
      this.toolStripMenuItemExportSelected.Text = "Export Selected Rows";
      this.ContextMenuStrip = this.contextMenuStrip1;
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);
    }

    private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      if (e.ClickedItem == this.toolStripMenuItemExportAll)
      {
        this.ExportRows(Enumerable.Range(0, this.Items.Count).ToArray<int>());
      }
      else
      {
        if (e.ClickedItem != this.toolStripMenuItemExportSelected)
          return;
        int[] Rows = new int[this.SelectedIndices.Count];
        this.SelectedIndices.CopyTo((Array) Rows, 0);
        this.ExportRows(Rows);
      }
    }

    private void ExportRows(int[] Rows)
    {
      try
      {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.Filter = "CSV files (*.csv)|*.csv";
        saveFileDialog.FilterIndex = 0;
        saveFileDialog.RestoreDirectory = false;
        if (saveFileDialog.ShowDialog() == DialogResult.OK)
        {
          using (Stream stream = saveFileDialog.OpenFile())
          {
            using (StreamWriter streamWriter = new StreamWriter(stream))
            {
              List<ListViewEx.ListViewFieldAdapter> list = this.mFields.Where<ListViewEx.ListViewFieldAdapter>((Func<ListViewEx.ListViewFieldAdapter, bool>) (x => x.Column.DisplayIndex != -1)).ToList<ListViewEx.ListViewFieldAdapter>();
              for (int index = 0; index < list.Count; ++index)
              {
                streamWriter.Write(list[index].Field.DisplayName);
                if (index < list.Count - 1)
                  streamWriter.Write(",");
              }
              streamWriter.Write("\n");
              foreach (int row in Rows)
              {
                object obj = this.mBinding.RetrieveItem(row);
                List<string> stringList = new List<string>();
                for (int index = 0; index < list.Count; ++index)
                {
                  ListViewEx.ListViewFieldAdapter viewFieldAdapter = list[index];
                  string str = !(viewFieldAdapter.Field is ItemRarityField) ? viewFieldAdapter.Field.Format(obj) : viewFieldAdapter.Field.AltFormat(obj);
                  streamWriter.Write(str.Replace(',', ' '));
                  if (index < list.Count - 1)
                    streamWriter.Write(",");
                }
                streamWriter.Write("\n");
              }
            }
          }
        }
        int num = (int) MessageBox.Show(string.Format("{0} rows successfully exported.", (object) Rows.Length));
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show("FFRK Inspector encountered an error while exporting the data.  " + ex.Message);
      }
    }

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
      ListViewEx.EnumChildWindows(this.Handle, new ListViewEx.EnumWinCallback(this.EnumWindowCallback), IntPtr.Zero);
      if (this.mHeaderRect.Contains(Control.MousePosition))
      {
        e.Cancel = true;
        this.mHeaderContextMenuStrip.Show(Control.MousePosition);
      }
      else
      {
        this.toolStripMenuItemExportAll.Enabled = this.Items.Count > 0;
        this.toolStripMenuItemExportSelected.Enabled = this.SelectedIndices.Count > 0;
      }
    }

    private class ToolStripItemTag
    {
      public int LastIndex;
      public int LastWidth;
      public ColumnHeader Header;
    }

    private struct RECT
    {
      public int Left;
      public int Top;
      public int Right;
      public int Bottom;
    }

    private struct ListViewFieldAdapter
    {
      public IListViewField Field;
      public ColumnHeader Column;
    }

    private delegate bool EnumWinCallback(IntPtr Hwnd, IntPtr LParam);
  }
}
