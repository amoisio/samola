using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MathExtensions.Cache
{
    /// <summary>
    /// Represents an enumerable cache.
    /// </summary>
    public interface IEnumerableCache<TEnumerable>
    {
        TEnumerable[] Items { get; }
        int Count { get; }
        void Add(TEnumerable item);
    }

    public class EnumerableListCache<TEnumerable> : IEnumerableCache<TEnumerable>
    {
        private static SingletonMemoryCache _memoryCache = SingletonMemoryCache.Instance;
        private static SingletonCacheLock _memoryCacheLock = SingletonCacheLock.Instance;
        private readonly string _cacheKey;

        public EnumerableListCache(string cacheKeyPrefix, int capacity)
        {
            _cacheKey = GetCacheKey(cacheKeyPrefix);
            InitializeCache(_cacheKey, capacity);
        }

        private void InitializeCache(string cacheKey, int capacity)
        {
            lock (_memoryCacheLock)
            {
                if (!_memoryCache.TryGetValue(cacheKey, out object value))
                {
                    using (var cacheEntry = _memoryCache.CreateEntry(cacheKey))
                    {
                        cacheEntry.Value = new List<TEnumerable>(capacity);
                    }
                }
            }
        }

        private static string GetCacheKey(string cachePrefix)
        {
            return $"{cachePrefix}.{typeof(TEnumerable).Name}.list";
        }

        public int Count
        {
            get
            {
                lock (_memoryCacheLock)
                {
                    return CachedItems.Count;
                }
            }
        }

        /// <summary>
        /// Add item to cache
        /// </summary>
        /// <param name="item"></param>
        public void Add(TEnumerable item)
        {
            lock (_memoryCacheLock)
            {
                CachedItems.Add(item);
            }
        }

        /// <summary>
        /// Get cached items as an array
        /// </summary>
        public TEnumerable[] Items
        {
            get
            {
                lock (_memoryCacheLock)
                {
                    return CachedItems.ToArray();
                }
            }
        }

        private List<TEnumerable> CachedItems
        {
            get
            {
                return _memoryCache.Get<List<TEnumerable>>(_cacheKey);
            }
        }
    }
}