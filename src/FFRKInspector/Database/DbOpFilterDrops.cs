// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpFilterDrops
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace FFRKInspector.Database
{
  internal class DbOpFilterDrops : IDbRequest
  {
    private List<BasicItemDropStats> mDropList;
    private SelectMultiParam<SchemaConstants.ItemType, uint> mItemTypes;
    private SelectMultiParam<SchemaConstants.Rarity, uint> mRarities;
    private SelectMultiParam<RealmSynergy.SynergyValue, uint> mSynergies;
    private SelectMultiParam<uint, uint> mWorlds;
    private SelectMultiParam<uint, uint> mDungeons;
    private SelectMultiParam<uint, uint> mBattles;
    private SelectSingleParam<string> mName;
    private bool mOnlyRepeatable;

    public bool RequiresTransaction => false;

    public SelectMultiParam<SchemaConstants.ItemType, uint> ItemTypes => this.mItemTypes;

    public SelectMultiParam<SchemaConstants.Rarity, uint> Rarities => this.mRarities;

    public SelectMultiParam<RealmSynergy.SynergyValue, uint> Synergies => this.mSynergies;

    public SelectMultiParam<uint, uint> Worlds => this.mWorlds;

    public SelectMultiParam<uint, uint> Dungeons => this.mDungeons;

    public SelectMultiParam<uint, uint> Battles => this.mBattles;

    public SelectSingleParam<string> Name => this.mName;

    public bool OnlyRepeatable
    {
      get => this.mOnlyRepeatable;
      set => this.mOnlyRepeatable = value;
    }

    public event DbOpFilterDrops.DataReadyCallback OnRequestComplete;

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      SelectBuilder selectBuilder = new SelectBuilder();
      selectBuilder.Table = "dungeon_drops";
      selectBuilder.Columns.Add("*");
      selectBuilder.Parameters.Add((ISelectParam) this.mItemTypes);
      selectBuilder.Parameters.Add((ISelectParam) this.mRarities);
      selectBuilder.Parameters.Add((ISelectParam) this.mSynergies);
      selectBuilder.Parameters.Add((ISelectParam) this.mWorlds);
      selectBuilder.Parameters.Add((ISelectParam) this.mDungeons);
      selectBuilder.Parameters.Add((ISelectParam) this.mBattles);
      selectBuilder.Parameters.Add((ISelectParam) this.mName);
      Dictionary<KeyValuePair<uint, uint>, BasicItemDropStats> dictionary = new Dictionary<KeyValuePair<uint, uint>, BasicItemDropStats>();
      using (MySqlCommand Command = new MySqlCommand(selectBuilder.ToString(), connection))
      {
        selectBuilder.Bind(Command);
        using (MySqlDataReader mySqlDataReader = Command.ExecuteReader())
        {
          while (mySqlDataReader.Read())
          {
            uint key1 = (uint) mySqlDataReader["battleid"];
            uint num1 = (uint) mySqlDataReader["itemid"];
            KeyValuePair<uint, uint> key2 = new KeyValuePair<uint, uint>(key1, num1);
            BasicItemDropStats basicItemDropStats = (BasicItemDropStats) null;
            if (!dictionary.TryGetValue(key2, out basicItemDropStats))
            {
              RealmSynergy.SynergyValue synergyValue = (RealmSynergy.SynergyValue) null;
              int ordinal = mySqlDataReader.GetOrdinal("item_series");
              if (!mySqlDataReader.IsDBNull(ordinal))
                synergyValue = RealmSynergy.FromSeries(Convert.ToUInt32(mySqlDataReader["item_series"]));
              basicItemDropStats = new BasicItemDropStats()
              {
                BattleId = key1,
                ItemId = num1,
                DungeonId = (uint) mySqlDataReader["dungeon_id"],
                WorldId = (uint) mySqlDataReader["world_id"],
                WorldName = (string) mySqlDataReader["world_name"],
                DungeonName = (string) mySqlDataReader["dungeon_name"],
                DungeonType = (SchemaConstants.DungeonType) mySqlDataReader["dungeon_type"],
                Rarity = (SchemaConstants.Rarity) mySqlDataReader["item_rarity"],
                Type = (SchemaConstants.ItemType) mySqlDataReader["item_type"],
                TimesRun = (uint) mySqlDataReader["times_run"],
                TimesRunWithHistogram = (uint) mySqlDataReader["times_run_with_histogram"],
                Synergy = synergyValue,
                BattleName = (string) mySqlDataReader["battle_name"],
                BattleStamina = (ushort) mySqlDataReader["battle_stamina"],
                ItemName = (string) mySqlDataReader["item_name"]
              };
              dictionary.Add(key2, basicItemDropStats);
            }
            int bucket = (int) mySqlDataReader["histo_bucket"];
            uint num2 = (uint) mySqlDataReader["histo_value"];
            if (bucket < 0)
              basicItemDropStats.TotalDrops = num2;
            else if (bucket > 0)
            {
              Debug.Assert((uint) bucket > 0U);
              basicItemDropStats.Histogram[bucket] = num2;
            }
          }
        }
      }
      this.mDropList = !this.mOnlyRepeatable ? dictionary.Values.ToList<BasicItemDropStats>() : dictionary.Values.Where<BasicItemDropStats>((Func<BasicItemDropStats, bool>) (x => x.IsBattleRepeatable)).ToList<BasicItemDropStats>();
      foreach (BasicItemDropStats mDrop in this.mDropList)
      {
        mDrop.Histogram[0] = mDrop.TimesRunWithHistogram;
        for (int bucket = 1; bucket < mDrop.Histogram.BucketCount; ++bucket)
          mDrop.Histogram[0] -= mDrop.Histogram[bucket];
      }
    }

    public void Respond()
    {
      if (this.OnRequestComplete == null)
        return;
      this.OnRequestComplete(this.mDropList);
    }

    public delegate void DataReadyCallback(List<BasicItemDropStats> items);
  }
}
