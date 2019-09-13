using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MathExtensions
{
    public class ShowDivisors : IConsoleExcutable
    {
        public string ExecutableName => "Show divisors";

        public void Run()
        {
            PrimeDecomposer primeDecomposer = new PrimeDecomposer(new Primes(200));

            Console.Write("Print divisors for > ");
            int number = Int32.Parse(Console.ReadLine());

            //Console.Write($"{number} : ");
            //foreach (var factor in decomposition)
            //{
            //    Console.Write($"({factor.Key}, {factor.Value}) ");
            //}

            //int[] a = new int[3] { 0, 1, 2 };
            //int[] b = new int[3] { 0, 1, 2 };
            //int[] c = new int[2] { 0, 1 };

            //List<int[]> l = new List<int[]>()
            //{
            //    a, b, c
            //};

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
            long n = Divisors.NumberOfDivisors(decomposition);
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
