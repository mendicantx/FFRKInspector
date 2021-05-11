// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.ListViewFields.ItemRarityField
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;

namespace FFRKInspector.UI.ListViewFields
{
  internal class ItemRarityField : ListViewField<BasicItemDropStats>
  {
    public ItemRarityField(string DisplayName)
      : base(DisplayName, FieldWidthStyle.Absolute, 65)
    {
    }

    protected override int CompareValues(BasicItemDropStats x, BasicItemDropStats y) => x.Rarity.CompareTo((object) y.Rarity);

    protected override string FormatValue(BasicItemDropStats value) => new string('★', (int) value.Rarity);

    protected override string AltFormatValue(BasicItemDropStats value) => ((int) value.Rarity).ToString();
  }
}
