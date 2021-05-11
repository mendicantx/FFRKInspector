// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.AppInit.DataUser
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData.Converters;
using Newtonsoft.Json;
using System;

namespace FFRKInspector.GameData.AppInit
{
  internal class DataUser
  {
    [JsonProperty("last_logined_at")]
    public ulong LastLoginTime;
    [JsonProperty("start_time_of_today")]
    [JsonConverter(typeof (EpochToDateTime))]
    public DateTime StartTimeOfToday;
  }
}
