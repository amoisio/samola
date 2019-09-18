using System.Collections.Generic;

namespace MathExtensions.Primes
{
    /// <summary>
    /// Generates N first prime numbers by looping over values from 2 to N and checking IsPrime for each one.
    /// </summary>
    public class PrimesSimple : PrimesBase
    {
        private int _n;

        public PrimesSimple(int n) : base(n, false)
        {
            _n = n;
        }

        protected override IEnumerable<int> GetPrimes(int[] previousPrimes)
        {
            int i = 2;
            var generated = 0;
            while (generated < _n)
            {
                while (!MathExt.IsPrime(i++)) { }
                generated++;
                yield return i - 1;
            }
        }
    }
}
