using Xunit;
using MathExtensions.Utilities;
using MathExtensions.Enumerables;
using System.Linq;

namespace MathExtensions.Tests
{
    public class AmicableNumbersTests
    {
        private readonly AmicableNumbersBuilder _builder;
        public AmicableNumbersTests()
        {
            _builder = new AmicableNumbersBuilder();
        }
       [Fact]
        public void AmicableNumbers_returns_numbers_which_have_an_amicable_number()
        {
            var limit = new CountLimit(2);
            _builder.Limit = limit;
            var decomposer = new PrimeDecomposer(new MaxValueLimit(284));
            var divisor = new DivisorCalculator(decomposer);
            _builder.Divisor = divisor;
            
            var amicableNumbers = _builder.Build();
            var numbers = amicableNumbers.ToArray();

            Assert.Equal(220, numbers[0]);
            Assert.Equal(284, numbers[1]);
        }
    }
}
