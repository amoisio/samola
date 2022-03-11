using System.Collections.Generic;

namespace Samola.Numbers.Primes
{
    /// <summary>
    /// A collection of prime numbers
    /// </summary>
    public interface IPrimes : IEnumerable<int>
    {
        /// <summary>
        /// Check if the given number is a prime number.
        /// </summary>
        /// <param name="value">Number to check</param>
        /// <returns>True if the value is prime number. False, otherwise.</returns>
        bool IsPrime(int value);
    }
}
