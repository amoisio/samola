using System.Collections.Generic;
using Samola.Numbers;

namespace Samola.Numbers.Enumerables
{
    /// <summary>
    /// Sieve of Eratosthenes builds primes up to a given natural number
    /// </summary>
    public class SieveOfEratosthenes
    {
        private int _upToInt;

        public SieveOfEratosthenes(int upToInt)
        {
            _upToInt = upToInt;
        }

        public IEnumerable<int> GetPrimes()
        {
            return CalculatePrimes(_upToInt);
        }

        public int GetNextPrime()
        {
            while (!MathExt.IsPrime(++_upToInt));
            return _upToInt;
        }

        private static IEnumerable<int> CalculatePrimes(int upToInt)
        {
            List<int> nonPrime = new List<int>();

            for (int i = 2; i <= upToInt; i++)
            {
                if (nonPrime.Contains(i))
                {
                    continue;
                }
                else
                {
                    yield return i;
                }

                for (int j = i + 1; j <= upToInt; j++)
                {
                    if (j % i == 0)
                    {
                        nonPrime.Add(j);
                    }
                }
            }
        }



    }
}
