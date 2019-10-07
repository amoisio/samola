Use the EnumerableCacheProvider for declaring the type and name of the cache. After this the provider can be used for getting a reference to the same cache on demand.

The EnumerableCache uses an implementation of IMemoryCache to persist a collection of objects into process memory. The EnumerableCache then exposes methods for directly manipulating the cached collection. 

Example 1. Creating a cache "primes" with an initial capacity of 100000.

	var memoryCache = new MemoryCache(new MemoryCacheOptions());
	string cachePrefix = "primes";
	int capacity = 100000;
    
	var provider = new EnumerableCacheProvider<int>(memoryCache, cachePrefix, 100);
	var cache = provider.CreateOrGet();
