using MathExtensions.Cache;
using MathExtensions.Enumerables;
using MathExtensions.Implementations;
using MathExtensions.Primes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MathExtensions.Tests
{
    public class AbundantNumbersTests
    {
        private EnumerableCacheProvider<int> _provider;
        private IPrimesCreator _primesCreator;
        private NumberClassifier _numberClassifier;
        private DivisorCalculator _divisorCalculator;

        public AbundantNumbersTests()
        {
            _primesCreator = new Primes6kFactory(PrimesBase.MAX_COUNT, true);
            _numberClassifier = new NumberClassifier(_primesCreator);
            _divisorCalculator = new DivisorCalculator(_primesCreator);
            _provider = new EnumerableCacheProvider<int>();
        }

        [Fact]
        public void Returns_only_abundant_numbers()
        {
            var maxLimit = new MaxValueLimit(28123);
            var abundantNumbers = new AbundantNumbers(_primesCreator, maxLimit, _provider);
            foreach(var aNumber in abundantNumbers)
            {
                var classification = _numberClassifier.Classify(aNumber);
                Assert.True(classification == NumberClassification.Abundant);

                var properSum = _divisorCalculator.GetProperDivisors(aNumber).Sum();
                Assert.True(properSum > aNumber);
            }
        }

        [Fact]
        public void MaxValueLimit_limits_the_abundant_numbers()
        {
            var maxLimit = new MaxValueLimit(28123);
            var abundantNumbers = new AbundantNumbers(_primesCreator, maxLimit, _provider);
            Assert.True(abundantNumbers.Last() <= 28123);
        }

        [Fact]
        public void CountLimit_limits_the_abundant_numbers()
        {
            var limit = new CountLimit(1000);
            var abundantNumbers = new AbundantNumbers(_primesCreator, limit, _provider);
            Assert.Equal(1000, abundantNumbers.Count());
        }
    }
}
