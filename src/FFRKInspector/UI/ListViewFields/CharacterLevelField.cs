// Decompiled with JetBrains decompiler
// Type: FFRKInspector.UI.ListViewFields.CharacterLevelField
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using FFRKInspector.GameData;
using System;

namespace FFRKInspector.UI.ListViewFields
{
  internal class CharacterLevelField : TrivialField<DataBuddyInformation, byte>
  {
    public CharacterLevelField(string DisplayName)
      : base(DisplayName, (Converter<DataBuddyInformation, byte>) (x => x.Level), FieldWidthStyle.AutoSize, 0)
    {
    }
  }
}
