using System.Collections.Generic;
using System.Linq;

namespace Samola.Collections
{
    /// <summary>
    /// Maximum items yielded count limit. Prompts calculation to stop once a predefined number of items
    /// has been yielded.
    /// </summary>
    public class MaximumYieldedCountLimit<TItem> : ICalculationLimit<TItem>
    {
        private readonly int _maxCount;
        public MaximumYieldedCountLimit(int maxCount)
        {
            _maxCount = maxCount;
        }
        
        public bool CanYield(TItem item, IEnumerable<TItem> previousItems)
        {
            return previousItems.Count() < _maxCount;
        }
    }
}