using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Samola.Algorithms.CalculatedEnumerable.State;

namespace Samola.Algorithms.CalculatedEnumerable
{
    /// <summary>
    /// An enumerable base class for calculated sequences of items.
    /// </summary>
    /// <typeparam name="TItem">Type to enumerate</typeparam>
    /// <typeparam name="TState">Type of enumeration state</typeparam>
    public abstract class CalculatedEnumerable<TItem, TState> : ICalculatedEnumerable<TItem>
        where TState : IEnumerationState<TItem>, new()
    {
        private readonly List<TItem> _precalculatedItems = new();
        
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
            var state = new TState();
            
            int len = _precalculatedItems.Count;
            if (len > 0) // Yield precalculated items if available 
            {
                for (var i = 0; i < len; i++)
                {
                    item = _precalculatedItems[i];
                    state.Yield(item);
                    yield return item;
                }
            }
            else // Compute the initial sequence item
            {
                item = CalculateInitial(state);
                state.Yield(item); 
                yield return item;
            }
            
            do // Calculate and yield new items
            {
                item = CalculateNext(state);
                state.Yield(item);
                yield return item;
            } while (true);
        }

        protected abstract TItem CalculateInitial(TState state);
        
        protected abstract TItem CalculateNext(TState state);
      
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
