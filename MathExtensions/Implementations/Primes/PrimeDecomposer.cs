﻿using MathExtensions.Construction;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions.Primes
{
    /// <summary>
    /// Calculates the prime decomposition of a given number
    /// </summary>
    public class PrimeDecomposer : IPrimeDecomposer
    {
        private readonly IPrimes _primes;
        public PrimeDecomposer(IPrimesCreator primesCreator)
        {
            _primes = primesCreator.Create();
        }

        public static PrimeDecomposer Create(IPrimesCreator primesCreator)
        {
            return new PrimeDecomposer(primesCreator);
        }

        public Dictionary<int, int> CalculateDecomposition(int number)
        {
            var decomposition = new Dictionary<int, int>();
            if (MathExt.IsPrime(number))
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

                    while (temp % prime == 0) {
                        if (decomposition.ContainsKey(prime))
                            decomposition[prime]++;
                        else
                            decomposition.Add(prime, 1);

                        temp = temp / prime;
                    }
                }
            }
            return decomposition;
        }
    }
}
