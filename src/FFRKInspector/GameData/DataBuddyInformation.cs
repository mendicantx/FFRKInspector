// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataBuddyInformation
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData.Converters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FFRKInspector.GameData
{
  internal class DataBuddyInformation
  {
    [JsonProperty("row")]
    public SchemaConstants.PartyFormation Formation;
    [JsonProperty("id")]
    public uint Id;
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("ability_1_id")]
    public uint Ability1;
    [JsonProperty("ability_2_id")]
    public uint Ability2;
    [JsonProperty("level")]
    public byte Level;
    [JsonProperty("level_max")]
    public byte LevelMax;
    [JsonProperty("soul_strike_id")]
    public uint SoulStrike;
    [JsonProperty("job_name")]
    public string Job;
    [JsonProperty("weapon_id")]
    public uint WeaponId;
    [JsonProperty("armor_id")]
    public uint ArmorId;
    [JsonProperty("accessory_id")]
    public uint AccessoryId;
    [JsonProperty("buddy_id")]
    public uint BuddyId;
    [JsonProperty("series_id")]
    public uint SeriesId;
    [JsonConverter(typeof (EquipUsageListConverter))]
    [JsonProperty("equipment_category")]
    public List<DataBuddyEquipUsage> EquipUsage;
    [JsonProperty("abilities")]
    public List<DataAbility> Abilities;
    [JsonProperty("soul_strikes")]
    public List<DataAbility> SoulStrikes;

    public IEnumerable<SchemaConstants.EquipmentCategory> UsableEquipCategories => this.EquipUsage.Select<DataBuddyEquipUsage, SchemaConstants.EquipmentCategory>((Func<DataBuddyEquipUsage, SchemaConstants.EquipmentCategory>) (x => x.Category));

    public IEnumerable<int> AbilityIds
    {
      get
      {
        foreach (DataAbility ability1 in this.Abilities)
        {
          DataAbility ability = ability1;
          int id = ability.AbilityId;
          yield return id;
          ability = (DataAbility) null;
        }
      }
    }
  }
}
