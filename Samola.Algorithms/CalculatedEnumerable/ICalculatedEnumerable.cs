using System.Collections.Generic;

namespace Samola.Algorithms.CalculatedEnumerable
{
    /// <summary>
    /// An enumerable whose values are calculated on demand.
    /// </summary>
    /// <typeparam name="TItem">Type to enumerate</typeparam>
    public interface ICalculatedEnumerable<out TItem> : IEnumerable<TItem>
    {
        /// <summary>
        /// Precalculate values for the enumerable.
        /// </summary>
        /// <param name="count">Number of items to precalculate</param>
        void Precalculate(int count);
    }
}