// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpLoadFeaturedItems
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace FFRKInspector.Database
{
  internal class DbOpLoadFeaturedItems : IDbRequest
  {
    private Dictionary<int, BasicFeaturedItemInfo> myItems;
    private uint myBannerID;
    private bool myIsJP;

    public bool RequiresTransaction => false;

    public event DbOpLoadFeaturedItems.DataReadyCallback OnRequestComplete;

    public DbOpLoadFeaturedItems(uint BannerID, bool isJP)
    {
      this.myItems = new Dictionary<int, BasicFeaturedItemInfo>();
      this.myBannerID = BannerID;
      this.myIsJP = isJP;
    }

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand(string.Format("SELECT * FROM banner_featured_items WHERE bannerid = {0} AND isJapanese = {1}", (object) this.myBannerID, (object) (this.myIsJP ? 1 : 0)), connection, transaction))
      {
        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
        {
          while (mySqlDataReader.Read())
          {
            BasicFeaturedItemInfo featuredItemInfo = new BasicFeaturedItemInfo();
            featuredItemInfo.BannerID = (uint) mySqlDataReader["bannerid"];
            byte b = (byte) mySqlDataReader["isJapanese"];
            featuredItemInfo.IsJP = b == (byte) 1;
            featuredItemInfo.ItemID = (uint) mySqlDataReader["itemid"];
            int key = this.HashValues(featuredItemInfo.BannerID, b, featuredItemInfo.ItemID);
            featuredItemInfo.DisplayOrder = (int) mySqlDataReader["disp_order"];
            featuredItemInfo.ItemImagePath = (string) mySqlDataReader["image_path"];
            featuredItemInfo.CharacterID = (uint) mySqlDataReader["character_id"];
            featuredItemInfo.HasSB = (byte) mySqlDataReader["has_sb"] == (byte) 1;
            featuredItemInfo.HasCharacterSB = (byte) mySqlDataReader["has_character_sb"] == (byte) 1;
            featuredItemInfo.LMID = (uint) mySqlDataReader["legend_materia_id"];
            featuredItemInfo.SBCategoryID = (int) mySqlDataReader["sb_category_id"];
            featuredItemInfo.Rate = this.ParseDecimal(mySqlDataReader["rate"]);
            featuredItemInfo.SBImagePath = (string) mySqlDataReader["sb_image_path"];
            featuredItemInfo.Description = (string) mySqlDataReader["sb_description"];
            this.myItems.Add(key, featuredItemInfo);
          }
        }
      }
    }

    private int HashValues(uint a, byte b, uint c) => ((1637 * 5479 + (int) a) * 5479 + (int) b) * 5479 + (int) c;

    private Decimal? ParseDecimal(object r) => r == DBNull.Value ? new Decimal?() : new Decimal?((Decimal) r);

    public void Respond()
    {
      if (this.OnRequestComplete == null)
        return;
      this.OnRequestComplete(this.myItems);
    }

    public delegate void DataReadyCallback(Dictionary<int, BasicFeaturedItemInfo> items);
  }
}
