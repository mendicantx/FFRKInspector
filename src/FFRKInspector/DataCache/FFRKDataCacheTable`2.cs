// Decompiled with JetBrains decompiler
// Type: FFRKInspector.DataCache.FFRKDataCacheTable`2
// Assembly: FFRKInspector, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C398C82F-AB84-4530-AFD0-F7F1D1457E23
// Assembly location: E:\workspaces\ffrki\FFRKInspector.dll

using System.Collections;
using System.Collections.Generic;

namespace FFRKInspector.DataCache
{
  internal class FFRKDataCacheTable<Key, Value> : IEnumerable<KeyValuePair<Key, Value>>, IEnumerable
  {
    private Dictionary<Key, Value> mCache = (Dictionary<Key, Value>) null;

    public IEnumerable<Key> Keys => (IEnumerable<Key>) this.mCache.Keys;

    public IEnumerable<Value> Values => (IEnumerable<Value>) this.mCache.Values;

    public FFRKDataCacheTable() => this.mCache = new Dictionary<Key, Value>();

    public bool Contains(Key k) => this.mCache.ContainsKey(k);

    public void Update(Key k, Value v)
    {
      if (this.mCache.ContainsKey(k))
        return;
      this.mCache.Add(k, v);
    }

    public void Clear() => this.mCache.Clear();

    public bool TryGetValue(Key key, out Value value) => this.mCache.TryGetValue(key, out value);

    public IEnumerator<KeyValuePair<Key, Value>> GetEnumerator() => (IEnumerator<KeyValuePair<Key, Value>>) this.mCache.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => (IEnumerator) this.mCache.GetEnumerator();
  }
}
