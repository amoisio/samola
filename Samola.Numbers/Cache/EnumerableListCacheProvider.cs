using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;

namespace Samola.Numbers.Cache
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
    public class EnumerableListCacheProvider<TEnumerable> : IEnumerableCacheProvider<TEnumerable>
    {
        private readonly string _cachePrefix;
        private readonly int _capacity;

        public EnumerableListCacheProvider(int capacity)
            : this (Guid.NewGuid().ToString(), capacity)
        {

        }

        internal EnumerableListCacheProvider(string cachePrefix, int capacity)
        {
            _cachePrefix = cachePrefix;
            _capacity = capacity;
        }

        public IEnumerableCache<TEnumerable> CreateOrGet()
        {
            return new EnumerableListCache<TEnumerable>(_cachePrefix, _capacity);
        }
    }
}