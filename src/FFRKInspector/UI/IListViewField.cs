// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.IListViewField
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

namespace FFRKInspector.UI
{
  public interface IListViewField
  {
    string DisplayName { get; }

    int InitialWidth { get; }

    FieldWidthStyle InitialWidthStyle { get; }

    int Compare(object x, object y);

    string Format(object value);

    string AltFormat(object value);
  }
}
