using MathExtensions.Cache;
using System;
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
        public T[] CachedItems => _cache?.Items;

        /// <summary>
        /// Cache settings
        /// </summary>
        private IEnumerableCache<T> _cache;
        private readonly IEnumerableCacheProvider<T> _cacheProvider;

        /// <summary>
        /// Limit condition for stopping the enumerable from yielding values.
        /// </summary>
        private readonly EnumerableLimit<T> _limitCondition;

        /// <summary>
        /// Calculation lock guarantees that enumerated value are calculated and added to the cache synchronously. 
        /// Ie. each value is calculated only once and placed in the cache in deterministic order.
        /// </summary>
        private static object _calculationLock = new object();

        /// <summary>
        /// Calculated enumerable without cache
        /// </summary>
        protected CalculatedEnumerable(EnumerableLimit<T> limit)
            : this(limit, null)
        {
            
        }

        /// <summary>
        /// Calculated enumerable with cache
        /// </summary>
        /// <param name="cacheProvider"></param>
        protected CalculatedEnumerable(EnumerableLimit<T> limit, IEnumerableCacheProvider<T> cacheProvider)
        {
            _cacheProvider = cacheProvider;
            _limitCondition = limit ?? throw new ArgumentNullException("limit", "A limit must be provided for the enumerable to prevent infinite enumeration.");
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (this.UseCache)
            {
                _cache = _cache ?? _cacheProvider.CreateOrGet();
                return GetCachedEnumerator();
            }
            else
            {
                return GetNonCachedEnumerator();
            }
        }

        private IEnumerator<T> GetCachedEnumerator()
        {
            int tempIndex = 0;
            int yieldedCount = 0;
            T tempItem = default(T);
            
            while (CanYield(tempItem, yieldedCount))
            {
                var tempItems = _cache.Items;
                int maxIndexToYield = tempItems.Length - 1;

                // Yield cached items from [temp, maxIndex]
                for (int i = tempIndex; i <= maxIndexToYield; i++)
                {
                    tempItem = tempItems[i];

                    if (!CanYield(tempItem, yieldedCount))
                        yield break;

                    yieldedCount++;
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
                        var cachedItems = _cache.Items;
                        foreach (var newItem in GetItems(cachedItems))
                        {
                            if (!CanYield(newItem, yieldedCount))
                                yield break;

                            yieldedCount++;
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
            int yieldedCount = 0;
            foreach (var newItem in GetItems(null))
            {
                if (!CanYield(newItem, yieldedCount))
                    yield break;

                yieldedCount++;
                yield return newItem;
            };
        }

        /// <summary>
        /// Returns new items
        /// </summary>
        protected abstract IEnumerable<T> GetItems(T[] previousItems);

        private bool CanYield(T item, int yieldedCount)
        {
            var state = EnumerationState.Create(item, yieldedCount);
            return CanYield(state);
        }

        /// <summary>
        /// The enumeration backstop condition. When fulfilled, the enumerable stops yielding new values.
        /// </summary>
        protected virtual bool CanYield(EnumerationState<T> state)
        {
            return _limitCondition.LimitOK(state);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}