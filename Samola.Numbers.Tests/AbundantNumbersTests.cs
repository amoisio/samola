using Samola.Numbers.Enumerables;
using Samola.Numbers.Utilities;
using System.Linq;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class AbundantNumbersTests
    {
        private readonly AbundantNumbersBuilder _builder;

        public AbundantNumbersTests()
        {
            _builder = new AbundantNumbersBuilder();
        }

        [Fact]
        public void Returns_only_abundant_numbers()
        {
            var divisor = new DivisorCalculator();
            var classifier = new NumberClassifier(divisor);

            _builder.Limit = new MaxValueLimit(28123);
            _builder.Classifier = classifier;
            var abundantNumbers = _builder.Build();
            foreach (var aNumber in abundantNumbers)
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
            _builder.Limit = new MaxValueLimit(28123);
            _builder.Classifier = new NumberClassifier();
            var abundantNumbers = _builder.Build();

            Assert.True(abundantNumbers.Last() <= 28123);
        }

        [Fact]
        public void CountLimit_limits_the_abundant_numbers()
        {
            _builder.Limit = new CountLimit(1000);
            _builder.Classifier = new NumberClassifier();
            var abundantNumbers = _builder.Build();
            Assert.Equal(1000, abundantNumbers.Count());
        }
    }
}
