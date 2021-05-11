// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Database.DbOpRecordGachaDraw
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data;

namespace FFRKInspector.Database
{
  internal class DbOpRecordGachaDraw : IDbRequest
  {
    private DataGachaDraw mGachaDraw;
    private List<DataGachaDrawItem> mGachaDrawItems;

    public bool RequiresTransaction => true;

    public DbOpRecordGachaDraw(DataGachaDraw gachaDraw, List<DataGachaDrawItem> gachaDrawItems)
    {
      this.mGachaDraw = gachaDraw;
      this.mGachaDrawItems = gachaDrawItems;
    }

    public void Execute(MySqlConnection connection, MySqlTransaction transaction)
    {
      bool isJapanese = this.mGachaDraw.ItemDetails[0].isJapanese;
      int totalRelics = 0;
      foreach (DataGachaDrawItem mGachaDrawItem in this.mGachaDrawItems)
        totalRelics += (int) mGachaDrawItem.DropNum;
      foreach (DataGachaDrawItem mGachaDrawItem in this.mGachaDrawItems)
        this.CallProcDrawItemEntry(connection, transaction, this.mGachaDraw, mGachaDrawItem, isJapanese, totalRelics);
    }

    public void Respond()
    {
    }

    private int CallProcDrawItemEntry(
      MySqlConnection connection,
      MySqlTransaction transaction,
      DataGachaDraw draw,
      DataGachaDrawItem item,
      bool isJP,
      int totalRelics)
    {
      using (MySqlCommand mySqlCommand = new MySqlCommand("insert_relic_draw_entry", connection, transaction))
      {
        mySqlCommand.CommandType = CommandType.StoredProcedure;
        mySqlCommand.Parameters.AddWithValue("@servertime", (object) draw.ServerTime);
        mySqlCommand.Parameters.AddWithValue("@uhash", (object) draw.User.ID);
        mySqlCommand.Parameters.AddWithValue("@iid", (object) item.ItemID);
        mySqlCommand.Parameters.AddWithValue("@num", (object) item.DropNum);
        mySqlCommand.Parameters.AddWithValue("@irarity", (object) item.Rarity);
        mySqlCommand.Parameters.AddWithValue("@sid", (object) draw.GachaSeriesID);
        mySqlCommand.Parameters.AddWithValue("@isjp", (object) isJP);
        mySqlCommand.Parameters.AddWithValue("@total", (object) totalRelics);
        return mySqlCommand.ExecuteNonQuery();
      }
    }
  }
}
