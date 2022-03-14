using System.Collections.Generic;

namespace Samola.Numbers.Primes
{
    /// <summary>
    /// Prime number decomposition
    /// </summary>
    public interface IPrimeDecomposition : IEnumerable<KeyValuePair<int, int>>
    {
        IPrimeDecomposition Pow(int k);
    }
}
