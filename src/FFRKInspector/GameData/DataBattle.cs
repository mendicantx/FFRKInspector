// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataBattle
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataBattle
  {
    [JsonProperty("round_num")]
    public uint RoundNumber;
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("has_boss", ItemConverterType = typeof (IntToBool))]
    public bool HasBoss;
    [JsonProperty("stamina")]
    public ushort Stamina;
    [JsonProperty("is_unlocked")]
    public bool IsUnlocked;
    [JsonProperty("dungeon_id")]
    public uint DungeonId;
    [JsonProperty("id")]
    public uint Id;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;
  }
}
