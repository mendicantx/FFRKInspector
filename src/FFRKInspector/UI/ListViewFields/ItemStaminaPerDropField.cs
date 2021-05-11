// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.ListViewFields.ItemStaminaPerDropField
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;

namespace FFRKInspector.UI.ListViewFields
{
  internal class ItemStaminaPerDropField : ListViewField<BasicItemDropStats>
  {
    private bool mUseStaminaToReachForNonRepetable;

    public bool UseStaminaToReachForNonRepeatable
    {
      get => this.mUseStaminaToReachForNonRepetable;
      set => this.mUseStaminaToReachForNonRepetable = value;
    }

    public ItemStaminaPerDropField(string DisplayName, bool useStaminaToReachForNonRep)
      : base(DisplayName)
    {
      this.mUseStaminaToReachForNonRepetable = useStaminaToReachForNonRep;
    }

    protected override int CompareValues(BasicItemDropStats x, BasicItemDropStats y) => this.GetValue(x).CompareTo(this.GetValue(y));

    protected override string FormatValue(BasicItemDropStats value) => this.GetValue(value).ToString("F");

    public float GetValue(BasicItemDropStats stats) => !this.mUseStaminaToReachForNonRepetable ? stats.StaminaPerDrop : (float) stats.StaminaToReachBattle + stats.StaminaPerDrop;
  }
}
