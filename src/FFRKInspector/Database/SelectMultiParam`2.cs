// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.SelectMultiParam`2
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace FFRKInspector.Database
{
  internal class SelectMultiParam<T, U> : ISelectParam
  {
    private string mColumn;
    private List<T> mValues;
    private Converter<T, U> mConverter;

    public bool HasValue => this.mValues.Count > 0;

    public string WhereClause
    {
      get
      {
        if (!this.HasValue)
          return (string) null;
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendFormat("{0} IN (", (object) this.mColumn);
        stringBuilder.AppendFormat("@{0}_value_0", (object) this.mColumn);
        for (int index = 1; index < this.mValues.Count; ++index)
          stringBuilder.AppendFormat(", @{0}_value_{1}", (object) this.mColumn, (object) index);
        stringBuilder.Append(")");
        return stringBuilder.ToString();
      }
    }

    public SelectMultiParam(string Column)
    {
      this.mColumn = Column;
      this.mValues = new List<T>();
    }

    public SelectMultiParam(string Column, Converter<T, U> Converter)
    {
      this.mColumn = Column;
      this.mValues = new List<T>();
      this.mConverter = Converter;
    }

    public void AddValue(T Value) => this.mValues.Add(Value);

    public void Bind(MySqlCommand Command)
    {
      for (int index = 0; index < this.mValues.Count; ++index)
      {
        string parameterName = string.Format("@{0}_value_{1}", (object) this.mColumn, (object) index);
        U u = this.mConverter == null ? (U) Convert.ChangeType((object) this.mValues[index], typeof (U)) : this.mConverter(this.mValues[index]);
        Command.Parameters.AddWithValue(parameterName, (object) u);
      }
    }
  }
}
