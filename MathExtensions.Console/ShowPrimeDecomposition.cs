using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions
{
    class ShowPrimeDecomposition : IConsoleExcutable
    {
        public string ExecutableName => "Print prime decompositions up to a number";

        public void Run()
        {
            PrimeDecomposer primeDecomposer = new PrimeDecomposer(new Primes(200));

            Console.Write("Print decompositions up to > ");
            int upTo = Int32.Parse(Console.ReadLine());
            for (int i = 1; i <= upTo; i++)
            {
                var decomposition = primeDecomposer.CalculateDecomposition(i);
                Console.Write($"{i} : ");
                foreach (var factor in decomposition)
                {
                    Console.Write($"({factor.Key}, {factor.Value}) ");
                }
                Console.WriteLine();
            }
        }
    }
}
