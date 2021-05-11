// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataAbilityOption
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
    public class DataAbilityOption
  {
    [JsonProperty("ability_animation_id")]
    public int AnimId;
    [JsonProperty("active_target_method")]
    public int ActiveTargetMethod;
    [JsonProperty("alias_name")]
    public string AliasName;
    [JsonProperty("arg1")]
    public int Arg1;
    [JsonProperty("arg2")]
    public int Arg2;
    [JsonProperty("arg3")]
    public int Arg3;
    [JsonProperty("arg4")]
    public int Arg4;
    [JsonProperty("arg5")]
    public int Arg5;
    [JsonProperty("arg6")]
    public int Arg6;
    [JsonProperty("arg7")]
    public int Arg7;
    [JsonProperty("arg8")]
    public int Arg8;
    [JsonProperty("arg9")]
    public int Arg9;
    [JsonProperty("arg10")]
    public int Arg10;
    [JsonProperty("cast_time")]
    public int CastTime;
    [JsonProperty("counter_enable")]
    public int CounterEnable;
    [JsonProperty("name")]
    public string OptionAbilityName;
    [JsonProperty("status_ailments_factor")]
    public int StatusFactor;
    [JsonProperty("status_ailments_id")]
    public int StatusId;
    [JsonProperty("target_death")]
    public int TargetDeath;
    [JsonProperty("target_method")]
    public int TargetMethod;
    [JsonProperty("target_range")]
    public int TargetRange;
    [JsonProperty("target_segment")]
    public int TargetSegment;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;
  }
}
