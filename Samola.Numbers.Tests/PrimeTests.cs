using Samola.Numbers.Primes.Generators;
using System.Linq;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class PrimeTests
    {

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
            var primes = new PrimesSimple();

            var actual = primes.Take(maxPrimes).ToArray();

            Assert.Equal(expectedPrimes, actual);
        }

        [Fact]
        public void PrimesSimple_primes_can_be_iterated_over_multiple_times()
        {
            var primes = new PrimesSimple();
            int p;
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

        [Theory]
        [InlineData(1, new int[] { 2 })]
        [InlineData(2, new int[] { 2, 3 })]
        [InlineData(3, new int[] { 2, 3, 5 })]
        [InlineData(4, new int[] { 2, 3, 5, 7 })]
        [InlineData(6, new int[] { 2, 3, 5, 7, 11, 13 })]
        [InlineData(7, new int[] { 2, 3, 5, 7, 11, 13, 17 })]
        [InlineData(5, new int[] { 2, 3, 5, 7, 11 })]
        public void Primes6k_generate_primes_correctly(int maxPrimes, int[] expectedPrimes)
        {
            var primes = new Primes6k();

            var actual = primes.Take(maxPrimes).ToArray();

            Assert.Equal(expectedPrimes, actual);
        }

        [Fact]
        public void Prime6k_primes_can_be_iterated_over_multiple_times()
        {
            var primes = new Primes6k();
            int p;
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
    }
}
