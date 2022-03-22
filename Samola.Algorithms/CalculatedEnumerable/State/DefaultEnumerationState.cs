namespace Samola.Algorithms.CalculatedEnumerable.State
{
    public class DefaultEnumerationState<TItem> : IEnumerationState<TItem>
    {
        public TItem PreviouslyYieldedItem { get; protected set; }
        public int YieldedCount { get; protected  set; }
        public virtual void Yield(TItem item)
        {
            PreviouslyYieldedItem = item;
            YieldedCount++;
        }
    }
}