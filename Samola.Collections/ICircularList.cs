using System;
using System.Collections.Generic;

namespace Samola.Collections
{
    public interface ICircularList<T> : IEnumerable<T>
    {
        /// <summary>
        /// Maximum number of items that can be stored in the buffer
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Number of items currently stored in the buffer
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds items to the end of the buffer
        /// </summary>
        /// <param name="item"></param>
        void Add(T item);

        /// <summary>
        /// Removes an item from the start of the buffer
        /// </summary>
        void Remove();

        /// <summary>
        /// Empties the array
        /// </summary>
        void Clear();

        Action<T> OnIndexChange { get; set; }
    }
}
