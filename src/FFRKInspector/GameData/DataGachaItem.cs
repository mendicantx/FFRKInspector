// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataGachaItem
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataGachaItem
  {
    [JsonProperty("category_name")]
    public string Category;
    [JsonProperty("probability")]
    public Decimal Probability;
    [JsonProperty("acc_max")]
    public int AccMax;
    [JsonProperty("acc_min")]
    public int AccMin;
    [JsonProperty("additional_bonus_attributes")]
    public List<DataGachaSeriesBannerEquipmentAttributes> AttributesRS;
    [JsonProperty("attributes")]
    public List<DataGachaSeriesBannerEquipmentAttributes> Attributes;
    [JsonProperty("atk_max")]
    public int AtkMax;
    [JsonProperty("atk_min")]
    public int AtkMin;
    [JsonProperty("base_hammering_num")]
    public int BaseRefine;
    [JsonProperty("category_id")]
    public int CategoryID;
    [JsonProperty("def_max")]
    public int DefMax;
    [JsonProperty("def_min")]
    public int DefMin;
    [JsonProperty("eva_max")]
    public int EvaMax;
    [JsonProperty("eva_min")]
    public int EvaMin;
    [JsonProperty("hammering_affect_param_key")]
    public string RefineStat;
    [JsonProperty("hp_max")]
    public int HPMax;
    [JsonProperty("hp_min")]
    public int HPMin;
    [JsonProperty("id")]
    public uint ItemID;
    [JsonProperty("image_path")]
    public string ImagePath;
    [JsonProperty("legend_materia")]
    public DataGachaSeriesBannerLM LegendMateria;
    [JsonProperty("legend_materia_id")]
    public uint LegendMateriaID;
    [JsonProperty("level_max")]
    public int LevelMax;
    [JsonProperty("matk_max")]
    public int MagMax;
    [JsonProperty("matk_min")]
    public int MagMin;
    [JsonProperty("max_hammering_num")]
    public int MaxRefine;
    [JsonProperty("mdef_max")]
    public int ResMax;
    [JsonProperty("mdef_min")]
    public int ResMin;
    [JsonProperty("mnd_max")]
    public int MndMax;
    [JsonProperty("mnd_min")]
    public int MndMin;
    [JsonProperty("name")]
    public string ItemName;
    [JsonProperty("rarity")]
    public ushort Rarity;
    [JsonProperty("series_id")]
    public uint SeriesID;
    [JsonProperty("soul_strike")]
    public DataGachaSeriesBannerSB SoulBreak;
    [JsonProperty("has_soul_strike")]
    public bool HasSoulBreak;
    [JsonProperty("has_someones_soul_strike")]
    public bool HasCharacterSoulBreak;
  }
}
