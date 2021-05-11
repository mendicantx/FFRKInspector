// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataEnemyParamCounters
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
    public class DataEnemyParamCounters
  {
    [JsonProperty("ability_id")]
    public uint AbilityId;
    [JsonProperty("condition_type")]
    public uint CondType;
    [JsonProperty("condition_value")]
    public uint CondValue;
    [JsonProperty("rate")]
    public uint Rate;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;
  }
}
