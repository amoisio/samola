using System.Diagnostics;
using Samola.Numbers.Utilities;

namespace Samola.Numbers
{
    class NumberOfDigitsPerformance : IConsoleExcutable
    {
        public string ExecutableName => "Measure difference in NumberOfDigits performances";

        public void Run()
        {
            Stopwatch stopwatch = new Stopwatch();

            int max = 100000000;
            for (int j = 1000; j <= max; j *= 10)
            {
                stopwatch.Restart();
                for (int i = 1; i <= j; i++)
                {
                    i.NumberOfDigits2();
                }
                stopwatch.Stop();
                System.Console.WriteLine($"NumberOfDigits ({j}) : {stopwatch.ElapsedMilliseconds}ms");

                stopwatch.Restart();
                for (int i = 1; i <= j; i++)
                {
                    i.NumberOfDigits();
                }
                stopwatch.Stop();
                System.Console.WriteLine($"NumberOfDigits2({j}) : {stopwatch.ElapsedMilliseconds}ms");

                stopwatch.Restart();
                for (int i = 1; i <= j; i++)
                {
                    i.NumberOfDigits3();
                }
                stopwatch.Stop();
                System.Console.WriteLine($"NumberOfDigits3({j}) : {stopwatch.ElapsedMilliseconds}ms");
            }
        }
    }
}
