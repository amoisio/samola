using System.Linq;
using Samola.Algorithms.CalculatedEnumerable;
using Samola.Algorithms.Sequences.Primes;
using Samola.Algorithms.Sequences;
using Xunit;

namespace Samola.Algorithms.Tests
{
    public class AbundantNumbersTests
    {
        private readonly DivisorCalculator _divisor;
        private readonly NumberClassifier _classifier;
        public AbundantNumbersTests()
        {
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);
            _divisor = new DivisorCalculator(decomposer);
            _classifier = new NumberClassifier(_divisor);
        }
        
        [Fact]
        public void Returns_only_abundant_numbers()
        {
            var abundantNumbers = new AbundantNumbers(_classifier, initialValue: 1)
                .TakeWhile(n => n <= 28123);
          
            foreach (var aNumber in abundantNumbers)
            {
                var classification = _classifier.Classify(aNumber);
                Assert.True(classification == NumberClassification.Abundant);

                var properSum = _divisor.GetProperDivisors(aNumber).Sum();
                Assert.True(properSum > aNumber);
            }
        }

        [Fact]
        public void MaxValueLimit_limits_the_abundant_numbers()
        {
            var abundantNumbers = new AbundantNumbers(_classifier, initialValue: 1)
                .TakeWhile(n => n <= 28123);

            Assert.True(abundantNumbers.Last() <= 28123);
        }

        [Fact]
        public void CountLimit_limits_the_abundant_numbers()
        {
            var abundantNumbers = new AbundantNumbers(_classifier, initialValue: 1)
                .Take(100);
            
            Assert.Equal(100, abundantNumbers.Count());
        }
    }
}
