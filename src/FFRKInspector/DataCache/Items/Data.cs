// Decompiled with JetBrains decompiler
// Type: FFRKInspector.DataCache.Items.Data
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;

namespace FFRKInspector.DataCache.Items
{
  public class Data
  {
    public string Name;
    public byte Rarity;
    public byte Type;
    public byte Subtype;
    public uint? Series;
    public EquipStats BaseStats;
    public EquipStats MaxStats;

    public bool AreStatsValid => this.BaseStats != null && this.MaxStats != null && (this.BaseStats.IsValid && this.MaxStats.IsValid);

    public override string ToString() => this.Name;
  }
}
