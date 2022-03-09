using System;
using System.Collections.Generic;
using System.Text;
using Samola.Numbers.Construction;
using Samola.Numbers.Enumerables;
using Samola.Numbers.Primes;
using Samola.Numbers.Utilities;
using System.Linq;
using Samola.Numbers.Counters;

namespace Samola.Numbers
{
    class ShowDigitPowerWalk : IConsoleExcutable
    {
        public string ExecutableName => "Find values equal to their digit powers.";

        private int GetMaximumDigit(int power)
        {
            int k = 0;
            int lowerBound = 0;
            int upperBound = 0;
            int maximumValue = 0;

            while (lowerBound <= maximumValue)
            {
                k++;
                // Lower and upper bounds of a k digit number
                lowerBound = (int)Math.Pow(10, k - 1);
                upperBound = (int)Math.Pow(10, k) - 1;

                // Maximum possible value of the sum of individual digits of a k digit number raised to power p
                maximumValue = k * (int)Math.Pow(9, power);

                Console.WriteLine($"{k} : [{lowerBound}, {upperBound}] [1, {maximumValue}]");
            }
            Console.WriteLine(k - 1);

            return k - 1;
        }

        public void Run()
        {
            Console.Write("Power >");
            int power = Int32.Parse(Console.ReadLine());

            int maxDigits = GetMaximumDigit(power);

            for (int numberOfDigits = 1; numberOfDigits <= maxDigits; numberOfDigits++)
            {
                Func<int, int> mapper = (d => (int)Math.Pow(d, power));
                var counter = new DialCounter(numberOfDigits, mapper);
                int lowerBound = (int)Math.Pow(10, numberOfDigits - 1);
                int upperBound = (int)Math.Pow(10, numberOfDigits) - 1;

                while (!counter.IsCounterEmpty)
                {
                    int sum = counter.Sum;
                    if (sum >= lowerBound && sum <= upperBound)
                    {
                        var dialDigits = counter.DialDigits;
                        var sumDigits = sum.ToDigits();

                        var ok = dialDigits.ContainsSameItems(sumDigits);

                        if (ok && sum != 1)
                        {
                            var dial = counter.ToString();
                            Console.WriteLine($"Sum: {sum} - {dial}");
                        }
                    }

                    counter.Roll();
                }
            }
        }


    }
}
