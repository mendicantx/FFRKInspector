// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataDungeonPrizeItem
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataDungeonPrizeItem
  {
    [JsonProperty("id")]
    public uint Id;
    [JsonProperty("is_got_grade_bonus_prize")]
    public int IsGradeBonus;
    [JsonProperty("name")]
    public string Name;
    [JsonProperty("num")]
    public uint Quantity;
    [JsonProperty("type_name")]
    public string TypeName;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;
  }
}
