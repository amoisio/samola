using System;
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
    }
}
