using Samola.Numbers.Enumerables;
using System.Collections.Generic;
using System.Linq;

namespace Samola.Numbers.Utilities
{
    /// <summary>
    /// Calculates the prime decomposition of a given number
    /// </summary>
    public class PrimeDecomposer 
    {
        private PrimeNumbers _primes;
        private readonly PrimeNumbersBuilder _builder;

        public PrimeDecomposer()
        {
            _builder = new PrimeNumbersBuilder();
            _builder.UseCache = true;
        }

        public Dictionary<int, int> CalculateDecomposition(int number)
        {
            var decomposition = new Dictionary<int, int>();

            if (number == 1)
            {
                decomposition.Add(number, 1);
            }

            _builder.Limit = new MaxValueLimit(number);
            _primes = _builder.Build();

            var primesHash = new HashSet<int>(_primes);
            
            // This check can be optimized
            // IsPrime computes the check. However, with we might already have knowledge of existing primes at this point
            // - If that is the case then we could simply check if number is contained in the prime hashset, making this check a O(1)
            if (primesHash.Contains(number))//MathExt.IsPrime(number)) 
            {
                decomposition.Add(number, 1);
            }
            else
            {
                var temp = number;
                foreach (var prime in primesHash)
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
