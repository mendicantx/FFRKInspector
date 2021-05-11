// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.ListListViewBinding`1
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.Collections.Generic;

namespace FFRKInspector.UI
{
  internal class ListListViewBinding<T> : IListViewBinding
  {
    private List<T> mCollection;

    public List<T> Collection
    {
      get => this.mCollection;
      set => this.mCollection = value;
    }

    public ListListViewBinding() => this.mCollection = new List<T>();

    public void SortData(Comparison<object> Comparer) => this.mCollection.Sort((Comparison<T>) ((x, y) => Comparer((object) x, (object) y)));

    public object RetrieveItem(int Index) => (object) this.mCollection[Index];
  }
}
