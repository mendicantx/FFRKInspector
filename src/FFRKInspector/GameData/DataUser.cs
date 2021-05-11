// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataUser
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataUser
  {
    [JsonProperty("dungeon_id")]
    public uint DungeonId;
    [JsonProperty("user_id")]
    public uint UserId;
    [JsonProperty("stamina")]
    public uint Stamina;
    [JsonProperty("gil")]
    public uint Gil;
    [JsonProperty("stamina_recovery_time_remaining")]
    public uint StaminaRecoveryTimeRemaining;
    [JsonProperty("max_stamina")]
    public uint MaxStamina;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;
  }
}
