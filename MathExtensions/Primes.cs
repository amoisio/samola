using System.Collections;
using System.Collections.Generic;

namespace MathExtensions
{
    /// <summary>
    /// Generates N first prime numbers
    /// </summary>
    public class Primes : IEnumerable<long>
    {
        private long _n;

        public Primes(long n)
        {
            _n = n;
        }

        /// <summary>
        /// Increments the number of primes to generate. 
        /// (Done this way to avoid using while(true) in the generation condition.
        /// </summary>
        public void IncrementN(long by)
        {
            _n += by;
        }

        public IEnumerator<long> GetEnumerator()
        {
            long i = 2;
            var generated = 0;
            while (generated < _n)
            {
                while (!IsPrime(i++)) { }
                generated++;
                yield return i - 1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Tests if a number is prime
        /// </summary>
        /// <param name="number">Number to test</param>
        public static bool IsPrime(long number)
        {
            return MathExt.IsPrime(number); 
        }
    }
}
