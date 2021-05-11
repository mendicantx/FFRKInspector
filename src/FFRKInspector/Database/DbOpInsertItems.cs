// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpInsertItems
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.DataCache.Items;
using FFRKInspector.GameData;
using FFRKInspector.Proxy;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace FFRKInspector.Database
{
  internal class DbOpInsertItems : IDbRequest
  {
    private List<DbOpInsertItems.ItemRecord> mItemRecords;

    public bool RequiresTransaction => true;

    public IList<DbOpInsertItems.ItemRecord> Items => (IList<DbOpInsertItems.ItemRecord>) this.mItemRecords;

    public DbOpInsertItems() => this.mItemRecords = new List<DbOpInsertItems.ItemRecord>();

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      foreach (DbOpInsertItems.ItemRecord mItemRecord in this.mItemRecords)
      {
        Key key = new Key() { ItemId = mItemRecord.Id };
        FFRKInspector.DataCache.Items.Data v = (FFRKInspector.DataCache.Items.Data) null;
        if (!FFRKProxy.Instance.Cache.Items.TryGetValue(key, out v) || !v.Name.Equals(mItemRecord.Name) || (int) v.Subtype != (int) mItemRecord.Subtype)
        {
          this.CallProcInsertItemEntry(connection, transaction, mItemRecord);
          v = new FFRKInspector.DataCache.Items.Data()
          {
            Type = (byte) mItemRecord.Type,
            Name = mItemRecord.Name,
            Rarity = (byte) mItemRecord.Rarity,
            Series = new uint?(mItemRecord.Series.Value),
            Subtype = mItemRecord.Subtype
          };
          FFRKProxy.Instance.Cache.Items.Update(key, v);
        }
      }
    }

    public void Respond()
    {
    }

    private void CallProcInsertItemEntry(
      MySqlConnection Connection,
      MySqlTransaction Transaction,
      DbOpInsertItems.ItemRecord Item)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("insert_item_entry", Connection, Transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@iid", (object) Item.Id);
        mySqlCommand.Parameters.AddWithValue("@iname", (object) Item.Name);
        mySqlCommand.Parameters.AddWithValue("@irarity", (object) Item.Rarity);
        mySqlCommand.Parameters.AddWithValue("@iseries", (object) Item.Series);
        mySqlCommand.Parameters.AddWithValue("@itype", (object) Item.Type);
        mySqlCommand.Parameters.AddWithValue("@isubtype", (object) Item.Subtype);
        mySqlCommand.ExecuteNonQuery();
      }
    }

    public class ItemRecord
    {
      public uint Id;
      public string Name;
      public SchemaConstants.Rarity Rarity;
      public uint? Series;
      public SchemaConstants.ItemType Type;
      public SchemaConstants.EquipmentCategory? EquipCategory;
      public SchemaConstants.OrbType? OrbType;

      public byte Subtype
      {
        get
        {
          switch (this.Type)
          {
            case SchemaConstants.ItemType.Weapon:
            case SchemaConstants.ItemType.Armor:
            case SchemaConstants.ItemType.Accessory:
              return (byte) this.EquipCategory.Value;
            case SchemaConstants.ItemType.Orb:
              return (byte) this.OrbType.Value;
            default:
              return 0;
          }
        }
      }
    }
  }
}
