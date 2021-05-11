// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpRecordBattleList
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System.Data;

namespace FFRKInspector.Database
{
  internal class DbOpRecordBattleList : IDbRequest
  {
    private EventListBattles mBattles;

    public bool RequiresTransaction => true;

    public DbOpRecordBattleList(EventListBattles battles) => this.mBattles = battles;

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      foreach (DataBattle battle in this.mBattles.Battles)
        this.CallProcInsertBattleEntry(connection, transaction, battle);
    }

    public void Respond()
    {
    }

    private int CallProcInsertBattleEntry(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataBattle battle)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("insert_battle_entry", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@bid", (object) battle.Id);
        mySqlCommand.Parameters.AddWithValue("@did", (object) battle.DungeonId);
        mySqlCommand.Parameters.AddWithValue("@bname", (object) battle.Name);
        mySqlCommand.Parameters.AddWithValue("@bstam", (object) battle.Stamina);
        return mySqlCommand.ExecuteNonQuery();
      }
    }
  }
}
