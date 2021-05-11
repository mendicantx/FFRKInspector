// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataAbility
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
    public class DataAbility
  {
    [JsonProperty("ability_id")]
    public int AbilityId;
    [JsonProperty("action_id")]
    public int ActionId;
    [JsonProperty("category_id")]
    public int CategoryId;
    [JsonProperty("exercise_type")]
    public int ExerciseType;
    [JsonProperty("options")]
    public DataAbilityOption Options;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;
  }
}
