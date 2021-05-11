// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.DeselectableListBox
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  internal class DeselectableListBox : ListBox
  {
    private ContextMenuStrip mContextMenu;

    [Browsable(true)]
    [Category("Behavior")]
    public event EventHandler SelectionCleared;

    public DeselectableListBox()
    {
      this.mContextMenu = new ContextMenuStrip();
      this.mContextMenu.Items.Add("Clear selected items");
      this.mContextMenu.ItemClicked += new ToolStripItemClickedEventHandler(this.ContextMenuStrip_ItemClicked);
      this.ContextMenuStrip = this.mContextMenu;
    }

    private void ContextMenuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
    {
      this.SelectedItems.Clear();
      if (this.SelectionCleared == null)
        return;
      this.SelectionCleared((object) this.mContextMenu, (EventArgs) null);
    }
  }
}
