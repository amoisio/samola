using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Samola.Collections
{
    /// <summary>
    /// An enumerable whose values are calculated on demand.
    /// Has the ability to cache calculated values for faster access in subsequent enumerations.
    /// By default, has a calculation limit of 100 000 items. 
    /// </summary>
    /// <typeparam name="TItem">Type to enumerate</typeparam>
    public abstract class CalculatedEnumerable<TItem> : ICalculatedEnumerable<TItem>
    {
        public const int DEFAULT_MAX_YIELDED = 100_000;
        private readonly List<TItem> _precalculatedItems;
        private readonly ICalculationLimit<TItem> _calculationLimit;
        
        protected CalculatedEnumerable(ICalculationLimit<TItem> calculationLimit)
        {
            _precalculatedItems = new List<TItem>();
            _calculationLimit = calculationLimit ?? new MaximumYieldedCountLimit<TItem>(DEFAULT_MAX_YIELDED);
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
            var yieldedItems = new List<TItem>(_precalculatedItems.Count);
            TItem item;
            // Yield precalculated items
            int len = _precalculatedItems.Count;
            for (int i = 0; i < len; i++)
            {
                item = _precalculatedItems[i];
                if (!_calculationLimit.CanYield(item, yieldedItems)) 
                {
                    yield break;
                }
                yield return item;
                yieldedItems.Add(item);
            }
            
            // Calculate and yield new items 
            item = CalculateNext(yieldedItems);
            while (_calculationLimit.CanYield(item, yieldedItems))
            {
                yield return item;
                yieldedItems.Add(item);
                item = CalculateNext(yieldedItems);
            }
        }

        protected abstract TItem CalculateNext(IReadOnlyList<TItem> previousItems);
        
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}