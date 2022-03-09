using Samola.Numbers.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class NaturalNumbersTests
    {
        private NaturalNumbersBuilder _builder;
        private PrimeNumbersBuilder _primesBuilder;
        public NaturalNumbersTests()
        {
            _builder = new NaturalNumbersBuilder();
            _primesBuilder = new PrimeNumbersBuilder();
        }

        [Fact]
        public void NaturalNumbers_returns_natural_numbers()
        {
            _builder.Limit = new MaxNaturalValueLimit(5000);
            var numbers = _builder.Build();
            var arr = numbers.Select(e => e.Value).ToArray();

            var naturalNumbers = Enumerable.Range(1, 5000).ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                Assert.Equal(naturalNumbers[i], arr[i]);
            }
        }

        [Fact]
        public void NaturalNumbers_know_if_they_are_prime()
        {
            _builder.Limit = new MaxNaturalValueLimit(5000);
            var numbers = _builder.Build();
            var arr = numbers.ToArray();

            _primesBuilder.Limit = new MaxValueLimit(5000);
            var primes = _primesBuilder.Build();

            foreach (var prime in primes)
            {
                var val = arr.Single(e => e.Value == prime);
                Assert.True(val.IsPrime);
            }
        }

        [Theory]
        [InlineData(2, NumberClassification.Deficient)]
        [InlineData(4, NumberClassification.Deficient)]
        [InlineData(5, NumberClassification.Deficient)]
        [InlineData(6, NumberClassification.Perfect)]
        [InlineData(7, NumberClassification.Deficient)]
        [InlineData(8, NumberClassification.Deficient)]
        [InlineData(9, NumberClassification.Deficient)]
        [InlineData(10, NumberClassification.Deficient)]
        [InlineData(11, NumberClassification.Deficient)]
        [InlineData(12, NumberClassification.Abundant)]
        [InlineData(13, NumberClassification.Deficient)]
        [InlineData(14, NumberClassification.Deficient)]
        [InlineData(15, NumberClassification.Deficient)]
        [InlineData(16, NumberClassification.Deficient)]
        [InlineData(17, NumberClassification.Deficient)]
        [InlineData(18, NumberClassification.Abundant)]
        [InlineData(19, NumberClassification.Deficient)]
        [InlineData(20, NumberClassification.Abundant)] //1, 2, 10, 4, 5, 
        [InlineData(21, NumberClassification.Deficient)] // 1, 3, 7, 
        [InlineData(22, NumberClassification.Deficient)] // 1, 2, 11, 
        public void NaturalNumber_know_they_are_deficient_perfect_or_abundant(int number, NumberClassification expected)
        {
            _builder.Limit = new MaxNaturalValueLimit(22);
            var numbers = _builder.Build();

            foreach (var n in numbers)
            {
                if (n.Value == number)
                    Assert.Equal(expected, n.Classification);
            }
        }
    }
}
