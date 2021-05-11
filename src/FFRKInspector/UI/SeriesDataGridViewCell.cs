// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.SeriesDataGridViewCell
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FFRKInspector.UI
{
  internal class SeriesDataGridViewCell : DataGridViewTextBoxCell
  {
    public override object ParseFormattedValue(
      object formattedValue,
      DataGridViewCellStyle cellStyle,
      TypeConverter formattedValueTypeConverter,
      TypeConverter valueTypeConverter)
    {
      if (formattedValue == null || formattedValue == DBNull.Value)
        return (object) DBNull.Value;
      string Name = (string) formattedValue;
      return Name == string.Empty ? (object) DBNull.Value : (object) RealmSynergy.FromName(Name).GameSeries;
    }

    protected override object GetFormattedValue(
      object value,
      int rowIndex,
      ref DataGridViewCellStyle cellStyle,
      TypeConverter valueTypeConverter,
      TypeConverter formattedValueTypeConverter,
      DataGridViewDataErrorContexts context)
    {
      return value == DBNull.Value || value == null ? (object) "" : (object) RealmSynergy.FromSeries((uint) value).Text;
    }
  }
}
