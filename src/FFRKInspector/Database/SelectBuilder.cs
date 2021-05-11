// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.SelectBuilder
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FFRKInspector.Database
{
  internal class SelectBuilder
  {
    private string mTable;
    private List<string> mColumns;
    private List<ISelectParam> mParameters;

    public string Table
    {
      get => this.mTable;
      set => this.mTable = value;
    }

    public IList<string> Columns => (IList<string>) this.mColumns;

    public IList<ISelectParam> Parameters => (IList<ISelectParam>) this.mParameters;

    public IList<ISelectParam> UsedParameters => (IList<ISelectParam>) this.mParameters.Where<ISelectParam>((Func<ISelectParam, bool>) (x => x.HasValue)).ToList<ISelectParam>();

    public SelectBuilder()
    {
      this.mColumns = new List<string>();
      this.mParameters = new List<ISelectParam>();
    }

    public override string ToString()
    {
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.Append("SELECT ");
      if (this.mColumns.Count == 0)
      {
        stringBuilder.Append("*");
      }
      else
      {
        stringBuilder.Append(this.mColumns[0]);
        for (int index = 1; index < this.mColumns.Count; ++index)
          stringBuilder.AppendFormat(", {0}", (object) this.mColumns[index]);
      }
      stringBuilder.AppendFormat(" FROM {0}", (object) this.mTable);
      IList<ISelectParam> usedParameters = this.UsedParameters;
      if (usedParameters.Count == 0)
        return stringBuilder.ToString();
      stringBuilder.AppendFormat(" WHERE ({0})", (object) usedParameters[0].WhereClause);
      for (int index = 1; index < usedParameters.Count; ++index)
        stringBuilder.AppendFormat(" AND ({0})", (object) usedParameters[index].WhereClause);
      return stringBuilder.ToString();
    }

    public void Bind(MySqlCommand Command)
    {
      foreach (ISelectParam usedParameter in (IEnumerable<ISelectParam>) this.UsedParameters)
        usedParameter.Bind(Command);
    }
  }
}
