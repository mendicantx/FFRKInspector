﻿// Decompiled with JetBrains decompiler
// Type: FFRKInspector.DataCache.Worlds.Data
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;

namespace FFRKInspector.DataCache.Worlds
{
  public class Data
  {
    public uint Series;
    public SchemaConstants.WorldType Type;
    public string Name;

    public override string ToString() => this.Name;
  }
}
