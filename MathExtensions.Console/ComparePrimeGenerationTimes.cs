using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace MathExtensions
{
    public class ComparePrimeGenerationTimes : IConsoleExcutable
    {
        public string ExecutableName => "Compare prime generation times";

        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();
            for (int k = 0; k < 20; k++)
            {
                int n = (int)Math.Pow(10, 5) * (k + 1);
                Primes control = new Primes(n);
                PrimesNew nprimes = PrimesNew.Create(n, PrimesGenerationRule.GenerateNPrimes);

                stopwatch.Restart();
                var ccount = control.ToArray();
                stopwatch.Stop();
                Console.WriteLine($"Control (10^{k}): {stopwatch.ElapsedMilliseconds}ms");

                stopwatch.Restart();
                var ncount = nprimes.ToArray();
                stopwatch.Stop();
                Console.WriteLine($"NPrimes (10^{k}): {stopwatch.ElapsedMilliseconds}ms");
            }
        }
    }
}
