using System.Collections.Generic;
using System.Linq;

namespace Samola.Numbers.Utilities
{
    public static class Combinatorics
    {
        public static long Factorial(int n)
        {
            long temp = 1;
            for(int i = 2; i <= n; i++)
            {
                temp *= i;
            }
            return temp;
        }

        public static long Combinations(int n, int k)
        {
            // Build nominators and denominators
            int smaller, larger;
            
            if (k > n - k)
            {
                smaller = n - k;
                larger = k;
            } else
            {
                smaller = k;
                larger = n - k;
            }

            long[] nominators = Range(larger + 1, n - larger).OrderByDescending(e => e).ToArray();
            long[] denominators = Range(1, smaller).OrderByDescending(e => e).ToArray();
            
            // Cancel out single factors
            int nominatorCount = nominators.Length;
            int denominatorCount = denominators.Length;
            for (int i = 0; i < nominatorCount; i++)
            {
                long nominator = nominators[i];
                for (int j = 0; j < denominatorCount; j++)
                {
                    long denominator = denominators[j];
                    if (nominator % denominator == 0)
                    {
                        nominator = nominator / denominator;
                        nominators[i] = nominator;
                        denominators[j] = 1;
                    }
                }
            }

            long fn = nominators.Aggregate((a, b) => a * b);
            long fd = denominators.Aggregate((a, b) => a * b);

            return fn / fd;
        }

        private static IEnumerable<long> Range(long start, long count)
        {
            for (long i = start; i < start + count; i++)
                yield return i;
        }
    }
}
