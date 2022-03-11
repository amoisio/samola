namespace Samola.Numbers.Primes
{
    /// <summary>
    /// Decomposer which computes the prime decomposition of numbers.
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
