using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using MathExtensions;

namespace MathExtensions.Tests
{
    public class PrimeDecomposerTests
    {
        [Fact]
        public void PrimeDecomposition_decomposes_number_14_correctly()
        {
            var decomposer = new PrimeDecomposer(PrimesNew.Create(10));

            var decomposition = decomposer.CalculateDecomposition(14);

            var expected = new Dictionary<long, long>()
            {
                { 2, 1 },
                { 7, 1 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_75_correctly()
        {
            var decomposer = new PrimeDecomposer(PrimesNew.Create(10));

            var decomposition = decomposer.CalculateDecomposition(75);

            var expected = new Dictionary<long, long>()
            {
                { 3, 1 },
                { 5, 2 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_420_correctly()
        {
            var decomposer = new PrimeDecomposer(PrimesNew.Create(10));

            var decomposition = decomposer.CalculateDecomposition(420);

            var expected = new Dictionary<long, long>()
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
            var decomposer = new PrimeDecomposer(PrimesNew.Create(10));

            var decomposition = decomposer.CalculateDecomposition(65536);

            var expected = new Dictionary<long, long>()
            {
                { 2, 16 },
            };
            Assert.Equal(expected, decomposition);
        }

    }
}
