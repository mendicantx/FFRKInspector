// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.ListViewFields.ItemSynergyField
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;

namespace FFRKInspector.UI.ListViewFields
{
  internal class ItemSynergyField : ListViewField<BasicItemDropStats>
  {
    public ItemSynergyField(string DisplayName)
      : base(DisplayName)
    {
    }

    protected override int CompareValues(BasicItemDropStats x, BasicItemDropStats y)
    {
      if (x.Synergy == y.Synergy)
        return 0;
      if (x.Synergy == null)
        return -1;
      return y.Synergy == null ? 1 : x.Synergy.Realm.CompareTo((object) y.Synergy.Realm);
    }

    protected override string FormatValue(BasicItemDropStats value) => value.Synergy == null ? string.Empty : value.Synergy.Text;
  }
}
