using MathExtensions.Cache;
using MathExtensions.Construction;
using MathExtensions.Enumerables;
using MathExtensions.Primes;
using MathExtensions.Utilities;
using System.Linq;
using Xunit;

namespace MathExtensions.Tests
{
    public class AbundantNumbersTests
    {
        private EnumerableListCacheProvider<int> _provider;

        public AbundantNumbersTests()
        {
            _provider = new EnumerableListCacheProvider<int>("abundantNumbers", 1000);
        }

        [Fact]
        public void Returns_only_abundant_numbers()
        {
            var maxLimit = new MaxValueLimit(28123);
            var decomposer = new PrimeDecomposer(maxLimit);
            var divisor = new DivisorCalculator(decomposer);
            var classifier = new NumberClassifier(divisor);
            var abundantNumbers = new AbundantNumbers(classifier, maxLimit, _provider);
            foreach(var aNumber in abundantNumbers)
            {
                var classification = classifier.Classify(aNumber);
                Assert.True(classification == NumberClassification.Abundant);

                var properSum = divisor.GetProperDivisors(aNumber).Sum();
                Assert.True(properSum > aNumber);
            }
        }

        [Fact]
        public void MaxValueLimit_limits_the_abundant_numbers()
        {
            var maxLimit = new MaxValueLimit(28123);
            var decomposer = new PrimeDecomposer(maxLimit);
            var divisor = new DivisorCalculator(decomposer);
            var classifier = new NumberClassifier(divisor);

            var abundantNumbers = new AbundantNumbers(classifier, maxLimit, _provider);
            Assert.True(abundantNumbers.Last() <= 28123);
        }

        [Fact]
        public void CountLimit_limits_the_abundant_numbers()
        {
            var limit = new CountLimit(1000);
            var decomposer = new PrimeDecomposer(limit);
            var divisor = new DivisorCalculator(decomposer);
            var classifier = new NumberClassifier(divisor);


            var abundantNumbers = new AbundantNumbers(classifier, limit, _provider);
            Assert.Equal(1000, abundantNumbers.Count());
        }
    }
}
