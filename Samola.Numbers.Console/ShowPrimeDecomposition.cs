using System;
using System.Collections.Generic;
using System.Text;
using Samola.Numbers.Construction;
using Samola.Numbers.Enumerables;
using Samola.Numbers.Primes;
using Samola.Numbers.Utilities;

namespace Samola.Numbers
{
    class ShowPrimeDecomposition : IConsoleExcutable
    {
        public string ExecutableName => "Print prime decompositions up to a number";

        public void Run()
        {
            Console.Write("Print decompositions up to > ");
            int upTo = Int32.Parse(Console.ReadLine());

            MaxValueLimit maxValueLimit = new MaxValueLimit(upTo);
            PrimeDecomposer primeDecomposer = new PrimeDecomposer(maxValueLimit);

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
