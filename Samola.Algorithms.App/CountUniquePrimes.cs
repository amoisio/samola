using System;
using System.Collections.Generic;
using System.Linq;
using Samola.Algorithms.Sequences;
using Samola.Algorithms.Utilities;

namespace Samola.Algorithms.App
{
    class CountUniquePrimes : IConsoleExcutable
    {
        public string ExecutableName => "Counts unique prime numbers in prime decompositions";

        public void Run()
        {
            int start = 2;
            Console.Write($"Count from {start} up to > ");
            int upTo = Int32.Parse(Console.ReadLine());

            var primes = new PrimeNumbers6k();
            var primeDecomposer = new PrimeDecomposer(primes);

            var result = new Dictionary<int, int>(1000);

            for (int i = start; i <= upTo; i++)
            {
                var decomposition = primeDecomposer.CalculateDecomposition(i);
                foreach (var pair in decomposition)
                {
                    if (result.ContainsKey(pair.Key))
                        result[pair.Key] += pair.Value;
                    else
                        result.Add(pair.Key, pair.Value);
                }
            }

            Console.WriteLine($"Count of primes: {result.Select(e => e.Value).Sum()}");
            Console.WriteLine($"Weighted average: {result.Select(e => e.Key * e.Value).Average()}");

            var nn = result.OrderByDescending(e => e.Value).TakeWhile(e => e.Value > 0.01 * upTo);
            Console.WriteLine($"99% of primes consist of {nn.Count()} distinct primes.");
            foreach (var prime in nn)
            {
                Console.WriteLine($"{prime.Key,-4}: {prime.Value}");
            }
        }
    }
}
