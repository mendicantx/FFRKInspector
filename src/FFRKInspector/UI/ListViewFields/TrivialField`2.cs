// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.ListViewFields.TrivialField`2
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;

namespace FFRKInspector.UI.ListViewFields
{
  internal class TrivialField<T, U> : ListViewField<T> where U : IComparable<U>
  {
    private Converter<T, U> mGetter;

    protected TrivialField(string DisplayName, Converter<T, U> Getter)
      : base(DisplayName)
    {
      this.mGetter = Getter;
    }

    protected TrivialField(
      string DisplayName,
      Converter<T, U> Getter,
      FieldWidthStyle WidthStyle,
      int Width)
      : base(DisplayName, WidthStyle, Width)
    {
      this.mGetter = Getter;
    }

    protected override int CompareValues(T x, T y) => this.mGetter(x).CompareTo(this.mGetter(y));

    protected override string FormatValue(T value) => this.mGetter(value).ToString();
  }
}
