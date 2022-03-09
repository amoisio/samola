using Samola.Numbers.Enumerables;
using System;
using System.Collections.Generic;

namespace Samola.Numbers.Utilities
{
    /// <summary>
    /// Calculates the prime decomposition of a given number
    /// </summary>
    [Obsolete]
    public class PrimeDecomposer
    {
        private readonly PrimeNumbersBuilder _builder;

        public PrimeDecomposer()
        {
            _builder = new PrimeNumbersBuilder
            {
                UseCache = true
            };
        }

        public Dictionary<int, int> CalculateDecomposition(int number)
        {
            _builder.Limit = new MaxValueLimit(number);
            var primes = _builder.Build();

            var decomposition = new Dictionary<int, int>();
            if (number == 1 || MathExt.IsPrime(number))
            {
                decomposition.Add(number, 1);
            }
            else
            {
                var temp = number;
                foreach (var prime in primes)
                {
                    if (temp == 1)
                        break;

                    while (temp % prime == 0)
                    {
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
