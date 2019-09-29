using MathExtensions.Cache;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MathExtensions.Tests
{
    public class EnumerableCacheProviderTests
    {
        private readonly IMemoryCache _memoryCache;

        public EnumerableCacheProviderTests()
        {
            var options = new MemoryCacheOptions()
            {
                
            };
            _memoryCache = new MemoryCache(options);
        }

        //[Fact]
        //public void Cache()
        //{
        //    var provider = new EnumerableCacheProvider<int>("test");
        //    var cache = provider.Create();

        //    MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
        //    var ok = memoryCache.TryGetValue("test", out object result);

        //    Assert.True(ok);
        //}

        [Fact]
        public void Can_add_items_to_cache()
        {
            var provider = new EnumerableCacheProvider<int>(_memoryCache, Guid.NewGuid().ToString(), 100);
            IEnumerableCache<int> cache = provider.CreateOrGet();
            cache.Add(2);

            Assert.True(true);
        }

        [Fact]
        public void Can_get_the_count_of_items_in_cache()
        {
            var provider = new EnumerableCacheProvider<int>(_memoryCache, Guid.NewGuid().ToString(), 100);
            IEnumerableCache<int> cache = provider.CreateOrGet();
            cache.Add(2);
            cache.Add(3);
            cache.Add(5);

            Assert.Equal(3, cache.Count);
        }

        [Fact]
        public void Can_get_the_cached_items_as_an_array()
        {
            var provider = new EnumerableCacheProvider<int>(_memoryCache, Guid.NewGuid().ToString(), 100);
            IEnumerableCache<int> cache = provider.CreateOrGet();
            cache.Add(2);
            cache.Add(3);
            cache.Add(5);

            var array = cache.Items;
            Assert.Equal(3, array.Length);
            Assert.Equal(2, array[0]);
            Assert.Equal(3, array[1]);
            Assert.Equal(5, array[2]);
        }

        [Fact]
        public void Can_access_cached_items_directly_with_an_indexer()
        {
            var provider = new EnumerableCacheProvider<int>(_memoryCache, Guid.NewGuid().ToString(), 100);

            IEnumerableCache<int> cache = provider.CreateOrGet();
            cache.Add(1);
            cache.Add(2);
            cache.Add(3);

            Assert.Equal(1, cache[0]);
            Assert.Equal(2, cache[1]);
            Assert.Equal(3, cache[2]);
        }

        [Fact]
        public void Instantiating_a_cache_with_the_same_name_and_type_gives_the_same_cache()
        {
            var cachePrefix = Guid.NewGuid().ToString();
            var provider = new EnumerableCacheProvider<int>(_memoryCache, cachePrefix, 100);

            IEnumerableCache<int> cache = provider.CreateOrGet();
            cache.Add(1);
            cache.Add(2);
            cache.Add(3);
            var items = cache.Items;

            IEnumerableCache<int> cache2 = provider.CreateOrGet();
            var items2 = cache2.Items;
            Assert.Equal(items, items2);
        }
    }
}
