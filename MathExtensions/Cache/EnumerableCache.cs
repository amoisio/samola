using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace MathExtensions.Cache
{
    /// <summary>
    /// Represents an enumerable, read-only, cache
    /// </summary>
    public interface IEnumerableCache<T>
    {
        T[] ToArray();
        int Count { get; }
        T this[int index] { get; }
        void Add(T item);
    }

    /// <summary>
    /// Simple enumerable cache based on encapsulating a list.
    /// </summary>
    public class EnumerableCache<T> : IEnumerableCache<T>
    {
        public const int INIT_CACHE_SIZE = 10000;
        private readonly IMemoryCache _cache;
        private readonly string _cacheName;

        public EnumerableCache(IMemoryCache memoryCache, string cacheName)
        {
            _cache = memoryCache;
            _cacheName = cacheName;

            using (var cacheEntry = _cache.CreateEntry(_cacheName))
            {
                cacheEntry.Value = new List<T>(INIT_CACHE_SIZE);
            }
        }

        public T this[int index] => CachedItems[index];

        public int Count => CachedItems.Count;

        public void Add(T item)
        {
            CachedItems.Add(item);
        }

        public T[] ToArray()
        {
            return CachedItems.ToArray();
        }

        private List<T> CachedItems => _cache.Get<List<T>>(_cacheName);
    }
}
