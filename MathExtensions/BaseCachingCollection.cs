using System.Collections;
using System.Collections.Generic;

namespace MathExtensions.Implementations
{
    public abstract class BaseCachingCollection<T> : ICachingCollection<T>
    {
        //
        // Constants
        //
        public const int MAX_COUNT = 2000000;
        public const int INIT_CACHE_CAPACITY = 15000;

        /// <summary>
        /// Maximum count of items that the enumerable can return
        /// </summary>
        private readonly int _maxCount;

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

        static BaseCachingCollection()
        {
            if (_cache == null)
                _cache = new List<T>(INIT_CACHE_CAPACITY);

            if (_cacheLock == null)
                _cacheLock = new object();
        }

        protected BaseCachingCollection(int maxCount, bool useCache)
        {
            _maxCount = maxCount;
            _useCache = useCache;
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_useCache)
                return GetCachedEnumerator();
            else
                return GetNonCachedEnumerator();
        }

        private IEnumerator<T> GetCachedEnumerator()
        {
            int yieldedCount = 0;
            T tempItem = default(T);
            int tempIndex = 0;
            int maxIndexToYield = _cache.Count - 1;

            while (yieldedCount < _maxCount)
            {
                // Yield cached items from [temp, max]
                for (int i = tempIndex; i <= maxIndexToYield; i++)
                {
                    tempItem = _cache[i];
                    yield return tempItem;
                    LastYielded = tempItem;

                    if (++yieldedCount >= _maxCount)
                        yield break;
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
                            yield return newItem;
                            LastYielded = newItem;

                            _cache.Add(newItem);

                            if (++yieldedCount == _maxCount)
                                yield break;
                        };
                    }
                }
            }
        }

        private IEnumerator<T> GetNonCachedEnumerator()
        {
            int yieldedCount = 0;
            foreach (var newItem in GetItems(null))
            {
                yield return newItem;
                LastYielded = newItem;

                if (++yieldedCount >= _maxCount)
                    yield break;
            };
        }

        protected abstract IEnumerable<T> GetItems(T[] previousItems);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}