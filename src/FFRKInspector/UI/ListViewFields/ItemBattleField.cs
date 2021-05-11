// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.ListViewFields.ItemBattleField
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using System;

namespace FFRKInspector.UI.ListViewFields
{
  internal class ItemBattleField : TrivialField<BasicItemDropStats, string>
  {
    public ItemBattleField(string DisplayName, FieldWidthStyle WidthStyle, int Width)
      : base(DisplayName, (Converter<BasicItemDropStats, string>) (x => x.BattleName), WidthStyle, Width)
    {
    }
  }
}
