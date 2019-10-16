using Xunit;
using System.Collections.Generic;
using Samola.Numbers.Utilities;
using System.Diagnostics;
using System.Linq;

namespace Samola.Numbers.Tests
{
    public class PrimeDecomposerTests
    {
        private PrimeDecomposer _decomposer;

        public PrimeDecomposerTests()
        {
            _decomposer = new PrimeDecomposer();
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_14_correctly()
        {
            var number = 14;

            var decomposition = _decomposer.CalculateDecomposition(number);

            var expected = new Dictionary<int, int>()
            {
                { 2, 1 },
                { 7, 1 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_ecomposes_number_75_correctly()
        {
            var decomposition = _decomposer.CalculateDecomposition(75);

            var expected = new Dictionary<int, int>()
            {
                { 3, 1 },
                { 5, 2 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_420_correctly()
        {
            var decomposition = _decomposer.CalculateDecomposition(420);

            var expected = new Dictionary<int, int>()
            {
                { 2, 2 },
                { 3, 1 },
                { 5, 1 },
                { 7, 1 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_65536_correctly()
        {
            var decomposition = _decomposer.CalculateDecomposition(65536);

            var expected = new Dictionary<int, int>()
            {
                { 2, 16 },
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_14_correctly_with_unlimited_primes()
        {
            var decomposition = _decomposer.CalculateDecomposition(14);

            var expected = new Dictionary<int, int>()
            {
                { 2, 1 },
                { 7, 1 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_75_correctly_with_unlimited_primes()
        {
            var decomposition = _decomposer.CalculateDecomposition(75);

            var expected = new Dictionary<int, int>()
            {
                { 3, 1 },
                { 5, 2 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_420_correctly_with_unlimited_primes()
        {
            var decomposition = _decomposer.CalculateDecomposition(420);

            var expected = new Dictionary<int, int>()
            {
                { 2, 2 },
                { 3, 1 },
                { 5, 1 },
                { 7, 1 }
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_65536_correctly_with_unlimited_primes()
        {
            var decomposition = _decomposer.CalculateDecomposition(65536);

            var expected = new Dictionary<int, int>()
            {
                { 2, 16 },
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void Prime_decomposer_decomposes_values_upto_10000_under_one_second()
        {
            int n = 10000;

            List<long> times = new List<long>();
            for (int j = 0; j < 5; j++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                for (int i = 1; i <= n; i++)
                {
                    _decomposer.CalculateDecomposition(i);
                }
                stopwatch.Stop();
                times.Add(stopwatch.ElapsedMilliseconds);
            }

            Assert.True(times.Average() < 1000);
        }
    }
}
