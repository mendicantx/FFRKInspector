// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataGachaSeriesBannerLM
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;

namespace FFRKInspector.GameData
{
  internal class DataGachaSeriesBannerLM
  {
    [JsonProperty("buddy_id")]
    public uint CharacterID;
    [JsonProperty("buddy_name")]
    public string CharacterName;
    [JsonProperty("description")]
    public string Description;
    [JsonProperty("id")]
    public uint ID;
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("image_path")]
    public string ImagePath;
  }
}
