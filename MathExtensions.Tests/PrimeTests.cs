using System;
using Xunit;

namespace MathExtensions.Tests
{
    public class PrimeTests
    {
        [Fact]
        public void IsPrime_methods_return_the_same_values()
        {
            int count = 2000000;
            for (int i = 2; i < count; i++)
            {
                Assert.Equal(MathExt.IsPrimeSimple(i), MathExt.IsPrime(i));
            }
        }
    }
}
