namespace Samola.Collections.CalculatedEnumerable
{
    /// <summary>
    /// Represents a calculation limit.
    /// </summary>
    /// <typeparam name="TItem">Type of the item resulting from the calculation.</typeparam>
    public interface ICalculationLimit<in TItem>
    {
        /// <summary>
        /// Determine if a given item can be yielded. 
        /// </summary>
        /// <param name="item">Item under consideration</param>
        /// <param name="yieldCount">Number of items yielded so far.</param>
        /// <returns>True, if the item can be yielded. False, otherwise.</returns>
        bool CanYield(TItem item, int yieldCount);
    }
}