using System;
using System.Linq;
using Samola.Collections;
using Samola.Numbers.CustomTypes;
using Samola.Numbers.Fibonacci;

namespace Samola.Numbers.App
{
    public class ShowFibonacci : IConsoleExcutable
    {
        public string ExecutableName => "Show Fibonacci numbers";

        public void Run()
        {
            Console.Write("Maximum terms > ");
            int max = Int32.Parse(Console.ReadLine());

            Console.Write("Display all (Y) > ");
            bool displayAll = Console.ReadLine() == "Y";

            var limit = new MaximumYieldedCountLimit<LargeInteger>(5000); // At most 5000 Fibonacci terms
            var numbers = new LargeFibonacciNumbers(limit);

            if (displayAll)
            {
                int index = 1;
                foreach (var number in numbers)
                {
                    Console.WriteLine($"F{index,-4}: {number}");
                    if (number.ToString().Length == max)
                        break;
                    index++;
                }
            }
            else
            {
                var terms = numbers.TakeWhile(n => n.ToString().Length <= max).ToArray();

                var termsLessThan = terms.Where(n => n.ToString().Length < max).ToArray();
                var len = termsLessThan.Length;
                var a = termsLessThan[len - 2];
                var b = termsLessThan[len - 1];
                var d = a + b;
                Console.WriteLine(termsLessThan[len - 2].ToString());
                Console.WriteLine(termsLessThan[len - 1].ToString());
                Console.WriteLine(terms.ToArray()[len].ToString());
                Console.WriteLine(len + 1);
            }
        }
    }
}
