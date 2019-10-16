using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Samola.Numbers.Utilities
{
    public static class NumberExtensions
    {

        /// <summary>
        /// Determines the number of digits in an integer by converting it to a string and calculating the number of characters in the string.
        /// Executes roughly in T = 7,5 timeunits
        /// </summary>
        public static int NumberOfDigits(this int s)
        {
            if (s == 0)
                return 1;
            else
                return Math.Abs(s).ToString().Length;
        }

        /// <summary>
        /// Determines the number of digits in an integer by using base-10 logarithm.
        /// Executes roughly in T = 3,9 timeunits
        /// </summary>
        public static int NumberOfDigits2(this int s)
        {
            var t = Math.Abs(s);
            if (t == 1 || t == 0)
                return 1;
            else
                return (int)Math.Ceiling(Math.Log10(t));
        }

        // Slow
        public static int NumberOfDigits3(this int s)
        {
            var t = Math.Abs(s);
            int digits = 1;

            while ((t = Math.DivRem(t, 10, out int remainder)) > 0)
                digits++;

            return digits;
        }

    }
}
