// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataEnemyAbilityOptions
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
    public class DataEnemyAbilityOptions
  {
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
    [JsonProperty("arg11")]
    public int Arg11;
    [JsonProperty("arg12")]
    public int Arg12;
    [JsonProperty("arg13")]
    public int Arg13;
    [JsonProperty("arg14")]
    public int Arg14;
    [JsonProperty("arg15")]
    public int Arg15;
    [JsonProperty("arg16")]
    public int Arg16;
    [JsonProperty("arg17")]
    public int Arg17;
    [JsonProperty("arg18")]
    public int Arg18;
    [JsonProperty("arg19")]
    public int Arg19;
    [JsonProperty("arg20")]
    public int Arg20;
    [JsonProperty("arg21")]
    public int Arg21;
    [JsonProperty("arg22")]
    public int Arg22;
    [JsonProperty("arg23")]
    public int Arg23;
    [JsonProperty("arg24")]
    public int Arg24;
    [JsonProperty("arg25")]
    public int Arg25;
    [JsonProperty("arg26")]
    public int Arg26;
    [JsonProperty("arg27")]
    public int Arg27;
    [JsonProperty("arg28")]
    public int Arg28;
    [JsonProperty("arg29")]
    public int Arg29;
    [JsonProperty("arg30")]
    public int Arg30;
    [JsonProperty("cast_time")]
    public int CastTime;
    [JsonProperty("counter_enable")]
    public int CounterEnable;
    [JsonProperty("name")]
    public string myName;
    [JsonProperty("status_ailments_factor")]
    public int StatusAilmentsFactor;
    [JsonProperty("status_ailments_id")]
    public int StatusAilmentsId;
    [JsonProperty("target_death")]
    public int TargetDeath;
    [JsonProperty("target_method")]
    public int TargetMethod;
    [JsonProperty("target_range")]
    public int TargetRange;
    [JsonProperty("target_segment")]
    public int TargetSegment;
    [JsonProperty("min_damage_threshold_type")]
    public int MinDamageThreshold;
    [JsonProperty("max_damage_threshold_type")]
    public int MaxDamageThreshold;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;

    public string Name => this.myName == null || this.myName.Equals("") ? "Attack" : this.myName;
  }
}
