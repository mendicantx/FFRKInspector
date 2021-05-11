// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpLoadAllDungeons
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.DataCache;
using FFRKInspector.DataCache.Dungeons;
using FFRKInspector.GameData;
using MySql.Data.MySqlClient;

namespace FFRKInspector.Database
{
  internal class DbOpLoadAllDungeons : IDbRequest
  {
    private FFRKDataCacheTable<Key, FFRKInspector.DataCache.Dungeons.Data> mDungeons;

    public bool RequiresTransaction => false;

    public event DbOpLoadAllDungeons.DataReadyCallback OnRequestComplete;

    public DbOpLoadAllDungeons() => this.mDungeons = new FFRKDataCacheTable<Key, FFRKInspector.DataCache.Dungeons.Data>();

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM dungeons", connection, transaction))
      {
        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
        {
          while (mySqlDataReader.Read())
          {
            Key k = new Key();
            FFRKInspector.DataCache.Dungeons.Data v = new FFRKInspector.DataCache.Dungeons.Data();
            k.DungeonId = (uint) mySqlDataReader["id"];
            v.WorldId = (uint) mySqlDataReader["world"];
            v.Series = (uint) mySqlDataReader["series"];
            v.Name = (string) mySqlDataReader["name"];
            v.Type = (SchemaConstants.DungeonType) mySqlDataReader["type"];
            v.Difficulty = (ushort) mySqlDataReader["difficulty"];
            this.mDungeons.Update(k, v);
          }
        }
      }
    }

    public void Respond()
    {
      if (this.OnRequestComplete == null)
        return;
      this.OnRequestComplete(this.mDungeons);
    }

    public delegate void DataReadyCallback(FFRKDataCacheTable<Key, FFRKInspector.DataCache.Dungeons.Data> dungeons);
  }
}
