// Decompiled with JetBrains decompiler
// Type: FFRKInspector.GameData.BasicFeaturedItemInfo
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;

namespace FFRKInspector.GameData
{
  internal class BasicFeaturedItemInfo
  {
    public uint BannerID;
    public bool IsJP;
    public uint ItemID;
    public int DisplayOrder;
    public string ItemImagePath;
    public uint CharacterID;
    public bool HasSB;
    public bool HasCharacterSB;
    public uint LMID;
    public int SBCategoryID;
    public Decimal? Rate;
    public string SBImagePath;
    public string Description;

    public bool RateRecorded => this.Rate.HasValue;
  }
}
