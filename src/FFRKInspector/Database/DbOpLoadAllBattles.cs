// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpLoadAllBattles
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.DataCache;
using FFRKInspector.DataCache.Battles;
using MySql.Data.MySqlClient;

namespace FFRKInspector.Database
{
  internal class DbOpLoadAllBattles : IDbRequest
  {
    private FFRKDataCacheTable<Key, FFRKInspector.DataCache.Battles.Data> mBattles;

    public bool RequiresTransaction => false;

    public event DbOpLoadAllBattles.DataReadyCallback OnRequestComplete;

    public DbOpLoadAllBattles() => this.mBattles = new FFRKDataCacheTable<Key, FFRKInspector.DataCache.Battles.Data>();

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM battles ORDER BY dungeon ASC, id ASC", connection, transaction))
      {
        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
        {
          uint num1 = 0;
          ushort num2 = 0;
          FFRKInspector.DataCache.Battles.Data data = (FFRKInspector.DataCache.Battles.Data) null;
          while (mySqlDataReader.Read())
          {
            Key k = new Key();
            FFRKInspector.DataCache.Battles.Data v = new FFRKInspector.DataCache.Battles.Data();
            k.BattleId = (uint) mySqlDataReader["id"];
            v.DungeonId = (uint) mySqlDataReader["dungeon"];
            v.Name = (string) mySqlDataReader["name"];
            v.Stamina = (ushort) mySqlDataReader["stamina"];
            v.Samples = (uint) mySqlDataReader["samples"];
            v.HistoSamples = (uint) mySqlDataReader["histo_samples"];
            v.Repeatable = true;
            if ((int) num1 != (int) v.DungeonId)
            {
              num2 = (ushort) 0;
              if (data != null)
                data.Repeatable = false;
            }
            else
              num2 += v.Stamina;
            v.StaminaToReach = num2;
            this.mBattles.Update(k, v);
            num1 = v.DungeonId;
            data = v;
          }
          data.Repeatable = false;
        }
      }
    }

    public void Respond()
    {
      if (this.OnRequestComplete == null)
        return;
      this.OnRequestComplete(this.mBattles);
    }

    public delegate void DataReadyCallback(FFRKDataCacheTable<Key, FFRKInspector.DataCache.Battles.Data> battles);
  }
}
