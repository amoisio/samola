using Samola.Numbers.Enumerables;
using System.Linq;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class SieveOfEratosthenesTests
    {
        [Fact]
        public void SieveOfEratosthenes_should_get_primes_up_to_given_number_10()
        {
            var sieve = new SieveOfEratosthenes(10);
            var primes = sieve.GetPrimes().ToArray();

            Assert.Equal(4, primes.Length);
            Assert.Equal(2, primes[0]);
            Assert.Equal(3, primes[1]);
            Assert.Equal(5, primes[2]);
            Assert.Equal(7, primes[3]);
        }

        [Fact]
        public void SieveOfEratosthenes_should_get_primes_up_to_given_number_120()
        {
            var sieve = new SieveOfEratosthenes(120);
            var primes = sieve.GetPrimes().ToArray();
            Assert.Contains(29, primes);
        }

        [Fact]
        public void SieveOfEratosthenes_should_be_able_to_calculate_next_prime()
        {
            var sieve = new SieveOfEratosthenes(8);
            var primes = sieve.GetPrimes().ToArray();

            Assert.Equal(4, primes.Length);
            Assert.Equal(7, primes.Max());

            var prime = sieve.GetNextPrime();
            primes = sieve.GetPrimes().ToArray();

            Assert.Equal(11, prime);
            Assert.Equal(5, primes.Length);
            Assert.Equal(11, primes.Max());
        }
    }
}
