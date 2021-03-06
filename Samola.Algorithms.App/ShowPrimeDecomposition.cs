using System;
using Samola.Algorithms.Sequences;
using Samola.Algorithms.Utilities;

namespace Samola.Algorithms.App
{
    class ShowPrimeDecomposition : IConsoleExcutable
    {
        public string ExecutableName => "Print prime decompositions up to a number";

        public void Run()
        {
            Console.Write("Print decompositions up to > ");
            int upTo = Int32.Parse(Console.ReadLine());

            var primes = new PrimeNumbers6k();
            var primeDecomposer = new PrimeDecomposer(primes);

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
