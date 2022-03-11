using Xunit;
using Samola.Numbers.Utilities;
using Samola.Numbers.Enumerables;
using System.Linq;
using Samola.Numbers.Primes.Generators;
using Samola.Numbers.Primes;

namespace Samola.Numbers.Tests
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
            //new MaxValueLimit(284)
            var primes = new Primes6k();
            var decomposer = new PrimeDecomposer(primes);
            var divisor = new DivisorCalculator(decomposer);
            _builder.Divisor = divisor;

            var amicableNumbers = _builder.Build();
            var numbers = amicableNumbers.ToArray();

            Assert.Equal(220, numbers[0]);
            Assert.Equal(284, numbers[1]);
        }

        [Fact]
        public void AmicableNumbers_works_correctly_with_the_countlimit()
        {
            //new MaxValueLimit(284)
            var primes = new Primes6k();
            var decomposer = new PrimeDecomposer(primes);
            DivisorCalculator divisor = new DivisorCalculator(decomposer);

            var builder = new AmicableNumbersBuilder();
            _builder.Limit = new CountLimit(2);
            _builder.Divisor = divisor;
            var numbers = _builder.Build().ToArray();

            Assert.Equal(504, numbers.Sum());
        }

        [Fact]
        public void AmicableNumbers_works_correctly_with_the_maxvaluelimit()
        {
            var upTo = 284;
            var limit = new MaxValueLimit(upTo);
            var primes = new Primes6k();
            var decomposer = new PrimeDecomposer(primes);
            DivisorCalculator divisor = new DivisorCalculator(decomposer);

            var builder = new AmicableNumbersBuilder();
            _builder.Limit = limit;
            _builder.Divisor = divisor;
            var numbers = _builder.Build().ToArray();

            Assert.Equal(504, numbers.Sum());
        }
    }
}
