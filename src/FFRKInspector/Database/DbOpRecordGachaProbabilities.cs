// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpRecordGachaProbabilities
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace FFRKInspector.Database
{
  internal class DbOpRecordGachaProbabilities : IDbRequest
  {
    private DataGachaSeriesItemDetails mItemDetails;
    private uint mSeriesID;
    private ulong serverTime;

    public bool RequiresTransaction => true;

    public DbOpRecordGachaProbabilities(
      DataGachaSeriesItemDetails itemDetails,
      uint seriesID,
      ulong serverTime)
    {
      this.mItemDetails = itemDetails;
      this.mSeriesID = seriesID;
      this.serverTime = serverTime;
    }

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      if (this.mItemDetails.Items == null || this.mItemDetails.Items.Count == 0)
        return;
      bool success = new Regex("[　-〿]|[\u3040-ゟ]|[゠-ヿ]|[\uFF00-\uFFEF]|[一-龯]|[★-☆]|[←-↕]|※").Match(this.mItemDetails.Items[0].ItemName).Success;
      if (this.CallProcRecordBannerProbabilities(connection, transaction, this.mItemDetails, this.mSeriesID, success) <= 0)
        return;
      List<uint> uintList = this.SelectFeaturedItemIDs(connection, transaction, this.mSeriesID);
      Decimal off5 = !this.mItemDetails.ProbabilityByRarity.FiveStar.HasValue ? 0M : this.mItemDetails.ProbabilityByRarity.FiveStar.Value;
      Decimal off6 = !this.mItemDetails.ProbabilityByRarity.SixStar.HasValue ? 0M : this.mItemDetails.ProbabilityByRarity.SixStar.Value;
      Decimal off7 = !this.mItemDetails.ProbabilityByRarity.SevenStar.HasValue ? 0M : this.mItemDetails.ProbabilityByRarity.SevenStar.Value;
      foreach (DataGachaItem equip in this.mItemDetails.Items)
      {
        if (uintList.Contains(equip.ItemID))
        {
          this.CallProcItemEntry(connection, transaction, equip);
          this.CallProcEquipmentStatsEntry(connection, transaction, equip);
          if (equip.LegendMateriaID != 0U && equip.LegendMateria != null)
          {
            uint characterId = equip.LegendMateria.CharacterID;
            string characterName = equip.LegendMateria.CharacterName;
            this.CallProcCharacterEntry(connection, transaction, characterId, characterName, equip.SeriesID);
          }
          if (this.CallProcRecordItemProbability(connection, transaction, equip, this.mSeriesID, success) > 0)
          {
            if (equip.Rarity == (ushort) 5)
              off5 -= equip.Probability;
            else if (equip.Rarity == (ushort) 6)
              off6 -= equip.Probability;
            else if (equip.Rarity == (ushort) 7)
              off7 -= equip.Probability;
          }
        }
      }
      this.CallProcRecordOffBannerProbabilities(connection, transaction, this.mSeriesID, success, off5, off6, off7);
    }

    public void Respond()
    {
    }

    private int CallProcRecordBannerProbabilities(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataGachaSeriesItemDetails itemDetails,
      uint SeriesID,
      bool isJP)
    {
      Decimal num1 = !itemDetails.ProbabilityByRarity.OneStar.HasValue ? 0M : itemDetails.ProbabilityByRarity.OneStar.Value;
      Decimal num2 = !itemDetails.ProbabilityByRarity.TwoStar.HasValue ? 0M : itemDetails.ProbabilityByRarity.TwoStar.Value;
      Decimal num3 = !itemDetails.ProbabilityByRarity.ThreeStar.HasValue ? 0M : itemDetails.ProbabilityByRarity.ThreeStar.Value;
      Decimal num4 = !itemDetails.ProbabilityByRarity.FourStar.HasValue ? 0M : itemDetails.ProbabilityByRarity.FourStar.Value;
      Decimal num5 = !itemDetails.ProbabilityByRarity.FiveStar.HasValue ? 0M : itemDetails.ProbabilityByRarity.FiveStar.Value;
      Decimal num6 = !itemDetails.ProbabilityByRarity.SixStar.HasValue ? 0M : itemDetails.ProbabilityByRarity.SixStar.Value;
      Decimal num7 = !itemDetails.ProbabilityByRarity.SevenStar.HasValue ? 0M : itemDetails.ProbabilityByRarity.SevenStar.Value;
      using (MySqlCommand mySqlCommand = new MySqlCommand("record_banner_probabilities", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@bid", (object) SeriesID);
        mySqlCommand.Parameters.AddWithValue("@isjp", (object) isJP);
        mySqlCommand.Parameters.AddWithValue("@r1", (object) num1);
        mySqlCommand.Parameters.AddWithValue("@r2", (object) num2);
        mySqlCommand.Parameters.AddWithValue("@r3", (object) num3);
        mySqlCommand.Parameters.AddWithValue("@r4", (object) num4);
        mySqlCommand.Parameters.AddWithValue("@r5", (object) num5);
        mySqlCommand.Parameters.AddWithValue("@r6", (object) num6);
        mySqlCommand.Parameters.AddWithValue("@r7", (object) num7);
        mySqlCommand.Parameters.AddWithValue("@assuredrarity", (object) itemDetails.AssuredRarity);
        mySqlCommand.Parameters.AddWithValue("@boostrate", (object) itemDetails.BoostRateAssured);
        mySqlCommand.Parameters.AddWithValue("@equalprob", (object) itemDetails.EqualProbInRarity);
        return mySqlCommand.ExecuteNonQuery();
      }
    }

    private int CallProcRecordOffBannerProbabilities(
      MySqlConnection connection,
      MySqlTransaction transaction,
      uint SeriesID,
      bool isJP,
      Decimal off5,
      Decimal off6,
      Decimal off7)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("record_off_banner_probabilities", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@bid", (object) SeriesID);
        mySqlCommand.Parameters.AddWithValue("@isjp", (object) isJP);
        mySqlCommand.Parameters.AddWithValue("@obr5", (object) off5);
        mySqlCommand.Parameters.AddWithValue("@obr6", (object) off6);
        mySqlCommand.Parameters.AddWithValue("@obr7", (object) off7);
        return mySqlCommand.ExecuteNonQuery();
      }
    }

    private int CallProcRecordItemProbability(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataGachaItem equip,
      uint SeriesID,
      bool isJP)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("record_item_probability", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@bid", (object) SeriesID);
        mySqlCommand.Parameters.AddWithValue("@isjp", (object) isJP);
        mySqlCommand.Parameters.AddWithValue("@iid", (object) equip.ItemID);
        mySqlCommand.Parameters.AddWithValue("@irate", (object) equip.Probability);
        return mySqlCommand.ExecuteNonQuery();
      }
    }

    private int CallProcItemEntry(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataGachaItem equip)
    {
      int num = equip.AccMax > 0 ? 1 : 2;
      using (MySqlCommand mySqlCommand = new MySqlCommand("insert_item_entry", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@iid", (object) equip.ItemID);
        mySqlCommand.Parameters.AddWithValue("@iname", (object) equip.ItemName);
        mySqlCommand.Parameters.AddWithValue("@irarity", (object) equip.Rarity);
        mySqlCommand.Parameters.AddWithValue("@iseries", (object) equip.SeriesID);
        mySqlCommand.Parameters.AddWithValue("@itype", (object) num);
        mySqlCommand.Parameters.AddWithValue("@isubtype", (object) equip.CategoryID);
        return mySqlCommand.ExecuteNonQuery();
      }
    }

    private int CallProcEquipmentStatsEntry(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataGachaItem equip)
    {
      int num = equip.AccMax > 0 ? 1 : 2;
      using (MySqlCommand mySqlCommand = new MySqlCommand("insert_equipment_stats_entry", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@iid", (object) equip.ItemID);
        mySqlCommand.Parameters.AddWithValue("@iname", (object) equip.ItemName);
        mySqlCommand.Parameters.AddWithValue("@atkmin", (object) equip.AtkMin);
        mySqlCommand.Parameters.AddWithValue("@magmin", (object) equip.MagMin);
        mySqlCommand.Parameters.AddWithValue("@accmin", (object) equip.AccMin);
        mySqlCommand.Parameters.AddWithValue("@defmin", (object) equip.DefMin);
        mySqlCommand.Parameters.AddWithValue("@resmin", (object) equip.ResMin);
        mySqlCommand.Parameters.AddWithValue("@evamin", (object) equip.EvaMin);
        mySqlCommand.Parameters.AddWithValue("@mndmin", (object) equip.MndMin);
        mySqlCommand.Parameters.AddWithValue("@atkmax", (object) equip.AtkMax);
        mySqlCommand.Parameters.AddWithValue("@magmax", (object) equip.MagMax);
        mySqlCommand.Parameters.AddWithValue("@accmax", (object) equip.AccMax);
        mySqlCommand.Parameters.AddWithValue("@defmax", (object) equip.DefMax);
        mySqlCommand.Parameters.AddWithValue("@resmax", (object) equip.ResMax);
        mySqlCommand.Parameters.AddWithValue("@evamax", (object) equip.EvaMax);
        mySqlCommand.Parameters.AddWithValue("@mndmax", (object) equip.MndMax);
        mySqlCommand.Parameters.AddWithValue("@irarity", (object) equip.Rarity);
        mySqlCommand.Parameters.AddWithValue("@iseries", (object) equip.SeriesID);
        mySqlCommand.Parameters.AddWithValue("@isubtype", (object) equip.CategoryID);
        mySqlCommand.Parameters.AddWithValue("@itype", (object) num);
        return mySqlCommand.ExecuteNonQuery();
      }
    }

    private int CallProcCharacterEntry(
      MySqlConnection connection,
      MySqlTransaction transaction,
      uint charID,
      string name,
      uint seriesID)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("insert_character_entry", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@cid", (object) charID);
        mySqlCommand.Parameters.AddWithValue("@cname", (object) name);
        mySqlCommand.Parameters.AddWithValue("@sid", (object) seriesID);
        return mySqlCommand.ExecuteNonQuery();
      }
    }

    private List<uint> SelectFeaturedItemIDs(
      MySqlConnection connection,
      MySqlTransaction transaction,
      uint bannerid)
    {
      List<uint> uintList = new List<uint>();
      using (MySqlCommand mySqlCommand = new MySqlCommand("SELECT itemid FROM banner_featured_items WHERE bannerid = " + (object) bannerid, connection, transaction))
      {
        using (MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader())
        {
          while (mySqlDataReader.Read())
            uintList.Add((uint) mySqlDataReader["itemid"]);
        }
      }
      return uintList;
    }
  }
}
