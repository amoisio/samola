using System.Linq;
using Samola.Collections;
using Samola.Numbers.Fibonacci;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class FibonacciTests
    {
        [Fact]
        public void FibonacciNumbers_calculates_fibonacci_numbers()
        {
            var limit = new MaximumYieldedCountLimit<int>(30);
            var fibonacci = new FibonacciNumbers(limit);
            
            var numbers = fibonacci.ToArray();

            for (int i = 0; i < 30; i++)
            {
                var expected = FibonacciNumbers.GetNthTerm(i + 1);
                Assert.Equal(expected, numbers[i]);
            }
        }
    }
}
