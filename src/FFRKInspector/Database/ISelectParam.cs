// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.ISelectParam
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using MySql.Data.MySqlClient;

namespace FFRKInspector.Database
{
  public interface ISelectParam
  {
    string WhereClause { get; }

    bool HasValue { get; }

    void Bind(MySqlCommand Command);
  }
}
