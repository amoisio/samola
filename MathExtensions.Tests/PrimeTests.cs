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
        public void Primes_and_PrimesNew_return_the_same_values()
        {
            int count = 200000;
            var control = new Primes(count).ToArray();
            var nprimes = PrimesNew.Create(count, PrimesGenerationRule.GenerateNPrimes).ToArray();

            for(int i = 0; i < count; i++)
            {
                Assert.Equal(control[i], nprimes[i]);
            }
        }

        [Theory]
        [InlineData(1, new long[] { 2 })]
        [InlineData(2, new long[] { 2, 3 })]
        [InlineData(3, new long[] { 2, 3, 5 })]
        [InlineData(4, new long[] { 2, 3, 5, 7 })]
        [InlineData(6, new long[] { 2, 3, 5, 7, 11, 13 })]
        [InlineData(7, new long[] { 2, 3, 5, 7, 11, 13, 17 })]
        [InlineData(5, new long[] { 2, 3, 5, 7, 11 })]
        public void Primes_generate_primes_correctly(int maxPrimes, long[] expectedPrimes)
        {
            Primes primes = new Primes(maxPrimes);

            long[] actual = primes.ToArray();

            Assert.Equal(expectedPrimes, actual);
        }

        [Theory]
        [InlineData(1, new long[] { 2 })]
        [InlineData(2, new long[] { 2, 3 })]
        [InlineData(3, new long[] { 2, 3, 5 })]
        [InlineData(4, new long[] { 2, 3, 5, 7 })]
        [InlineData(6, new long[] { 2, 3, 5, 7, 11, 13 })]
        [InlineData(7, new long[] { 2, 3, 5, 7, 11, 13, 17 })]
        [InlineData(5, new long[] { 2, 3, 5, 7, 11 })]
        public void PrimesNew_generate_primes_correctly(int maxPrimes, long[] expectedPrimes)
        {
            var primes = PrimesNew.Create(maxPrimes, PrimesGenerationRule.GenerateNPrimes);

            long[] actual = primes.ToArray();

            Assert.Equal(expectedPrimes, actual);
        }

        [Fact]
        public void PrimesNew_and_Primes_generate_the_same_primes()
        {
            int maxPrimes = 5000;

            var primes1 = new Primes(maxPrimes).ToArray();
            var primes2 = PrimesNew.Create(maxPrimes, PrimesGenerationRule.GenerateNPrimes).ToArray();

            for (int i = 0; i < primes1.Length; i++)
            {
                Assert.Equal(primes1[i], primes2[i]);
            }
        }

        [Fact]
        public void Prime_calculators_return_the_same_primes()
        {
            var primes = new Primes(20).ToArray();
            var primesFast = new Primes(20).ToArray();

            for (int i = 0; i < 20; i++)
            {
                Assert.Equal(primes[i], primesFast[i]);
            }


        }
    }
}
