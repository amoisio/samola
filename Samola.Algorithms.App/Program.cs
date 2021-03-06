using System;

namespace Samola.Algorithms.App
{
    class Program
    {
        private static Menu _menu;

        static void Main(string[] args)
        {
            _menu = new Menu();
            RegisterExecutables();

            _menu.ReadAndExecute();

            Console.WriteLine("Press any key to end...");
            Console.ReadKey();
        }

        private static void RegisterExecutables()
        {
            _menu.Executables.Add(new IsPrimePerformance());
            _menu.Executables.Add(new ShowPrimeDecomposition());
            _menu.Executables.Add(new ShowDivisors());
            _menu.Executables.Add(new AmicableNumbersPerformance());
            _menu.Executables.Add(new PrimeGenerationPerformance());
            _menu.Executables.Add(new ShowFibonacci());
            _menu.Executables.Add(new NumberOfDigitsPerformance());
            _menu.Executables.Add(new ShowDecimalDigits());
            _menu.Executables.Add(new CountUniquePrimes());
            _menu.Executables.Add(new ShowDigitPowerWalk());
            _menu.Executables.Add(new ShowMultiplicandRanges());
        }
    }
}
