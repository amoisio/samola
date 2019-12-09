using Samola.Numbers.Cache;
using Samola.Numbers.Construction;
using Samola.Numbers.Enumerables;
using Samola.Numbers.Primes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class PrimeTests
    {
        private IPrimesCreator _primesCreator;

        public PrimeTests()
        {
            _primesCreator = new Primes6kFactory(PrimesBase.MAX_COUNT, true);
        }

        [Fact]
        public void IsPrimeSimple_return_the_same_values_as_IsPrimeBase()
        {
            int count = 20000;
            for (int i = 1; i < count; i++)
            {
                Assert.Equal(MathExt.IsPrimeBase(i), MathExt.IsPrimeSimple(i));
            }
        }

        [Fact]
        public void IsPrimeSimple_return_the_same_values_as_IsPrimeCached()
        {
            int count = 20000;
            for (int i = 1; i < count; i++)
            {
                Assert.Equal(MathExt.IsPrimeBase(i), MathExt.IsPrimeCached(i));
            }
        }

        [Fact]
        public void IsPrimeSimple_return_the_same_values_as_IsPrimeCachedNoLocks()
        {
            int count = 20000;
            for (int i = 1; i < count; i++)
            {
                Assert.Equal(MathExt.IsPrimeBase(i), MathExt.IsPrimeCachedNoLocks(i));
            }
        }

        [Fact]
        public void IsPrimeSimple_return_the_same_values_as_IsPrimeSimple6k()
        {
            int count = 20000;
            for (int i = 1; i < count; i++)
            {
                Assert.Equal(MathExt.IsPrimeBase(i), MathExt.IsPrimeSimple6k(i));
            }
        }

        [Fact]
        public void IsPrimeSimple_return_the_same_values_as_IsPrimeSimple6kCached()
        {
            int count = 20000;
            for (int i = 1; i < count; i++)
            {
                Assert.Equal(MathExt.IsPrimeBase(i), MathExt.IsPrimeSimple6kCached(i));
            }
        }

        [Fact]
        public void PrimesSimple_and_PrimesNew_return_the_same_values()
        {
            int count = 20000;
            var control = new PrimesSimple(count).ToArray();
            var nprimes = PrimesNew.Create(count, PrimesGenerationRule.GenerateNPrimes).ToArray();

            for (int i = 0; i < count; i++)
            {
                Assert.Equal(control[i], nprimes[i]);
            }
        }

        [Theory]
        [InlineData(1, new int[] { 2 })]
        [InlineData(2, new int[] { 2, 3 })]
        [InlineData(3, new int[] { 2, 3, 5 })]
        [InlineData(4, new int[] { 2, 3, 5, 7 })]
        [InlineData(6, new int[] { 2, 3, 5, 7, 11, 13 })]
        [InlineData(7, new int[] { 2, 3, 5, 7, 11, 13, 17 })]
        [InlineData(5, new int[] { 2, 3, 5, 7, 11 })]
        public void PrimesSimple_generate_primes_correctly(int maxPrimes, int[] expectedPrimes)
        {
            var primes = new PrimesSimple(maxPrimes);

            int[] actual = primes.ToArray();

            Assert.Equal(expectedPrimes, actual);
        }

        [Theory]
        [InlineData(1, new int[] { 2 })]
        [InlineData(2, new int[] { 2, 3 })]
        [InlineData(3, new int[] { 2, 3, 5 })]
        [InlineData(4, new int[] { 2, 3, 5, 7 })]
        [InlineData(6, new int[] { 2, 3, 5, 7, 11, 13 })]
        [InlineData(7, new int[] { 2, 3, 5, 7, 11, 13, 17 })]
        [InlineData(5, new int[] { 2, 3, 5, 7, 11 })]
        public void PrimesNew_generate_primes_correctly(int maxPrimes, int[] expectedPrimes)
        {
            var primes = PrimesNew.Create(maxPrimes, PrimesGenerationRule.GenerateNPrimes);

            int[] actual = primes.ToArray();

            Assert.Equal(expectedPrimes, actual);
        }

        [Fact]
        public void PrimesNew_and_PrimesSimple_generate_the_same_primes()
        {
            int maxPrimes = 5000;

            var primes1 = new PrimesSimple(maxPrimes).ToArray();
            var primes2 = PrimesNew.Create(maxPrimes, PrimesGenerationRule.GenerateNPrimes).ToArray();

            for (int i = 0; i < primes1.Length; i++)
            {
                Assert.Equal(primes1[i], primes2[i]);
            }
        }

        [Fact]
        public void Prime_calculators_return_the_same_primes()
        {
            var primes = new PrimesSimple(20).ToArray();
            var primesFast = new PrimesSimple(20).ToArray();

            for (int i = 0; i < 20; i++)
            {
                Assert.Equal(primes[i], primesFast[i]);
            }
        }

        [Fact]
        public void Prime_iteration_can_be_stopped_midstream()
        {
            var primes = new Primes6kFactory(PrimesBase.MAX_COUNT, true);
            long p = 0;
            foreach (var prime in primes.Create())
            {
                p = prime;
                if (prime == 17)
                    break;
            }

            Assert.Equal(17, p);
        }

        [Fact]
        public void Primes_can_be_iterated_over_multiple_times()
        {
            var primesCreator = new Primes6kFactory(PrimesBase.MAX_COUNT, true);
            long p = 0;
            var primes = primesCreator.Create();
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
        public void PrimesSimple_and_PrimesNewCached_return_the_same_values()
        {
            int count = 20000;
            var control = new PrimesSimple(count).ToArray();
            var nprimes = Primes6k.Create(count, true).ToArray();

            Assert.Equal(control.Length, nprimes.Length);
            for (int i = 0; i < count; i++)
            {
                Assert.Equal(control[i], nprimes[i]);
            }
        }

        [Theory]
        [InlineData(1, new int[] { 2 })]
        [InlineData(2, new int[] { 2, 3 })]
        [InlineData(3, new int[] { 2, 3, 5 })]
        [InlineData(4, new int[] { 2, 3, 5, 7 })]
        [InlineData(6, new int[] { 2, 3, 5, 7, 11, 13 })]
        [InlineData(7, new int[] { 2, 3, 5, 7, 11, 13, 17 })]
        [InlineData(5, new int[] { 2, 3, 5, 7, 11 })]
        public void PrimesNewCached_generate_primes_correctly(int maxPrimes, int[] expectedPrimes)
        {
            var primes = Primes6k.Create(maxPrimes, false);

            int[] actual = primes.ToArray();

            Assert.Equal(expectedPrimes, actual);
        }

        [Fact]
        public void Iterating_over_a_large_cached_prime_collection_should_be_faster_than_non_cached()
        {
            var nonCached = Primes6k.Create(Primes6k.MAX_COUNT, false);
            var cached = Primes6k.Create(Primes6k.MAX_COUNT, true);

            int reps = 10;
            int maxCount = 10000;

            Stopwatch stopwatch = new Stopwatch();

            List<long> nonCachedTotals = new List<long>(reps);
            List<long> cachedTotals = new List<long>();
            for (int i = 0; i < reps; i++)
            {
                int count = 0;
                stopwatch.Restart();
                foreach (var prime in nonCached)
                {
                    if (count++ > maxCount)
                        break;
                }
                stopwatch.Stop();
                nonCachedTotals.Add(stopwatch.ElapsedMilliseconds);
                count = 0;
                stopwatch.Restart();
                foreach (var prime in cached)
                {
                    if (count++ > maxCount)
                        break;
                }
                stopwatch.Stop();
                cachedTotals.Add(stopwatch.ElapsedMilliseconds);
            }

            Assert.True(nonCachedTotals.Average() > cachedTotals.Average());
        }
    }
}