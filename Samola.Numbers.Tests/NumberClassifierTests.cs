﻿using Samola.Numbers.Utilities;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class NumberClassifierTests
    {
        private NumberClassifier _classifier;

        public NumberClassifierTests()
        {
            _classifier = new NumberClassifier();
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
        public void Classifier_classfies_values_upto_10000_under_one_second()
        {
            int n = 10000;

            List<long> times = new List<long>();
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

            Assert.True(times.Average() < 1000);
        }
    }
}