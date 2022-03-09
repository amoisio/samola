using Microsoft.Extensions.Caching.Memory;

namespace Samola.Numbers.Cache
{
    internal sealed class SingletonMemoryCache : IMemoryCache
    {
        private static readonly SingletonMemoryCache _instance = new SingletonMemoryCache();

        private readonly MemoryCache _memoryCache;

        // Explicit static constructor to tell C# compiler
        // not to mark type as beforefieldinit
        static SingletonMemoryCache() { }

        private SingletonMemoryCache()
        {
            _memoryCache = new MemoryCache(new MemoryCacheOptions());
        }

        public static SingletonMemoryCache Instance
        {
            get
            {
                return _instance;
            }
        }

        public ICacheEntry CreateEntry(object key)
        {
            return _memoryCache.CreateEntry(key);
        }

        public void Dispose()
        {
            _memoryCache.Dispose();
        }

        public void Remove(object key)
        {
            _memoryCache.Remove(key);
        }

        public bool TryGetValue(object key, out object value)
        {
            return _memoryCache.TryGetValue(key, out value);
        }
    }
}
