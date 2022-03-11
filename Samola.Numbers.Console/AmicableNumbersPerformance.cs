﻿using System;
using System.Diagnostics;
using Samola.Numbers.Primes;
using Samola.Numbers.Primes.Generators;
using Samola.Numbers.Utilities;

namespace Samola.Numbers
{
    public class AmicableNumbersPerformance : IConsoleExcutable
    {
        public string ExecutableName => "Measure amicable number computation performance.";

        public void Run()
        {
            Console.Write("Compute amicable numbers up to > ");
            int number = Int32.Parse(Console.ReadLine());

            var primes = new Primes6k();
            var decomposer = new PrimeDecomposer(primes);
            var divisor = new DivisorCalculator(decomposer);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            var amicableNumber = new AmicableNumberCalculator(divisor);

            for (int i = 1; i <= number; i++)
            {
                var aNumber = amicableNumber.FindAmicableNumber(i);

                if (aNumber.HasValue)
                {
                    Console.WriteLine($"{i,4} <-A-> {aNumber}");
                }
            }
            stopwatch.Stop();
            Console.WriteLine($"Computation took: {stopwatch.ElapsedMilliseconds} ms.");
        }
    }
}
