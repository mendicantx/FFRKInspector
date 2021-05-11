// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataGachaDraw
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataGachaDraw
  {
    [JsonProperty("SERVER_TIME")]
    public ulong ServerTime;
    [JsonProperty("gacha_series_id")]
    public uint GachaSeriesID;
    [JsonProperty("drop_item_map")]
    public JObject DropItems;
    [JsonProperty("user")]
    public DataGachaDrawUser User;
    [JsonProperty("items")]
    public List<DataGachaDrawItemDetails> ItemDetails;
  }
}
