using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions.Cache
{
    /// <summary>
    /// Represents a factory for instantiating enumerable cache objects
    /// </summary>
    public interface IEnumerableCacheProvider<T>
    {
        IEnumerableCache<T> Create();
    }

    /// <summary>
    /// Cache provider for enumerable values
    /// </summary>
    public class EnumerableCacheProvider<T> : IEnumerableCacheProvider<T>
    {
        private readonly string _cacheName;
        public EnumerableCacheProvider(string cacheName)
        {
            _cacheName = cacheName;
        }

        public IEnumerableCache<T> Create()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            return new EnumerableCache<T>(memoryCache, _cacheName);
        }
    }
}
