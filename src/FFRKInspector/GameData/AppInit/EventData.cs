// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.AppInit.EventData
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;

namespace FFRKInspector.GameData.AppInit
{
  internal class EventData
  {
    [JsonProperty("world_id")]
    public uint WorldId;
    [JsonProperty("type")]
    public SchemaConstants.EventType type;
  }
}
