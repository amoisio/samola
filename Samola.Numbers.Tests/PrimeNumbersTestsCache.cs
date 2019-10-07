using Samola.Numbers.Cache;
using Samola.Numbers.Construction;
using Samola.Numbers.Enumerables;
using Samola.Numbers.Primes;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class PrimeNumbersTestsCache
    {
        private PrimeNumbersBuilder _builder;

        public PrimeNumbersTestsCache()
        {
            _builder = new PrimeNumbersBuilder();
        }

        [Theory]
        [InlineData(1, new int[] { 2 })]
        [InlineData(2, new int[] { 2, 3 })]
        [InlineData(3, new int[] { 2, 3, 5 })]
        [InlineData(4, new int[] { 2, 3, 5, 7 })]
        [InlineData(6, new int[] { 2, 3, 5, 7, 11, 13 })]
        [InlineData(7, new int[] { 2, 3, 5, 7, 11, 13, 17 })]
        [InlineData(5, new int[] { 2, 3, 5, 7, 11 })]
        public void PrimeNumbers_generate_primes_correctly(int maxPrimes, int[] expectedPrimes)
        {
            _builder.Limit = new CountLimit(maxPrimes);
            var primes = _builder.Build();

            int[] actual = primes.ToArray();

            Assert.Equal(expectedPrimes, actual);
        }

        [Fact]
        public void PrimeNumbers_and_PrimesSimple_generate_the_same_primes()
        {
            int maxPrimes = 5000;

            var primes1 = new PrimesSimple(maxPrimes).ToArray();

            _builder.Limit = new CountLimit(maxPrimes);
            var primes2a = _builder.Build();
            var primes2 = primes2a.ToArray();

            for (int i = 0; i < primes1.Length; i++)
            {
                Assert.Equal(primes1[i], primes2[i]);
            }
        }

        [Fact]
        public void PrimeNumbers_iteration_can_be_stopped_midstream()
        {
            _builder.Limit = new CountLimit(1000000);
            var primes = _builder.Build();
            long p = 0;
            foreach (var prime in primes)
            {
                p = prime;
                if (prime == 17)
                    break;
            }

            Assert.Equal(17, p);
        }

        [Fact]
        public void PrimeNumbers_can_be_iterated_over_multiple_times()
        {
            _builder.Limit = new CountLimit(1000000);
            var primes = _builder.Build();
            long p = 0;
            foreach (var prime in primes)
            {
                p = prime;
                if (prime == 17)
                    break;
            }

            foreach (var prime in primes)
            {
                if (prime == 17)
                    break;

                Assert.False(prime >= 17);
                Assert.True(prime < 17);
            }
        }

        [Fact]
        public void CountLimit_forces_prime_number_generation_to_halt()
        {
            int maxCount = 1000;
            _builder.Limit = new CountLimit(maxCount);
            var primes = _builder.Build();

            int count = 0;
            foreach(var prime in primes)
            {
                count++;
            }

            Assert.Equal(maxCount, count);
            Assert.Equal(maxCount, primes.Count());
        }

        [Fact]
        public void MaxValueLimit_forces_prime_number_generation_to_halt()
        {
            int maxValue = 950;
            _builder.Limit = new MaxValueLimit(maxValue);
            var primes = _builder.Build();

            int temp = 0;
            foreach (var prime in primes)
            {
                temp = prime;
            }
            Assert.True(temp < maxValue);

            int count = primes.Count();
            _builder.Limit = new CountLimit(count + 1);
            primes = _builder.Build();
            temp = primes.Last();

            Assert.True(temp > maxValue);
        }
    }
}
