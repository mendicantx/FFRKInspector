// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataGachaSeriesItemDetails
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataGachaSeriesItemDetails
  {
    [JsonProperty("prob_by_rarity")]
    public DataGachaSeriesProbabilitiesByRarity ProbabilityByRarity;
    [JsonProperty("equipments")]
    public List<DataGachaItem> Items;
    [JsonProperty("assured_rarity")]
    public ushort AssuredRarity;
    [JsonProperty("boost_rate_for_assured_lot")]
    public Decimal BoostRateAssured;
    [JsonProperty("is_equal_prob_in_same_rarity")]
    public ushort EqualProbInRarity;
  }
}
