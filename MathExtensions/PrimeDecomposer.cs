using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions
{
    /// <summary>
    /// Calculates the prime decomposition of a given number
    /// </summary>
    public class PrimeDecomposer
    {
        private readonly IPrimes _primes;
        public PrimeDecomposer(IPrimes primesGenerator)
        {
            _primes = primesGenerator;
        }

        public Dictionary<long, long> CalculateDecomposition(long number)
        {
            var decomposition = new Dictionary<long, long>();
            if (MathExt.IsPrime(number))
            {
                decomposition.Add(number, 1);
            }
            else
            {
                var temp = number;
                foreach (var prime in _primes)
                {
                    if (temp == 1)
                        break;

                    while ( temp % prime == 0) {
                        if (decomposition.ContainsKey(prime))
                            decomposition[prime]++;
                        else
                            decomposition.Add(prime, 1);

                        temp = temp / prime;
                    }
                }
            }
            return decomposition;
        }
    }
}
