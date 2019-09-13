using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace MathExtensions.Tests
{
    public class AmicableNumberTests
    {
        [Theory]
        [InlineData(220, 284)]
        [InlineData(284, 220)]
        public void AmicableNumbers_works(int number, int expected)
        {
            var amicableNumber = AmicableNumber.FindAmicableNumber(number);
            Assert.Equal(expected, amicableNumber);
        }
    }
}
