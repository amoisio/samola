using Samola.Numbers.CustomTypes;
using Samola.Numbers.Enumerables;
using Samola.Numbers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samola.Collections;
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
                var expected = Fibonacci.GetNth(i + 1).Values[0];
                Assert.Equal(expected, numbers[i]);
            }
        }
    }
}
