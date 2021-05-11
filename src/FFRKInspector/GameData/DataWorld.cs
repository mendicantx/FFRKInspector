// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataWorld
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData.Converters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataWorld
  {
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("series_id")]
    public uint SeriesId;
    [JsonProperty("Id")]
    public uint Id;
    [JsonProperty("type")]
    public ushort Type;
    [JsonConverter(typeof (EpochToDateTime))]
    [JsonProperty("kept_out_at")]
    public DateTime KeptOutAt;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;
  }
}
