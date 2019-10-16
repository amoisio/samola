using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Samola.Numbers.Primes;
using Samola.Numbers.Construction;
using Samola.Numbers.Utilities;
using Samola.Numbers.Enumerables;
using Samola.Numbers.CustomTypes;

namespace Samola.Numbers
{
    public class ShowFibonacci : IConsoleExcutable
    {
        public string ExecutableName => "Show Fibonacci numbers";

        public void Run()
        {
            Console.Write("Maximum terms > ");
            int max = Int32.Parse(Console.ReadLine());

            FibonacciNumbersBuilder builder = new FibonacciNumbersBuilder();
            builder.Limit = new LargeIntegerCountLimit(5000); // At most 5000 Fibonacci terms
            var numbers = builder.Build();

            var terms = numbers.TakeWhile(n => n.ToString().Length <= max).ToArray();

            var termsLessThan = terms.Where(n => n.ToString().Length < max).ToArray();
            var len = termsLessThan.Length;
            var a = termsLessThan[len - 2];
            var b = termsLessThan[len - 1];
            var d = a + b;
            Console.WriteLine(termsLessThan[len - 2].ToString());
            Console.WriteLine(termsLessThan[len - 1].ToString());
            Console.WriteLine(terms.ToArray()[len].ToString());
            Console.WriteLine(len);
        }
    }
}
