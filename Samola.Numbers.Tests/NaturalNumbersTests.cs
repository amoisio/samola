using Samola.Numbers.Enumerables;
using System.Linq;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class NaturalNumbersTests
    {
        private readonly NaturalNumbersBuilder _builder;
        public NaturalNumbersTests()
        {
            _builder = new NaturalNumbersBuilder();
        }

        [Fact]
        public void NaturalNumbers_returns_natural_numbers()
        {
            _builder.Limit = new MaxNaturalValueLimit(5000);
            var numbers = _builder.Build();
            var arr = numbers.Select(e => e.Value).ToArray();

            var naturalNumbers = Enumerable.Range(1, 5000).ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                Assert.Equal(naturalNumbers[i], arr[i]);
            }
        }
    }
}
