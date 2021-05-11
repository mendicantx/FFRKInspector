// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.Party.DataEquipmentInformation
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;

namespace FFRKInspector.GameData.Party
{
  internal class DataEquipmentInformation
  {
    [JsonProperty("id")]
    public uint InstanceId;
    [JsonProperty("equipment_id")]
    public uint EquipmentId;
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("evolution_num")]
    public SchemaConstants.EvolutionLevel EvolutionNumber;
    [JsonProperty("level")]
    public byte Level;
    [JsonProperty("level_max")]
    public byte LevelMax;
    [JsonProperty("rarity")]
    public SchemaConstants.Rarity Rarity;
    [JsonProperty("base_rarity")]
    public SchemaConstants.Rarity BaseRarity;
    [JsonProperty("series_id")]
    public uint SeriesId;
    [JsonProperty("equipment_type")]
    public SchemaConstants.ItemType Type;
    [JsonProperty("atk")]
    public short Atk;
    [JsonProperty("matk")]
    public short Mag;
    [JsonProperty("acc")]
    public short Acc;
    [JsonProperty("def")]
    public short Def;
    [JsonProperty("mdef")]
    public short Res;
    [JsonProperty("eva")]
    public short Eva;
    [JsonProperty("mnd")]
    public short Mnd;
    [JsonProperty("series_atk")]
    public short SeriesAtk;
    [JsonProperty("series_matk")]
    public short SeriesMag;
    [JsonProperty("series_acc")]
    public short SeriesAcc;
    [JsonProperty("series_def")]
    public short SeriesDef;
    [JsonProperty("series_mdef")]
    public short SeriesRes;
    [JsonProperty("series_eva")]
    public short SeriesEva;
    [JsonProperty("series_mnd")]
    public short SeriesMnd;
    [JsonProperty("category_id")]
    public SchemaConstants.EquipmentCategory Category;
  }
}
