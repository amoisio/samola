using Xunit;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Samola.Numbers.Primes;
using System;
using Samola.Collections;

namespace Samola.Numbers.Tests
{
    public class PrimeDecompositionTests
    {
        [Fact]
        public void PrimeDecomposition_decomposes_number_14_correctly()
        {
            var number = 14;
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);

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
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);

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
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);

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
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);

            var decomposition = decomposer.CalculateDecomposition(65536);

            var expected = new Dictionary<int, int>()
            {
                { 2, 16 },
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void Prime_decomposition_decomposes_values_upto_10000_under_two_seconds()
        {
            int n = 10000;
            var limit = new MaximumYieldedValueLimit(n);
            var primes = new PrimeNumbers6k(limit);
            var decomposer = new PrimeDecomposer(primes);

            var times = new List<long>();
            for (int j = 0; j < 5; j++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                for (int i = 1; i <= n; i++)
                {
                    decomposer.CalculateDecomposition(i);
                }
                stopwatch.Stop();
                times.Add(stopwatch.ElapsedMilliseconds);
            }

            Assert.InRange(times.Average(), 0, 2000);
        }

        [Fact]
        public void Equals_works()
        {
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);

            var decomposition = decomposer.CalculateDecomposition(50);

            var same = decomposer.CalculateDecomposition(50);
            var diff1 = decomposer.CalculateDecomposition(250);
            var diff2 = decomposer.CalculateDecomposition(2);
            var diff3 = decomposer.CalculateDecomposition(13 * 7 * 7 * 3 * 3);

            Assert.True(decomposition.Equals(same));
            Assert.False(decomposition.Equals(diff1));
            Assert.False(decomposition.Equals(diff2));
            Assert.False(decomposition.Equals(diff3));
        }

        [Fact]
        public void Can_be_raised_to_power_k()
        {
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);
            var decomposition = decomposer.CalculateDecomposition(5 * 5 * 2);

            var powComp = decomposition.Pow(2);

            var same = decomposer.CalculateDecomposition(5 * 5 * 5 * 5 * 2 * 2);

            Assert.True(powComp.Equals(same));
        }

        [Fact]
        public void Pow_operation_does_not_modify_decomposition_in_place()
        {
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);
            var decomposition = decomposer.CalculateDecomposition(5 * 5 * 2);

            var powComp = decomposition.Pow(2);

            var same = decomposer.CalculateDecomposition(5 * 5 * 5 * 5 * 2 * 2);

            Assert.True(powComp.Equals(same));
            Assert.False(decomposition.Equals(same));
        }

        [Fact]
        public void Comparison_works_as_expected()
        {
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);
            var composition1 = decomposer.CalculateDecomposition(50);
            var composition2 = decomposer.CalculateDecomposition(51);
            var reference1 = decomposer.CalculateDecomposition(50);
            var reference2 = decomposer.CalculateDecomposition(49);
            
            Assert.Equal(reference1, composition1);
            Assert.NotEqual(reference2, composition1);
            Assert.NotEqual(reference1, composition2);
            Assert.NotEqual(reference2, composition2);
        }
    }
}
