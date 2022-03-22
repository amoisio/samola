using System;

namespace Samola.Extensions
{
    public static class NumberExtensions
    {

        /// <summary>
        /// Determines the number of digits in an integer by converting it to a string and calculating the number of characters in the string.
        /// Executes roughly in T = 7,5 timeunits
        /// </summary>
        public static int NumberOfDigits2(this int s)
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
        public static int NumberOfDigits(this int s)
        {
            var t = Math.Abs(s) + 1; // + 1 offsets digit calculation for exact powers of 10
            if (t == 1)
                return 1;
            else
                return (int)Math.Ceiling(Math.Log10(t));
        }

        // Slow
        public static int NumberOfDigits3(this int s)
        {
            var t = Math.Abs(s);
            int digits = 1;

            while ((t = Math.DivRem(t, 10, out _)) > 0)
                digits++;

            return digits;
        }

        public static int[] ToDigits(this int number)
        {
            int digitCount = (int)Math.Floor(Math.Log10(number)) + 1;

            int[] digits = new int[digitCount];

            int temp = number;
            for (int i = 0; i < digitCount; i++)
            {
                temp = Math.DivRem(temp, 10, out int result);
                digits[i] = result;
            }

            return digits;
        }

    }
}
