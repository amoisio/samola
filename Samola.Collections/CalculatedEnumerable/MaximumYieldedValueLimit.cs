using System;

namespace Samola.Collections.CalculatedEnumerable
{
    public class MaximumYieldedValueLimit<TItem> : ICalculationLimit<TItem>
        where TItem : IComparable<TItem>
    {
        private readonly TItem _limit;

        public MaximumYieldedValueLimit(TItem limit)
        {
            _limit = limit ?? throw new ArgumentNullException(nameof(limit), "Limit must be given.");
        }

        public bool CanYield(TItem item, int yieldedCount)
        {
            if (item == null)
            {
                return false;
            }

            // item preceded or is the same as the limit
            return item.CompareTo(_limit) <= 0;
        }
    }
}
