namespace Samola.Algorithms.CalculatedEnumerable.State
{
    public interface IEnumerationState<TItem>
    {
        /// <summary>
        /// Determines how many items have already been yielded.
        /// </summary>
        int YieldedCount { get; }
        
        /// <summary>
        /// Perform state book keeping for yielding a value
        /// </summary>
        void Yield(TItem item);
        
        /// <summary>
        /// Previously yielded item.
        /// </summary>
        TItem PreviouslyYieldedItem { get; }
    }
}
