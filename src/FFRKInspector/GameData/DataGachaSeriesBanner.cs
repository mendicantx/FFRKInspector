// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataGachaSeriesBanner
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;

namespace FFRKInspector.GameData
{
  internal class DataGachaSeriesBanner
  {
    [JsonProperty("disp_order")]
    public int DisplayOrder;
    [JsonProperty("gacha_series_id")]
    public int SeriesID;
    [JsonProperty("item_id")]
    public uint ItemID;
    [JsonProperty("equipment")]
    public DataGachaSeriesBannerEquipment Equipment;
  }
}
