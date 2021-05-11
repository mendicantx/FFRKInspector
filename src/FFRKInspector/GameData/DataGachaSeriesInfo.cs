// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataGachaSeriesInfo
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace FFRKInspector.GameData
{
  internal class DataGachaSeriesInfo
  {
    [JsonProperty("series_id")]
    public uint SeriesId;
    [JsonProperty("series_name")]
    public string SeriesName;
    [JsonProperty("box_list")]
    public List<DataGachaSeriesBox> Boxes;
    [JsonProperty("banner_list")]
    public List<DataGachaSeriesBanner> Banners;
    [JsonProperty("opened_at")]
    public ulong TimeOpened;
    [JsonProperty("closed_at")]
    public ulong TimeClosed;
    [JsonProperty("line_up_image_path")]
    public string LineupImagePath;

    public bool isJapanese => new Regex("[　-〿]|[\u3040-ゟ]|[゠-ヿ]|[\uFF00-\uFFEF]|[一-龯]|[★-☆]|[←-↕]|※").Match(this.SeriesName).Success;
  }
}
