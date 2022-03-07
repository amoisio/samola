using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Samola.Numbers
{
    class IsPrimePerformance : IConsoleExcutable
    {
        public string ExecutableName => "Measure performances of different IsPrime tests.";

        public void Run()
        {
            Dictionary<string, long[]> totals = new Dictionary<string, long[]>();
            int count = 2000000;
            int reps = 10;

            // Numbers to test
            var numbers = Enumerable.Range(1, count);

            totals.Add("Simple", Perforce("Simple", MathExt.IsPrimeSimple, reps, numbers, 1));
            totals.Add("Cached", Perforce("Cached", MathExt.IsPrimeCached, reps, numbers, 2));
            totals.Add("CachedNoLocks", Perforce("Cached No Locks", MathExt.IsPrimeCachedNoLocks, reps, numbers, 3));
            totals.Add("Simple6k", Perforce("Simple6k", MathExt.IsPrimeSimple6k, reps, numbers, 4));
            totals.Add("Simple6kCache", Perforce("Simple6k Cached", MathExt.IsPrimeSimple6kCached, reps, numbers, 5));

            Console.WriteLine();
            foreach (var total in totals)
            {
                Console.WriteLine($"{total.Key,-4}: Avg: {total.Value.Average()}ms.");
            }
        }


        private static long[] Perforce(string label, Func<long, bool> primeFn, int reps, IEnumerable<int> numbers, int y)
        {
            Console.WriteLine($"{label}:");

            Stopwatch stopwatch = new Stopwatch();
            List<long> timings = new List<long>();

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
