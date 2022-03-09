using Samola.Numbers.CustomTypes;
using Samola.Numbers.Enumerables;
using Samola.Numbers.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class FibonacciTests
    {
        private FibonacciNumbersBuilder _builder;

        public FibonacciTests()
        {
            _builder = new FibonacciNumbersBuilder();
            _builder.UseCache = false;
            _builder.Limit = new LargeIntegerCountLimit(30);
        }

        [Fact]
        public void FibonacciNumbers_calculates_fibonacci_numbers()
        {
            var numbers = _builder.Build().ToArray();

            for (int i = 1; i < 30; i++)
            {
                var expected = Fibonacci.GetNth(i);
                Assert.Equal(expected.ToString(), numbers[i - 1].ToString());
            }
        }
    }
}
