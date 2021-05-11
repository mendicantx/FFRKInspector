// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.Party.DataPartySlotToBuddyId
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;

namespace FFRKInspector.GameData.Party
{
  internal class DataPartySlotToBuddyId
  {
    [JsonProperty("1")]
    public uint First;
    [JsonProperty("2")]
    public uint Second;
    [JsonProperty("3")]
    public uint Third;
    [JsonProperty("4")]
    public uint Fourth;
    [JsonProperty("5")]
    public uint Fifth;
  }
}
