using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MathExtensions.Primes;

namespace MathExtensions
{
    public class AmicableNumbersPerformance : IConsoleExcutable
    {
        public string ExecutableName => "Measure amicable number computation performance.";

        public void Run()
        {
            Console.Write("Compute amicable numbers up to > ");
            int number = Int32.Parse(Console.ReadLine());

            var primesCreator = new Primes6kFactory(number, true);
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            var amicableNumber = new AmicableNumberCalculator(primesCreator);

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
