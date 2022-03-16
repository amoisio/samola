namespace Samola.Collections.CalculatedEnumerable
{
    /// <summary>
    /// Maximum items yielded count limit. Prompts calculation to stop once a predefined number of items
    /// has been yielded.
    /// </summary>
    public class MaximumYieldedCountLimit<TItem> : ICalculationLimit<TItem>
    {
        private const int DefaultMaxYielded = 100_000;
        private readonly int _maxCount;
        public MaximumYieldedCountLimit(int maxCount)
        {
            _maxCount = maxCount;
        }

        public static MaximumYieldedCountLimit<TItem> Default 
            => new MaximumYieldedCountLimit<TItem>(DefaultMaxYielded);

        public bool CanYield(TItem item, int yieldedCount)
        {
            return yieldedCount < _maxCount;
        }
    }
}