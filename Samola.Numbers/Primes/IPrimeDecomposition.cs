using System.Collections.Generic;

namespace Samola.Numbers.Primes
{
    public interface IPrimeDecomposition : IEnumerable<KeyValuePair<int, int>>
    {
        IPrimeDecomposition Pow(int k);
    }
}
