using Xunit;
using System.Linq;
using Samola.Algorithms.Sequences.Primes;
using Samola.Algorithms.Sequences;

namespace Samola.Algorithms.Tests
{
    public class AmicableNumbersTests
    {
        private readonly DivisorCalculator _divisor;

        public AmicableNumbersTests()
        {
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);
            _divisor = new DivisorCalculator(decomposer);
        }
        
        [Fact]
        public void AmicableNumbers_returns_amicable_number_pairs()
        {
            var amicableNumbers = new AmicableNumbers(_divisor, 1)
                .Take(1); 
            
            var numbers = amicableNumbers.First();

            Assert.Equal(220, numbers.Item1);
            Assert.Equal(284, numbers.Item2);
        }
    }
}
