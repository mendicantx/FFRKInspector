﻿// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataEnemyAbility
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
    public class DataEnemyAbility
  {
    [JsonProperty("ability_id")]
    public uint AbilityId;
    [JsonProperty("action_id")]
    public uint ActionId;
    [JsonProperty("exercise_type")]
    public uint ExerciseType;
    [JsonProperty("options")]
    public DataEnemyAbilityOptions Options;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;

    public string name
    {
      get
      {
        if (this.Options.myName != null && !this.Options.myName.Equals(""))
          return this.Options.myName;
        return this.ActionId == 52U || this.ActionId == 81U ? "Wait" : "Attack";
      }
    }
  }
}
