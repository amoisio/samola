namespace MathExtensions.Enumerables
{
    /// <summary>
    /// Helper class for constructing EnumerationState objects
    /// </summary>
    public static class EnumerationState
    {
        public static EnumerationState<T> Create<T>(T item, int yieldedCount)
        {
            return new EnumerationState<T>(item, yieldedCount);
        }
    }

    /// <summary>
    /// Represents the state of an enumerable state machine. Encapsulates the item to be yielded next as well as the count of items
    /// yielded so far. When yieldedCount == 0 then item is default(T).
    /// </summary>
    public class EnumerationState<T>
    {
        public EnumerationState(T item, int yieldedCount)
        {
            this.Item = item;
            this.YieldedCount = yieldedCount;
        }

        public T Item { get; }
        public int YieldedCount { get; }
    }
}
