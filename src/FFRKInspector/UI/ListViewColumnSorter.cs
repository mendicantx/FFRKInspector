// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.ListViewColumnSorter
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  internal class ListViewColumnSorter : IComparer
  {
    private int mColumn;
    private SortOrder mOrder;
    private Dictionary<int, ListViewColumnSorter.SortParameters> mSorters;

    public int SortColumn
    {
      get => this.mColumn;
      set => this.mColumn = value;
    }

    public SortOrder Order
    {
      get => this.mOrder;
      set => this.mOrder = value;
    }

    public ListViewColumnSorter()
    {
      this.mColumn = 0;
      this.mOrder = SortOrder.None;
      this.mSorters = new Dictionary<int, ListViewColumnSorter.SortParameters>();
    }

    public void AddSorter<T>(int Index) => this.mSorters[Index] = new ListViewColumnSorter.SortParameters()
    {
      Comparer = (IComparer) new ListViewColumnSorter.TypedStringComparer<T>(),
      Converter = (IConverter<string, string>) null
    };

    public void AddSorter<T>(int Index, IConverter<string, string> Converter) => this.mSorters[Index] = new ListViewColumnSorter.SortParameters()
    {
      Comparer = (IComparer) new ListViewColumnSorter.TypedStringComparer<T>(),
      Converter = Converter
    };

    public int Compare(object x, object y)
    {
      if (this.mOrder == SortOrder.None)
        return 0;
      ListViewItem listViewItem1 = (ListViewItem) x;
      ListViewItem listViewItem2 = (ListViewItem) y;
      string u1 = listViewItem1.SubItems[this.mColumn].Text;
      string u2 = listViewItem2.SubItems[this.mColumn].Text;
      ListViewColumnSorter.SortParameters sortParameters = (ListViewColumnSorter.SortParameters) null;
      int num;
      if (!this.mSorters.TryGetValue(this.mColumn, out sortParameters))
      {
        num = CaseInsensitiveComparer.Default.Compare((object) u1, (object) u2);
      }
      else
      {
        if (sortParameters.Converter != null)
        {
          u1 = sortParameters.Converter.Convert(u1);
          u2 = sortParameters.Converter.Convert(u2);
        }
        num = sortParameters.Comparer.Compare((object) u1, (object) u2);
      }
      return this.mOrder == SortOrder.Ascending ? num : -num;
    }

    private class SortParameters
    {
      public IComparer Comparer;
      public IConverter<string, string> Converter;
    }

    private class TypedStringComparer<T> : IComparer
    {
      public int Compare(object x, object y) => Comparer<T>.Default.Compare((T) Convert.ChangeType(x, typeof (T)), (T) Convert.ChangeType(y, typeof (T)));
    }
  }
}
