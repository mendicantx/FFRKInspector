// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.EquipStats
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

namespace FFRKInspector.GameData
{
  public class EquipStats
  {
    public short? Atk;
    public short? Mag;
    public short? Acc;
    public short? Def;
    public short? Res;
    public short? Eva;
    public short? Mnd;

    public bool IsValid
    {
      get
      {
        short? nullable = this.Atk;
        if ((nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?()).HasValue)
          return true;
        nullable = this.Mag;
        if ((nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?()).HasValue)
          return true;
        nullable = this.Acc;
        if ((nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?()).HasValue)
          return true;
        nullable = this.Def;
        if ((nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?()).HasValue)
          return true;
        nullable = this.Res;
        if ((nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?()).HasValue)
          return true;
        nullable = this.Eva;
        if ((nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?()).HasValue)
          return true;
        nullable = this.Mnd;
        return (nullable.HasValue ? new int?((int) nullable.GetValueOrDefault()) : new int?()).HasValue;
      }
    }
  }
}
