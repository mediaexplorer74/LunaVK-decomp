// Decompiled with JetBrains decompiler
// Type: App1uwp.Library.LRUCache`2
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

using System;
using System.Collections;
using System.Collections.Generic;

#nullable disable
namespace App1uwp.Library
{
  public class LRUCache<K, V> : IEnumerable<LRUCacheItem<K, V>>, IEnumerable
  {
    private Dictionary<K, LinkedListNode<LRUCacheItem<K, V>>> cacheMap = new Dictionary<K, LinkedListNode<LRUCacheItem<K, V>>>();
    private LinkedList<LRUCacheItem<K, V>> lruList = new LinkedList<LRUCacheItem<K, V>>();
    private int _maxCapacity;
    private Func<V, int> _getValueCapacityFunc;
    private int _currentCapacity;

    public Action<LRUCacheItem<K, V>> OnRemovedCallback { get; set; }

    public LRUCache(int capacity, Func<V, int> getValueCapacity = null)
    {
      this._maxCapacity = capacity;
      this._getValueCapacityFunc = getValueCapacity;
    }

    public V Get(K key)
    {
      LinkedListNode<LRUCacheItem<K, V>> node;
      if (!this.cacheMap.TryGetValue(key, out node))
        return default (V);
      V v = node.Value.value;
      this.lruList.Remove(node);
      this.lruList.AddLast(node);
      return v;
    }

    public void Add(K key, V val, bool allowRemove = true)
    {
      if (this.cacheMap.ContainsKey(key))
        return;
      if (this._getValueCapacityFunc == null)
      {
        if (this.cacheMap.Count >= this._maxCapacity & allowRemove)
          this.RemoveFirst();
      }
      else if (allowRemove)
      {
        while (this._currentCapacity >= this._maxCapacity && this.lruList.Count > 0)
          this.RemoveFirst();
      }
      LinkedListNode<LRUCacheItem<K, V>> node = new LinkedListNode<LRUCacheItem<K, V>>(new LRUCacheItem<K, V>(key, val));
      this.lruList.AddLast(node);
      this.cacheMap.Add(key, node);
      if (this._getValueCapacityFunc == null)
        return;
      this._currentCapacity += this._getValueCapacityFunc(val);
    }

    private void RemoveFirst()
    {
      LinkedListNode<LRUCacheItem<K, V>> first = this.lruList.First;
      this.lruList.RemoveFirst();
      if (this._getValueCapacityFunc != null)
        this._currentCapacity -= this._getValueCapacityFunc(first.Value.value);
      this.cacheMap.Remove(first.Value.key);
      if (this.OnRemovedCallback == null)
        return;
      this.OnRemovedCallback(first.Value);
    }

    public IEnumerator<LRUCacheItem<K, V>> GetEnumerator()
    {
      return (IEnumerator<LRUCacheItem<K, V>>) this.lruList.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable) this.lruList).GetEnumerator();
  }
}
