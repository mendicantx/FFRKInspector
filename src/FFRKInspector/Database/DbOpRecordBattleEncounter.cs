// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpRecordBattleEncounter
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using FFRKInspector.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FFRKInspector.Database
{
  internal class DbOpRecordBattleEncounter : IDbRequest
  {
    private EventBattleInitiated mEncounter;

    public bool RequiresTransaction => true;

    public DbOpRecordBattleEncounter(EventBattleInitiated encounter) => this.mEncounter = encounter;

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      this.CallProcRecordBattleEncounter(connection, transaction, this.mEncounter.Battle);
      List<DropEvent> list = this.mEncounter.Battle.Drops.ToList<DropEvent>();
      foreach (BasicEnemyInfo enemy in this.mEncounter.Battle.Enemies)
        this.CallProcInsertEnemyEntry(connection, transaction, enemy.EnemyId, enemy.EnemyName);
      IEnumerable<DropEvent> source1 = list.Where<DropEvent>((Func<DropEvent, bool>) (x => x.ItemType != DataEnemyDropItem.DropItemType.Gold));
      foreach (DropEvent drop in source1)
        this.CallProcRecordDropsForBattleAndEnemy(connection, transaction, this.mEncounter.Battle.BattleId, drop);
      foreach (IGrouping<uint, DropEvent> source2 in source1.GroupBy<DropEvent, uint>((Func<DropEvent, uint>) (x => x.ItemId)))
      {
        uint count = (uint) source2.Sum<DropEvent>((Func<DropEvent, long>) (x => (long) x.NumberOfItems));
        this.CallProcRecordDropsForBattle(connection, transaction, this.mEncounter.Battle.BattleId, source2.Key, count);
      }
      Log.LogFormat("Committing drop information for battle #{0}.  {1} items.", (object) this.mEncounter.Battle.BattleId, (object) list.Count);
    }

    public void Respond()
    {
    }

    private int CallProcRecordBattleEncounter(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataActiveBattle battle)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("record_battle_encounter", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@battle_id", (object) battle.BattleId);
        return mySqlCommand.ExecuteNonQuery();
      }
    }

    private int CallProcInsertEnemyEntry(
      MySqlConnection connection,
      MySqlTransaction transaction,
      uint EnemyId,
      string EnemyName)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("insert_enemy_entry", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@enemy_id", (object) EnemyId);
        mySqlCommand.Parameters.AddWithValue("@enemy_name", (object) EnemyName);
        return mySqlCommand.ExecuteNonQuery();
      }
    }

    private int CallProcRecordDropsForBattleAndEnemy(
      MySqlConnection connection,
      MySqlTransaction transaction,
      uint battle_id,
      DropEvent drop)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("record_drops_for_battle_and_enemy", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@battle_id", (object) battle_id);
        mySqlCommand.Parameters.AddWithValue("@item_id", (object) drop.ItemId);
        mySqlCommand.Parameters.AddWithValue("@enemy_id", (object) drop.EnemyId);
        mySqlCommand.Parameters.AddWithValue("@item_count", (object) drop.NumberOfItems);
        return mySqlCommand.ExecuteNonQuery();
      }
    }

    private int CallProcRecordDropsForBattle(
      MySqlConnection connection,
      MySqlTransaction transaction,
      uint battle_id,
      uint item_id,
      uint count)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("record_drops_for_battle", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@battle_id", (object) battle_id);
        mySqlCommand.Parameters.AddWithValue("@item_id", (object) item_id);
        mySqlCommand.Parameters.AddWithValue("@item_count", (object) count);
        return mySqlCommand.ExecuteNonQuery();
      }
    }
  }
}
