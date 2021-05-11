// Decompiled with JetBrains decompiler
// Type: FFRKInspector.missingItemsDataSetTableAdapters.missing_itemsTableAdapter
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.Properties;
using MySql.Data.MySqlClient;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace FFRKInspector.missingItemsDataSetTableAdapters
{
  [DataObject(true)]
  [Designer("Microsoft.VSDesigner.DataSource.Design.TableAdapterDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  [ToolboxItem(true)]
  [HelpKeyword("vs.data.TableAdapter")]
  [DesignerCategory("code")]
  public class missing_itemsTableAdapter : Component
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

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public bool ClearBeforeFill
    {
      get => this._clearBeforeFill;
      set => this._clearBeforeFill = value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public missing_itemsTableAdapter() => this.ClearBeforeFill = true;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private void InitAdapter()
    {
      this._adapter = new MySqlDataAdapter();
      this._adapter.TableMappings.Add((object) new DataTableMapping()
      {
        SourceTable = "Table",
        DataSetTable = "missing_items",
        ColumnMappings = {
          {
            "equipment_id",
            "equipment_id"
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
            "series",
            "series"
          },
          {
            "subtype",
            "subtype"
          },
          {
            "type",
            "type"
          }
        }
      });
      this._adapter.DeleteCommand = new MySqlCommand();
      this._adapter.DeleteCommand.Connection = this.Connection;
      this._adapter.DeleteCommand.CommandText = "DELETE FROM missing_items\r\nWHERE        (equipment_id = @p1)";
      this._adapter.DeleteCommand.CommandType = CommandType.Text;
      MySqlParameter mySqlParameter1 = new MySqlParameter();
      mySqlParameter1.ParameterName = "@p1";
      mySqlParameter1.DbType = DbType.Int32;
      mySqlParameter1.MySqlDbType = MySqlDbType.Int32;
      mySqlParameter1.IsNullable = true;
      mySqlParameter1.SourceColumn = "equipment_id";
      mySqlParameter1.SourceVersion = DataRowVersion.Original;
      this._adapter.DeleteCommand.Parameters.Add(mySqlParameter1);
      this._adapter.InsertCommand = new MySqlCommand();
      this._adapter.InsertCommand.Connection = this.Connection;
      this._adapter.InsertCommand.CommandText = "INSERT INTO `missing_items` (`equipment_id`, `name`, `base_atk`, `base_mag`, `base_acc`, `base_def`, `base_res`, `base_eva`, `base_mnd`, `max_atk`, `max_mag`, `max_acc`, `max_def`, `max_res`, `max_eva`, `max_mnd`, `rarity`, `series`, `subtype`, `type`) VALUES (@p1, @p2, @p3, @p4, @p5, @p6, @p7, @p8, @p9, @p10, @p11, @p12, @p13, @p14, @p15, @p16, @p17, @p18, @p19, @p20)";
      this._adapter.InsertCommand.CommandType = CommandType.Text;
      MySqlParameter mySqlParameter2 = new MySqlParameter();
      mySqlParameter2.ParameterName = "@p1";
      mySqlParameter2.DbType = DbType.UInt32;
      mySqlParameter2.MySqlDbType = MySqlDbType.UInt32;
      mySqlParameter2.IsNullable = true;
      mySqlParameter2.SourceColumn = "equipment_id";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter2);
      MySqlParameter mySqlParameter3 = new MySqlParameter();
      mySqlParameter3.ParameterName = "@p2";
      mySqlParameter3.DbType = DbType.String;
      mySqlParameter3.MySqlDbType = MySqlDbType.VarChar;
      mySqlParameter3.IsNullable = true;
      mySqlParameter3.SourceColumn = "name";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter3);
      MySqlParameter mySqlParameter4 = new MySqlParameter();
      mySqlParameter4.ParameterName = "@p3";
      mySqlParameter4.DbType = DbType.Int16;
      mySqlParameter4.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter4.IsNullable = true;
      mySqlParameter4.SourceColumn = "base_atk";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter4);
      MySqlParameter mySqlParameter5 = new MySqlParameter();
      mySqlParameter5.ParameterName = "@p4";
      mySqlParameter5.DbType = DbType.Int16;
      mySqlParameter5.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter5.IsNullable = true;
      mySqlParameter5.SourceColumn = "base_mag";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter5);
      MySqlParameter mySqlParameter6 = new MySqlParameter();
      mySqlParameter6.ParameterName = "@p5";
      mySqlParameter6.DbType = DbType.Int16;
      mySqlParameter6.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter6.IsNullable = true;
      mySqlParameter6.SourceColumn = "base_acc";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter6);
      MySqlParameter mySqlParameter7 = new MySqlParameter();
      mySqlParameter7.ParameterName = "@p6";
      mySqlParameter7.DbType = DbType.Int16;
      mySqlParameter7.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter7.IsNullable = true;
      mySqlParameter7.SourceColumn = "base_def";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter7);
      MySqlParameter mySqlParameter8 = new MySqlParameter();
      mySqlParameter8.ParameterName = "@p7";
      mySqlParameter8.DbType = DbType.Int16;
      mySqlParameter8.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter8.IsNullable = true;
      mySqlParameter8.SourceColumn = "base_res";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter8);
      MySqlParameter mySqlParameter9 = new MySqlParameter();
      mySqlParameter9.ParameterName = "@p8";
      mySqlParameter9.DbType = DbType.Int16;
      mySqlParameter9.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter9.IsNullable = true;
      mySqlParameter9.SourceColumn = "base_eva";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter9);
      MySqlParameter mySqlParameter10 = new MySqlParameter();
      mySqlParameter10.ParameterName = "@p9";
      mySqlParameter10.DbType = DbType.Int16;
      mySqlParameter10.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter10.IsNullable = true;
      mySqlParameter10.SourceColumn = "base_mnd";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter10);
      MySqlParameter mySqlParameter11 = new MySqlParameter();
      mySqlParameter11.ParameterName = "@p10";
      mySqlParameter11.DbType = DbType.Int16;
      mySqlParameter11.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter11.IsNullable = true;
      mySqlParameter11.SourceColumn = "max_atk";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter11);
      MySqlParameter mySqlParameter12 = new MySqlParameter();
      mySqlParameter12.ParameterName = "@p11";
      mySqlParameter12.DbType = DbType.Int16;
      mySqlParameter12.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter12.IsNullable = true;
      mySqlParameter12.SourceColumn = "max_mag";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter12);
      MySqlParameter mySqlParameter13 = new MySqlParameter();
      mySqlParameter13.ParameterName = "@p12";
      mySqlParameter13.DbType = DbType.Int16;
      mySqlParameter13.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter13.IsNullable = true;
      mySqlParameter13.SourceColumn = "max_acc";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter13);
      MySqlParameter mySqlParameter14 = new MySqlParameter();
      mySqlParameter14.ParameterName = "@p13";
      mySqlParameter14.DbType = DbType.Int16;
      mySqlParameter14.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter14.IsNullable = true;
      mySqlParameter14.SourceColumn = "max_def";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter14);
      MySqlParameter mySqlParameter15 = new MySqlParameter();
      mySqlParameter15.ParameterName = "@p14";
      mySqlParameter15.DbType = DbType.Int16;
      mySqlParameter15.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter15.IsNullable = true;
      mySqlParameter15.SourceColumn = "max_res";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter15);
      MySqlParameter mySqlParameter16 = new MySqlParameter();
      mySqlParameter16.ParameterName = "@p15";
      mySqlParameter16.DbType = DbType.Int16;
      mySqlParameter16.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter16.IsNullable = true;
      mySqlParameter16.SourceColumn = "max_eva";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter16);
      MySqlParameter mySqlParameter17 = new MySqlParameter();
      mySqlParameter17.ParameterName = "@p16";
      mySqlParameter17.DbType = DbType.Int16;
      mySqlParameter17.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter17.IsNullable = true;
      mySqlParameter17.SourceColumn = "max_mnd";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter17);
      MySqlParameter mySqlParameter18 = new MySqlParameter();
      mySqlParameter18.ParameterName = "@p17";
      mySqlParameter18.DbType = DbType.Byte;
      mySqlParameter18.MySqlDbType = MySqlDbType.UByte;
      mySqlParameter18.IsNullable = true;
      mySqlParameter18.SourceColumn = "rarity";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter18);
      MySqlParameter mySqlParameter19 = new MySqlParameter();
      mySqlParameter19.ParameterName = "@p18";
      mySqlParameter19.DbType = DbType.UInt32;
      mySqlParameter19.MySqlDbType = MySqlDbType.UInt32;
      mySqlParameter19.IsNullable = true;
      mySqlParameter19.SourceColumn = "series";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter19);
      MySqlParameter mySqlParameter20 = new MySqlParameter();
      mySqlParameter20.ParameterName = "@p19";
      mySqlParameter20.DbType = DbType.Byte;
      mySqlParameter20.MySqlDbType = MySqlDbType.UByte;
      mySqlParameter20.IsNullable = true;
      mySqlParameter20.SourceColumn = "subtype";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter20);
      MySqlParameter mySqlParameter21 = new MySqlParameter();
      mySqlParameter21.ParameterName = "@p20";
      mySqlParameter21.DbType = DbType.Byte;
      mySqlParameter21.MySqlDbType = MySqlDbType.UByte;
      mySqlParameter21.IsNullable = true;
      mySqlParameter21.SourceColumn = "type";
      this._adapter.InsertCommand.Parameters.Add(mySqlParameter21);
      this._adapter.UpdateCommand = new MySqlCommand();
      this._adapter.UpdateCommand.Connection = this.Connection;
      this._adapter.UpdateCommand.CommandText = "UPDATE       missing_items\r\nSET                name = @p2, base_atk = @p3, base_mag = @p4, base_acc = @p5, base_def = @p6, base_res = @p7, base_eva = @p8, base_mnd = @p9, max_atk = @p10, \r\n                         max_mag = @p11, max_acc = @p12, max_def = @p13, max_res = @p14, max_eva = @p15, max_mnd = @p16, rarity = @p17, series = @p18, subtype = @p19, \r\n                         type = @p20\r\nWHERE        (equipment_id = @p21)";
      this._adapter.UpdateCommand.CommandType = CommandType.Text;
      MySqlParameter mySqlParameter22 = new MySqlParameter();
      mySqlParameter22.ParameterName = "@p2";
      mySqlParameter22.DbType = DbType.String;
      mySqlParameter22.MySqlDbType = MySqlDbType.VarChar;
      mySqlParameter22.Size = 45;
      mySqlParameter22.IsNullable = true;
      mySqlParameter22.SourceColumn = "name";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter22);
      MySqlParameter mySqlParameter23 = new MySqlParameter();
      mySqlParameter23.ParameterName = "@p3";
      mySqlParameter23.DbType = DbType.Int16;
      mySqlParameter23.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter23.IsNullable = true;
      mySqlParameter23.SourceColumn = "base_atk";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter23);
      MySqlParameter mySqlParameter24 = new MySqlParameter();
      mySqlParameter24.ParameterName = "@p4";
      mySqlParameter24.DbType = DbType.Int16;
      mySqlParameter24.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter24.IsNullable = true;
      mySqlParameter24.SourceColumn = "base_mag";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter24);
      MySqlParameter mySqlParameter25 = new MySqlParameter();
      mySqlParameter25.ParameterName = "@p5";
      mySqlParameter25.DbType = DbType.Int16;
      mySqlParameter25.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter25.IsNullable = true;
      mySqlParameter25.SourceColumn = "base_acc";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter25);
      MySqlParameter mySqlParameter26 = new MySqlParameter();
      mySqlParameter26.ParameterName = "@p6";
      mySqlParameter26.DbType = DbType.Int16;
      mySqlParameter26.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter26.IsNullable = true;
      mySqlParameter26.SourceColumn = "base_def";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter26);
      MySqlParameter mySqlParameter27 = new MySqlParameter();
      mySqlParameter27.ParameterName = "@p7";
      mySqlParameter27.DbType = DbType.Int16;
      mySqlParameter27.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter27.IsNullable = true;
      mySqlParameter27.SourceColumn = "base_res";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter27);
      MySqlParameter mySqlParameter28 = new MySqlParameter();
      mySqlParameter28.ParameterName = "@p8";
      mySqlParameter28.DbType = DbType.Int16;
      mySqlParameter28.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter28.IsNullable = true;
      mySqlParameter28.SourceColumn = "base_eva";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter28);
      MySqlParameter mySqlParameter29 = new MySqlParameter();
      mySqlParameter29.ParameterName = "@p9";
      mySqlParameter29.DbType = DbType.Int16;
      mySqlParameter29.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter29.IsNullable = true;
      mySqlParameter29.SourceColumn = "base_mnd";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter29);
      MySqlParameter mySqlParameter30 = new MySqlParameter();
      mySqlParameter30.ParameterName = "@p10";
      mySqlParameter30.DbType = DbType.Int16;
      mySqlParameter30.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter30.IsNullable = true;
      mySqlParameter30.SourceColumn = "max_atk";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter30);
      MySqlParameter mySqlParameter31 = new MySqlParameter();
      mySqlParameter31.ParameterName = "@p11";
      mySqlParameter31.DbType = DbType.Int16;
      mySqlParameter31.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter31.IsNullable = true;
      mySqlParameter31.SourceColumn = "max_mag";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter31);
      MySqlParameter mySqlParameter32 = new MySqlParameter();
      mySqlParameter32.ParameterName = "@p12";
      mySqlParameter32.DbType = DbType.Int16;
      mySqlParameter32.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter32.IsNullable = true;
      mySqlParameter32.SourceColumn = "max_acc";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter32);
      MySqlParameter mySqlParameter33 = new MySqlParameter();
      mySqlParameter33.ParameterName = "@p13";
      mySqlParameter33.DbType = DbType.Int16;
      mySqlParameter33.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter33.IsNullable = true;
      mySqlParameter33.SourceColumn = "max_def";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter33);
      MySqlParameter mySqlParameter34 = new MySqlParameter();
      mySqlParameter34.ParameterName = "@p14";
      mySqlParameter34.DbType = DbType.Int16;
      mySqlParameter34.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter34.IsNullable = true;
      mySqlParameter34.SourceColumn = "max_res";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter34);
      MySqlParameter mySqlParameter35 = new MySqlParameter();
      mySqlParameter35.ParameterName = "@p15";
      mySqlParameter35.DbType = DbType.Int16;
      mySqlParameter35.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter35.IsNullable = true;
      mySqlParameter35.SourceColumn = "max_eva";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter35);
      MySqlParameter mySqlParameter36 = new MySqlParameter();
      mySqlParameter36.ParameterName = "@p16";
      mySqlParameter36.DbType = DbType.Int16;
      mySqlParameter36.MySqlDbType = MySqlDbType.Int16;
      mySqlParameter36.IsNullable = true;
      mySqlParameter36.SourceColumn = "max_mnd";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter36);
      MySqlParameter mySqlParameter37 = new MySqlParameter();
      mySqlParameter37.ParameterName = "@p17";
      mySqlParameter37.DbType = DbType.Object;
      mySqlParameter37.MySqlDbType = MySqlDbType.Byte;
      mySqlParameter37.Size = 1024;
      mySqlParameter37.IsNullable = true;
      mySqlParameter37.SourceColumn = "rarity";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter37);
      MySqlParameter mySqlParameter38 = new MySqlParameter();
      mySqlParameter38.ParameterName = "@p18";
      mySqlParameter38.DbType = DbType.Int32;
      mySqlParameter38.MySqlDbType = MySqlDbType.Int32;
      mySqlParameter38.IsNullable = true;
      mySqlParameter38.SourceColumn = "series";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter38);
      MySqlParameter mySqlParameter39 = new MySqlParameter();
      mySqlParameter39.ParameterName = "@p19";
      mySqlParameter39.DbType = DbType.Object;
      mySqlParameter39.MySqlDbType = MySqlDbType.Byte;
      mySqlParameter39.Size = 1024;
      mySqlParameter39.IsNullable = true;
      mySqlParameter39.SourceColumn = "subtype";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter39);
      MySqlParameter mySqlParameter40 = new MySqlParameter();
      mySqlParameter40.ParameterName = "@p20";
      mySqlParameter40.DbType = DbType.Object;
      mySqlParameter40.MySqlDbType = MySqlDbType.Byte;
      mySqlParameter40.Size = 1024;
      mySqlParameter40.IsNullable = true;
      mySqlParameter40.SourceColumn = "type";
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter40);
      MySqlParameter mySqlParameter41 = new MySqlParameter();
      mySqlParameter41.ParameterName = "@p21";
      mySqlParameter41.DbType = DbType.Int32;
      mySqlParameter41.MySqlDbType = MySqlDbType.Int32;
      mySqlParameter41.IsNullable = true;
      mySqlParameter41.SourceColumn = "equipment_id";
      mySqlParameter41.SourceVersion = DataRowVersion.Original;
      this._adapter.UpdateCommand.Parameters.Add(mySqlParameter41);
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
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
      this._commandCollection[0].CommandText = "SELECT        equipment_id, name, base_atk, base_mag, base_acc, base_def, base_res, base_eva, base_mnd, max_atk, max_mag, max_acc, max_def, max_res, max_eva, \r\n                         max_mnd, rarity, series, subtype, type\r\nFROM            missing_items";
      this._commandCollection[0].CommandType = CommandType.Text;
    }

    [DataObjectMethod(DataObjectMethodType.Fill, true)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    [DebuggerNonUserCode]
    public virtual int Fill(
      missingItemsDataSet.missing_itemsDataTable dataTable)
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      if (this.ClearBeforeFill)
        dataTable.Clear();
      return this.Adapter.Fill((DataTable) dataTable);
    }

    [DataObjectMethod(DataObjectMethodType.Select, true)]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    public virtual missingItemsDataSet.missing_itemsDataTable GetData()
    {
      this.Adapter.SelectCommand = this.CommandCollection[0];
      missingItemsDataSet.missing_itemsDataTable missingItemsDataTable = new missingItemsDataSet.missing_itemsDataTable();
      this.Adapter.Fill((DataTable) missingItemsDataTable);
      return missingItemsDataTable;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    [DebuggerNonUserCode]
    public virtual int Update(
      missingItemsDataSet.missing_itemsDataTable dataTable)
    {
      return this.Adapter.Update((DataTable) dataTable);
    }

    [HelpKeyword("vs.data.TableAdapter")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public virtual int Update(missingItemsDataSet dataSet) => this.Adapter.Update((DataSet) dataSet, "missing_items");

    [DebuggerNonUserCode]
    [HelpKeyword("vs.data.TableAdapter")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public virtual int Update(DataRow dataRow) => this.Adapter.Update(new DataRow[1]
    {
      dataRow
    });

    [HelpKeyword("vs.data.TableAdapter")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public virtual int Update(DataRow[] dataRows) => this.Adapter.Update(dataRows);

    [DebuggerNonUserCode]
    [HelpKeyword("vs.data.TableAdapter")]
    [DataObjectMethod(DataObjectMethodType.Delete, true)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public virtual int Delete(int p1)
    {
      this.Adapter.DeleteCommand.Parameters[0].Value = (object) p1;
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
    [DataObjectMethod(DataObjectMethodType.Insert, true)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [HelpKeyword("vs.data.TableAdapter")]
    public virtual int Insert(
      uint p1,
      string p2,
      short? p3,
      short? p4,
      short? p5,
      short? p6,
      short? p7,
      short? p8,
      short? p9,
      short? p10,
      short? p11,
      short? p12,
      short? p13,
      short? p14,
      short? p15,
      short? p16,
      byte? p17,
      uint? p18,
      byte? p19,
      byte? p20)
    {
      this.Adapter.InsertCommand.Parameters[0].Value = (object) p1;
      this.Adapter.InsertCommand.Parameters[1].Value = p2 != null ? (object) p2 : throw new ArgumentNullException(nameof (p2));
      if (p3.HasValue)
        this.Adapter.InsertCommand.Parameters[2].Value = (object) p3.Value;
      else
        this.Adapter.InsertCommand.Parameters[2].Value = (object) DBNull.Value;
      if (p4.HasValue)
        this.Adapter.InsertCommand.Parameters[3].Value = (object) p4.Value;
      else
        this.Adapter.InsertCommand.Parameters[3].Value = (object) DBNull.Value;
      if (p5.HasValue)
        this.Adapter.InsertCommand.Parameters[4].Value = (object) p5.Value;
      else
        this.Adapter.InsertCommand.Parameters[4].Value = (object) DBNull.Value;
      if (p6.HasValue)
        this.Adapter.InsertCommand.Parameters[5].Value = (object) p6.Value;
      else
        this.Adapter.InsertCommand.Parameters[5].Value = (object) DBNull.Value;
      if (p7.HasValue)
        this.Adapter.InsertCommand.Parameters[6].Value = (object) p7.Value;
      else
        this.Adapter.InsertCommand.Parameters[6].Value = (object) DBNull.Value;
      if (p8.HasValue)
        this.Adapter.InsertCommand.Parameters[7].Value = (object) p8.Value;
      else
        this.Adapter.InsertCommand.Parameters[7].Value = (object) DBNull.Value;
      if (p9.HasValue)
        this.Adapter.InsertCommand.Parameters[8].Value = (object) p9.Value;
      else
        this.Adapter.InsertCommand.Parameters[8].Value = (object) DBNull.Value;
      if (p10.HasValue)
        this.Adapter.InsertCommand.Parameters[9].Value = (object) p10.Value;
      else
        this.Adapter.InsertCommand.Parameters[9].Value = (object) DBNull.Value;
      if (p11.HasValue)
        this.Adapter.InsertCommand.Parameters[10].Value = (object) p11.Value;
      else
        this.Adapter.InsertCommand.Parameters[10].Value = (object) DBNull.Value;
      if (p12.HasValue)
        this.Adapter.InsertCommand.Parameters[11].Value = (object) p12.Value;
      else
        this.Adapter.InsertCommand.Parameters[11].Value = (object) DBNull.Value;
      if (p13.HasValue)
        this.Adapter.InsertCommand.Parameters[12].Value = (object) p13.Value;
      else
        this.Adapter.InsertCommand.Parameters[12].Value = (object) DBNull.Value;
      if (p14.HasValue)
        this.Adapter.InsertCommand.Parameters[13].Value = (object) p14.Value;
      else
        this.Adapter.InsertCommand.Parameters[13].Value = (object) DBNull.Value;
      if (p15.HasValue)
        this.Adapter.InsertCommand.Parameters[14].Value = (object) p15.Value;
      else
        this.Adapter.InsertCommand.Parameters[14].Value = (object) DBNull.Value;
      if (p16.HasValue)
        this.Adapter.InsertCommand.Parameters[15].Value = (object) p16.Value;
      else
        this.Adapter.InsertCommand.Parameters[15].Value = (object) DBNull.Value;
      if (p17.HasValue)
        this.Adapter.InsertCommand.Parameters[16].Value = (object) p17.Value;
      else
        this.Adapter.InsertCommand.Parameters[16].Value = (object) DBNull.Value;
      if (p18.HasValue)
        this.Adapter.InsertCommand.Parameters[17].Value = (object) p18.Value;
      else
        this.Adapter.InsertCommand.Parameters[17].Value = (object) DBNull.Value;
      if (p19.HasValue)
        this.Adapter.InsertCommand.Parameters[18].Value = (object) p19.Value;
      else
        this.Adapter.InsertCommand.Parameters[18].Value = (object) DBNull.Value;
      if (p20.HasValue)
        this.Adapter.InsertCommand.Parameters[19].Value = (object) p20.Value;
      else
        this.Adapter.InsertCommand.Parameters[19].Value = (object) DBNull.Value;
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

    [HelpKeyword("vs.data.TableAdapter")]
    [DebuggerNonUserCode]
    [DataObjectMethod(DataObjectMethodType.Update, true)]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public virtual int Update(
      string p2,
      short? p3,
      short? p4,
      short? p5,
      short? p6,
      short? p7,
      short? p8,
      short? p9,
      short? p10,
      short? p11,
      short? p12,
      short? p13,
      short? p14,
      short? p15,
      short? p16,
      object p17,
      int p18,
      object p19,
      object p20,
      int p21)
    {
      this.Adapter.UpdateCommand.Parameters[0].Value = p2 != null ? (object) p2 : throw new ArgumentNullException(nameof (p2));
      if (p3.HasValue)
        this.Adapter.UpdateCommand.Parameters[1].Value = (object) p3.Value;
      else
        this.Adapter.UpdateCommand.Parameters[1].Value = (object) DBNull.Value;
      if (p4.HasValue)
        this.Adapter.UpdateCommand.Parameters[2].Value = (object) p4.Value;
      else
        this.Adapter.UpdateCommand.Parameters[2].Value = (object) DBNull.Value;
      if (p5.HasValue)
        this.Adapter.UpdateCommand.Parameters[3].Value = (object) p5.Value;
      else
        this.Adapter.UpdateCommand.Parameters[3].Value = (object) DBNull.Value;
      if (p6.HasValue)
        this.Adapter.UpdateCommand.Parameters[4].Value = (object) p6.Value;
      else
        this.Adapter.UpdateCommand.Parameters[4].Value = (object) DBNull.Value;
      if (p7.HasValue)
        this.Adapter.UpdateCommand.Parameters[5].Value = (object) p7.Value;
      else
        this.Adapter.UpdateCommand.Parameters[5].Value = (object) DBNull.Value;
      if (p8.HasValue)
        this.Adapter.UpdateCommand.Parameters[6].Value = (object) p8.Value;
      else
        this.Adapter.UpdateCommand.Parameters[6].Value = (object) DBNull.Value;
      if (p9.HasValue)
        this.Adapter.UpdateCommand.Parameters[7].Value = (object) p9.Value;
      else
        this.Adapter.UpdateCommand.Parameters[7].Value = (object) DBNull.Value;
      if (p10.HasValue)
        this.Adapter.UpdateCommand.Parameters[8].Value = (object) p10.Value;
      else
        this.Adapter.UpdateCommand.Parameters[8].Value = (object) DBNull.Value;
      if (p11.HasValue)
        this.Adapter.UpdateCommand.Parameters[9].Value = (object) p11.Value;
      else
        this.Adapter.UpdateCommand.Parameters[9].Value = (object) DBNull.Value;
      if (p12.HasValue)
        this.Adapter.UpdateCommand.Parameters[10].Value = (object) p12.Value;
      else
        this.Adapter.UpdateCommand.Parameters[10].Value = (object) DBNull.Value;
      if (p13.HasValue)
        this.Adapter.UpdateCommand.Parameters[11].Value = (object) p13.Value;
      else
        this.Adapter.UpdateCommand.Parameters[11].Value = (object) DBNull.Value;
      if (p14.HasValue)
        this.Adapter.UpdateCommand.Parameters[12].Value = (object) p14.Value;
      else
        this.Adapter.UpdateCommand.Parameters[12].Value = (object) DBNull.Value;
      if (p15.HasValue)
        this.Adapter.UpdateCommand.Parameters[13].Value = (object) p15.Value;
      else
        this.Adapter.UpdateCommand.Parameters[13].Value = (object) DBNull.Value;
      if (p16.HasValue)
        this.Adapter.UpdateCommand.Parameters[14].Value = (object) p16.Value;
      else
        this.Adapter.UpdateCommand.Parameters[14].Value = (object) DBNull.Value;
      this.Adapter.UpdateCommand.Parameters[15].Value = p17 != null ? p17 : throw new ArgumentNullException(nameof (p17));
      this.Adapter.UpdateCommand.Parameters[16].Value = (object) p18;
      this.Adapter.UpdateCommand.Parameters[17].Value = p19 != null ? p19 : throw new ArgumentNullException(nameof (p19));
      this.Adapter.UpdateCommand.Parameters[18].Value = p20 != null ? p20 : throw new ArgumentNullException(nameof (p20));
      this.Adapter.UpdateCommand.Parameters[19].Value = (object) p21;
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
  }
}
