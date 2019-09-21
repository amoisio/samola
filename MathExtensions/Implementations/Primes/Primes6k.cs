using System;
using System.Collections.Generic;
using System.Linq;

namespace MathExtensions.Primes
{
    /// <summary>
    /// Generates N first prime numbers
    /// </summary>
    public class Primes6k : PrimesBase2
    {
        private Primes6k(int maxCount, bool useCache)
            : base(maxCount, useCache)
        {

        }

        public static Primes6k Create(int maxCount, bool useCache)
        {
            return new Primes6k(maxCount, useCache);
        }

        protected override IEnumerable<int> GetPrimes(int[] previousPrimes)
        {
            List<int> primes = null;
            int lastPrime = 0;
            if (previousPrimes != null && previousPrimes.Length > 0)
            {
                primes = new List<int>(previousPrimes);
                lastPrime = previousPrimes[previousPrimes.Length - 1];
            }
            else
            {
                primes = new List<int>();
            }

            foreach (var candidate in GetPrimeCandidates(lastPrime))
            {
                bool isPrime = false;
                foreach (var prime in primes)
                {
                    if (prime * prime > candidate)
                    {
                        isPrime = true;
                        break;
                    }

                    if (candidate % prime == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (primes.Count == 0)
                    isPrime = true;

                if (isPrime)
                {
                    primes.Add(candidate);
                    yield return candidate;
                }
            }
        }

        private static IEnumerable<int> GetPrimeCandidates(int startFrom)
        {
            if (startFrom < 2)
            {
                yield return 2;
            }

            if (startFrom < 3)
            {
                yield return 3;
            }

            int k = 0;
            if (startFrom < 5)
            {
                k = 1;
            }
            else
            {
                k = Math.DivRem(startFrom + 1, 6, out int remainder);

                if (remainder == 0)
                {
                    yield return 6 * k + 1;
                }

                k++;
            }

            while (true)
            {
                yield return 6 * k - 1;
                yield return 6 * k + 1;
                k++;
            }
        }
    }
}
