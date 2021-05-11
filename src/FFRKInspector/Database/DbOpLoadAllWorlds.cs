// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpLoadAllWorlds
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.DataCache;
using FFRKInspector.DataCache.Worlds;
using FFRKInspector.GameData;
using MySql.Data.MySqlClient;

namespace FFRKInspector.Database
{
  internal class DbOpLoadAllWorlds : IDbRequest
  {
    private FFRKDataCacheTable<Key, FFRKInspector.DataCache.Worlds.Data> mWorlds;

    public bool RequiresTransaction => false;

    public event DbOpLoadAllWorlds.DataReadyCallback OnRequestComplete;

    public DbOpLoadAllWorlds() => this.mWorlds = new FFRKDataCacheTable<Key, FFRKInspector.DataCache.Worlds.Data>();

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM worlds", connection, transaction))
      {
        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
        {
          while (mySqlDataReader.Read())
          {
            Key k = new Key();
            FFRKInspector.DataCache.Worlds.Data v = new FFRKInspector.DataCache.Worlds.Data();
            k.WorldId = (uint) mySqlDataReader["id"];
            v.Name = (string) mySqlDataReader["name"];
            v.Series = (uint) mySqlDataReader["series"];
            v.Type = (SchemaConstants.WorldType) mySqlDataReader["type"];
            this.mWorlds.Update(k, v);
          }
        }
      }
    }

    public void Respond()
    {
      if (this.OnRequestComplete == null)
        return;
      this.OnRequestComplete(this.mWorlds);
    }

    public delegate void DataReadyCallback(FFRKDataCacheTable<Key, FFRKInspector.DataCache.Worlds.Data> battles);
  }
}
