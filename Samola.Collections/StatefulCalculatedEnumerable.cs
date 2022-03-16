using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Samola.Collections
{
    /// <summary>
    /// An enumerable base class meant for calculated sequences of items.
    /// Features:
    /// - Precalculate values for faster access.
    /// - Customizable calculation limit to prevent infinite enumeration. (By default, yields 100 000 items.)
    /// - Customizable enumeration state to propagation of custom state between iterations  
    /// </summary>
    /// <typeparam name="TItem">Type to enumerate</typeparam>
    /// <typeparam name="TState">Type of enumeration state</typeparam>
    public abstract class StatefulCalculatedEnumerable<TItem, TState> : ICalculatedEnumerable<TItem>
        where TState : class, IEnumerationState<TItem>
    {
        private readonly List<TItem> _precalculatedItems;
        private readonly ICalculationLimit<TItem> _calculationLimit;

        protected StatefulCalculatedEnumerable(ICalculationLimit<TItem> calculationLimit)
        {
            _precalculatedItems = new List<TItem>();
            _calculationLimit = calculationLimit ?? MaximumYieldedCountLimit<TItem>.Default;
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
            var state = InitializeState();

            // Yield precalculated items
            int len = _precalculatedItems.Count;
            for (int i = 0; i < len; i++)
            {
                item = _precalculatedItems[i];
                if (!_calculationLimit.CanYield(item, yieldedCount))
                {
                    yield break;
                }

                state.RegisterYieldedItem(item);
                yieldedCount++;
                yield return item;
            }

            // Calculate and yield new items 
            item = CalculateNext(state);
            while (_calculationLimit.CanYield(item, yieldedCount))
            {
                state.RegisterYieldedItem(item);
                yieldedCount++;
                yield return item;
                item = CalculateNext(state);
            }
        }

        protected abstract TState InitializeState();
        protected abstract TItem CalculateNext(TState state);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
