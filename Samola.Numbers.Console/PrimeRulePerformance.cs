using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Samola.Numbers.Primes;

namespace Samola.Numbers
{
    class PrimeRulePerformance : IConsoleExcutable
    {
        public string ExecutableName => "Measure performances of different prime generation rules.";

        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            Dictionary<string, long[]> totals = new Dictionary<string, long[]>();
            int reps = 10;
            int[] dataPoints = new int[] { 10, 100, 500, 1000, 5000, 10000, 30000, 70000, 100000, 150000, 300000 };

            double totalN = 0;
            double totalU = 0;
            foreach (var dataPoint in dataPoints)
            {
                List<long> timesN = new List<long>(reps);
                List<long> timesU = new List<long>(reps);
                int[] Nprimes = null;
                int[] Uprimes = null;
                for (int i = 0; i < reps; i++)
                {
                    var primesN = PrimesNew.Create(dataPoint, PrimesGenerationRule.GenerateNPrimes);
                    stopwatch.Restart();
                    Nprimes = primesN.ToArray();
                    stopwatch.Stop();
                    timesN.Add(stopwatch.ElapsedMilliseconds);

                    var primesU = PrimesNew.Create(Nprimes.Last(), PrimesGenerationRule.GenaratePrimesUpToN);
                    stopwatch.Restart();
                    Uprimes = primesU.ToArray();
                    stopwatch.Stop();
                    timesU.Add(stopwatch.ElapsedMilliseconds);
                }
                totalN += timesN.Average();
                totalU += timesU.Average();
                Console.WriteLine($"NPrimes with {dataPoint,7}th prime = {Nprimes.Last()}: Min: {timesN.Min()} Avg: {timesN.Average()} Max: {timesN.Max()} (ms).");
                Console.WriteLine($"Primes counted up to number  = {Uprimes.Last()}: Min: {timesU.Min()} Avg: {timesU.Average()} Max: {timesU.Max()} (ms).");
            }

            Console.WriteLine($"Total execution time for NPrime: {totalN}");
            Console.WriteLine($"Total execution time for UPrime: {totalU}");
        }
    }
}
