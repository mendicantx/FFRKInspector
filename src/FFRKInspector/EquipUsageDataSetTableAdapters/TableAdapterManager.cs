// Decompiled with JetBrains decompiler
// Type: FFRKInspector.EquipUsageDataSetTableAdapters.TableAdapterManager
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using MySql.Data.MySqlClient;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Data.Common;
using System.Diagnostics;

namespace FFRKInspector.EquipUsageDataSetTableAdapters
{
  [DesignerCategory("code")]
  [ToolboxItem(true)]
  [Designer("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerDesigner, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
  [HelpKeyword("vs.data.TableAdapterManager")]
  public class TableAdapterManager : Component
  {
    private TableAdapterManager.UpdateOrderOption _updateOrder;
    private equip_usageTableAdapter _equip_usageTableAdapter;
    private charactersTableAdapter _charactersTableAdapter;
    private bool _backupDataSetBeforeUpdate;
    private IDbConnection _connection;

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public TableAdapterManager.UpdateOrderOption UpdateOrder
    {
      get => this._updateOrder;
      set => this._updateOrder = value;
    }

    [Editor("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor")]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public equip_usageTableAdapter equip_usageTableAdapter
    {
      get => this._equip_usageTableAdapter;
      set => this._equip_usageTableAdapter = value;
    }

    [Editor("Microsoft.VSDesigner.DataSource.Design.TableAdapterManagerPropertyEditor, Microsoft.VSDesigner, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", "System.Drawing.Design.UITypeEditor")]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public charactersTableAdapter charactersTableAdapter
    {
      get => this._charactersTableAdapter;
      set => this._charactersTableAdapter = value;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public bool BackupDataSetBeforeUpdate
    {
      get => this._backupDataSetBeforeUpdate;
      set => this._backupDataSetBeforeUpdate = value;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [Browsable(false)]
    [DebuggerNonUserCode]
    public IDbConnection Connection
    {
      get
      {
        if (this._connection != null)
          return this._connection;
        if (this._equip_usageTableAdapter != null && this._equip_usageTableAdapter.Connection != null)
          return (IDbConnection) this._equip_usageTableAdapter.Connection;
        return this._charactersTableAdapter != null && this._charactersTableAdapter.Connection != null ? (IDbConnection) this._charactersTableAdapter.Connection : (IDbConnection) null;
      }
      set => this._connection = value;
    }

    [Browsable(false)]
    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public int TableAdapterInstanceCount
    {
      get
      {
        int num = 0;
        if (this._equip_usageTableAdapter != null)
          ++num;
        if (this._charactersTableAdapter != null)
          ++num;
        return num;
      }
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private int UpdateUpdatedRows(
      EquipUsageDataSet dataSet,
      List<DataRow> allChangedRows,
      List<DataRow> allAddedRows)
    {
      int num = 0;
      if (this._charactersTableAdapter != null)
      {
        DataRow[] realUpdatedRows = this.GetRealUpdatedRows(dataSet.characters.Select((string) null, (string) null, DataViewRowState.ModifiedCurrent), allAddedRows);
        if (realUpdatedRows != null && (uint) realUpdatedRows.Length > 0U)
        {
          num += this._charactersTableAdapter.Update(realUpdatedRows);
          allChangedRows.AddRange((IEnumerable<DataRow>) realUpdatedRows);
        }
      }
      if (this._equip_usageTableAdapter != null)
      {
        DataRow[] realUpdatedRows = this.GetRealUpdatedRows(dataSet.equip_usage.Select((string) null, (string) null, DataViewRowState.ModifiedCurrent), allAddedRows);
        if (realUpdatedRows != null && (uint) realUpdatedRows.Length > 0U)
        {
          num += this._equip_usageTableAdapter.Update(realUpdatedRows);
          allChangedRows.AddRange((IEnumerable<DataRow>) realUpdatedRows);
        }
      }
      return num;
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    private int UpdateInsertedRows(EquipUsageDataSet dataSet, List<DataRow> allAddedRows)
    {
      int num = 0;
      if (this._charactersTableAdapter != null)
      {
        DataRow[] dataRows = dataSet.characters.Select((string) null, (string) null, DataViewRowState.Added);
        if (dataRows != null && (uint) dataRows.Length > 0U)
        {
          num += this._charactersTableAdapter.Update(dataRows);
          allAddedRows.AddRange((IEnumerable<DataRow>) dataRows);
        }
      }
      if (this._equip_usageTableAdapter != null)
      {
        DataRow[] dataRows = dataSet.equip_usage.Select((string) null, (string) null, DataViewRowState.Added);
        if (dataRows != null && (uint) dataRows.Length > 0U)
        {
          num += this._equip_usageTableAdapter.Update(dataRows);
          allAddedRows.AddRange((IEnumerable<DataRow>) dataRows);
        }
      }
      return num;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private int UpdateDeletedRows(EquipUsageDataSet dataSet, List<DataRow> allChangedRows)
    {
      int num = 0;
      if (this._equip_usageTableAdapter != null)
      {
        DataRow[] dataRows = dataSet.equip_usage.Select((string) null, (string) null, DataViewRowState.Deleted);
        if (dataRows != null && (uint) dataRows.Length > 0U)
        {
          num += this._equip_usageTableAdapter.Update(dataRows);
          allChangedRows.AddRange((IEnumerable<DataRow>) dataRows);
        }
      }
      if (this._charactersTableAdapter != null)
      {
        DataRow[] dataRows = dataSet.characters.Select((string) null, (string) null, DataViewRowState.Deleted);
        if (dataRows != null && (uint) dataRows.Length > 0U)
        {
          num += this._charactersTableAdapter.Update(dataRows);
          allChangedRows.AddRange((IEnumerable<DataRow>) dataRows);
        }
      }
      return num;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private DataRow[] GetRealUpdatedRows(DataRow[] updatedRows, List<DataRow> allAddedRows)
    {
      if (updatedRows == null || updatedRows.Length < 1 || (allAddedRows == null || allAddedRows.Count < 1))
        return updatedRows;
      List<DataRow> dataRowList = new List<DataRow>();
      for (int index = 0; index < updatedRows.Length; ++index)
      {
        DataRow updatedRow = updatedRows[index];
        if (!allAddedRows.Contains(updatedRow))
          dataRowList.Add(updatedRow);
      }
      return dataRowList.ToArray();
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    public virtual int UpdateAll(EquipUsageDataSet dataSet)
    {
      if (dataSet == null)
        throw new ArgumentNullException(nameof (dataSet));
      if (!dataSet.HasChanges())
        return 0;
      if (this._equip_usageTableAdapter != null && !this.MatchTableAdapterConnection((IDbConnection) this._equip_usageTableAdapter.Connection))
        throw new ArgumentException("All TableAdapters managed by a TableAdapterManager must use the same connection string.");
      if (this._charactersTableAdapter != null && !this.MatchTableAdapterConnection((IDbConnection) this._charactersTableAdapter.Connection))
        throw new ArgumentException("All TableAdapters managed by a TableAdapterManager must use the same connection string.");
      IDbConnection connection = this.Connection;
      if (connection == null)
        throw new ApplicationException("TableAdapterManager contains no connection information. Set each TableAdapterManager TableAdapter property to a valid TableAdapter instance.");
      bool flag = false;
      if ((connection.State & ConnectionState.Broken) == ConnectionState.Broken)
        connection.Close();
      if (connection.State == ConnectionState.Closed)
      {
        connection.Open();
        flag = true;
      }
      IDbTransaction dbTransaction = connection.BeginTransaction();
      if (dbTransaction == null)
        throw new ApplicationException("The transaction cannot begin. The current data connection does not support transactions or the current state is not allowing the transaction to begin.");
      List<DataRow> allChangedRows = new List<DataRow>();
      List<DataRow> allAddedRows = new List<DataRow>();
      List<DataAdapter> dataAdapterList = new List<DataAdapter>();
      Dictionary<object, IDbConnection> dictionary = new Dictionary<object, IDbConnection>();
      int num = 0;
      DataSet dataSet1 = (DataSet) null;
      if (this.BackupDataSetBeforeUpdate)
      {
        dataSet1 = new DataSet();
        dataSet1.Merge((DataSet) dataSet);
      }
      try
      {
        if (this._equip_usageTableAdapter != null)
        {
          dictionary.Add((object) this._equip_usageTableAdapter, (IDbConnection) this._equip_usageTableAdapter.Connection);
          this._equip_usageTableAdapter.Connection = (MySqlConnection) connection;
          this._equip_usageTableAdapter.Transaction = (MySqlTransaction) dbTransaction;
          if (this._equip_usageTableAdapter.Adapter.AcceptChangesDuringUpdate)
          {
            this._equip_usageTableAdapter.Adapter.AcceptChangesDuringUpdate = false;
            dataAdapterList.Add((DataAdapter) this._equip_usageTableAdapter.Adapter);
          }
        }
        if (this._charactersTableAdapter != null)
        {
          dictionary.Add((object) this._charactersTableAdapter, (IDbConnection) this._charactersTableAdapter.Connection);
          this._charactersTableAdapter.Connection = (MySqlConnection) connection;
          this._charactersTableAdapter.Transaction = (MySqlTransaction) dbTransaction;
          if (this._charactersTableAdapter.Adapter.AcceptChangesDuringUpdate)
          {
            this._charactersTableAdapter.Adapter.AcceptChangesDuringUpdate = false;
            dataAdapterList.Add((DataAdapter) this._charactersTableAdapter.Adapter);
          }
        }
        if (this.UpdateOrder == TableAdapterManager.UpdateOrderOption.UpdateInsertDelete)
        {
          num += this.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows);
          num += this.UpdateInsertedRows(dataSet, allAddedRows);
        }
        else
        {
          num += this.UpdateInsertedRows(dataSet, allAddedRows);
          num += this.UpdateUpdatedRows(dataSet, allChangedRows, allAddedRows);
        }
        num += this.UpdateDeletedRows(dataSet, allChangedRows);
        dbTransaction.Commit();
        if (0 < allAddedRows.Count)
        {
          DataRow[] array = new DataRow[allAddedRows.Count];
          allAddedRows.CopyTo(array);
          for (int index = 0; index < array.Length; ++index)
            array[index].AcceptChanges();
        }
        if (0 < allChangedRows.Count)
        {
          DataRow[] array = new DataRow[allChangedRows.Count];
          allChangedRows.CopyTo(array);
          for (int index = 0; index < array.Length; ++index)
            array[index].AcceptChanges();
        }
      }
      catch (Exception ex)
      {
        dbTransaction.Rollback();
        if (this.BackupDataSetBeforeUpdate)
        {
          Debug.Assert(dataSet1 != null);
          dataSet.Clear();
          dataSet.Merge(dataSet1);
        }
        else if (0 < allAddedRows.Count)
        {
          DataRow[] array = new DataRow[allAddedRows.Count];
          allAddedRows.CopyTo(array);
          for (int index = 0; index < array.Length; ++index)
          {
            DataRow dataRow = array[index];
            dataRow.AcceptChanges();
            dataRow.SetAdded();
          }
        }
        throw ex;
      }
      finally
      {
        if (flag)
          connection.Close();
        if (this._equip_usageTableAdapter != null)
        {
          this._equip_usageTableAdapter.Connection = (MySqlConnection) dictionary[(object) this._equip_usageTableAdapter];
          this._equip_usageTableAdapter.Transaction = (MySqlTransaction) null;
        }
        if (this._charactersTableAdapter != null)
        {
          this._charactersTableAdapter.Connection = (MySqlConnection) dictionary[(object) this._charactersTableAdapter];
          this._charactersTableAdapter.Transaction = (MySqlTransaction) null;
        }
        if (0 < dataAdapterList.Count)
        {
          DataAdapter[] array = new DataAdapter[dataAdapterList.Count];
          dataAdapterList.CopyTo(array);
          for (int index = 0; index < array.Length; ++index)
            array[index].AcceptChangesDuringUpdate = true;
        }
      }
      return num;
    }

    [DebuggerNonUserCode]
    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    protected virtual void SortSelfReferenceRows(
      DataRow[] rows,
      DataRelation relation,
      bool childFirst)
    {
      Array.Sort<DataRow>(rows, (IComparer<DataRow>) new TableAdapterManager.SelfReferenceComparer(relation, childFirst));
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    [DebuggerNonUserCode]
    protected virtual bool MatchTableAdapterConnection(IDbConnection inputConnection) => this._connection != null || (this.Connection == null || inputConnection == null || string.Equals(this.Connection.ConnectionString, inputConnection.ConnectionString, StringComparison.Ordinal));

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    public enum UpdateOrderOption
    {
      InsertUpdateDelete,
      UpdateInsertDelete,
    }

    [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
    private class SelfReferenceComparer : IComparer<DataRow>
    {
      private DataRelation _relation;
      private int _childFirst;

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      internal SelfReferenceComparer(DataRelation relation, bool childFirst)
      {
        this._relation = relation;
        if (childFirst)
          this._childFirst = -1;
        else
          this._childFirst = 1;
      }

      [DebuggerNonUserCode]
      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      private DataRow GetRoot(DataRow row, out int distance)
      {
        Debug.Assert(row != null);
        DataRow dataRow = row;
        distance = 0;
        IDictionary<DataRow, DataRow> dictionary = (IDictionary<DataRow, DataRow>) new Dictionary<DataRow, DataRow>();
        dictionary[row] = row;
        for (DataRow parentRow = row.GetParentRow(this._relation, DataRowVersion.Default); parentRow != null && !dictionary.ContainsKey(parentRow); parentRow = parentRow.GetParentRow(this._relation, DataRowVersion.Default))
        {
          ++distance;
          dataRow = parentRow;
          dictionary[parentRow] = parentRow;
        }
        if (distance == 0)
        {
          dictionary.Clear();
          dictionary[row] = row;
          for (DataRow parentRow = row.GetParentRow(this._relation, DataRowVersion.Original); parentRow != null && !dictionary.ContainsKey(parentRow); parentRow = parentRow.GetParentRow(this._relation, DataRowVersion.Original))
          {
            ++distance;
            dataRow = parentRow;
            dictionary[parentRow] = parentRow;
          }
        }
        return dataRow;
      }

      [GeneratedCode("System.Data.Design.TypedDataSetGenerator", "4.0.0.0")]
      [DebuggerNonUserCode]
      public int Compare(DataRow row1, DataRow row2)
      {
        if (row1 == row2)
          return 0;
        if (row1 == null)
          return -1;
        if (row2 == null)
          return 1;
        int distance1 = 0;
        DataRow root1 = this.GetRoot(row1, out distance1);
        int distance2 = 0;
        DataRow root2 = this.GetRoot(row2, out distance2);
        if (root1 == root2)
          return this._childFirst * distance1.CompareTo(distance2);
        Debug.Assert(root1.Table != null && root2.Table != null);
        return root1.Table.Rows.IndexOf(root1) < root2.Table.Rows.IndexOf(root2) ? -1 : 1;
      }
    }
  }
}
