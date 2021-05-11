// Decompiled with JetBrains decompiler
// Type: FFRKInspector.EquipUsageDataSetTableAdapters.equip_usageTableAdapter
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
  [ToolboxItem(true)]
  [DesignerCategory("code")]
  [DataObject(true)]
  [Designer("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  [HelpKeyword("vs.data.TableAdapter")]
  public class equip_usageTableAdapter : Component
  {
    private MySqlDataAdapter _adapter;
    private MySqlConnection _connection;
    private MySqlTransaction _transaction;
    private MySqlCommand[] _commandCollection;
    private bool _clearBeforeFill;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected internal MySqlDataAdapter Adapter
    {
      get
      {
        if (this._adapter == null)
          this.InitAdapter();
        return this._adapter;
      }
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
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

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public equip_usageTableAdapter() => this.ClearBeforeFill = true;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private void InitAdapter()
    {
      this._adapter = new MySqlDataAdapter();
      this._adapter.TableMappings.Add((object) new DataTableMapping()
      {
        SourceTable = "Table",
        DataSetTable = "equip_usage",
        ColumnMappings = {
          {
            "character_id",
            "character_id"
          },
          {
            "equip_category",
            "equip_category"
          }
        }
      });
      this._adapter.DeleteCommand = new MySqlCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText = "DELETE FROM `equip_usage` WHERE ((`character_id` = @p1) AND (`equip_category` = @p2))";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      MySqlParameter mySqlParameter1 = new MySqlParameter();
      mySqlParameter1.ParameterName = "@p1";
      mySqlParameter1.DbType = DbType.UInt32;
      mySqlParameter1.MySqlDbType = MySqlDbType.UInt32;
      mySqlParameter1.IsNullable = true;
      mySqlParameter1.SourceColumn = "character_id";
      mySqlParameter1.SourceVersion = DataRowVersion.Original;
      this._adapter.DeleteCommand.Parameters.Add(mySqlParameter1);
      MySqlParameter mySqlParameter2 = new MySqlParameter();
      mySqlParameter2.ParameterName = "@p2";
      mySqlParameter2.DbType = DbType.Byte;
      mySqlParameter2.MySqlDbType = MySqlDbType.UByte;
      mySqlParameter2.IsNullable = true;
      mySqlParameter2.SourceColumn = "equip_category";
      mySqlParameter2.SourceVersion = DataRowVersion.Original;
      this._adapter.DeleteCommand.Parameters.Add(mySqlParameter2);
      this._adapter.InsertCommand = new MySqlCommand();
      this._adapter.InsertCommand.Connection = this.Connection;
      this._adapter.InsertCommand.CommandText = "INSERT INTO `equip_usage` (`character_id`, `equip_category`) VALUES (@p1, @p2)";
      this._adapter.InsertCommand.CommandType = CommandType.Text;
      MySqlParameter mySqlParameter3 = new MySqlParameter();
      mySqlParameter3.ParameterName = "@p1";
      mySqlParameter3.DbType = DbType.UInt32;
      mySqlParameter3.MySqlDbType = MySqlDbType.UInt32;
      mySqlParameter3.IsNullable = true;
      mySqlParameter3.SourceColumn = "character_id";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter3);
      MySqlParameter mySqlParameter4 = new MySqlParameter();
      mySqlParameter4.ParameterName = "@p2";
      mySqlParameter4.DbType = DbType.Byte;
      mySqlParameter4.MySqlDbType = MySqlDbType.UByte;
      mySqlParameter4.IsNullable = true;
      mySqlParameter4.SourceColumn = "equip_category";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter4);
      this._adapter.UpdateCommand = new MySqlCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText = "UPDATE `equip_usage` SET `character_id` = @p1, `equip_category` = @p2 WHERE ((`character_id` = @p3) AND (`equip_category` = @p4))";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      MySqlParameter mySqlParameter5 = new MySqlParameter();
      mySqlParameter5.ParameterName = "@p1";
      mySqlParameter5.DbType = DbType.UInt32;
      mySqlParameter5.MySqlDbType = MySqlDbType.UInt32;
      mySqlParameter5.IsNullable = true;
      mySqlParameter5.SourceColumn = "character_id";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter5);
      MySqlParameter mySqlParameter6 = new MySqlParameter();
      mySqlParameter6.ParameterName = "@p2";
      mySqlParameter6.DbType = DbType.Byte;
      mySqlParameter6.MySqlDbType = MySqlDbType.UByte;
      mySqlParameter6.IsNullable = true;
      mySqlParameter6.SourceColumn = "equip_category";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter6);
      MySqlParameter mySqlParameter7 = new MySqlParameter();
      mySqlParameter7.ParameterName = "@p3";
      mySqlParameter7.DbType = DbType.UInt32;
      mySqlParameter7.MySqlDbType = MySqlDbType.UInt32;
      mySqlParameter7.IsNullable = true;
      mySqlParameter7.SourceColumn = "character_id";
      mySqlParameter7.SourceVersion = DataRowVersion.Original;
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter7);
      MySqlParameter mySqlParameter8 = new MySqlParameter();
      mySqlParameter8.ParameterName = "@p4";
      mySqlParameter8.DbType = DbType.Byte;
      mySqlParameter8.MySqlDbType = MySqlDbType.UByte;
      mySqlParameter8.IsNullable = true;
      mySqlParameter8.SourceColumn = "equip_category";
      mySqlParameter8.SourceVersion = DataRowVersion.Original;
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter8);
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
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
      this._commandCollection[0].CommandText = "SELECT `character_id`, `equip_category` FROM `equip_usage`";
      this._commandCollection[0].CommandType = CommandType.Text;
    }

    [DebuggerNonUserCode]
    [DataObjectMethod(DataObjectMethodType.Fill, true)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    public virtual int Fill(EquipUsageDataSet.equip_usageDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
        dataTable.Clear();
      return this.Adapter.Fill((DataTable) dataTable);
    }

    [HelpKeyword("vs.data.TableAdapter")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    [DataObjectMethod(DataObjectMethodType.Select, true)]
    public virtual EquipUsageDataSet.equip_usageDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      EquipUsageDataSet.equip_usageDataTable equipUsageDataTable = new EquipUsageDataSet.equip_usageDataTable();
      this.Adapter.Fill((DataTable) equipUsageDataTable);
      return equipUsageDataTable;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    [HelpKeyword("vs.data.TableAdapter")]
    public virtual int Update(EquipUsageDataSet.equip_usageDataTable dataTable) => this.Adapter.Update((DataTable) dataTable);

    [HelpKeyword("vs.data.TableAdapter")]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public virtual int Update(EquipUsageDataSet dataSet) => this.Adapter.Update((DataSet) dataSet, "equip_usage");

    [HelpKeyword("vs.data.TableAdapter")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public virtual int Update(DataRow dataRow) => this.Adapter.Update(new DataRow[1]
    {
      dataRow
    });

    [HelpKeyword("vs.data.TableAdapter")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public virtual int Update(DataRow[] dataRows) => this.Adapter.Update(dataRows);

    [DebuggerNonUserCode]
    [DataObjectMethod(DataObjectMethodType.Delete, true)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    public virtual int Delete(uint p1, byte p2)
    {
      this.Adapter.DeleteCommand.Parameters[0].Value = (object) p1;
      this.Adapter.DeleteCommand.Parameters[1].Value = (object) p2;
      ConnectionState state = this.Adapter.DeleteCommand.Connection.State;
      if ((this.Adapter.DeleteCommand.Connection.State & ConnectionState.Open) != ConnectionState.Open)
        this.Adapter.DeleteCommand.Connection.Open();
      try
      {
        return this.Adapter.DeleteCommand.ExecuteNonQuery();
      }
      finally
      {
        if (state == ConnectionState.Closed)
          this.Adapter.DeleteCommand.Connection.Close();
      }
    }

    [DebuggerNonUserCode]
    [HelpKeyword("vs.data.TableAdapter")]
    [DataObjectMethod(DataObjectMethodType.Insert, true)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public virtual int Insert(uint p1, byte p2)
    {
      this.Adapter.InsertCommand.Parameters[0].Value = (object) p1;
      this.Adapter.InsertCommand.Parameters[1].Value = (object) p2;
      ConnectionState state = this.Adapter.InsertCommand.Connection.State;
      if ((this.Adapter.InsertCommand.Connection.State & ConnectionState.Open) != ConnectionState.Open)
        this.Adapter.InsertCommand.Connection.Open();
      try
      {
        return this.Adapter.InsertCommand.ExecuteNonQuery();
      }
      finally
      {
        if (state == ConnectionState.Closed)
          this.Adapter.InsertCommand.Connection.Close();
      }
    }

    [DataObjectMethod(DataObjectMethodType.Update, true)]
    [DebuggerNonUserCode]
    [HelpKeyword("vs.data.TableAdapter")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public virtual int Update(uint p1, byte p2, uint p3, byte p4)
    {
      this.Adapter.UpdateCommand.Parameters[0].Value = (object) p1;
      this.Adapter.UpdateCommand.Parameters[1].Value = (object) p2;
      this.Adapter.UpdateCommand.Parameters[2].Value = (object) p3;
      this.Adapter.UpdateCommand.Parameters[3].Value = (object) p4;
      ConnectionState state = this.Adapter.UpdateCommand.Connection.State;
      if ((this.Adapter.UpdateCommand.Connection.State & ConnectionState.Open) != ConnectionState.Open)
        this.Adapter.UpdateCommand.Connection.Open();
      try
      {
        return this.Adapter.UpdateCommand.ExecuteNonQuery();
      }
      finally
      {
        if (state == ConnectionState.Closed)
          this.Adapter.UpdateCommand.Connection.Close();
      }
    }

    [DataObjectMethod(DataObjectMethodType.Update, true)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    [HelpKeyword("vs.data.TableAdapter")]
    public virtual int Update(uint p3, byte p4) => this.Update(p3, p4, p3, p4);
  }
}
