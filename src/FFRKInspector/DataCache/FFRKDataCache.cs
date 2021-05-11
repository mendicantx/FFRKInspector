// Decompiled with JetBrains decompiler
// Type: FFRKInspector.DataCache.FFRKDataCache
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

namespace FFRKInspector.DataCache
{
  internal class FFRKDataCache
  {
    private FFRKDataCacheTable<FFRKInspector.DataCache.Dungeons.Key, FFRKInspector.DataCache.Dungeons.Data> mDungeons;
    private FFRKDataCacheTable<FFRKInspector.DataCache.Worlds.Key, FFRKInspector.DataCache.Worlds.Data> mWorlds;
    private FFRKDataCacheTable<FFRKInspector.DataCache.Battles.Key, FFRKInspector.DataCache.Battles.Data> mBattles;
    private FFRKDataCacheTable<FFRKInspector.DataCache.Items.Key, FFRKInspector.DataCache.Items.Data> mItems;
    private FFRKDataCacheTable<FFRKInspector.DataCache.Banners.Key, FFRKInspector.DataCache.Banners.Data> mBanners;
    private object mSyncRoot;

    public FFRKDataCacheTable<FFRKInspector.DataCache.Dungeons.Key, FFRKInspector.DataCache.Dungeons.Data> Dungeons
    {
      get => this.mDungeons;
      set => this.mDungeons = value;
    }

    public FFRKDataCacheTable<FFRKInspector.DataCache.Worlds.Key, FFRKInspector.DataCache.Worlds.Data> Worlds
    {
      get => this.mWorlds;
      set => this.mWorlds = value;
    }

    public FFRKDataCacheTable<FFRKInspector.DataCache.Battles.Key, FFRKInspector.DataCache.Battles.Data> Battles
    {
      get => this.mBattles;
      set => this.mBattles = value;
    }

    public FFRKDataCacheTable<FFRKInspector.DataCache.Items.Key, FFRKInspector.DataCache.Items.Data> Items
    {
      get => this.mItems;
      set => this.mItems = value;
    }

    public FFRKDataCacheTable<FFRKInspector.DataCache.Banners.Key, FFRKInspector.DataCache.Banners.Data> Banners
    {
      get => this.mBanners;
      set => this.mBanners = value;
    }

    public object SyncRoot => this.mSyncRoot;

    public FFRKDataCache()
    {
      this.mDungeons = new FFRKDataCacheTable<FFRKInspector.DataCache.Dungeons.Key, FFRKInspector.DataCache.Dungeons.Data>();
      this.mWorlds = new FFRKDataCacheTable<FFRKInspector.DataCache.Worlds.Key, FFRKInspector.DataCache.Worlds.Data>();
      this.mBattles = new FFRKDataCacheTable<FFRKInspector.DataCache.Battles.Key, FFRKInspector.DataCache.Battles.Data>();
      this.mItems = new FFRKDataCacheTable<FFRKInspector.DataCache.Items.Key, FFRKInspector.DataCache.Items.Data>();
      this.mBanners = new FFRKDataCacheTable<FFRKInspector.DataCache.Banners.Key, FFRKInspector.DataCache.Banners.Data>();
      this.mSyncRoot = new object();
    }

    public delegate void CacheRefreshedDelegate();
  }
}
