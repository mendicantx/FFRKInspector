// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataDungeonSession
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataDungeonSession
  {
    [JsonProperty("world_id")]
    public uint WorldId;
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("series_id")]
    public uint SeriesId;
    [JsonProperty("battle_id")]
    public uint BattleId;
    [JsonProperty("dungeon_id")]
    public uint DungeonId;
    [JsonProperty("challenge_level")]
    public uint Difficulty;
    [JsonProperty("type")]
    public uint Type;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;
  }
}
