// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpLoadAllItems
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.DataCache;
using FFRKInspector.DataCache.Items;
using FFRKInspector.GameData;
using MySql.Data.MySqlClient;

namespace FFRKInspector.Database
{
  internal class DbOpLoadAllItems : IDbRequest
  {
    private FFRKDataCacheTable<Key, FFRKInspector.DataCache.Items.Data> mItems;

    public bool RequiresTransaction => false;

    public event DbOpLoadAllItems.DataReadyCallback OnRequestComplete;

    public DbOpLoadAllItems() => this.mItems = new FFRKDataCacheTable<Key, FFRKInspector.DataCache.Items.Data>();

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT i.id, i.name, i.rarity, i.series, i.type, i.subtype,        s.base_atk, s.base_mag, s.base_acc, s.base_def, s.base_res, s.base_eva, s.base_mnd,        s.max_atk, s.max_mag, s.max_acc, s.max_def, s.max_res, s.max_eva, s.max_mnd FROM   items i LEFT OUTER JOIN equipment_stats s ON s.equipment_id = i.id", connection, transaction))
      {
        using (MySqlDataReader Record = mySqlCommand.ExecuteReader())
        {
          while (Record.Read())
          {
            Key k = new Key();
            FFRKInspector.DataCache.Items.Data v = new FFRKInspector.DataCache.Items.Data();
            k.ItemId = (uint) Record["id"];
            v.Name = (string) Record["name"];
            v.Rarity = (byte) Record["rarity"];
            v.Series = Record.GetValueOrNull<uint>("series");
            v.Type = (byte) Record["type"];
            v.Subtype = (byte) Record["subtype"];
            v.BaseStats = new EquipStats();
            v.BaseStats.Atk = Record.GetValueOrNull<short>("base_atk");
            v.BaseStats.Mag = Record.GetValueOrNull<short>("base_mag");
            v.BaseStats.Acc = Record.GetValueOrNull<short>("base_acc");
            v.BaseStats.Def = Record.GetValueOrNull<short>("base_def");
            v.BaseStats.Res = Record.GetValueOrNull<short>("base_res");
            v.BaseStats.Eva = Record.GetValueOrNull<short>("base_eva");
            v.BaseStats.Mnd = Record.GetValueOrNull<short>("base_mnd");
            v.MaxStats = new EquipStats();
            v.MaxStats.Atk = Record.GetValueOrNull<short>("max_atk");
            v.MaxStats.Mag = Record.GetValueOrNull<short>("max_mag");
            v.MaxStats.Acc = Record.GetValueOrNull<short>("max_acc");
            v.MaxStats.Def = Record.GetValueOrNull<short>("max_def");
            v.MaxStats.Res = Record.GetValueOrNull<short>("max_res");
            v.MaxStats.Eva = Record.GetValueOrNull<short>("max_eva");
            v.MaxStats.Mnd = Record.GetValueOrNull<short>("max_mnd");
            this.mItems.Update(k, v);
          }
        }
      }
    }

    public void Respond()
    {
      if (this.OnRequestComplete == null)
        return;
      this.OnRequestComplete(this.mItems);
    }

    public delegate void DataReadyCallback(FFRKDataCacheTable<Key, FFRKInspector.DataCache.Items.Data> items);
  }
}
