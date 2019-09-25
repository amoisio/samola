using System.Collections.Generic;

namespace MathExtensions.Cache
{
    /// <summary>
    /// Represents an enumerable, read-only, cache
    /// </summary>
    public interface IEnumerableCache<T>
    {
        T[] ToArray();
        int Count { get; }
        T this[int index] { get; }
        void Add(T item);
    }

    /// <summary>
    /// Simple enumerable cache based on encapsulating a list.
    /// </summary>
    public class EnumerableCache<T> : IEnumerableCache<T>
    {
        public const int INIT_CACHE_SIZE = 10000;
        private readonly List<T> _cache;

        public EnumerableCache()
        {
            _cache = new List<T>(INIT_CACHE_SIZE);
        }

        public T this[int index] => _cache[index];

        public int Count => _cache.Count;

        public void Add(T item)
        {
            _cache.Add(item);
        }

        public T[] ToArray()
        {
            return _cache.ToArray();
        }
    }
}
