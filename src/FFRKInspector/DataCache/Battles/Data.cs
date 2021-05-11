// Decompiled with JetBrains decompiler
// Type: FFRKInspector.DataCache.Battles.Data
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

namespace FFRKInspector.DataCache.Battles
{
  public class Data
  {
    public uint DungeonId;
    public string Name;
    public ushort Stamina;
    public uint Samples;
    public uint HistoSamples;
    public bool Repeatable;
    public ushort StaminaToReach;

    public override string ToString() => this.Name;
  }
}
