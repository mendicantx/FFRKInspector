// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataEnemyConstraints
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataEnemyConstraints
  {
    [JsonProperty("ability_tag")]
    public string AbilityTag;
    [JsonProperty("constraint_type")]
    public uint ConstraintType;
    [JsonProperty("constraint_value")]
    public string ConstraintValue;
    [JsonProperty("enemy_status_id")]
    public uint EnemyStatusId;
    [JsonProperty("options")]
    public string Options;
    [JsonProperty("priority")]
    public uint Priority;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;
  }
}
