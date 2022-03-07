using System;
using System.Collections.Generic;

namespace Samola.Numbers.Primes
{
    [Obsolete("Use Primes6k instead")]
    public class PrimesNew : PrimesBase
    {
        private readonly int _n;
        private readonly PrimesGenerationRule _rule;

        /// <summary>
        /// Does not use cache.
        /// </summary>
        private PrimesNew(int n, PrimesGenerationRule rule) : base(n, false)
        {
            _n = n;
            _rule = rule;
        }

        public static PrimesNew Create(int n, PrimesGenerationRule rule = PrimesGenerationRule.GenerateNPrimes)
        {
            return new PrimesNew(n, rule);
        }

        /// <summary>
        /// Does not utilize the 'from' argument.   
        /// </summary>
        protected override IEnumerable<int> GetPrimes(int[] previousPrimes)
        {
            switch (_rule)
            {
                default:
                case PrimesGenerationRule.GenerateNPrimes:
                    return GetNPrimes(_n);
                case PrimesGenerationRule.GenaratePrimesUpToN:
                    return GetUpToNPrimes(_n);
            }
        }

        // TODO: Add comments
        // TODO: What if we run out of primes because of too low n?
        private static IEnumerable<int> GetNPrimes(int n)
        {
            List<int> primes = new List<int>();
            int generated = 0;
            foreach(var candidate in GetPrimeCandidates())
            {
                bool isPrime = false;
                foreach(var prime in primes)
                {
                    if (prime * prime > candidate)
                    {
                        isPrime = true;
                        break;
                    }

                    if (candidate % prime == 0)
                    {
                        isPrime = false;
                        break;
                    }
                }

                if (primes.Count == 0)
                    isPrime = true;

                if (isPrime)
                {
                    primes.Add(candidate);
                    yield return candidate;
                    if (++generated >= n) yield break;
                }
            }
        }

        private static IEnumerable<int> GetPrimeCandidates()
        {
            yield return 2;
            yield return 3;
            int k = 1;
            while (true)
            {
                yield return 6 * k - 1;
                yield return 6 * k + 1;
                k++;
            }
        }

        private static IEnumerable<int> GetUpToNPrimes(int n)
        {
            if (n >= 2) yield return 2;
            if (n >= 3) yield return 3;
            if (n >= 5)
            {
                for (int k = 1; 6 * k - 1 <= n; k++)
                {
                    int lvalue = 6 * k - 1;
                    if (MathExt.IsPrime(lvalue)) yield return lvalue;
                    int rvalue = 6 * k + 1;
                    if (rvalue <= n && MathExt.IsPrime(rvalue)) yield return rvalue;
                }
            }
        }
    }
}
