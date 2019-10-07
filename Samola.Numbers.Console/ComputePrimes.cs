using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathExtensions.Primes;

namespace MathExtensions
{
    public class ComputePrimes : IConsoleExcutable
    {
        public string ExecutableName => "Compute primes with 3 different ways";

        public void Run()
        {
            List<Tuple<long, long, long>> primes = new List<Tuple<long, long, long>>();

            Console.WriteLine("Enter the number of primes to compute.");
            Console.Write("> ");
            int n = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Enter the upper limit of primes to compute to.");
            Console.Write("> ");
            int u = Int32.Parse(Console.ReadLine());

            var control = new PrimesSimple(n).ToArray();
            var nprimes = PrimesNew.Create(n, PrimesGenerationRule.GenerateNPrimes).ToArray();
            var uprimes = PrimesNew.Create(u, PrimesGenerationRule.GenaratePrimesUpToN).ToArray();

            var len = control.Length;
            for(int i = 0; i < len; i++)
            {
                long cp = control.Length > i ? control[i] : -1;
                long np = nprimes.Length > i ? nprimes[i] : -1;
                long up = uprimes.Length > i ? uprimes[i] : -1;
                primes.Add(Tuple.Create(cp, np, up));
            }

            Console.WriteLine("Generated primes:");

            foreach(var pval in primes)
            {
                Console.WriteLine($"{pval.Item1, 4}, {pval.Item2, 4}, {pval.Item3, 4}");
            }
        }
    }
}
