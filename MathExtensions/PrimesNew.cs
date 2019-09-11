using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathExtensions
{
    /// <summary>
    /// Generates N first prime numbers
    /// </summary>
    public class PrimesNew : IPrimes
    {
        private readonly long _n;
        private readonly PrimesGenerationRule _rule;

        private PrimesNew(long n, PrimesGenerationRule rule)
        {
            _n = n;
            _rule = rule;
        }

        public static PrimesNew Create(long n, PrimesGenerationRule rule = PrimesGenerationRule.GenerateNPrimes)
        {
            return new PrimesNew(n, rule);
        }

        public IEnumerator<long> GetEnumerator()
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

        private static IEnumerator<long> GetNPrimes(long n)
        {
            List<long> primes = new List<long>();
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

        private static IEnumerable<long> GetPrimeCandidates()
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

        private static IEnumerator<long> GetUpToNPrimes(long n)
        {
            if (n >= 2) yield return 2;
            if (n >= 3) yield return 3;
            if (n >= 5)
            {
                for (int k = 1; 6 * k - 1 <= n; k++)
                {
                    long lvalue = 6 * k - 1;
                    if (MathExt.IsPrime(lvalue)) yield return lvalue;
                    long rvalue = 6 * k + 1;
                    if (rvalue <= n && MathExt.IsPrime(rvalue)) yield return rvalue;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public enum PrimesGenerationRule
    {
        GenerateNPrimes = 0,
        GenaratePrimesUpToN = 1
    }

}
