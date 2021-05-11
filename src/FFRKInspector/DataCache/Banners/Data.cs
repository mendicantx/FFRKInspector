// Decompiled with JetBrains decompiler
// Type: FFRKInspector.DataCache.Banners.Data
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;

namespace FFRKInspector.DataCache.Banners
{
  public class Data
  {
    public uint BannerId;
    public bool isJP;
    public string BannerName;
    public ulong TimeOpened;
    public ulong TimeClosed;
    public string LineupImgPath;
    public Decimal? Rate1;
    public Decimal? Rate2;
    public Decimal? Rate3;
    public Decimal? Rate4;
    public Decimal? Rate5;
    public Decimal? Rate6;
    public Decimal? Rate7;
    public Decimal? OffBannerRate5;
    public Decimal? OffBannerRate6;
    public Decimal? OffBannerRate7;
    public Decimal? BoostRateAssured;
    public byte AssuredRarity;
    public bool EqualProbInRarity;

    public override string ToString() => this.BannerName;
  }
}
