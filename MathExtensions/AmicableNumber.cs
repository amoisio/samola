using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathExtensions
{
    public static class AmicableNumber
    {
        public static long? FindAmicableNumber(long number)
        {
            var divisors = Divisors.GetProperDivisors(number);
            var sumNumber = divisors.Sum(); // a = number, d(a) = b = sumNumber
            var sumDivisors = Divisors.GetProperDivisors(sumNumber);
            var sumSum = sumDivisors.Sum(); // d(b) = sumSum

            if (number != sumNumber && number == sumSum)
            {
                return sumNumber;
            }
            else
            {
                return null;
            }
        }
    }
}
