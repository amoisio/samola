
using Samola.Numbers.Enumerables;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class DecimalDigitsTests
    {
        [Theory]
        [InlineData(2, new int[] { 5 })]
        [InlineData(3, new int[] { 3, 3, 3, 3, 3, 3, 3, 3, 3, 3 })]
        [InlineData(4, new int[] { 2, 5 })]
        [InlineData(5, new int[] { 2  })]
        [InlineData(6, new int[] { 1, 6, 6, 6, 6, 6, 6, 6, 6, 6 })]
        [InlineData(7, new int[] { 1, 4, 2, 8, 5, 7, 1, 4, 2, 8 })]
        [InlineData(8, new int[] { 1, 2, 5 })]
        [InlineData(9, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 })]
        [InlineData(10, new int[] { 1 })]
        public void DecimalNumbers_properly_yield_decimal_digits(int denominator, int[] expected)
        {
            var digits = new DecimalDigits(denominator, tailCount: 10);
            var numbers = new List<int>();

            foreach(var digit in digits)
            {
                numbers.Add(digit);
            }

            Assert.Equal(expected, numbers.ToArray());
        }

        [Theory]
        [InlineData(2, false, null)]
        [InlineData(3, true, "3")]
        [InlineData(4, false, null)]
        [InlineData(5, false, null)]
        [InlineData(6, true, "6")]
        [InlineData(7, true, "142857")]
        [InlineData(8, false, null)]
        [InlineData(9, true, "1")]
        [InlineData(10, false, null)]
        public void DecimalNumbers_properly_identify_if_recurring_decimal_parts_exist(int denominator, bool expectedExists, string recurrenceFraction)
        {
            var digits = new DecimalDigits(denominator, tailCount: 10);
            var numbers = new List<int>();

            var arr = digits.ToArray();

            Assert.Equal(expectedExists, digits.RecurringDecimalFractionFound);
            Assert.Equal(recurrenceFraction, digits.RecurringDecimalFraction);
        }

        [Theory]
        [InlineData(2, "0.5")]
        [InlineData(3, "0.(3)")]
        [InlineData(4, "0.25")]
        [InlineData(5, "0.2")]
        [InlineData(6, "0.1(6)")]
        [InlineData(7, "0.(142857)")]
        [InlineData(8, "0.125")]
        [InlineData(9, "0.(1)")]
        [InlineData(10, "0.1")]
        public void DecimalNumbers_properly_print_the_string_representation_of_the_decimal(int denominator, string expected)
        {
            var digits = new DecimalDigits(denominator, tailCount: 10);
            var numbers = new List<int>();

            var arr = digits.ToArray();

            Assert.Equal(expected, "0." + digits.DecimalPart);
        }
    }
}
