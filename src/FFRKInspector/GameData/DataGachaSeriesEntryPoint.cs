// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataGachaSeriesEntryPoint
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;

namespace FFRKInspector.GameData
{
  internal class DataGachaSeriesEntryPoint
  {
    [JsonProperty("entry_point_id")]
    public uint EntryPointId;
    [JsonProperty("pay_cost")]
    public uint PayCost;
    [JsonProperty("pay_id")]
    public DataGachaSeriesEntryPoint.PayId CurrencyType;

    public enum PayId
    {
      Gems = 0,
      Mythril = 91000000, // 0x056C8CC0
    }
  }
}
