using MathExtensions.Primes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MathExtensions
{
    class PrimeGenerationPerformance : IConsoleExcutable
    {
        public string ExecutableName => "Measure consecutive prime generation performances.";

        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();

            int[] dataPoints = new int[] { 10, 100, 500, 1000, 5000, 10000, 30000, 70000, 100000, 150000, 300000 };

            Console.WriteLine("Prime generation with provider PrimesFactory.Create");
            foreach (var dataPoint in dataPoints)
            {
                var primesN = PrimesNew.Create(dataPoint, PrimesGenerationRule.GenerateNPrimes);

                stopwatch.Restart();
                foreach (var prime in primesN) { }
                stopwatch.Stop();
                long totalN = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();
                foreach (var prime in primesN) { }
                stopwatch.Stop();
                long totalU = stopwatch.ElapsedMilliseconds;

                Console.WriteLine($"Datapoints {dataPoint,6}. First iteration : {totalN}ms. Second iteration : {totalU} ms.");
            }

            Console.WriteLine("Prime generation with PrimesNewCached without cache");
            foreach (var dataPoint in dataPoints)
            {
                var primesN = Primes6k.Create(dataPoint, false);

                stopwatch.Restart();
                foreach (var prime in primesN) { }
                stopwatch.Stop();
                long totalN = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();
                foreach (var prime in primesN) { }
                stopwatch.Stop();
                long totalU = stopwatch.ElapsedMilliseconds;

                Console.WriteLine($"Datapoints {dataPoint, 6}. First iteration : {totalN}ms. Second iteration : {totalU} ms.");
            }

            Console.WriteLine("Prime generation with PrimesNewCached with cache");
            foreach (var dataPoint in dataPoints)
            {
                var primesN = Primes6k.Create(dataPoint, true);

                stopwatch.Restart();
                foreach (var prime in primesN) { }
                stopwatch.Stop();
                long totalN = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();
                foreach (var prime in primesN) { }
                stopwatch.Stop();
                long totalU = stopwatch.ElapsedMilliseconds;

                Console.WriteLine($"Datapoints {dataPoint,6}. First iteration : {totalN}ms. Second iteration : {totalU} ms.");
            }
        }
    }
}
