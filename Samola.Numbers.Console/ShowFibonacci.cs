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
            builder.Limit = new LargeIntegerCountLimit(5000);

            var numbers = builder.Build();
            LargeInteger previous;

            int c = max;
            var arr = numbers.TakeWhile(n => n.ToString().Length <= c).ToArray();

            var arr1 = arr.Where(n => n.ToString().Length < c).ToArray();
            var len = arr1.Length;
            var a = arr1[len - 2];
            var b = arr1[len - 1];
            var d = a + b;
            Console.WriteLine(arr1[len - 2].ToString());
            Console.WriteLine(arr1[len - 1].ToString());
            Console.WriteLine(arr.First(n => n.ToString().Length == c).ToString());


            //foreach (var number in numbers)
            //{
            //    if (number.ToString().Length == 10)
            //    {
            //        Console.WriteLine($"{previous.ToString()}");
            //        Console.WriteLine($"{number.ToString()}");
            //        break;
            //    }

            //    previous = number;

            //}
        }
    }
}
