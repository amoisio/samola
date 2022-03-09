using System;
using System.Linq;
using Samola.Numbers.Enumerables;

namespace Samola.Numbers
{
    public class ShowDecimalDigits : IConsoleExcutable
    {
        public string ExecutableName => "Show decimal digits";

        public void Run()
        {
            Console.Write("Maximum terms > ");
            int max = Int32.Parse(Console.ReadLine());


            for (int i = 2; i <= max; i++)
            {
                DecimalDigits digits = new DecimalDigits(i);
                var arr = digits.ToArray();

                int len = digits.HasRecurrence
                    ? digits.RecurringDecimalFraction.Length
                    : 0;

                Console.WriteLine($"{i,-4}: {len,4}: 0.{digits.DecimalPart}");
            }
        }
    }
}
