// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpLoadAllBanners
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.DataCache;
using FFRKInspector.DataCache.Banners;
using MySql.Data.MySqlClient;
using System;

namespace FFRKInspector.Database
{
  internal class DbOpLoadAllBanners : IDbRequest
  {
    private FFRKDataCacheTable<Key, FFRKInspector.DataCache.Banners.Data> mBanners;

    public bool RequiresTransaction => false;

    public event DbOpLoadAllBanners.DataReadyCallback OnRequestComplete;

    public DbOpLoadAllBanners() => this.mBanners = new FFRKDataCacheTable<Key, FFRKInspector.DataCache.Banners.Data>();

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT * FROM banners WHERE hidden = 0 ORDER BY isJapanese ASC, id ASC", connection, transaction))
      {
        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
        {
          while (mySqlDataReader.Read())
          {
            Key k = new Key();
            FFRKInspector.DataCache.Banners.Data v = new FFRKInspector.DataCache.Banners.Data();
            k.BannerKey = (uint) mySqlDataReader["id"] * 2U + (uint) (byte) mySqlDataReader["isJapanese"];
            v.BannerId = (uint) mySqlDataReader["id"];
            byte num = (byte) mySqlDataReader["isJapanese"];
            v.isJP = num == (byte) 1;
            v.BannerName = (string) mySqlDataReader["name"];
            v.TimeOpened = (ulong) mySqlDataReader["time_opened"];
            v.TimeClosed = (ulong) mySqlDataReader["time_closed"];
            v.LineupImgPath = (string) mySqlDataReader["line_up_image_path"];
            v.Rate1 = this.ParseDecimal(mySqlDataReader["rate1"]);
            v.Rate2 = this.ParseDecimal(mySqlDataReader["rate2"]);
            v.Rate3 = this.ParseDecimal(mySqlDataReader["rate3"]);
            v.Rate4 = this.ParseDecimal(mySqlDataReader["rate4"]);
            v.Rate5 = this.ParseDecimal(mySqlDataReader["rate5"]);
            v.Rate6 = this.ParseDecimal(mySqlDataReader["rate6"]);
            v.Rate7 = this.ParseDecimal(mySqlDataReader["rate7"]);
            v.OffBannerRate5 = this.ParseDecimal(mySqlDataReader["off_banner_rate5"]);
            v.OffBannerRate6 = this.ParseDecimal(mySqlDataReader["off_banner_rate6"]);
            v.OffBannerRate7 = this.ParseDecimal(mySqlDataReader["off_banner_rate7"]);
            v.BoostRateAssured = this.ParseDecimal(mySqlDataReader["boost_rate_assured"]);
            v.AssuredRarity = mySqlDataReader["assured_rarity"] == DBNull.Value ? (byte) 0 : (byte) mySqlDataReader["assured_rarity"];
            v.EqualProbInRarity = mySqlDataReader["equal_prob_in_rarity"] != DBNull.Value && (byte) mySqlDataReader["equal_prob_in_rarity"] == (byte) 1;
            this.mBanners.Update(k, v);
          }
        }
      }
    }

    private Decimal? ParseDecimal(object r) => r == DBNull.Value ? new Decimal?() : new Decimal?((Decimal) r);

    public void Respond()
    {
      if (this.OnRequestComplete == null)
        return;
      this.OnRequestComplete(this.mBanners);
    }

    public delegate void DataReadyCallback(FFRKDataCacheTable<Key, FFRKInspector.DataCache.Banners.Data> banners);
  }
}
