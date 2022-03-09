using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Samola.Numbers.Utilities;

namespace Samola.Numbers.Tests
{
    public class NumberExtensionsTests
    {
        [Theory]
        [InlineData(0123, 3)]
        [InlineData(+83723, 5)]
        [InlineData(-14, 2)]
        [InlineData(0, 1)]
        [InlineData(-0317, 3)]
        [InlineData(1000, 4)]
        public void NumberOfDigits_correctly_calculates_the_number_or_digits(int value, int expected)
        {
            Assert.Equal(expected, value.NumberOfDigits());
        }

        [Theory]
        [InlineData(123, new int[] { 3, 2, 1 })]
        [InlineData(014, new int[] { 4, 1 })]
        [InlineData(987532, new int[] { 2, 3, 5, 7, 8, 9 })]
        public void IntToArray_splits_integer_digits_into_separate_numbers(int value, int[] expectedDigits)
        {
            var digits = value.ToDigits();

            Assert.Equal(expectedDigits, digits);
        }
    }
}
