namespace Samola.Algorithms.Utilities
{
    /// <summary>
    /// Prime number decomposition calculator
    /// </summary>
    public interface IPrimeDecomposer
    {
        /// <summary>
        /// Compute the prime decomposition of a given number
        /// </summary>
        /// <param name="number">Number to decompose</param>
        /// <returns>Prime decomposition</returns>
        IPrimeDecomposition CalculateDecomposition(int number);
    }
}
