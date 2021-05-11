// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.SelectSingleParam`1
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using MySql.Data.MySqlClient;
using System;

namespace FFRKInspector.Database
{
  internal class SelectSingleParam<T> : ISelectParam
  {
    private string mColumn;
    private T mValue;
    private bool mIsSet;
    private SelectSingleParam<T>.ParamOperator mOperator;

    public T Value
    {
      get => this.mValue;
      set
      {
        this.mValue = value;
        this.mIsSet = true;
      }
    }

    public string ParamName => "@" + this.mColumn + "_value";

    public bool HasValue
    {
      get
      {
        if (!this.mIsSet || (object) this.mValue == null)
          return false;
        return typeof (T) != typeof (string) || (string) Convert.ChangeType((object) this.mValue, typeof (string)) != string.Empty;
      }
    }

    public string WhereClause
    {
      get
      {
        if (!this.HasValue)
          return (string) null;
        string str = "=";
        switch (this.mOperator)
        {
          case SelectSingleParam<T>.ParamOperator.Equals:
            str = "=";
            break;
          case SelectSingleParam<T>.ParamOperator.Like:
            str = "LIKE";
            break;
          case SelectSingleParam<T>.ParamOperator.Greater:
            str = ">";
            break;
        }
        return string.Format("{0} {1} {2}", (object) this.mColumn, (object) str, (object) this.ParamName);
      }
    }

    public SelectSingleParam(string Column, SelectSingleParam<T>.ParamOperator Operator)
    {
      this.mColumn = Column;
      this.mIsSet = false;
      this.mOperator = Operator;
    }

    public void Bind(MySqlCommand Command)
    {
      if (this.mOperator == SelectSingleParam<T>.ParamOperator.Like)
        Command.Parameters.AddWithValue(this.ParamName, (object) ("%" + (object) this.mValue + "%"));
      else
        Command.Parameters.AddWithValue(this.ParamName, (object) this.mValue);
    }

    public enum ParamOperator
    {
      Equals,
      Like,
      Greater,
    }
  }
}
