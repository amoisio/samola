using System.Collections.Generic;

namespace Samola.Numbers.Primes
{
    public class PrimeDecomposer : IPrimeDecomposer
    {
        private readonly IPrimeNumberGenerator _primeNumberGenerator;

        public PrimeDecomposer(IPrimeNumberGenerator primeNumberGenerator)
        {
            _primeNumberGenerator = primeNumberGenerator;
        }

        public IPrimeDecomposition CalculateDecomposition(int number)
        {
            var decomposition = new Dictionary<int, int>(25);
            if (number == 1 || _primeNumberGenerator.IsPrime(number))
            {
                decomposition.Add(number, 1);
            }
            else
            {
                var temp = number;
                foreach (var prime in _primeNumberGenerator)
                {
                    if (temp == 1)
                        break;

                    while (temp % prime == 0)
                    {
                        if (decomposition.ContainsKey(prime))
                            decomposition[prime]++;
                        else
                            decomposition.Add(prime, 1);

                        temp /= prime;
                    }
                }
            }
            return new PrimeDecomposition(decomposition);
        }
    }
}
