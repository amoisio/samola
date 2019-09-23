using System.Collections;
using System.Collections.Generic;

namespace MathExtensions.Implementations
{
    public abstract class BaseCachingCollection<T> : ICachingCollection<T>
    {
        //
        // Constants
        //
        public const int INIT_CACHE_CAPACITY = 15000;

        /// <summary>
        /// Backstop limit for enumerating items
        /// </summary>
        private readonly EnumerateLimit<T> _limit;

        /// <summary>
        /// Set to true to collect computed items into the cache
        /// </summary>
        private readonly bool _useCache;

        // CACHE
        private static List<T> _cache;
        private static object _cacheLock;
        protected List<T> Cache => _cache;
        // /CACHE

        public T LastYielded { get; private set; }
        public int YieldedCount { get; private set; }

        static BaseCachingCollection()
        {
            if (_cache == null)
                _cache = new List<T>(INIT_CACHE_CAPACITY);

            if (_cacheLock == null)
                _cacheLock = new object();
        }

        protected BaseCachingCollection(EnumerateLimit<T> limit, bool useCache)
        {
            _limit = limit;
            _useCache = useCache;
        }

        public IEnumerator<T> GetEnumerator()
        {
            this.YieldedCount = 0;
            this.LastYielded = default(T);

            if (_useCache)
                return GetCachedEnumerator();
            else
                return GetNonCachedEnumerator();
        }

        private IEnumerator<T> GetCachedEnumerator()
        {
            T tempItem = default(T);
            int tempIndex = 0;
            int maxIndexToYield = _cache.Count - 1;

            while (_limit.CanStillYield(this.YieldedCount, this.LastYielded))
            {
                // Yield cached items from [temp, max]
                for (int i = tempIndex; i <= maxIndexToYield; i++)
                {
                    tempItem = _cache[i];

                    if (!_limit.CanStillYield(this.YieldedCount, tempItem))
                        yield break;

                    this.YieldedCount++;
                    this.LastYielded = tempItem;
                    yield return tempItem;
                }

                lock (_cacheLock)
                {
                    int newMaxIndex = _cache.Count - 1;
                    if (newMaxIndex > maxIndexToYield)
                    {
                        // Cache contains new items to yield - release lock, go back and yield them
                        tempIndex = maxIndexToYield + 1;
                        maxIndexToYield = newMaxIndex;
                    }
                    else
                    {
                        var cachedItems = _cache.ToArray();
                        foreach (var newItem in GetItems(cachedItems))
                        {
                            if (!_limit.CanStillYield(this.YieldedCount, newItem))
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

        private IEnumerator<T> GetNonCachedEnumerator()
        {
            foreach (var newItem in GetItems(null))
            {
                if (!_limit.CanStillYield(this.YieldedCount, newItem))
                    yield break;

                this.YieldedCount++;
                this.LastYielded = newItem;
                yield return newItem;
            };
        }

        protected abstract IEnumerable<T> GetItems(T[] previousItems);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}