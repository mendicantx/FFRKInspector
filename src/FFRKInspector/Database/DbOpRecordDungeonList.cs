// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpRecordDungeonList
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System.Data;

namespace FFRKInspector.Database
{
  internal class DbOpRecordDungeonList : IDbRequest
  {
    private EventListDungeons mDungeonList;

    public bool RequiresTransaction => true;

    public DbOpRecordDungeonList(EventListDungeons dungeons) => this.mDungeonList = dungeons;

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      this.CallProcInsertWorldEntry(connection, transaction, this.mDungeonList.World);
      foreach (DataDungeon dungeon in this.mDungeonList.Dungeons)
        this.CallProcInsertDungeonEntry(connection, transaction, this.mDungeonList.World, dungeon);
    }

    public void Respond()
    {
    }

    private int CallProcInsertWorldEntry(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataWorld world)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("insert_world_entry", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@wid", (object) world.Id);
        mySqlCommand.Parameters.AddWithValue("@series", (object) world.SeriesId);
        mySqlCommand.Parameters.AddWithValue("@type", (object) world.Type);
        mySqlCommand.Parameters.AddWithValue("@name", (object) world.Name);
        return mySqlCommand.ExecuteNonQuery();
      }
    }

    private int CallProcInsertDungeonEntry(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataWorld world,
      DataDungeon dungeon)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("insert_dungeon_entry", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@did", (object) dungeon.Id);
        mySqlCommand.Parameters.AddWithValue("@world_id", (object) world.Id);
        mySqlCommand.Parameters.AddWithValue("@series_id", (object) dungeon.SeriesId);
        mySqlCommand.Parameters.AddWithValue("@dname", (object) dungeon.Name);
        mySqlCommand.Parameters.AddWithValue("@dtype", (object) dungeon.Type);
        mySqlCommand.Parameters.AddWithValue("@ddiff", (object) dungeon.Difficulty);
        return mySqlCommand.ExecuteNonQuery();
      }
    }
  }
}
