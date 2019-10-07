using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Samola.Numbers;
using Samola.Numbers.Primes;
using Samola.Numbers.Construction;
using Samola.Numbers.Utilities;
using Samola.Numbers.Enumerables;

namespace Samola.Numbers.Tests
{
    public class PrimeDecomposerTests
    {
        [Fact]
        public void PrimeDecomposition_decomposes_number_14_correctly()
        {
            var number = 14;
            var maxValueLimit = new MaxValueLimit(number);
            var decomposer = new PrimeDecomposer(maxValueLimit);

            var decomposition = decomposer.CalculateDecomposition(number);

            var expected = new Dictionary<int, int>()
            {
                { 2, 1 },
                { 7, 1 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_75_correctly()
        {
            var maxValueLimit = new MaxValueLimit(75);
            var decomposer = new PrimeDecomposer(maxValueLimit);

            var decomposition = decomposer.CalculateDecomposition(75);

            var expected = new Dictionary<int, int>()
            {
                { 3, 1 },
                { 5, 2 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_420_correctly()
        {
            var maxValueLimit = new MaxValueLimit(420);
            var decomposer = new PrimeDecomposer(maxValueLimit);

            var decomposition = decomposer.CalculateDecomposition(420);

            var expected = new Dictionary<int, int>()
            {
                { 2, 2 },
                { 3, 1 },
                { 5, 1 },
                { 7, 1 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_65536_correctly()
        {
            var maxValueLimit = new MaxValueLimit(65536);
            var decomposer = new PrimeDecomposer(maxValueLimit);

            var decomposition = decomposer.CalculateDecomposition(65536);

            var expected = new Dictionary<int, int>()
            {
                { 2, 16 },
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_14_correctly_with_unlimited_primes()
        {
            var decomposer = new PrimeDecomposer(CountLimit.Default);

            var decomposition = decomposer.CalculateDecomposition(14);

            var expected = new Dictionary<int, int>()
            {
                { 2, 1 },
                { 7, 1 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_75_correctly_with_unlimited_primes()
        {
            var decomposer = new PrimeDecomposer(CountLimit.Default);

            var decomposition = decomposer.CalculateDecomposition(75);

            var expected = new Dictionary<int, int>()
            {
                { 3, 1 },
                { 5, 2 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_420_correctly_with_unlimited_primes()
        {
            var decomposer = new PrimeDecomposer(CountLimit.Default);

            var decomposition = decomposer.CalculateDecomposition(420);

            var expected = new Dictionary<int, int>()
            {
                { 2, 2 },
                { 3, 1 },
                { 5, 1 },
                { 7, 1 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_65536_correctly_with_unlimited_primes()
        {
            var decomposer = new PrimeDecomposer(CountLimit.Default);

            var decomposition = decomposer.CalculateDecomposition(65536);

            var expected = new Dictionary<int, int>()
            {
                { 2, 16 },
            };
            Assert.Equal(expected, decomposition);
        }

    }
}
