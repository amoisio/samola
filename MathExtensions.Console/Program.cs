using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MathExtensions
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, long[]> totals = new Dictionary<string, long[]>();
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("IsPrime - performance testing:");

            int count = 2000000;
            int reps = 10;
            var numbers = Enumerable.Range(1, count);

            List<long> timings = new List<long>();
            Console.WriteLine("Current:");

            for (int i = 0; i < reps; i++)
            {
                Console.SetCursorPosition(6, 1);
                Console.Write($"{i + 1} :");
                stopwatch.Restart();
                foreach (var number in numbers)
                {
                    MathExt.IsPrime(number);
                }
                timings.Add(stopwatch.ElapsedMilliseconds);
            }
            stopwatch.Stop();
            totals.Add("Base", timings.ToArray());
            timings.Clear();

            Console.WriteLine();
            Console.Write("Simple :");

            for (int i = 0; i < reps; i++)
            {
                Console.SetCursorPosition(6, 2);
                Console.Write($"{i + 1} :");
                stopwatch.Restart();
                foreach (var number in numbers)
                {
                    MathExt.IsPrimeSimple(number);
                }
                timings.Add(stopwatch.ElapsedMilliseconds);
            }
            stopwatch.Stop();
            totals.Add("V2", timings.ToArray());
            timings.Clear();

            Console.WriteLine();
            foreach (var total in totals)
            {
                Console.WriteLine($"{total.Key,-4}: Avg: {total.Value.Average()}ms.");
            }

            Console.ReadLine();
        }
    }
}
