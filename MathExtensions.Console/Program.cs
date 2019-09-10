using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MathExtensions
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
            _menu.Executables.Add(new ComputePrimes());
            _menu.Executables.Add(new ComparePrimeGenerationTimes());
        }
    }
}
