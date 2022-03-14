using System.Collections.Generic;

namespace Samola.Collections
{
    /// <summary>
    /// An enumerable whose values are calculated on demand.
    /// Has the ability to precalculate a number of values. Implementations should always yield precalculated
    /// values before calculating new values.
    /// </summary>
    /// <typeparam name="TItem">Type to enumerate</typeparam>
    public interface ICalculatedEnumerable<out TItem> : IEnumerable<TItem>
    {
        /// <summary>
        /// Precalculate values for the enumerable. Precalculation should always mean that these values can be yielded
        /// without the cost of calculating said value.
        /// </summary>
        /// <param name="count">Number of items to precalculate</param>
        void Precalculate(int count);
    }
}