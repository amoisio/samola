using Samola.Numbers.Enumerables;
using System.Collections.Generic;

namespace Samola.Numbers.Utilities
{
    /// <summary>
    /// Calculates the prime decomposition of a given number
    /// </summary>
    public class PrimeDecomposer 
    {
        private readonly PrimeNumbers _primes;

        public PrimeDecomposer(IntegerLimit limit)
        {
            PrimeNumbersBuilder builder = new PrimeNumbersBuilder()
            {
                Limit = limit,
                UseCache = true
            };
            _primes = builder.Build();
        }

        public Dictionary<int, int> CalculateDecomposition(int number)
        {
            var decomposition = new Dictionary<int, int>();
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

                    while (temp % prime == 0) {
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
