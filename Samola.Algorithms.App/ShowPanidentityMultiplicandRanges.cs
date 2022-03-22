using System;
using Samola.Algorithms.Utilities;
using Samola.Extensions;

namespace Samola.Algorithms.App
{
    class ShowMultiplicandRanges : IConsoleExcutable
    {
        public string ExecutableName => "Show Panidentity multiplicand ranges";

        public void Run()
        {
            int digits = 9;
            Console.WriteLine($"Digits: {digits}");

            for (int multiplier = 2; multiplier < 100; multiplier++)
            {
                var range = MiscellaneousUtilities.ComputeMultiplicandRangeForPandigitalIdentity(multiplier, digits);

                int totalDigits1 = multiplier.NumberOfDigits() + range.Item1.NumberOfDigits() + (multiplier * range.Item1).NumberOfDigits();
                int totalDigits2 = multiplier.NumberOfDigits() + range.Item2.NumberOfDigits() + (multiplier * range.Item2).NumberOfDigits();

                Console.WriteLine($"{multiplier} x [{range.Item1} {range.Item2}] = [{multiplier * range.Item1} {multiplier * range.Item2}] - Digits: [{totalDigits1} {totalDigits2}]");
            }
        }
    }
}