using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookUpTable<K, V>
{
    Func<K, V> _action;
    Dictionary<K, V> _cache = new Dictionary<K, V>();
    public LookUpTable(Func<K, V> action)
    {
        _action = action;
    }
    public V GetValue(K key)
    {
        if (!_cache.ContainsKey(key))
        {
            _cache[key] = _action(key);
        }
        return _cache[key];
    }
}
