// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpFilterDraws
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FFRKInspector.Database
{
  internal class DbOpFilterDraws : IDbRequest
  {
    private Dictionary<long, BasicRelicDropInfo> myDraws;
    private uint myBannerID;
    private bool myIsJP;

    public bool RequiresTransaction => false;

    public event DbOpFilterDraws.DataReadyCallback OnRequestComplete;

    public DbOpFilterDraws(uint BannerID, bool isJP)
    {
      this.myDraws = new Dictionary<long, BasicRelicDropInfo>();
      this.myBannerID = BannerID;
      this.myIsJP = isJP;
    }

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand(string.Format("SELECT relic_draws.*, items.name FROM relic_draws LEFT OUTER JOIN items ON relic_draws.itemid = items.id WHERE relic_draws.gacha_series_id = {0} AND relic_draws.isJapanese = {1}", (object) this.myBannerID, (object) (this.myIsJP ? 1 : 0)), connection, transaction))
      {
        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
        {
          while (mySqlDataReader.Read())
          {
            BasicRelicDropInfo basicRelicDropInfo = new BasicRelicDropInfo();
            basicRelicDropInfo.ServerTime = (ulong) mySqlDataReader["server_time"];
            basicRelicDropInfo.UserHash = (int) mySqlDataReader["userhash"];
            basicRelicDropInfo.ItemID = (uint) mySqlDataReader["itemid"];
            long key = this.HashValues(basicRelicDropInfo.ServerTime, basicRelicDropInfo.UserHash, basicRelicDropInfo.ItemID);
            basicRelicDropInfo.BannerID = (uint) mySqlDataReader["gacha_series_id"];
            byte num = (byte) mySqlDataReader["isJapanese"];
            basicRelicDropInfo.IsJP = num == (byte) 1;
            basicRelicDropInfo.DropNum = (ushort) mySqlDataReader["drop_num"];
            basicRelicDropInfo.Rarity = (byte) mySqlDataReader["rarity"];
            basicRelicDropInfo.ItemTotal = (ushort) mySqlDataReader["total_items_pulled"];
            basicRelicDropInfo.ItemName = (string) mySqlDataReader["name"];
            this.myDraws.Add(key, basicRelicDropInfo);
          }
        }
      }
    }

    private long HashValues(ulong a, int b, uint c) => ((1637L * 5479L + (long) (int) a) * 5479L + (long) b) * 5479L + (long) (int) c;

    private Decimal? ParseDecimal(object r) => r == DBNull.Value ? new Decimal?() : new Decimal?((Decimal) r);

    public void Respond()
    {
      if (this.OnRequestComplete == null)
        return;
      this.OnRequestComplete(this.myDraws);
    }

    public delegate void DataReadyCallback(Dictionary<long, BasicRelicDropInfo> draws);
  }
}
