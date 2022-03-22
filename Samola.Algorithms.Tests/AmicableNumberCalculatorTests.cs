using Xunit;
using Samola.Algorithms.Sequences;
using Samola.Algorithms.Utilities;

namespace Samola.Algorithms.Tests
{
    public class AmicableNumberCalculatorTests
    {
        [Theory]
        [InlineData(220, 284)]
        [InlineData(284, 220)]
        public void AmicableNumbers_works(int number, int expected)
        {
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);
            var divisor = new DivisorCalculator(decomposer);
            var aNumber = new AmicableNumberCalculator(divisor);
            var amicableNumber = aNumber.FindAmicableNumber(number);
            Assert.Equal(expected, amicableNumber);
        }
    }
}
