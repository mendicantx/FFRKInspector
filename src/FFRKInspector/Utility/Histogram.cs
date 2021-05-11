// Decompiled with JetBrains decompiler
// Type: FFRKInspector.Utility.Histogram
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System;
using System.Collections.Generic;
using System.Linq;

namespace FFRKInspector.Utility
{
  internal class Histogram
  {
    private List<uint> mHistogram;
    private int mMinBuckets;

    public int MinBucketCount
    {
      get => this.mMinBuckets;
      set
      {
        if (this.mMinBuckets == value)
          return;
        this.mMinBuckets = value;
        if (this.mHistogram.Count >= this.mMinBuckets)
          return;
        this.AddBuckets(this.mMinBuckets - this.mHistogram.Count);
      }
    }

    public int BucketCount
    {
      get => this.mHistogram.Count;
      set
      {
        if (this.mHistogram.Count == value)
          return;
        if (this.mHistogram.Count > value)
          this.RemoveBucketsStartingAt(value);
        else
          this.AddBuckets(value - this.mHistogram.Count);
      }
    }

    public uint this[int bucket]
    {
      get => this.mHistogram[bucket];
      set
      {
        this.EnsureBucket(bucket);
        this.mHistogram[bucket] = value;
      }
    }

    public Histogram(int MinBuckets)
    {
      this.mMinBuckets = MinBuckets;
      this.mHistogram = Enumerable.Repeat<uint>(0U, this.mMinBuckets).ToList<uint>();
    }

    public void TrimBack()
    {
      int lastIndex = this.mHistogram.FindLastIndex((Predicate<uint>) (x => x > 0U));
      if (lastIndex <= 0)
        return;
      this.RemoveBucketsStartingAt(lastIndex);
    }

    private void EnsureBucket(int bucket)
    {
      if (this.mHistogram.Count > bucket)
        return;
      this.AddBuckets(bucket - this.mHistogram.Count + 1);
    }

    private void AddBuckets(int count) => this.mHistogram.AddRange(Enumerable.Repeat<uint>(0U, count));

    private void RemoveBucketsStartingAt(int index) => this.mHistogram.RemoveRange(index, this.mHistogram.Count - index);
  }
}
