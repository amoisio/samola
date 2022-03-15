using Samola.Collections;

namespace Samola.Numbers.Primes
{
    /// <summary>
    /// Base class for prime generator algorithms with can have parts of their primes pregenerated
    /// </summary>
    public abstract class PrimeNumbers : CalculatedEnumerable<int>, IPrimeNumerable 
    {
        /// <summary>
        /// Maximum count of primes to generate. When the limit is reached, a prime generation exception is thrown.
        /// </summary>
        public const int MAX_COUNT = 2_000_000;

        /// <summary>
        /// Construct a new prime generator algorithm
        /// </summary>
        /// <param name="limit">Pregenerated prime numbers</param>
        protected PrimeNumbers(ICalculationLimit<int> limit = null)
            : base(limit ?? new MaximumYieldedCountLimit<int>(MAX_COUNT))
        {
            
        }

        public abstract bool IsPrime(int number);
    }
}
