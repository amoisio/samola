using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace MathExtensions.Cache
{
    /// <summary>
    /// Represents a factory for instantiating enumerable cache objects
    /// </summary>
    public interface IEnumerableCacheProvider<TEnumerable>
    {
        /// <summary>
        /// Creates or gets a handle to a memory cache.
        /// </summary>
        IEnumerableCache<TEnumerable> CreateOrGet();
    }

    /// <summary>
    /// Cache provider for a list of values. Time complexities are that of a List.
    /// </summary>
    public class EnumerableCacheProvider<TEnumerable> : IEnumerableCacheProvider<TEnumerable>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string _cachePrefix;
        private readonly int _capacity;
        public EnumerableCacheProvider(IMemoryCache memoryCache, string cachePrefix, int capacity)
        {
            _memoryCache = memoryCache;
            _cachePrefix = cachePrefix;
            _capacity = capacity;
        }

        public IEnumerableCache<TEnumerable> CreateOrGet()
        {
            string cacheName = GetCacheKey(_cachePrefix);

            if (!_memoryCache.TryGetValue(cacheName, out object value))
            {
                using (var cacheEntry = _memoryCache.CreateEntry(cacheName))
                {
                    cacheEntry.Value = new List<TEnumerable>(_capacity);
                }
            }

            return new EnumerableCache<TEnumerable>(_memoryCache, cacheName);
        }

        public string GetCacheKey(string cacheName)
        {
            return $"{cacheName}.{typeof(TEnumerable).Name}.list";
        }
    }
}
