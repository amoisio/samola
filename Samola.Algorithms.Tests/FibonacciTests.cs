using System.Linq;
using Samola.Algorithms.Sequences;
using Xunit;

namespace Samola.Algorithms.Tests
{
    public class FibonacciTests
    {
        [Fact]
        public void FibonacciNumbers_calculates_fibonacci_numbers()
        {
            var fibonacci = new FibonacciNumbers().Take(30);
            
            var numbers = fibonacci.ToArray();

            for (int i = 0; i < 30; i++)
            {
                var expected = FibonacciNumbers.GetNthTerm(i + 1);
                Assert.Equal(expected, numbers[i]);
            }
        }
    }
}
