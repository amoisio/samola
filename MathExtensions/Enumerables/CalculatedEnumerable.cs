using MathExtensions.Cache;
using System.Collections;
using System.Collections.Generic;

namespace MathExtensions.Enumerables
{
    /// <summary>
    /// An enumerable base class whose values are calculated on demand. Has the ability to cache calculated values 
    /// for faster access in subsequent enumerations.
    /// </summary>
    /// <typeparam name="T">Type to enumerate</typeparam>
    public abstract class CalculatedEnumerable<T> : IEnumerable<T>
    {
        /// <summary>
        /// Indicates whether cache is in use.
        /// </summary>
        public bool UseCache => _cacheProvider != null;

        /// <summary>
        /// Gets the contents of the cache.
        /// </summary>
        public T[] CachedItems => _cache?.ToArray();

        /// <summary>
        /// Calculation lock guarantees that enumerated value are calculated and added to the cache synchronously. 
        /// Ie. each value is calculated only once and placed in the cache in deterministic order.
        /// </summary>
        private static object _calculationLock = new object();

        /// <summary>
        /// The enumerable cache
        /// TODO: Consider including a cache provider as well
        /// </summary>
        private IEnumerableCache<T> _cache;

        /// <summary>
        /// Cache provider. Called
        /// </summary>
        private readonly IEnumerableCacheProvider<T> _cacheProvider;

        /// <summary>
        /// The last item yielded by the enumerable
        /// </summary>
        public T LastYielded { get; private set; }

        /// <summary>
        /// The number of items yielded 
        /// </summary>
        public int YieldedCount { get; private set; }

        /// <summary>
        /// Calculated enumerable without a cache
        /// </summary>
        protected CalculatedEnumerable()
        {
            _cacheProvider = null;
        }

        /// <summary>
        /// Calculated enumerable with a cache created by the cache provider
        /// </summary>
        protected CalculatedEnumerable(IEnumerableCacheProvider<T> cacheProvider)
        {
            _cacheProvider = cacheProvider;
        }

        public IEnumerator<T> GetEnumerator()
        {
            this.YieldedCount = 0;
            this.LastYielded = default(T);

            if (this.UseCache)
            {
                if (_cache == null)
                    _cache = _cacheProvider.Create();

                return GetCachedEnumerator();
            }
            else
            {
                return GetNonCachedEnumerator();
            }
        }

        private IEnumerator<T> GetCachedEnumerator()
        {
            T tempItem = default(T);
            int tempIndex = 0;
            int maxIndexToYield = _cache.Count - 1;

            while (CanYield(tempItem))
            {
                // Yield cached items from [temp, maxIndex]
                for (int i = tempIndex; i <= maxIndexToYield; i++)
                {
                    tempItem = _cache[i];

                    if (!CanYield(tempItem))
                        yield break;

                    this.YieldedCount++;
                    this.LastYielded = tempItem;
                    yield return tempItem;
                }

                // Yielded up to maxIndex, lock and compute the next item to yield...
                lock (_calculationLock)
                {
                    // ...but before that, check if the cache contain additional items to yield
                    int newMaxIndex = _cache.Count - 1;
                    if (newMaxIndex > maxIndexToYield)
                    {
                        // And if it does release the lock, go back and yield them
                        tempIndex = maxIndexToYield + 1;
                        maxIndexToYield = newMaxIndex;
                    }
                    else
                    {
                        // Otherwise, compute new values to yield. Also, store each new value in the cache.
                        //
                        // This obviously blocks other threads from computing new values. The downside of this is
                        // that threads may end up blocking an extended period of time while the first thread is computing 
                        // new values.
                        // 
                        // TODO: Find a way around this issue. Ideally, waiting threads could yield newly cached values as soon
                        // as they are added in the cache. And after that remain waiting for the lock.
                        var cachedItems = _cache.ToArray();
                        foreach (var newItem in GetItems(cachedItems))
                        {
                            if (!CanYield(newItem))
                                yield break;

                            this.YieldedCount++;
                            this.LastYielded = newItem;
                            yield return newItem;
                            _cache.Add(newItem);
                        };
                    }
                }
            }
        }

        /// <summary>
        /// A non-caching, and hence, non-blocking enumerator
        /// </summary>
        private IEnumerator<T> GetNonCachedEnumerator()
        {
            foreach (var newItem in GetItems(null))
            {
                if (!CanYield(newItem))
                    yield break;

                this.YieldedCount++;
                this.LastYielded = newItem;
                yield return newItem;
            };
        }

        /// <summary>
        /// Returns new items
        /// </summary>
        protected abstract IEnumerable<T> GetItems(T[] previousItems);

        private bool CanYield(T item)
        {
            var state = EnumerationState.Create(item, this.YieldedCount);
            return CanYield(state);
        }

        /// <summary>
        /// The enumeration backstop condition. When fulfilled, the enumerable stops yielding new values.
        /// </summary>
        protected abstract bool CanYield(EnumerationState<T> state);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}