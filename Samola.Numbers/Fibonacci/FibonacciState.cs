using Samola.Collections.CalculatedEnumerable;

namespace Samola.Numbers.Fibonacci
{
    public class FibonacciState<TItem> : IEnumerationState<TItem> 
        where TItem : struct
    {
        public void RegisterYieldedItem(TItem item)
        {
            PenultimateValue = LastValue;
            LastValue = item;
        }

        public TItem? PenultimateValue { get; private set; }
        public TItem? LastValue { get; private set; }
    }
}