// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.ListViewFields.ItemDropsPerRunField
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;

namespace FFRKInspector.UI.ListViewFields
{
  internal class ItemDropsPerRunField : ListViewField<BasicItemDropStats>
  {
    public ItemDropsPerRunField(string DisplayName)
      : base(DisplayName)
    {
    }

    protected override int CompareValues(BasicItemDropStats x, BasicItemDropStats y) => x.DropsPerRun.CompareTo(y.DropsPerRun);

    protected override string FormatValue(BasicItemDropStats value) => value.DropsPerRun.ToString("F");
  }
}
