// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.EnumDataViewGridComboBoxCell`1
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  public class EnumDataViewGridComboBoxCell<T> : DataGridViewComboBoxCell where T : struct
  {
    public EnumDataViewGridComboBoxCell() => this.DataSource = (object) Enum.GetValues(typeof (T)).Cast<T>().Select<T, string>((Func<T, string>) (x => x.ToString())).ToArray<string>();

    public override object ParseFormattedValue(
      object formattedValue,
      DataGridViewCellStyle cellStyle,
      TypeConverter formattedValueTypeConverter,
      TypeConverter valueTypeConverter)
    {
      if (formattedValue == null || formattedValue == DBNull.Value)
        return (object) DBNull.Value;
      string str = (string) formattedValue;
      return str == string.Empty ? (object) DBNull.Value : Enum.Parse(typeof (T), str, true);
    }

    protected override object GetFormattedValue(
      object value,
      int rowIndex,
      ref DataGridViewCellStyle cellStyle,
      TypeConverter valueTypeConverter,
      TypeConverter formattedValueTypeConverter,
      DataGridViewDataErrorContexts context)
    {
      if (value == DBNull.Value || value == null)
        return (object) "";
      string str = value.ToString();
      T result;
      return Enum.TryParse<T>(str, true, out result) ? (object) result.ToString() : (object) str;
    }
  }
}
