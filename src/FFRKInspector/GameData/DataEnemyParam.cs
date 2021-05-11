// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataEnemyParam
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace FFRKInspector.GameData
{
    public class DataEnemyParam
  {
    [JsonProperty("disp_name")]
    public string Name;
    [JsonProperty("id")]
    public uint Id;
    [JsonProperty("max_hp")]
    public uint MaxHp;
    [JsonProperty("lv")]
    public uint Lv;
    [JsonProperty("acc")]
    public uint Acc;
    [JsonProperty("critical")]
    public uint Crit;
    [JsonProperty("eva")]
    public uint Eva;
    [JsonProperty("exp")]
    public uint Exp;
    [JsonProperty("atk")]
    public uint Atk;
    [JsonProperty("def")]
    public uint Def;
    [JsonProperty("matk")]
    public uint Mag;
    [JsonProperty("mdef")]
    public uint Res;
    [JsonProperty("mnd")]
    public uint Mnd;
    [JsonProperty("spd")]
    public uint Spd;
    [JsonProperty("cast_time_type")]
    public uint CastTimeCode;
    [JsonProperty("animation_info")]
    public DataEnemyParamAnimationInfo AnimationInfo;
    [JsonProperty("abilities")]
    public List<DataEnemyParamAbilities> Abilities;
    [JsonProperty("counters")]
    public List<DataEnemyParamCounters> Counters;
    [JsonProperty("def_attributes")]
    public List<DataDefAttributes> DefAttributes;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;

    public string CastTime
    {
      get
      {
        Type enumType = typeof (SchemaConstants.CastTimeType);
        string name = Enum.GetName(enumType, (object) this.CastTimeCode);
        return ((DescriptionAttribute) enumType.GetMember(name)[0].GetCustomAttributes(typeof (DescriptionAttribute), false)[0]).Description;
      }
    }
  }
}
