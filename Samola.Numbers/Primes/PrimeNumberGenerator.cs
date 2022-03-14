using System.Collections;
using System.Collections.Generic;

namespace Samola.Numbers.Primes
{
    /// <summary>
    /// Base class for prime generator algorithms with can have parts of their primes pregenerated
    /// </summary>
    public abstract class PrimeNumberGenerator : IPrimeNumberGenerator 
    {
        /// <summary>
        /// Maximum count of primes to generate. When the limit is reached, a prime generation exception is thrown.
        /// </summary>
        public int MAX_COUNT = 2_000_000;
        private readonly IEnumerable<int> _pregeneratedPrimes;

        /// <summary>
        /// Construct a new prime generator algorithm
        /// </summary>
        /// <param name="pregeneratedPrimes">Pregenerated prime numbers</param>
        protected PrimeNumberGenerator(IEnumerable<int> pregeneratedPrimes = null)
        {
            _pregeneratedPrimes = pregeneratedPrimes ?? new List<int>();
        }

        public abstract bool IsPrime(int number);
        
        protected abstract int GenerateNext(IReadOnlyList<int> previousPrimes);

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<int> GetEnumerator()
        {
            int yieldCount = 0;
            var primes = new List<int>(_pregeneratedPrimes);

            // Yield pregenerated primes
            foreach (var prime in primes)
            {
                if (yieldCount >= MAX_COUNT) 
                {
                    throw new PrimeGenerationException($"Maximum yield count reached. Yielded {yieldCount} items.");
                }
                yield return prime;
                yieldCount++;
            }
            
            // Generate and yield new primes
            while (true) 
            {
                if (yieldCount >= MAX_COUNT)
                {
                    throw new PrimeGenerationException($"Maximum yield count reached. Yielded {yieldCount} items.");
                }
                var newPrime = GenerateNext(primes);
                yield return newPrime;
                yieldCount++;
                primes.Add(newPrime);
            }
        }
    }
}
