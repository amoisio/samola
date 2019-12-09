﻿using System.Collections.Generic;
using System.Linq;

namespace Samola.Numbers.Utilities
{
    public class DivisorCalculator
    {
        private PrimeDecomposer _primeDecomposer;

        public DivisorCalculator() : this(new PrimeDecomposer())
        {

        }

        public DivisorCalculator(PrimeDecomposer primeDecomposer)
        {
            _primeDecomposer = primeDecomposer;
        }

        public int NumberOfDivisors(int number)
        {
            var decomposition = _primeDecomposer.CalculateDecomposition(number);
            return NumberOfDivisors(decomposition);
        }

        public int NumberOfDivisors(Dictionary<int, int> decomposition)
        {
            // if the decomposition only consists of the trivial prime 1 then return 1
            if (decomposition.Count == 1 && decomposition.First().Key == 1)
                return 1;

            var D = decomposition.Select(e => e.Value + 1)
                .Aggregate((x, y) => x * y);

            return D;
        }

        public HashSet<int> GetDivisors(int number)
        {
            var decomposition = _primeDecomposer.CalculateDecomposition(number);
            return GetDivisors(decomposition);
        }

        public HashSet<int> GetProperDivisors(int n)
        {
            var divisors = GetDivisors(n);
            divisors.Remove(n);
            return divisors;
        }


        public HashSet<int> GetDivisors(Dictionary<int, int> decomposition)
        {
            // Calculate a helper array
            var darr = decomposition.ToArray();
            var len = decomposition.Count;
            int[] m1 = new int[len]; // max exponent + 1
            int[] t = new int[len]; // cumulative products of m1 cells
            for (int i = 0; i < len; i++)
            {
                m1[i] = (int)darr[i].Value + 1;
                if (i == 0)
                    t[i] = 1;
                else
                    t[i] = m1[i - 1] * t[i - 1];
            }

            // Calculate the divisors
            int n = NumberOfDivisors(decomposition);
            HashSet<int> divisors = new HashSet<int>();
            for (int i = 0; i < n; i++)
            {
                int temp = 1;
                for (int j = 0; j < len; j++)
                {
                    int exponent = (i / t[j]) % m1[j];
                    temp *= MathExt.Pow(darr[j].Key, exponent);
                }
                divisors.Add(temp);
            }
            return divisors;
        }
    }
}