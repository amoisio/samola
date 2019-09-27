using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MathExtensions.Primes;
using MathExtensions.Construction;

namespace MathExtensions.Tests
{
    public class AmicableNumberTests
    {
        [Theory]
        [InlineData(220, 284)]
        [InlineData(284, 220)]
        public void AmicableNumbers_works(int number, int expected)
        {
            var primesCreator = new Primes6kFactory(number, true);
            var aNumber = new AmicableNumberCalculator(primesCreator);
            var amicableNumber = aNumber.FindAmicableNumber(number);
            Assert.Equal(expected, amicableNumber);
        }
    }
}
