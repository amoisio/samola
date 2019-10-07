using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class PowTests
    {
        [Theory]
        [InlineData(3, 0, 1)]
        [InlineData(3, 1, 3)]
        [InlineData(3, 2, 9)]
        [InlineData(3, 3, 27)]
        [InlineData(2, 1, 2)]
        [InlineData(2, 2, 4)]
        [InlineData(2, 3, 8)]
        [InlineData(2, 4, 16)]
        public void Pow_calculates_powers_correctly(int x, int y, long expected)
        {
            Assert.Equal(expected, MathExt.Pow(x, y));
        }
    }
}
