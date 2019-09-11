using System.Collections;
using System.Collections.Generic;

namespace MathExtensions
{
    /// <summary>
    /// Generates N first prime numbers
    /// </summary>
    public class Primes : IPrimes
    {
        private long _n;

        public Primes(long n)
        {
            _n = n;
        }

        public IEnumerator<long> GetEnumerator()
        {
            long i = 2;
            var generated = 0;
            while (generated < _n)
            {
                while (!MathExt.IsPrime(i++)) { }
                generated++;
                yield return i - 1;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
