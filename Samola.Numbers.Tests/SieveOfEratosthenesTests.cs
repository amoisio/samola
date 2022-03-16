using Samola.Numbers.Enumerables;
using System.Linq;
using Samola.Numbers.Primes;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class SieveOfEratosthenesTests
    {
        [Fact]
        public void SieveOfEratosthenes_should_get_primes_up_to_given_number_10()
        {
            var sieve = new SieveOfEratosthenes(10);
            var primes = sieve.ToArray();

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
            var primes = sieve.ToArray();
            Assert.Contains(29, primes);
        }
    }
}
