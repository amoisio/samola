using System.Collections.Generic;

namespace Samola.Numbers.Primes
{
    /// <summary>
    /// A prime number generator.
    /// </summary>
    public interface IPrimeNumberGenerator : IEnumerable<int>
    {
        /// <summary>
        /// Check is given number is a prime number.
        /// </summary>
        /// <param name="number">Number to check</param>
        /// <returns>True, if number is a prime number. False, otherwise.</returns>
        bool IsPrime(int number);
    }
}
