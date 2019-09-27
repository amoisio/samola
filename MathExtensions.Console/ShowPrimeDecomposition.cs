using System;
using System.Collections.Generic;
using System.Text;
using MathExtensions.Construction;
using MathExtensions.Primes;

namespace MathExtensions
{
    class ShowPrimeDecomposition : IConsoleExcutable
    {
        public string ExecutableName => "Print prime decompositions up to a number";

        public void Run()
        {
            var primesCreator = new Primes6kFactory(200, true);
            PrimeDecomposer primeDecomposer = new PrimeDecomposer(primesCreator);

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
