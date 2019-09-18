using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace MathExtensions.Primes
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
}