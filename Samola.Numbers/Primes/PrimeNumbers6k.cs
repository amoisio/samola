using System;
using System.Collections.Generic;
using System.Linq;
using Samola.Collections;

namespace Samola.Numbers.Primes
{
    public class PrimeNumbers6k : PrimeNumbers
    {
        public PrimeNumbers6k(ICalculationLimit<int> limit = null)
            : base(limit)
        {
            
        }

        public override bool IsPrime(int number)
        {
            if (number < 1)
            {
                return false;
            }

            if (number <= 3)
            {
                return true;
            }

            if (number % 2 == 0 || number % 3 == 0)
            {
                return false;
            }

            for (int k = 1; (6 * k - 1) * (6 * k - 1) <= number; k++)
            {
                int lvalue = 6 * k - 1;
                if (number % lvalue == 0)
                    return false;

                int rvalue = 6 * k + 1;
                if (number % rvalue == 0)
                    return false;
            }

            return true;
        }

        protected override int CalculateNext(IReadOnlyList<int> previousItems)
        {
            var lastPrime = previousItems.LastOrDefault();
            if (lastPrime < 2)
            {
                return 2;
            }

            if (lastPrime < 3)
            {
                return 3;
            }

            var (k, a) = DetermineCoefficients(lastPrime);
            foreach (var candidate in GetPrimeCandidates(k, a))
            {
                foreach (var prime in previousItems)
                {
                    if (prime * prime > candidate)
                    {
                        return candidate;
                    }

                    if (candidate % prime == 0)
                    {
                        break;
                    }
                }
            }

            throw new InvalidOperationException("Prime generation failed!");
        }

        /// <summary>
        /// Determine k and a in [prime] = 6 * k + a
        /// </summary>
        private static (int k, int a) DetermineCoefficients(int prime)
        {
            if (prime < 5)
            {
                return (0, 0);
            }

            int k = Math.DivRem(prime + 1, 6, out int remainder);
            if (remainder == 0)
            {
                return (k, -1);
            }

            k = Math.DivRem(prime - 1, 6, out remainder);
            if (remainder == 0)
            {
                return (k, 1);
            }

            throw new InvalidOperationException("6k + a decomposition failed.");
        }

        private static IEnumerable<int> GetPrimeCandidates(int k, int a)
        {
            if (a == -1)
            {
                yield return 6 * k + 1;
            }

            while (true)
            {
                k++;
                yield return 6 * k - 1;
                yield return 6 * k + 1;
            }
        }
    }
}