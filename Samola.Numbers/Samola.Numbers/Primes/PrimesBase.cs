using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace Samola.Numbers.Primes
{
    public abstract class PrimesBase : IPrimes
    {
        public const int MAX_COUNT = 2000000;
        /// <summary>
        /// Maximum count of primes that the enumerable can return
        /// </summary>
        private readonly int _maxCount;

        /// <summary>
        /// Set to true to collect computed primes into a cache
        /// </summary>
        private readonly bool _useCache;

        // CACHE
        private static ConcurrentDictionary<int, int> _primeCache;
        protected ConcurrentDictionary<int, int> PrimeCache => _primeCache;
        // /CACHE

        public int LastYieldedPrime { get; private set; }

        static PrimesBase()
        {
            if (_primeCache == null)
                _primeCache = new ConcurrentDictionary<int, int>();
        }

        protected PrimesBase(int maxCount, bool useCache)
        {
            _maxCount = maxCount;
            _useCache = useCache;
        }

        public IEnumerator<int> GetEnumerator()
        {
            int tempCount = 0;
            int tempValue = 0;
            int[] cachedPrimes = null;

            // Yield cached primes
            if (_useCache)
            {
                cachedPrimes = _primeCache
                    .Keys
                    .OrderBy(e => e)
                    .ToArray();

                int len = Math.Min(cachedPrimes.Length, _maxCount);

                foreach (var cachedPrime in cachedPrimes)
                {
                    tempValue = cachedPrime;
                    yield return tempValue;
                    LastYieldedPrime = tempValue;
                }
                tempCount += len;
            }

            if (tempCount < _maxCount)
            {
                foreach (var newPrime in GetPrimes(cachedPrimes))
                {
                    // yield the calculated value
                    yield return newPrime;
                    LastYieldedPrime = newPrime;

                    // cache the calculated value
                    if (_useCache)
                        _primeCache.TryAdd(newPrime, newPrime);

                    if (++tempCount == _maxCount)
                        yield break;
                };
            }
        }

        protected abstract IEnumerable<int> GetPrimes(int[] previousPrimes);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public abstract class PrimesBase2 : IPrimes
    {
        public const int MAX_COUNT = 2000000;
        /// <summary>
        /// Maximum count of primes that the enumerable can return
        /// </summary>
        private readonly int _maxCount;

        /// <summary>
        /// Set to true to collect computed primes into a cache
        /// </summary>
        private readonly bool _useCache;

        // CACHE
        private static List<int> _primeCache;
        private static object _cacheLock;
        private const int INIT_CACHE_CAPACITY = 15000;
        protected List<int> PrimeCache => _primeCache;
        // /CACHE

        public int LastYieldedPrime { get; private set; }

        static PrimesBase2()
        {
            if (_primeCache == null)
                _primeCache = new List<int>(INIT_CACHE_CAPACITY);

            if (_cacheLock == null)
                _cacheLock = new object();
        }

        protected PrimesBase2(int maxCount, bool useCache)
        {
            _maxCount = maxCount;
            _useCache = useCache;
        }

        public IEnumerator<int> GetEnumerator()
        {
            if (_useCache)
                return GetCachedEnumerator();
            else
                return GetNonCachedEnumerator();
        }

        protected abstract IEnumerable<int> GetPrimes(int[] previousPrimes);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private IEnumerator<int> GetCachedEnumerator()
        {
            int yieldedCount = 0;
            int tempValue = 0;
            int tempIndex = 0;
            int maxIndexToYield = _primeCache.Count - 1;

            while (yieldedCount < _maxCount)
            {
                // Yield cached primes from [temp, max]
                for (int i = tempIndex; i <= maxIndexToYield; i++)
                {
                    tempValue = _primeCache[i];
                    yield return tempValue;
                    LastYieldedPrime = tempValue;

                    if (++yieldedCount >= _maxCount)
                        yield break;
                }

                lock (_cacheLock)
                {
                    int newMaxIndex = _primeCache.Count - 1;
                    if (newMaxIndex > maxIndexToYield)
                    {
                        // Cache contains new primes to yield - go back and yield them
                        tempIndex = maxIndexToYield + 1;
                        maxIndexToYield = newMaxIndex;
                    }
                    else
                    {
                        // All cached primes yielded - calculate new primes
                        var cachedPrimes = _primeCache.ToArray();
                        foreach (var newPrime in GetPrimes(cachedPrimes))
                        {
                            yield return newPrime;
                            LastYieldedPrime = newPrime;

                            _primeCache.Add(newPrime);

                            if (++yieldedCount == _maxCount)
                                yield break;
                        };
                    }
                }
            }
        }

        private IEnumerator<int> GetNonCachedEnumerator()
        {
            int yieldedCount = 0;
            foreach (var newPrime in GetPrimes(null))
            {
                yield return newPrime;
                LastYieldedPrime = newPrime;

                if (++yieldedCount >= _maxCount)
                    yield break;
            };
        }
    }
}