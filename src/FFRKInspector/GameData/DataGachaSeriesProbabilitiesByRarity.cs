// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataGachaSeriesProbabilitiesByRarity
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using System;

namespace FFRKInspector.GameData
{
  internal class DataGachaSeriesProbabilitiesByRarity
  {
    [JsonProperty("1")]
    public Decimal? OneStar;
    [JsonProperty("2")]
    public Decimal? TwoStar;
    [JsonProperty("3")]
    public Decimal? ThreeStar;
    [JsonProperty("4")]
    public Decimal? FourStar;
    [JsonProperty("5")]
    public Decimal? FiveStar;
    [JsonProperty("6")]
    public Decimal? SixStar;
    [JsonProperty("7")]
    public Decimal? SevenStar;
  }
}
