// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DataRecordExtensions
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.Data;

namespace FFRKInspector.Database
{
  public static class DataRecordExtensions
  {
    public static bool ColumnExists(this IDataRecord Record, string Column)
    {
      for (int i = 0; i < Record.FieldCount; ++i)
      {
        if (Record.GetName(i).Equals(Column, StringComparison.CurrentCultureIgnoreCase))
          return true;
      }
      return false;
    }

    public static T? GetValueOrNull<T>(this IDataRecord Record, string Column) where T : struct
    {
      int ordinal = Record.GetOrdinal(Column);
      if (ordinal == -1)
        return new T?();
      return Record.IsDBNull(ordinal) ? new T?() : new T?((T) Record[ordinal]);
    }
  }
}
