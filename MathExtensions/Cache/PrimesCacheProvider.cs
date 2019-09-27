using Microsoft.Extensions.Caching.Memory;

namespace MathExtensions.Cache
{
    /// <summary>
    /// Cache provider for prime numbers
    /// </summary>
    public class PrimesCacheProvider : IEnumerableCacheProvider<int>
    {
        private readonly string _cacheName;
        public PrimesCacheProvider(string cacheName)
        {
            _cacheName = cacheName;
        }

        public IEnumerableCache<int> Create()
        {
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            return new EnumerableCache<int>(memoryCache, _cacheName);
        }
    }
}
