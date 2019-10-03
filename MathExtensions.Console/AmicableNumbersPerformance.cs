using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MathExtensions.Construction;
using MathExtensions.Enumerables;
using MathExtensions.Primes;
using MathExtensions.Utilities;

namespace MathExtensions
{
    public class AmicableNumbersPerformance : IConsoleExcutable
    {
        public string ExecutableName => "Measure amicable number computation performance.";

        public void Run()
        {
            Console.Write("Compute amicable numbers up to > ");
            int number = Int32.Parse(Console.ReadLine());

            var maxValueLimit = new MaxValueLimit(number);
            var decomposer = new PrimeDecomposer(maxValueLimit);
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
