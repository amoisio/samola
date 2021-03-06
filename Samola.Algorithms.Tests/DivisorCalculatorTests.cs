using Xunit;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Samola.Algorithms.Sequences;
using Samola.Algorithms.Utilities;

namespace Samola.Algorithms.Tests
{
    public class DivisorCalculatorTests
    {
        private DivisorCalculator _calculator;

        public DivisorCalculatorTests()
        {
            var primes = new PrimeNumbers6k();
            var decomposer = new PrimeDecomposer(primes);
            _calculator = new DivisorCalculator(decomposer);
        }

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
            var D = _calculator.NumberOfDivisors(number);

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
            var D = _calculator.NumberOfDivisors(number);

            Assert.Equal(expectedDivisors, D);
        }

        [Fact]
        public void Divisors_return_all_divisors_of_a_number()
        {
            int number = 180;
            var divisors = _calculator.GetDivisors(number);

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

        [Fact]
        public void ProperDivisors_contain_all_divisors_except_the_number_itself()
        {
            var number = 180;

            var divisors = _calculator.GetProperDivisors(number);
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
            Assert.DoesNotContain(180, divisors);
        }

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
        public void Divisors_should_calculate_the_correct_amount_with_unlimited_primes(int number, int expectedDivisors)
        {
            var D = _calculator.NumberOfDivisors(number);

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
        public void Divisors_should_return_1_and_prime_for_prime_numbers_with_unlimited_primes(int number, int expectedDivisors)
        {
            var D = _calculator.NumberOfDivisors(number);

            Assert.Equal(expectedDivisors, D);
        }

        [Fact]
        public void Divisors_return_all_divisors_of_a_number_with_unlimited_primes()
        {
            int number = 180;
            var divisors = _calculator.GetDivisors(number);

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

        [Fact]
        public void ProperDivisors_contain_all_divisors_except_the_number_itself_with_unlimited_primes()
        {
            var divisors = _calculator.GetProperDivisors(180);
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
            Assert.DoesNotContain(180, divisors);
        }

        [Fact]
        public void Divisor_computes_proper_divisors_for_values_upto_10000_under_two_second()
        {
            int n = 10000;

            List<long> times = new List<long>();
            for (int j = 0; j < 5; j++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                for (int i = 1; i <= n; i++)
                {
                    _calculator.GetProperDivisors(i);
                }
                stopwatch.Stop();
                times.Add(stopwatch.ElapsedMilliseconds);
            }

            Assert.InRange(times.Average(), 0, 2000);
        }
    }
}
