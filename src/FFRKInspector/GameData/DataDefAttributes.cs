// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataDefAttributes
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
    public class DataDefAttributes
  {
    [JsonProperty("attribute_id")]
    public uint Id;
    [JsonProperty("factor")]
    public uint Factor;
    [JsonExtensionData]
    public Dictionary<string, JToken> UnknownValues;
  }
}
