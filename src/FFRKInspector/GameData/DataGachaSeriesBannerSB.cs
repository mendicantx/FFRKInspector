// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataGachaSeriesBannerSB
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;

namespace FFRKInspector.GameData
{
  internal class DataGachaSeriesBannerSB
  {
    [JsonProperty("allowed_buddy_id")]
    public uint CharacterID;
    [JsonProperty("allowed_buddy_name")]
    public string CharacterName;
    [JsonProperty("description")]
    public string Description;
    [JsonProperty("id")]
    public uint ID;
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("soul_strike_category_id")]
    public uint SBTypeID;
    [JsonProperty("soul_strike_category_name")]
    public string SBTypeName;
    [JsonProperty("image_path")]
    public string ImagePath;
  }
}
