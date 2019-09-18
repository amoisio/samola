using MathExtensions.Primes;
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
            for (int k = 0; k < 10; k++)
            {
                int n = (int)Math.Pow(10, 5) * (k + 1);
                var control = new PrimesSimple(n);
                var nprimes = PrimesNew.Create(n, PrimesGenerationRule.GenerateNPrimes);
                var cprimes = Primes6k.Create(n, true);

                stopwatch.Restart();
                var ccount = control.ToArray();
                stopwatch.Stop();
                Console.WriteLine($"Control (10^{k}): {stopwatch.ElapsedMilliseconds}ms");

                stopwatch.Restart();
                var ncount = nprimes.ToArray();
                stopwatch.Stop();
                Console.WriteLine($"NPrimes (10^{k}): {stopwatch.ElapsedMilliseconds}ms");

                stopwatch.Restart();
                var cacount = cprimes.ToArray();
                stopwatch.Stop();
                Console.WriteLine($"CPrimes (10^{k}): {stopwatch.ElapsedMilliseconds}ms");
            }
        }
    }
}
