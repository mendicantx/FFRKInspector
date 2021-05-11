// Decompiled with JetBrains decompiler
// Type: FFRKInspector.DataCache.Dungeons.Data
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;

namespace FFRKInspector.DataCache.Dungeons
{
  public class Data
  {
    public uint WorldId;
    public uint Series;
    public string Name;
    public SchemaConstants.DungeonType Type;
    public ushort Difficulty;

    public override string ToString() => this.Name;
  }
}
