using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Samola.Numbers.Primes.Generators;

namespace Samola.Numbers
{
    class IsPrimePerformance : IConsoleExcutable
    {
        public string ExecutableName => "Measure performances of different IsPrime tests.";

        public void Run()
        {
            var totals = new Dictionary<string, long[]>();
            int count = 2000000;
            int reps = 10;

            // Numbers to test
            var numbers = Enumerable.Range(1, count);

            var simple = new PrimesSimple();
            totals.Add("Simple", Perforce("Simple", simple.IsPrime, reps, numbers, 1));
            var sixk = new Primes6k();
            totals.Add("6k", Perforce("6k", sixk.IsPrime, reps, numbers, 4));

            Console.WriteLine();
            foreach (var total in totals)
            {
                Console.WriteLine($"{total.Key,-4}: Avg: {total.Value.Average()}ms.");
            }
        }


        private static long[] Perforce(string label, Func<int, bool> primeFn, int reps, IEnumerable<int> numbers, int y)
        {
            Console.WriteLine($"{label}:");

            var stopwatch = new Stopwatch();
            var timings = new List<long>();

            for (int i = 0; i < reps; i++)
            {
                Console.SetCursorPosition(label.Length + 2, y);
                Console.Write($"{i + 1}:");
                stopwatch.Restart();
                foreach (var number in numbers)
                {
                    primeFn(number);
                }
                timings.Add(stopwatch.ElapsedMilliseconds);
            }
            stopwatch.Stop();
            Console.WriteLine();
            return timings.ToArray();
        }
    }
}
