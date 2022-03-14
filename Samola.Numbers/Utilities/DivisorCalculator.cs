using System;
using System.Collections.Generic;
using System.Linq;
using Samola.Numbers.Primes;

namespace Samola.Numbers.Utilities
{
    public class DivisorCalculator
    {
        private readonly IPrimeDecomposer _primeDecomposer;

        public DivisorCalculator() : this(new PrimeNumbers6k()) { }

        public DivisorCalculator(IPrimeNumberGenerator primeNumberGenerator) : this(new PrimeDecomposer(primeNumberGenerator)) { }

        public DivisorCalculator(IPrimeDecomposer primeDecomposer)
        {
            _primeDecomposer = primeDecomposer;
        }

        public int NumberOfDivisors(int number)
        {
            var decomposition = _primeDecomposer.CalculateDecomposition(number);
            return NumberOfDivisors(decomposition);
        }

        public int NumberOfDivisors(IPrimeDecomposition decomposition)
        {
            // if the decomposition only consists of the trivial prime 1 then return 1
            if (decomposition.Count() == 1 && decomposition.First().Key == 1)
                return 1;

            var D = decomposition.Select(e => e.Value + 1)
                .Aggregate((x, y) => x * y);

            return D;
        }

        /// <summary>
        /// Get all unique divisors of a number (except the trivial 1).
        /// </summary>
        /// <param name="number"></param>
        public HashSet<int> GetDivisors(int number)
        {
            var decomposition = _primeDecomposer.CalculateDecomposition(number);
            return GetDivisors(decomposition);
        }

        /// <summary>
        /// Get all unique proper divisors of a number.
        /// Proper divisors do not include 1 or the number itself.
        /// </summary>
        /// <param name="number"></param>
        public HashSet<int> GetProperDivisors(int number)
        {
            var divisors = GetDivisors(number);
            divisors.Remove(number);
            return divisors;
        }

        public HashSet<int> GetDivisors(IPrimeDecomposition decomposition)
        {
            // Calculate a helper array
            var darr = decomposition.ToArray();
            var len = decomposition.Count();
            int[] m1 = new int[len]; // max exponent + 1
            int[] t = new int[len]; // cumulative products of m1 cells
            for (int i = 0; i < len; i++)
            {
                m1[i] = darr[i].Value + 1;
                if (i == 0)
                    t[i] = 1;
                else
                    t[i] = m1[i - 1] * t[i - 1];
            }

            // Calculate the divisors
            int n = NumberOfDivisors(decomposition);
            var divisors = new HashSet<int>();
            for (int i = 0; i < n; i++)
            {
                int temp = 1;
                for (int j = 0; j < len; j++)
                {
                    int exponent = i / t[j] % m1[j];
                    var pow = Math.Pow(darr[j].Key, exponent);
                    temp *= (int)pow;
                }
                divisors.Add(temp);
            }
            return divisors;
        }
    }
}