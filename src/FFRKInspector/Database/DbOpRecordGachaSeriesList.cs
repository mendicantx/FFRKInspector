// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpRecordGachaSeriesList
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System.Data;

namespace FFRKInspector.Database
{
  internal class DbOpRecordGachaSeriesList : IDbRequest
  {
    private DataGachaSeriesList mGachaSeries;

    public bool RequiresTransaction => true;

    public DbOpRecordGachaSeriesList(DataGachaSeriesList SeriesList) => this.mGachaSeries = SeriesList;

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      foreach (DataGachaSeriesInfo series in this.mGachaSeries.SeriesList)
      {
        bool isJP = series.isJapanese;
        if (isJP && series.SeriesName.Contains("(ww"))
          isJP = false;
        if (this.CallProcInsertBannerEntry(connection, transaction, series, isJP) > 0)
        {
          foreach (DataGachaSeriesBanner banner in series.Banners)
          {
            if (banner.ItemID > 0U)
            {
              DataGachaSeriesBannerEquipment equipment = banner.Equipment;
              uint num = 0;
              string name = "";
              if (equipment.LegendMateriaID != 0U && equipment.LegendMateria != null)
              {
                num = equipment.LegendMateria.CharacterID;
                name = equipment.LegendMateria.CharacterName;
              }
              else if (equipment.HasCharacterSoulBreak && equipment.HasSoulBreak && equipment.SoulBreak != null)
              {
                num = equipment.SoulBreak.CharacterID;
                name = equipment.SoulBreak.CharacterName;
              }
              if (num != 0U && !name.Equals(""))
                this.CallProcCharacterEntry(connection, transaction, num, name, equipment.SeriesID);
              ushort type = equipment.AccMax > 0 ? (ushort) 1 : (ushort) 2;
              this.CallProcItemEntry(connection, transaction, equipment, type);
              this.CallProcEquipmentStatsEntry(connection, transaction, equipment, type);
              this.CallProcInsertFeaturedItemEntry(connection, transaction, series, banner, isJP, num);
            }
          }
        }
      }
    }

    public void Respond()
    {
    }

    private int CallProcEquipmentStatsEntry(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataGachaSeriesBannerEquipment equip,
      ushort type)
    {
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
        mySqlCommand.Parameters.AddWithValue("@itype", (object) type);
        return mySqlCommand.ExecuteNonQuery();
      }
    }

    private int CallProcItemEntry(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataGachaSeriesBannerEquipment equip,
      ushort type)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("insert_item_entry", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@iid", (object) equip.ItemID);
        mySqlCommand.Parameters.AddWithValue("@iname", (object) equip.ItemName);
        mySqlCommand.Parameters.AddWithValue("@irarity", (object) equip.Rarity);
        mySqlCommand.Parameters.AddWithValue("@iseries", (object) equip.SeriesID);
        mySqlCommand.Parameters.AddWithValue("@itype", (object) type);
        mySqlCommand.Parameters.AddWithValue("@isubtype", (object) equip.CategoryID);
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

    private int CallProcInsertBannerEntry(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataGachaSeriesInfo info,
      bool isJP)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("insert_banner_entry", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@bid", (object) info.SeriesId);
        mySqlCommand.Parameters.AddWithValue("@isjp", (object) isJP);
        mySqlCommand.Parameters.AddWithValue("@bname", (object) info.SeriesName);
        mySqlCommand.Parameters.AddWithValue("@t_opened", (object) info.TimeOpened);
        mySqlCommand.Parameters.AddWithValue("@t_closed", (object) info.TimeClosed);
        mySqlCommand.Parameters.AddWithValue("@lineupimgpath", (object) info.LineupImagePath);
        return mySqlCommand.ExecuteNonQuery();
      }
    }

    private int CallProcInsertFeaturedItemEntry(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataGachaSeriesInfo info,
      DataGachaSeriesBanner banner,
      bool isJP,
      uint myCharID)
    {
      DataGachaSeriesBannerEquipment equipment = banner.Equipment;
      uint num = 0;
      if (equipment.HasSoulBreak && equipment.SoulBreak != null)
        num = equipment.SoulBreak.SBTypeID;
      string str1 = "";
      string str2 = "";
      if (equipment.HasSoulBreak && equipment.SoulBreak != null)
      {
        str1 = equipment.SoulBreak.ImagePath;
        str2 = equipment.SoulBreak.Description;
      }
      else if (equipment.LegendMateriaID != 0U && equipment.LegendMateria != null)
      {
        str1 = equipment.LegendMateria.ImagePath;
        str2 = equipment.LegendMateria.Description;
      }
      using (MySqlCommand mySqlCommand = new MySqlCommand("insert_banner_featured_item_entry", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@bid", (object) info.SeriesId);
        mySqlCommand.Parameters.AddWithValue("@isjp", (object) isJP);
        mySqlCommand.Parameters.AddWithValue("@iid", (object) banner.ItemID);
        mySqlCommand.Parameters.AddWithValue("@dorder", (object) banner.DisplayOrder);
        mySqlCommand.Parameters.AddWithValue("@imgpath", (object) equipment.ImagePath);
        mySqlCommand.Parameters.AddWithValue("@charid", (object) myCharID);
        mySqlCommand.Parameters.AddWithValue("@hassb", (object) equipment.HasSoulBreak);
        mySqlCommand.Parameters.AddWithValue("@hascharsb", (object) equipment.HasCharacterSoulBreak);
        mySqlCommand.Parameters.AddWithValue("@lmid", (object) equipment.LegendMateriaID);
        mySqlCommand.Parameters.AddWithValue("@sbtype", (object) num);
        mySqlCommand.Parameters.AddWithValue("@sbimgpath", (object) str1);
        mySqlCommand.Parameters.AddWithValue("@sbdescription", (object) str2);
        return mySqlCommand.ExecuteNonQuery();
      }
    }
  }
}
