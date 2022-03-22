using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Samola.Algorithms.Sequences.Primes;
using Samola.Algorithms.Sequences;
using Xunit;

namespace Samola.Algorithms.Tests
{
    public class NumberClassifierTests
    {
        private readonly NumberClassifier _classifier;

        public NumberClassifierTests()
        {
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);
            var divisor = new DivisorCalculator(decomposer);
            _classifier = new NumberClassifier(divisor);
        }

        [Theory]
        [InlineData(6, NumberClassification.Perfect)]
        [InlineData(12, NumberClassification.Abundant)]
        [InlineData(17, NumberClassification.Deficient)]
        public void Classifier_classifies_numbers_correctly(int number, NumberClassification expected)
        {
            var result = _classifier.Classify(number);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Classifier_classfies_values_upto_10000_under_two_seconds()
        {
            int n = 10000;

            var times = new List<long>();
            for (int j = 0; j < 5; j++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                for (int i = 1; i <= n; i++)
                {
                    _classifier.Classify(i);
                }
                stopwatch.Stop();
                times.Add(stopwatch.ElapsedMilliseconds);
            }

            Assert.InRange(times.Average(), 0, 2000);
        }
    }
}
