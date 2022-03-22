using System.Linq;
using Samola.Algorithms.Sequences;
using Xunit;

namespace Samola.Algorithms.Tests
{
    public class LargeIntegerTests
    {
        [Theory]
        [InlineData(3, 5, 8)]
        [InlineData(13, 5, 8, 1)]
        [InlineData(7, 35, 2, 4)]
        [InlineData(7000, 35, 5, 3, 0, 7)]
        public void LargeInteger_can_be_added_with_integers(int left, int right, params int[] expected)
        {
            var ctx = new LargeIntegerContext(2);
            LargeInteger l = ctx.CreateInteger(left);

            LargeInteger result = l + right;
            Assert.Equal(expected, result.Digits.ToArray());

            LargeInteger result2 = right + l;
            Assert.Equal(expected, result2.Digits.ToArray());
        }

        [Theory]
        [InlineData(3, 5, 8)]
        [InlineData(13, 5, 8, 1)]
        [InlineData(7, 35, 2, 4)]
        [InlineData(7000, 35, 5, 3, 0, 7)]
        public void LargeInteger_can_be_added_with_other_largeintegers(int left, int right, params int[] expected)
        {
            var ctx = new LargeIntegerContext(2);
            LargeInteger l = ctx.CreateInteger(left);
            LargeInteger r = ctx.CreateInteger(right);

            LargeInteger result = l + r;
            Assert.Equal(expected, result.Digits.ToArray());
        }

        [Theory]
        [InlineData(3, 5, 5, 1)]
        [InlineData(13, 5, 5, 6)]
        [InlineData(7, 35, 5, 4, 2)]
        [InlineData(7000, 33, 0, 0, 0, 1, 3, 2)]
        public void LargeInteger_can_be_multiplied_with_integers(int left, int right, params int[] expected)
        {
            var ctx = new LargeIntegerContext(2);
            LargeInteger l = ctx.CreateInteger(left);

            LargeInteger result = l * right;
            Assert.Equal(expected, result.Digits.ToArray());

            LargeInteger result2 = right * l;
            Assert.Equal(expected, result2.Digits.ToArray());
        }

        [Fact]
        public void IntegerContext_causes_the_value_to_be_split_into_separate_values()
        {
            var ctx = new LargeIntegerContext(2);
            LargeInteger l = ctx.CreateInteger(111);

            Assert.Equal(2, l.Values.Count());
            Assert.Equal(1, l.Values[1]);
        }

        [Fact]
        public void ToString_prints_leading_zeros_in_less_significant_values()
        {
            string val = "";
            var ctx = new LargeIntegerContext(3);
            LargeInteger l = ctx.CreateInteger(122999);
            LargeInteger r = ctx.CreateInteger(1);
            val = (l + r).ToString();

            Assert.Equal("123000", val);
        }

        [Fact]
        public void ToString_doesnot_print_leading_zeros_in_the_most_significant_value()
        {
            string val = "";
            var ctx = new LargeIntegerContext(3);
            LargeInteger l = ctx.CreateInteger(22999);
            LargeInteger r = ctx.CreateInteger(1);

            val = (l + r).ToString();

            Assert.Equal("23000", val);
        }
    }
}
