using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Samola.Numbers.Primes;
using Samola.Numbers.Construction;
using Samola.Numbers.Utilities;
using Samola.Numbers.Enumerables;

namespace Samola.Numbers
{
    public class ShowDivisors : IConsoleExcutable
    {
        public string ExecutableName => "Show divisors";

        public void Run()
        {
            Console.Write("Print divisors for > ");
            int number = Int32.Parse(Console.ReadLine());
            //new MaxValueLimit(number)
            var primeDecomposer = new PrimeDecomposer();
            var divisorCalculator = new DivisorCalculator(primeDecomposer);

            

            // Calculate the prime decomposition of the number
            var decomposition = primeDecomposer.CalculateDecomposition(number);

            // Calculate a helper array
            var darr = decomposition.ToArray();
            int len = decomposition.Count;
            long[] m1 = new long[len]; // max exponent + 1
            long[] t = new long[len]; // cumulative products of m1 cells
            for (int i = 0; i < len; i++)
            {
                m1[i] = darr[i].Value + 1;
                if (i == 0)
                    t[i] = 1;
                else
                    t[i] = m1[i-1] * t[i-1];
            }

            // Calculate the divisors
            long n = divisorCalculator.NumberOfDivisors(decomposition);
            long[] divisors = new long[n];
            for (int i = 0; i < n; i++)
            {
                long temp = 1L;
                for(int j = 0; j < len; j++)
                {
                    long exponent = (i / t[j]) % m1[j];
                    temp *= (long)Math.Pow(darr[j].Key, exponent);
                }
                divisors[i] = temp;
            }

            foreach (var divisor in divisors.OrderBy(e => e))
            {
                Console.WriteLine(divisor);
            }
        }
    }
}
