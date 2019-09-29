using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace MathExtensions.Cache
{
    /// <summary>
    /// Represents an enumerable cache.
    /// </summary>
    public interface IEnumerableCache<TEnumerable>
    {
        TEnumerable[] Items { get; }
        int Count { get; }
        TEnumerable this[int index] { get; }
        void Add(TEnumerable item);
    }

    public class EnumerableCache<TEnumerable> : IEnumerableCache<TEnumerable>
    {
        private readonly IMemoryCache _memoryCache;
        private readonly string _cacheName;

        /// <summary>
        /// All writes to the cached array of items are synchronized with this.
        /// </summary>
        private static readonly object _writeLock = new object();

        public EnumerableCache(IMemoryCache memoryCache, string cacheName)
        {
            _memoryCache = memoryCache;
            _cacheName = cacheName;
        }

        public TEnumerable this[int index] => CachedItems[index];

        public int Count => CachedItems.Count;

        public void Add(TEnumerable item)
        {
            lock (_writeLock)
            {
                
                CachedItems.Add(item);
            }
        }

        public TEnumerable[] Items => CachedItems.ToArray();

        private List<TEnumerable> CachedItems => _memoryCache.Get<List<TEnumerable>>(_cacheName);
    }
}