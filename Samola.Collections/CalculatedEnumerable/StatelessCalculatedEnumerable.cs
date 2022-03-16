using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Samola.Collections.CalculatedEnumerable
{
    /// <summary>
    /// An enumerable base class meant for stateless calculated sequences of items.
    /// Stateless :
    /// - Calculation of the next item does not depend on previous state.
    /// Features:
    /// - Precalculate values for faster access.
    /// - Customizable calculation limit to prevent infinite enumeration. (By default, yields 100 000 items.)
    /// </summary>
    /// <typeparam name="TItem">Type to enumerate</typeparam>
    public abstract class StatelessCalculatedEnumerable<TItem> : ICalculatedEnumerable<TItem>
    {
        private readonly List<TItem> _precalculatedItems;
        private readonly ICalculationLimit<TItem> _limit;

        protected StatelessCalculatedEnumerable(ICalculationLimit<TItem> limit)
        {
            _precalculatedItems = new List<TItem>();
            _limit = limit ?? MaximumYieldedCountLimit<TItem>.Default;
        }

        public void Precalculate(int count)
        {
            _precalculatedItems.Clear();
            _precalculatedItems.Capacity = count;
            var precalculatedItems = this.Take(count);
            _precalculatedItems.AddRange(precalculatedItems);
        }

        public IEnumerator<TItem> GetEnumerator()
        {
            TItem item;
            int yieldedCount = 0;

            // Yield precalculated items
            int len = _precalculatedItems.Count;
            for (int i = 0; i < len; i++)
            {
                item = _precalculatedItems[i];
                if (!_limit.CanYield(item, yieldedCount))
                {
                    yield break;
                }

                yieldedCount++;
                yield return item;
            }

            // Calculate and yield new items 
            item = CalculateNext();
            while (_limit.CanYield(item, yieldedCount))
            {
                yieldedCount++;
                yield return item;
                item = CalculateNext();
            }
        }
        
        protected abstract TItem CalculateNext();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
