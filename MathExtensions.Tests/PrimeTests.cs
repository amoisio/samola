using MathExtensions.Primes;
using System;
using System.Linq;
using Xunit;

namespace MathExtensions.Tests
{
    public class PrimeTests
    {
        [Fact]
        public void IsPrimeSimple_return_the_same_values_as_IsPrimeBase()
        {
            int count = 2000000;
            for (int i = 1; i < count; i++)
            {
                Assert.Equal(MathExt.IsPrimeBase(i), MathExt.IsPrimeSimple(i));
            }
        }

        [Fact]
        public void IsPrimeSimple_return_the_same_values_as_IsPrimeCached()
        {
            int count = 2000000;
            for (int i = 1; i < count; i++)
            {
                Assert.Equal(MathExt.IsPrimeBase(i), MathExt.IsPrimeCached(i));
            }
        }

        [Fact]
        public void IsPrimeSimple_return_the_same_values_as_IsPrimeCachedNoLocks()
        {
            int count = 2000000;
            for (int i = 1; i < count; i++)
            {
                Assert.Equal(MathExt.IsPrimeBase(i), MathExt.IsPrimeCachedNoLocks(i));
            }
        }

        [Fact]
        public void IsPrimeSimple_return_the_same_values_as_IsPrimeSimple6k()
        {
            int count = 2000000;
            for (int i = 1; i < count; i++)
            {
                Assert.Equal(MathExt.IsPrimeBase(i), MathExt.IsPrimeSimple6k(i));
            }
        }

        [Fact]
        public void IsPrimeSimple_return_the_same_values_as_IsPrimeSimple6kCached()
        {
            int count = 2000000;
            for (int i = 1; i < count; i++)
            {
                Assert.Equal(MathExt.IsPrimeBase(i), MathExt.IsPrimeSimple6kCached(i));
            }
        }

        [Fact]
        public void PrimesSimple_and_PrimesNew_return_the_same_values()
        {
            int count = 200000;
            var control = new PrimesSimple(count).ToArray();
            var nprimes = PrimesNew.Create(count, PrimesGenerationRule.GenerateNPrimes).ToArray();

            for(int i = 0; i < count; i++)
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
            foreach(var prime in primes.Create())
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
            int count = 200000;
            var control = new PrimesSimple(count).ToArray();
            var nprimes = Primes6k.Create(count, true).ToArray();
            //var diff = new int[nprimes.Length];
            Assert.Equal(control.Length, nprimes.Length);
            for (int i = 0; i < count; i++)
            {
                Assert.Equal(control[i], nprimes[i]);
                //diff[i] = nprimes[i] - control[i];
            }

            //var d = diff.Where(e => e > 0).ToArray();
            //Assert.Equal(0, d.Length);
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
    }
}
