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

    ///// <summary>
    ///// Generates N first prime numbers
    ///// </summary>
    //public class Primes2 : IEnumerable<long>
    //{
    //    private long? _numberToGenerate;
    //    private long? _countToGenerate;

    //    public Primes2(long countToGenerate)
    //    {
    //        _n = n;
    //    }

    //    /// <summary>
    //    /// Increments the number of primes to generate. 
    //    /// (Done this way to avoid using while(true) in the generation condition.
    //    /// </summary>
    //    public void IncrementN(long by)
    //    {
    //        _n += by;
    //    }

    //    public IEnumerator<long> GetEnumerator()
    //    {
    //        var generated = 0;

    //        if (_n == 1)
    //        {
    //            yield return 2;
    //            generated++;
    //        } 
    //        yield return 2;
    //        yield return 3;
            

    //        int k = 1;
    //        while (generated < _n)
    //        {
    //            yield return 6 * k - 1;
    //            yield return 6 * k + 1;
    //            generated += 2;
    //        }
    //    }

    //    IEnumerator IEnumerable.GetEnumerator()
    //    {
    //        return GetEnumerator();
    //    }

    //    /// <summary>
    //    /// Tests if a number is prime
    //    /// </summary>
    //    /// <param name="number">Number to test</param>
    //    public static bool IsPrime(long number)
    //    {
    //        return MathExt.IsPrime(number);
    //    }
    //}
}
