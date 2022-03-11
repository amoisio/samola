﻿using Samola.Numbers.Primes;
using Samola.Numbers.Primes.Generators;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Samola.Numbers
{
    class PrimeGenerationPerformance : IConsoleExcutable
    {
        public string ExecutableName => "Measure consecutive prime generation performances.";

        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();

            int[] dataPoints = new int[] { 10, 100, 500, 1000, 5000, 10000, 30000, 70000, 100000, 150000, 300000 };

            Console.WriteLine("Prime generation with simple prime algorithm");
            foreach (var dataPoint in dataPoints)
            {
                var primesN = new PrimesSimple();

                stopwatch.Restart();
                var arr1 = primesN.Take(dataPoint).ToArray();
                stopwatch.Stop();
                var totalN = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();
                var arr2 = primesN.Take(dataPoint).ToArray();
                stopwatch.Stop();
                var totalU = stopwatch.ElapsedMilliseconds;

                Console.WriteLine($"Datapoints {dataPoint,6}. First iteration : {totalN}ms. Second iteration : {totalU} ms.");
            }

            Console.WriteLine("Prime generation with 6k prime algorithm");
            foreach (var dataPoint in dataPoints)
            {
                var primesN = new Primes6k();

                stopwatch.Restart();
                var arr1 = primesN.Take(dataPoint).ToArray();
                stopwatch.Stop();
                var totalN = stopwatch.ElapsedMilliseconds;

                stopwatch.Restart();
                var arr12= primesN.Take(dataPoint).ToArray();
                stopwatch.Stop();
                var totalU = stopwatch.ElapsedMilliseconds;

                Console.WriteLine($"Datapoints {dataPoint,6}. First iteration : {totalN}ms. Second iteration : {totalU} ms.");
            }
        }
    }
}
