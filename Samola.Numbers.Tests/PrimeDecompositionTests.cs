using Xunit;
using System.Collections.Generic;
using Samola.Numbers.Utilities;
using System.Diagnostics;
using System.Linq;
using Samola.Numbers.Enumerables;
using Samola.Numbers.Comparers;

namespace Samola.Numbers.Tests
{
    public class PrimeDecompositionTests
    {
        [Fact]
        public void PrimeDecomposition_decomposes_number_14_correctly()
        {
            var number = 14;

            var decomposition = PrimeDecomposition.Create(number);

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
            var decomposition = PrimeDecomposition.Create(75);

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
            var decomposition = PrimeDecomposition.Create(420);

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
            var decomposition = PrimeDecomposition.Create(65536);

            var expected = new Dictionary<int, int>()
            {
                { 2, 16 },
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void PrimeDecomposition_decomposes_number_14_correctly_with_unlimited_primes()
        {
            var decomposition = PrimeDecomposition.Create(14);

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
            var decomposition = PrimeDecomposition.Create(75);

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
            var decomposition = PrimeDecomposition.Create(420);

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
            var decomposition = PrimeDecomposition.Create(65536);

            var expected = new Dictionary<int, int>()
            {
                { 2, 16 },
            };
            Assert.Equal(expected, decomposition);
        }

        [Fact]
        public void Prime_decomposition_decomposes_values_upto_10000_under_one_second()
        {
            int n = 10000;

            List<long> times = new List<long>();
            for (int j = 0; j < 5; j++)
            {
                Stopwatch stopwatch = Stopwatch.StartNew();
                for (int i = 1; i <= n; i++)
                {
                    PrimeDecomposition.Create(i);
                }
                stopwatch.Stop();
                times.Add(stopwatch.ElapsedMilliseconds);
            }

            Assert.True(times.Average() < 1000);
        }

        [Fact]
        public void Equals_works()
        {
            PrimeDecomposition decomposition = PrimeDecomposition.Create(50);

            PrimeDecomposition same = PrimeDecomposition.Create(50);
            PrimeDecomposition diff1 = PrimeDecomposition.Create(250);
            PrimeDecomposition diff2 = PrimeDecomposition.Create(2);
            PrimeDecomposition diff3 = PrimeDecomposition.Create(13*7*7*3*3);
            
            Assert.True(decomposition.Equals(same));
            Assert.False(decomposition.Equals(diff1));
            Assert.False(decomposition.Equals(diff2));
            Assert.False(decomposition.Equals(diff3));
        }

        [Fact]
        public void Can_be_raised_to_power_k()
        {
            PrimeDecomposition decomposition = PrimeDecomposition.Create(5*5*2);

            var powComp = decomposition.Pow(2);

            PrimeDecomposition same = PrimeDecomposition.Create(5*5*5*5*2*2);

            Assert.True(powComp.Equals(same));
        }

        [Fact]
        public void Pow_operation_does_not_modify_decomposition_in_place()
        {
            PrimeDecomposition decomposition = PrimeDecomposition.Create(5 * 5 * 2);

            var powComp = decomposition.Pow(2);

            PrimeDecomposition same = PrimeDecomposition.Create(5 * 5 * 5 * 5 * 2 * 2);

            Assert.True(powComp.Equals(same));
            Assert.False(decomposition.Equals(same));
        }

        [Fact]
        public void EqualityComparer_works_as_expected()
        {
            HashSet<PrimeDecomposition> d = new HashSet<PrimeDecomposition>(new PrimeDecompositionEqualityComparer());
            d.Add(PrimeDecomposition.Create(50));
            d.Add(PrimeDecomposition.Create(51));

            var decomp1 = PrimeDecomposition.Create(50);
            var decomp2 = PrimeDecomposition.Create(49);
            Assert.Contains(decomp1, d);
            Assert.DoesNotContain(decomp2, d);
        }
    }
}
