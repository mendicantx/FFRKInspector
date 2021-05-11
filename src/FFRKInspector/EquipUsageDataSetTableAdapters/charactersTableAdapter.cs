// Decompiled with JetBrains decompiler
// Type: FFRKInspector.EquipUsageDataSetTableAdapters.charactersTableAdapter
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Properties;
using MySql.Data.MySqlClient;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace FFRKInspector.EquipUsageDataSetTableAdapters
{
  [Designer("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  [HelpKeyword("vs.data.TableAdapter")]
  [DesignerCategory("code")]
  [ToolboxItem(true)]
  [DataObject(true)]
  public class charactersTableAdapter : Component
  {
    private MySqlDataAdapter _adapter;
    private MySqlConnection _connection;
    private MySqlTransaction _transaction;
    private MySqlCommand[] _commandCollection;
    private bool _clearBeforeFill;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected internal MySqlDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
          this.InitAdapter();
        return this._adapter;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    internal MySqlConnection Connection
    {
      get
      {
        if (this._connection == null)
          this.InitConnection();
        return this._connection;
      }
      set
      {
        this._connection = value;
        if (this.Adapter.InsertCommand != null)
          this.Adapter.InsertCommand.Connection = value;
        if (this.Adapter.DeleteCommand != null)
          this.Adapter.DeleteCommand.Connection = value;
        if (this.Adapter.UpdateCommand != null)
          this.Adapter.UpdateCommand.Connection = value;
        for (int index = 0; index < this.CommandCollection.Length; ++index)
        {
          if (this.CommandCollection[index] != null)
            this.CommandCollection[index].Connection = value;
        }
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    internal MySqlTransaction Transaction
    {
      get => this._transaction;
      set
      {
        this._transaction = value;
        for (int index = 0; index < this.CommandCollection.Length; ++index)
          this.CommandCollection[index].Transaction = this._transaction;
        if (this.Adapter != null && this.Adapter.DeleteCommand != null)
          this.Adapter.DeleteCommand.Transaction = this._transaction;
        if (this.Adapter != null && this.Adapter.InsertCommand != null)
          this.Adapter.InsertCommand.Transaction = this._transaction;
        if (this.Adapter == null || this.Adapter.UpdateCommand == null)
          return;
        this.Adapter.UpdateCommand.Transaction = this._transaction;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected MySqlCommand[] CommandCollection
    {
      get
      {
        if (this._commandCollection == null)
          this.InitCommandCollection();
        return this._commandCollection;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool ClearBeforeFill
    {
      get => this._clearBeforeFill;
      set => this._clearBeforeFill = value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public charactersTableAdapter() => this.ClearBeforeFill = true;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private void InitAdapter()
    {
      this._adapter = new MySqlDataAdapter();
      this._adapter.TableMappings.Add((object) new DataTableMapping()
      {
        SourceTable = "Table",
        DataSetTable = "characters",
        ColumnMappings = {
          {
            "id",
            "id"
          },
          {
            "name",
            "name"
          }
        }
      });
      this._adapter.DeleteCommand = new MySqlCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText = "DELETE FROM `characters` WHERE ((`id` = @p1) AND (`name` = @p2))";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      MySqlParameter mySqlParameter1 = new MySqlParameter();
      mySqlParameter1.ParameterName = "@p1";
      mySqlParameter1.DbType = DbType.UInt32;
      mySqlParameter1.MySqlDbType = MySqlDbType.UInt32;
      mySqlParameter1.IsNullable = true;
      mySqlParameter1.SourceColumn = "id";
      mySqlParameter1.SourceVersion = DataRowVersion.Original;
      this._adapter.DeleteCommand.Parameters.Add(mySqlParameter1);
      MySqlParameter mySqlParameter2 = new MySqlParameter();
      mySqlParameter2.ParameterName = "@p2";
      mySqlParameter2.DbType = DbType.String;
      mySqlParameter2.MySqlDbType = MySqlDbType.VarChar;
      mySqlParameter2.IsNullable = true;
      mySqlParameter2.SourceColumn = "name";
      mySqlParameter2.SourceVersion = DataRowVersion.Original;
      this._adapter.DeleteCommand.Parameters.Add(mySqlParameter2);
      this._adapter.UpdateCommand = new MySqlCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText = "UPDATE `characters` SET `id` = @p1, `name` = @p2 WHERE ((`id` = @p3) AND (`name` = @p4))";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      MySqlParameter mySqlParameter3 = new MySqlParameter();
      mySqlParameter3.ParameterName = "@p1";
      mySqlParameter3.DbType = DbType.UInt32;
      mySqlParameter3.MySqlDbType = MySqlDbType.UInt32;
      mySqlParameter3.IsNullable = true;
      mySqlParameter3.SourceColumn = "id";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter3);
      MySqlParameter mySqlParameter4 = new MySqlParameter();
      mySqlParameter4.ParameterName = "@p2";
      mySqlParameter4.DbType = DbType.String;
      mySqlParameter4.MySqlDbType = MySqlDbType.VarChar;
      mySqlParameter4.IsNullable = true;
      mySqlParameter4.SourceColumn = "name";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter4);
      MySqlParameter mySqlParameter5 = new MySqlParameter();
      mySqlParameter5.ParameterName = "@p3";
      mySqlParameter5.DbType = DbType.UInt32;
      mySqlParameter5.MySqlDbType = MySqlDbType.UInt32;
      mySqlParameter5.IsNullable = true;
      mySqlParameter5.SourceColumn = "id";
      mySqlParameter5.SourceVersion = DataRowVersion.Original;
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter5);
      MySqlParameter mySqlParameter6 = new MySqlParameter();
      mySqlParameter6.ParameterName = "@p4";
      mySqlParameter6.DbType = DbType.String;
      mySqlParameter6.MySqlDbType = MySqlDbType.VarChar;
      mySqlParameter6.IsNullable = true;
      mySqlParameter6.SourceColumn = "name";
      mySqlParameter6.SourceVersion = DataRowVersion.Original;
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter6);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitConnection()
    {
      this._connection = new MySqlConnection();
      this._connection.ConnectionString = Settings.Default.ffrktestConnectionString;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitCommandCollection()
    {
      this._commandCollection = new MySqlCommand[1];
      this._commandCollection[0] = new MySqlCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText = "SELECT        id, name\r\nFROM            characters";
      this._commandCollection[0].CommandType = CommandType.Text;
    }

    [DebuggerNonUserCode]
    [DataObjectMethod(DataObjectMethodType.Fill, true)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    public virtual int Fill(EquipUsageDataSet.charactersDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
        dataTable.Clear();
      return this.Adapter.Fill((DataTable) dataTable);
    }

    [DataObjectMethod(DataObjectMethodType.Select, true)]
    [HelpKeyword("vs.data.TableAdapter")]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public virtual EquipUsageDataSet.charactersDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      EquipUsageDataSet.charactersDataTable charactersDataTable = new EquipUsageDataSet.charactersDataTable();
      this.Adapter.Fill((DataTable) charactersDataTable);
      return charactersDataTable;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    public virtual int Update(EquipUsageDataSet.charactersDataTable dataTable) => this.Adapter.Update((DataTable) dataTable);

    [HelpKeyword("vs.data.TableAdapter")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public virtual int Update(EquipUsageDataSet dataSet) => this.Adapter.Update((DataSet) dataSet, "characters");

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    public virtual int Update(DataRow dataRow) => this.Adapter.Update(new DataRow[1]
    {
      dataRow
    });

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    [DebuggerNonUserCode]
    public virtual int Update(DataRow[] dataRows) => this.Adapter.Update(dataRows);
  }
}
