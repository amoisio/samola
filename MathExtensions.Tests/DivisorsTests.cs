﻿using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MathExtensions;

namespace MathExtensions.Tests
{
    public class DivisorsTests
    {
        /// 1: 1
        /// 3: 1,3
        /// 6: 1,2,3,6
        /// 10: 1,2,5,10
        /// 15: 1,3,5,15
        /// 21: 1,3,7,21
        /// 28: 1,2,4,7,14,28
        [Theory]
        [InlineData(1, 1)]
        [InlineData(3, 2)]
        [InlineData(6, 4)]
        [InlineData(10, 4)]
        [InlineData(15, 4)]
        [InlineData(21, 4)]
        [InlineData(28, 6)]
        [InlineData(3022111, 4)]
        public void Divisors_should_calculate_the_correct_amount(int number, int expectedDivisors)
        {
            var primes = PrimesNew.Create(2000, PrimesGenerationRule.GenerateNPrimes);
            var D = Divisors.NumberOfDivisors(number, new PrimeDecomposer(primes));

            Assert.Equal(expectedDivisors, D);
        }

        /// 1: 1
        /// 3: 1,3
        /// 6: 1,2,3,6
        /// 10: 1,2,5,10
        /// 15: 1,3,5,15
        /// 21: 1,3,7,21
        /// 28: 3022111
        [Theory]
        [InlineData(3, 2)]
        public void Divisors_should_return_1_and_prime_for_prime_numbers(int number, int expectedDivisors)
        {
            var primes = PrimesNew.Create(2000, PrimesGenerationRule.GenerateNPrimes);
            var D = Divisors.NumberOfDivisors(number, new PrimeDecomposer(primes));

            Assert.Equal(expectedDivisors, D);
        }

        [Fact]
        public void Divisors_return_all_divisors_of_a_number()
        {
            int number = 180;

            var decomposer = new PrimeDecomposer(PrimesNew.Create(number, PrimesGenerationRule.GenaratePrimesUpToN));
            var decomposition = decomposer.CalculateDecomposition(number);
            var divisors = Divisors.GetDivisors(decomposition);

            Assert.Contains(1, divisors);
            Assert.Contains(2, divisors);
            Assert.Contains(3, divisors);
            Assert.Contains(4, divisors);
            Assert.Contains(5, divisors);
            Assert.Contains(6, divisors);
            Assert.Contains(9, divisors);
            Assert.Contains(10, divisors);
            Assert.Contains(12, divisors);
            Assert.Contains(15, divisors);
            Assert.Contains(18, divisors);
            Assert.Contains(20, divisors);
            Assert.Contains(30, divisors);
            Assert.Contains(36, divisors);
            Assert.Contains(45, divisors);
            Assert.Contains(60, divisors);
            Assert.Contains(90, divisors);
            Assert.Contains(180, divisors);
        }
    }
}
