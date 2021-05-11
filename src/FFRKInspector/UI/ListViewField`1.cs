// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.ListViewField`1
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

namespace FFRKInspector.UI
{
  internal abstract class ListViewField<T> : IListViewField
  {
    private string mDisplayName;
    private FieldWidthStyle mWidthStyle;
    private int mInitialWidth;

    public string DisplayName => this.mDisplayName;

    public int InitialWidth => this.mInitialWidth;

    public FieldWidthStyle InitialWidthStyle => this.mWidthStyle;

    protected ListViewField(string DisplayName)
    {
      this.mDisplayName = DisplayName;
      this.mWidthStyle = FieldWidthStyle.AutoSize;
      this.mInitialWidth = 0;
    }

    protected ListViewField(string DisplayName, FieldWidthStyle WidthStyle, int InitialWidth)
    {
      this.mDisplayName = DisplayName;
      this.mWidthStyle = WidthStyle;
      this.mInitialWidth = InitialWidth;
    }

    public int Compare(object x, object y) => this.CompareValues((T) x, (T) y);

    public string Format(object value) => this.FormatValue((T) value);

    public string AltFormat(object value) => this.AltFormatValue((T) value);

    protected abstract int CompareValues(T x, T y);

    protected abstract string FormatValue(T value);

    protected virtual string AltFormatValue(T value) => this.FormatValue(value);
  }
}
