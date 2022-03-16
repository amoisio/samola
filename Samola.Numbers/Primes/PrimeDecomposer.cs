using System.Collections.Generic;

namespace Samola.Numbers.Primes
{
    public class PrimeDecomposer : IPrimeDecomposer
    {
        private readonly IPrimeNumerable<int> _primes;

        public PrimeDecomposer(IPrimeNumerable<int> primes)
        {
            _primes = primes;
        }

        public IPrimeDecomposition CalculateDecomposition(int number)
        {
            var decomposition = new Dictionary<int, int>(25);
            if (number == 1 || _primes.IsPrime(number))
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
