using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions.Cache
{
    /// <summary>
    /// Represents a factory for instantiating enumerable cache objects
    /// </summary>
    public interface IEnumerableCacheProvider<T>
    {
        IEnumerableCache<T> Create();
    }

    /// <summary>
    /// Cache provider for a list base enumerable cache
    /// </summary>
    public class EnumerableCacheProvider<T> : IEnumerableCacheProvider<T>
    {
        public IEnumerableCache<T> Create()
        {
            return new EnumerableCache<T>();
        }
    }
}
