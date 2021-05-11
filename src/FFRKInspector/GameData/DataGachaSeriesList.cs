// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataGachaSeriesList
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using Newtonsoft.Json;
using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataGachaSeriesList
  {
    [JsonProperty("series_list")]
    public List<DataGachaSeriesInfo> SeriesList;
  }
}
