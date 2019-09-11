using System;
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
    }
}
