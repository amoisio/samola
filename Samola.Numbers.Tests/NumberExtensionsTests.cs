﻿using System;
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
        public void NumberOfDigits_correctly_calculates_the_number_or_digits(int value, int expected)
        {
            Assert.Equal(expected, value.NumberOfDigits());
        }
    }
}
