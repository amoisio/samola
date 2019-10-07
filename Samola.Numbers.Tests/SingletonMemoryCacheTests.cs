using Samola.Numbers.Cache;
using Samola.Numbers.Enumerables;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class SingletonMemoryCacheTests
    {
        //[Fact]
        //public void SingletonMemoryCache_caches_into_same_cache_same_thread()
        //{
        //    var memoryCache = SingletonMemoryCache.Instance;

        //    using(var cacheEntry = memoryCache.CreateEntry("test"))
        //    {
        //        cacheEntry.Value = 1;
        //    }

        //    var memoryCache1 = SingletonMemoryCache.Instance;

        //    var ok = memoryCache1.TryGetValue("test", out object temp);

        //    Assert.True(ok);
        //    Assert.Equal(1, temp);
        //}

        //[Fact]
        //public void SingletonMemoryCache_caches_into_same_cache_separate_threads()
        //{

        //    Thread th1 = new Thread(() =>
        //    {
        //        var memoryCache = SingletonMemoryCache.Instance;

        //        using (var cacheEntry = memoryCache.CreateEntry("test1"))
        //        {
        //            cacheEntry.Value = 1;
        //        }
        //    });
        //    th1.Start();

        //    Thread th2 = new Thread(() =>
        //    {
        //        var memoryCache = SingletonMemoryCache.Instance;

        //        using (var cacheEntry = memoryCache.CreateEntry("test2"))
        //        {
        //            cacheEntry.Value = 2;
        //        }

                
        //    });

        //    th2.Start();

        //    Thread.Sleep(100);

        //    var m = SingletonMemoryCache.Instance;
        //    var ok1 = m.TryGetValue("test1", out object temp1);
        //    var ok2 = m.TryGetValue("test2", out object temp2);
        //    Assert.True(ok1);
        //    Assert.Equal(1, temp1);
        //    Assert.True(ok2);
        //    Assert.Equal(2, temp2);
        //}

        //[Fact]
        //public void Cache_actually_caches_generated_primes()
        //{
        //    int maxPrimes = 50;
        //    var memoryCache = new MemoryCache(new MemoryCacheOptions());
        //    var provider = new EnumerableCacheProvider<int>(memoryCache, "primes", 1000000);
        //    var primes1 = new PrimeNumbers(new CountLimit(maxPrimes), provider).ToArray();

        //    var primes2 = new PrimeNumbers(new CountLimit(maxPrimes), provider).ToArray();

        //    Assert.True(true);
        //}


    }
}
