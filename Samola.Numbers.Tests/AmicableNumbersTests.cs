using Xunit;
using Samola.Numbers.Utilities;
using Samola.Numbers.Enumerables;
using System.Linq;
using Samola.Collections;
using Samola.Numbers.Primes;

namespace Samola.Numbers.Tests
{
    public class AmicableNumbersTests
    {
        [Fact]
        public void AmicableNumbers_returns_numbers_which_have_an_amicable_number()
        {
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);
            var divisor = new DivisorCalculator(decomposer);
            var limit = new MaximumYieldedCountLimit<int>(2);
            var amicableNumbers = new AmicableNumbers(divisor, limit);
            
            var numbers = amicableNumbers.ToArray();

            Assert.Equal(220, numbers[0]);
            Assert.Equal(284, numbers[1]);
        }

        [Fact]
        public void AmicableNumbers_works_correctly_with_the_countlimit()
        {
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);
            var divisor = new DivisorCalculator(decomposer);
            var limit = new MaximumYieldedCountLimit<int>(2);
            var amicableNumbers = new AmicableNumbers(divisor, limit);
            
            var numbers = amicableNumbers.ToArray();

            Assert.Equal(2, numbers.Length);
        }

        [Fact]
        public void AmicableNumbers_works_correctly_with_the_maxvaluelimit()
        {
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);
            var divisor = new DivisorCalculator(decomposer);
            var limit = new MaximumYieldedValueLimit(284);
            var amicableNumbers = new AmicableNumbers(divisor, limit);
            
            var numbers = amicableNumbers.ToArray();

            Assert.Equal(504, numbers.Sum());
        }
    }
}
