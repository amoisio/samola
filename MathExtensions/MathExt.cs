using System;
using System.Collections.Generic;

namespace MathExtensions
{
    public static class MathExt
    {
        /// <summary>
        /// Tests if a number is prime
        /// </summary>
        /// <param name="number">Number to test</param>
        public static bool IsPrimeSimple(long number)
        {
            double max = Math.Sqrt(number);
            for (long i = 2; i <= max; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        private static List<long> _primes = new List<long>(200) { 2 };

        public static bool IsPrime(long number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be >= 1.");

            if (number == 2 || number == 1)
                return true;

            if (number % 2 == 0)
                return false;

            double max = Math.Sqrt(number);
            int len = _primes.Count;
            long tempPrime = 0;
            for(int i = 0; i < len && tempPrime <= max; i++)
            {
                tempPrime = _primes[i];
                if (number % tempPrime == 0)
                {
                    return false;
                }
            }

            for (long i = tempPrime; i <= max; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            _primes.Add(number);
            return true;
        }
    }
}



