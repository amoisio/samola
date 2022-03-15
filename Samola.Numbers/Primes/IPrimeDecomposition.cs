using System;
using System.Collections.Generic;

namespace Samola.Numbers.Primes
{
    /// <summary>
    /// Prime number decomposition
    /// </summary>
    public interface IPrimeDecomposition : IEnumerable<KeyValuePair<int, int>>
    {
        /// <summary>
        /// Raise the decomposition to power k.
        /// </summary>
        /// <param name="k">Exponent</param>
        /// <returns>A new decomposition raised to power k.</returns>
        IPrimeDecomposition Pow(int k);

        /// <summary>
        /// Number of prime factors in the decomposition.
        /// </summary>
        int Factors { get; }
    }
}
