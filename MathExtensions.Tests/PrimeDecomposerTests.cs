using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using MathExtensions;
using MathExtensions.Primes;

namespace MathExtensions.Tests
{
    public class PrimeDecomposerTests
    {
        [Fact]
        public void PrimeDecomposition_decomposes_number_14_correctly()
        {
            var number = 14;
            var primesCreator = new Primes6kFactory(number, true);
            var decomposer = new PrimeDecomposer(primesCreator);

            var decomposition = decomposer.CalculateDecomposition(number);

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
            var primesCreator = new Primes6kFactory(75, true);
            var decomposer = new PrimeDecomposer(primesCreator);

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
            var primesCreator = new Primes6kFactory(420, true);
            var decomposer = new PrimeDecomposer(primesCreator);

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
            var primesCreator = new Primes6kFactory(65536, true);
            var decomposer = new PrimeDecomposer(primesCreator);

            var decomposition = decomposer.CalculateDecomposition(65536);

            var expected = new Dictionary<long, long>()
            {
                { 2, 16 },
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_14_correctly_with_unlimited_primes()
        {
            var primesCreator = new Primes6kFactory(PrimesBase.MAX_COUNT, true);
            var decomposer = PrimeDecomposer.Create(primesCreator);

            var decomposition = decomposer.CalculateDecomposition(14);

            var expected = new Dictionary<long, long>()
            {
                { 2, 1 },
                { 7, 1 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_75_correctly_with_unlimited_primes()
        {
            var primesCreator = new Primes6kFactory(PrimesBase.MAX_COUNT, true);
            var decomposer = PrimeDecomposer.Create(primesCreator);

            var decomposition = decomposer.CalculateDecomposition(75);

            var expected = new Dictionary<long, long>()
            {
                { 3, 1 },
                { 5, 2 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_420_correctly_with_unlimited_primes()
        {
            var primesCreator = new Primes6kFactory(PrimesBase.MAX_COUNT, true);
            var decomposer = PrimeDecomposer.Create(primesCreator);

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
        public void PrimeDecomposition_decomposes_number_65536_correctly_with_unlimited_primes()
        {
            var primesCreator = new Primes6kFactory(PrimesBase.MAX_COUNT, true);
            var decomposer = PrimeDecomposer.Create(primesCreator);

            var decomposition = decomposer.CalculateDecomposition(65536);

            var expected = new Dictionary<long, long>()
            {
                { 2, 16 },
            };
            Assert.Equal(expected, decomposition);
        }

    }
}
