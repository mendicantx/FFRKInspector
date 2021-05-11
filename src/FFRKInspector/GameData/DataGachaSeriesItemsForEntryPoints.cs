// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.DataGachaSeriesItemsForEntryPoints
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System.Collections.Generic;

namespace FFRKInspector.GameData
{
  internal class DataGachaSeriesItemsForEntryPoints
  {
    public Dictionary<uint, DataGachaSeriesItemsForEntryPoints.ItemsForEntryPoint> Gachas;

    public DataGachaSeriesItemsForEntryPoints() => this.Gachas = new Dictionary<uint, DataGachaSeriesItemsForEntryPoints.ItemsForEntryPoint>();

    public class ItemsForEntryPoint
    {
      public DataGachaSeriesEntryPoint EntryPoint;
      public DataGachaSeriesItemDetails ItemDetails;
    }
  }
}
