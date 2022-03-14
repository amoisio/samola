using Samola.Numbers.Enumerables;
using Samola.Numbers.Utilities;
using System.Linq;
using Samola.Collections;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class AbundantNumbersTests
    {
        [Fact]
        public void Returns_only_abundant_numbers()
        {
            var divisor = new DivisorCalculator();
            var classifier = new NumberClassifier(divisor);
            var limit = new MaximumYieldedValueLimit(28123);

            var abundantNumbers = new AbundantNumbers(classifier, limit);
          
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
            var divisor = new DivisorCalculator();
            var classifier = new NumberClassifier(divisor);
            var limit = new MaximumYieldedValueLimit(28123);
            
            var abundantNumbers = new AbundantNumbers(classifier, limit);

            Assert.True(abundantNumbers.Last() <= 28123);
        }

        [Fact]
        public void CountLimit_limits_the_abundant_numbers()
        {
            var divisor = new DivisorCalculator();
            var classifier = new NumberClassifier(divisor);
            var limit = new MaximumYieldedCountLimit<int>(1000);
            
            var abundantNumbers = new AbundantNumbers(classifier, limit);
            
            Assert.Equal(1000, abundantNumbers.Count());
        }
    }
}
