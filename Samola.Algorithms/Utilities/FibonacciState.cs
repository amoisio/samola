using Samola.Algorithms.CalculatedEnumerable.State;

namespace Samola.Algorithms.Utilities
{
    public sealed class FibonacciState<TItem> : DefaultEnumerationState<TItem> 
    {
        public TItem PenultimateYieldedValue { get; private set; }

        public override void Yield(TItem item)
        {
            PenultimateYieldedValue = PreviouslyYieldedItem;
            base.Yield(item);
        }
    }
}