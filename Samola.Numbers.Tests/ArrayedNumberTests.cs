using Samola.Numbers.CustomTypes;
using Samola.Numbers.Utilities;
using System;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class ArrayedNumberTests
    {
        [Theory]
        [InlineData(57, 7, 5)]
        [InlineData(124, 4, 2, 1)]
        [InlineData(9836, 6, 3, 8, 9)]
        [InlineData(30917, 7, 1, 9, 0, 3)]
        [InlineData(00007, 7)]
        public void ArrayedNumbers_represent_numbers_in_array_of_digits(int number, params int[] digits)
        {
            ArrayedNumber anumber = new ArrayedNumber(number);

            Assert.Equal(digits, anumber.digits);
        }

        [Theory]
        [InlineData(123, 56, 179)]
        [InlineData(13, 5621, 5634)]
        [InlineData(764, 347, 1111)]
        [InlineData(754, 150, 904)]
        public void ArrayedNumbers_can_be_added_together(int left, int right, int expected)
        {
            ArrayedNumber aLeft = new ArrayedNumber(left.ToDigits());
            ArrayedNumber aRight = new ArrayedNumber(right.ToDigits());
            ArrayedNumber aExpected = new ArrayedNumber(expected.ToDigits());

            var sum = aLeft + aRight;
            Assert.Equal(aExpected.digits, sum.digits);
        }

        [Fact]
        public void ArrayedNumbers_can_add_values_over_type_max()
        {
            int max = Int32.MaxValue;
            ArrayedNumber aLeft = new ArrayedNumber(max.ToDigits());
            ArrayedNumber aRight = new ArrayedNumber(353.ToDigits());

            var sum = aLeft + aRight;
            Assert.Equal(new int[] { 0, 0, 0, 4, 8, 4, 7, 4, 1, 2 }, sum.digits);
        }

        [Theory]
        [InlineData(8, 12, 96)]
        [InlineData(13, 4, 52)]
        [InlineData(40, 39, 1560)]
        public void ArrayedNumbers_can_be_multiplied_together(int left, int right, int expected)
        {
            ArrayedNumber aLeft = new ArrayedNumber(left.ToDigits());
            ArrayedNumber aExpected = new ArrayedNumber(expected.ToDigits());

            ArrayedNumber product = aLeft * right;
            Assert.Equal(aExpected.digits, product.digits);
        }

        [Fact]
        public void ArrayedNumbers_can_multiply_values_over_type_max()
        {
            int max = Int32.MaxValue;
            ArrayedNumber aLeft = new ArrayedNumber(max.ToDigits());
            int rightop = 2;

            var product = aLeft * rightop;
            Assert.Equal(new int[] { 4, 9, 2, 7, 6, 9, 4, 9, 2, 4 }, product.digits);
        }
    }
}
