using Samola.Collections;

namespace Samola.Numbers.Primes
{
    /// <summary>
    /// Represents a type which provides a way of checking if a number is a prime number.
    /// </summary>
    public interface IPrimeNumerable
    {
        /// <summary>
        /// Check if given number is a prime number.
        /// </summary>
        /// <param name="number">Number to check</param>
        /// <returns>True, if number is a prime number. False, otherwise.</returns>
        bool IsPrime(int number);
    }
}
