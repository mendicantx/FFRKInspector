// Decompiled with JetBrains decompiler
// Type: FFRKInspector.equipmentStatsDataSetTableAdapters.equipment_statsTableAdapter
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

namespace FFRKInspector.equipmentStatsDataSetTableAdapters
{
  [HelpKeyword("vs.data.TableAdapter")]
  [DataObject(true)]
  [Designer("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  [ToolboxItem(true)]
  [DesignerCategory("code")]
  public class equipment_statsTableAdapter : Component
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

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
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
    public equipment_statsTableAdapter() => this.ClearBeforeFill = true;

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private void InitAdapter()
    {
      this._adapter = new MySqlDataAdapter();
      this._adapter.TableMappings.Add((object) new DataTableMapping()
      {
        SourceTable = "Table",
        DataSetTable = "equipment_stats",
        ColumnMappings = {
          {
            "equipment_id",
            "equipment_id"
          },
          {
            "base_atk",
            "base_atk"
          },
          {
            "base_mag",
            "base_mag"
          },
          {
            "base_acc",
            "base_acc"
          },
          {
            "base_def",
            "base_def"
          },
          {
            "base_res",
            "base_res"
          },
          {
            "base_eva",
            "base_eva"
          },
          {
            "base_mnd",
            "base_mnd"
          },
          {
            "max_atk",
            "max_atk"
          },
          {
            "max_mag",
            "max_mag"
          },
          {
            "max_acc",
            "max_acc"
          },
          {
            "max_def",
            "max_def"
          },
          {
            "max_res",
            "max_res"
          },
          {
            "max_eva",
            "max_eva"
          },
          {
            "max_mnd",
            "max_mnd"
          },
          {
            "name",
            "name"
          },
          {
            "rarity",
            "rarity"
          },
          {
            "type",
            "type"
          },
          {
            "subtype",
            "subtype"
          }
        }
      });
      this._adapter.UpdateCommand = new MySqlCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText = "UPDATE       equipment_stats s RIGHT JOIN items i ON s.equipment_id=i.id\r\nSET                s.base_atk = @p1, s.base_mag = @p2, s.base_acc = @p3, s.base_def = @p4, s.base_res = @p5, s.base_eva = @p6, s.base_mnd = @p7, s.max_atk = @p8, s.max_mag = @p9, \r\n                         s.max_acc = @p10, s.max_def = @p11, s.max_res = @p12, s.max_eva = @p13, s.max_mnd = @p14, i.name=@p15, i.rarity=@p16, i.type=@p17, i.subtype=@p18\r\nWHERE        (i.id = @p19)";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      MySqlParameter mySqlParameter1 = new MySqlParameter();
      mySqlParameter1.ParameterName = "@p1";
      mySqlParameter1.DbType = DbType.Int16;
      mySqlParameter1.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter1.IsNullable = true;
      mySqlParameter1.SourceColumn = "base_atk";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter1);
      MySqlParameter mySqlParameter2 = new MySqlParameter();
      mySqlParameter2.ParameterName = "@p2";
      mySqlParameter2.DbType = DbType.Int16;
      mySqlParameter2.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter2.IsNullable = true;
      mySqlParameter2.SourceColumn = "base_mag";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter2);
      MySqlParameter mySqlParameter3 = new MySqlParameter();
      mySqlParameter3.ParameterName = "@p3";
      mySqlParameter3.DbType = DbType.Int16;
      mySqlParameter3.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter3.IsNullable = true;
      mySqlParameter3.SourceColumn = "base_acc";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter3);
      MySqlParameter mySqlParameter4 = new MySqlParameter();
      mySqlParameter4.ParameterName = "@p4";
      mySqlParameter4.DbType = DbType.Int16;
      mySqlParameter4.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter4.IsNullable = true;
      mySqlParameter4.SourceColumn = "base_def";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter4);
      MySqlParameter mySqlParameter5 = new MySqlParameter();
      mySqlParameter5.ParameterName = "@p5";
      mySqlParameter5.DbType = DbType.Int16;
      mySqlParameter5.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter5.IsNullable = true;
      mySqlParameter5.SourceColumn = "base_res";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter5);
      MySqlParameter mySqlParameter6 = new MySqlParameter();
      mySqlParameter6.ParameterName = "@p6";
      mySqlParameter6.DbType = DbType.Int16;
      mySqlParameter6.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter6.IsNullable = true;
      mySqlParameter6.SourceColumn = "base_eva";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter6);
      MySqlParameter mySqlParameter7 = new MySqlParameter();
      mySqlParameter7.ParameterName = "@p7";
      mySqlParameter7.DbType = DbType.Int16;
      mySqlParameter7.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter7.IsNullable = true;
      mySqlParameter7.SourceColumn = "base_mnd";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter7);
      MySqlParameter mySqlParameter8 = new MySqlParameter();
      mySqlParameter8.ParameterName = "@p8";
      mySqlParameter8.DbType = DbType.Int16;
      mySqlParameter8.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter8.IsNullable = true;
      mySqlParameter8.SourceColumn = "max_atk";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter8);
      MySqlParameter mySqlParameter9 = new MySqlParameter();
      mySqlParameter9.ParameterName = "@p9";
      mySqlParameter9.DbType = DbType.Int16;
      mySqlParameter9.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter9.IsNullable = true;
      mySqlParameter9.SourceColumn = "max_mag";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter9);
      MySqlParameter mySqlParameter10 = new MySqlParameter();
      mySqlParameter10.ParameterName = "@p10";
      mySqlParameter10.DbType = DbType.Int16;
      mySqlParameter10.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter10.IsNullable = true;
      mySqlParameter10.SourceColumn = "max_acc";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter10);
      MySqlParameter mySqlParameter11 = new MySqlParameter();
      mySqlParameter11.ParameterName = "@p11";
      mySqlParameter11.DbType = DbType.Int16;
      mySqlParameter11.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter11.IsNullable = true;
      mySqlParameter11.SourceColumn = "max_def";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter11);
      MySqlParameter mySqlParameter12 = new MySqlParameter();
      mySqlParameter12.ParameterName = "@p12";
      mySqlParameter12.DbType = DbType.Int16;
      mySqlParameter12.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter12.IsNullable = true;
      mySqlParameter12.SourceColumn = "max_res";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter12);
      MySqlParameter mySqlParameter13 = new MySqlParameter();
      mySqlParameter13.ParameterName = "@p13";
      mySqlParameter13.DbType = DbType.Int16;
      mySqlParameter13.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter13.IsNullable = true;
      mySqlParameter13.SourceColumn = "max_eva";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter13);
      MySqlParameter mySqlParameter14 = new MySqlParameter();
      mySqlParameter14.ParameterName = "@p14";
      mySqlParameter14.DbType = DbType.Int16;
      mySqlParameter14.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter14.IsNullable = true;
      mySqlParameter14.SourceColumn = "max_mnd";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter14);
      MySqlParameter mySqlParameter15 = new MySqlParameter();
      mySqlParameter15.ParameterName = "@p15";
      mySqlParameter15.DbType = DbType.String;
      mySqlParameter15.MySqlDbType = MySqlDbType.VarChar;
      mySqlParameter15.IsNullable = true;
      mySqlParameter15.SourceColumn = "name";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter15);
      MySqlParameter mySqlParameter16 = new MySqlParameter();
      mySqlParameter16.ParameterName = "@p16";
      mySqlParameter16.DbType = DbType.Byte;
      mySqlParameter16.MySqlDbType = MySqlDbType.UByte;
      mySqlParameter16.IsNullable = true;
      mySqlParameter16.SourceColumn = "rarity";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter16);
      MySqlParameter mySqlParameter17 = new MySqlParameter();
      mySqlParameter17.ParameterName = "@p17";
      mySqlParameter17.DbType = DbType.Byte;
      mySqlParameter17.MySqlDbType = MySqlDbType.UByte;
      mySqlParameter17.IsNullable = true;
      mySqlParameter17.SourceColumn = "type";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter17);
      MySqlParameter mySqlParameter18 = new MySqlParameter();
      mySqlParameter18.ParameterName = "@p18";
      mySqlParameter18.DbType = DbType.Byte;
      mySqlParameter18.MySqlDbType = MySqlDbType.UByte;
      mySqlParameter18.IsNullable = true;
      mySqlParameter18.SourceColumn = "subtype";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter18);
      MySqlParameter mySqlParameter19 = new MySqlParameter();
      mySqlParameter19.ParameterName = "@p19";
      mySqlParameter19.DbType = DbType.Int32;
      mySqlParameter19.MySqlDbType = MySqlDbType.Int32;
      mySqlParameter19.IsNullable = true;
      mySqlParameter19.SourceColumn = "equipment_id";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter19);
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private void InitConnection()
    {
      this._connection = new MySqlConnection();
      this._connection.ConnectionString = Settings.Default.ffrktestConnectionString;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private void InitCommandCollection()
    {
      this._commandCollection = new MySqlCommand[1];
      this._commandCollection[0] = new MySqlCommand();
      this._commandCollection[0].Connection = this.Connection;
      this._commandCollection[0].CommandText = "SELECT        equipment_stats.equipment_id, items.name, items.rarity, items.type, items.subtype, equipment_stats.base_atk, equipment_stats.base_mag, \r\n                         equipment_stats.base_acc, equipment_stats.base_def, equipment_stats.base_res, equipment_stats.base_eva, equipment_stats.base_mnd, \r\n                         equipment_stats.max_atk, equipment_stats.max_mag, equipment_stats.max_acc, equipment_stats.max_def, equipment_stats.max_res, equipment_stats.max_eva, \r\n                         equipment_stats.max_mnd\r\nFROM            equipment_stats LEFT OUTER JOIN\r\n                         items ON equipment_stats.equipment_id = items.id";
      this._commandCollection[0].CommandType = CommandType.Text;
    }

    [DataObjectMethod(DataObjectMethodType.Fill, true)]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    public virtual int Fill(
      equipmentStatsDataSet.equipment_statsDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
        dataTable.Clear();
      return this.Adapter.Fill((DataTable) dataTable);
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DataObjectMethod(DataObjectMethodType.Select, true)]
    [HelpKeyword("vs.data.TableAdapter")]
    [DebuggerNonUserCode]
    public virtual equipmentStatsDataSet.equipment_statsDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      equipmentStatsDataSet.equipment_statsDataTable equipmentStatsDataTable = new equipmentStatsDataSet.equipment_statsDataTable();
      this.Adapter.Fill((DataTable) equipmentStatsDataTable);
      return equipmentStatsDataTable;
    }

    [HelpKeyword("vs.data.TableAdapter")]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public virtual int Update(
      equipmentStatsDataSet.equipment_statsDataTable dataTable)
    {
      return this.Adapter.Update((DataTable) dataTable);
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    [DebuggerNonUserCode]
    public virtual int Update(equipmentStatsDataSet dataSet) => this.Adapter.Update((DataSet) dataSet, "equipment_stats");

    [HelpKeyword("vs.data.TableAdapter")]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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
