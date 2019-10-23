using Samola.Numbers.Counters;
using System.Collections.Generic;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class DialCounterTests
    {
        [Theory]
        [MemberData(nameof(PrintData))]
        public void DialCounter_can_print_itself_out(string expected, DialCounter counter)
        {
            Assert.Equal(expected, counter.ToString());
        }

        public static IEnumerable<object[]> PrintData =>
            new List<object[]>
            {
                new object[] { "9999", new DialCounter(4) },
                new object[] { "9746", new DialCounter(new int[] { 9, 7, 4, 6 }) }
            };

        [Theory]
        [MemberData(nameof(Count1Data))]
        public void DialCounter_counts_down_normally(string expected, DialCounter counter)
        {
            int dials = counter.Dials;

            var ok = counter.Roll(dials - 1);

            Assert.True(ok);
            Assert.Equal(expected, counter.ToString());
        }

        public static IEnumerable<object[]> Count1Data =>
            new List<object[]>
            {
                new object[] { "9998", new DialCounter(4) },
                new object[] { "973", new DialCounter(new int[] { 9, 7, 4 }) }
            };

        [Fact]
        public void DialCounter_can_we_initialized_with_number_of_dials()
        {
            DialCounter counter = new DialCounter(3);

            Assert.Equal(3, counter.Dials);
            Assert.Equal("999", counter.ToString());
        }

        [Fact]
        public void DialCounter_can_we_initialized_with_dial_values()
        {
            DialCounter counter = new DialCounter(new int[] { 5, 4, 3, 2, 1 });

            Assert.Equal(5, counter.Dials);
            Assert.Equal("54321", counter.ToString());
        }

        [Theory]
        [MemberData(nameof(Count2Data))]
        public void DialCounter_items_to_the_right_are_reset_on_count_down(string expected, DialCounter counter)
        {
            var ok = counter.Roll(1);

            Assert.True(ok);
            Assert.Equal(expected, counter.ToString());
        }

        public static IEnumerable<object[]> Count2Data =>
            new List<object[]>
            {
                new object[] { "9888", new DialCounter(4) },
                new object[] { "966", new DialCounter(new int[] { 9, 7, 4 }) }
            };

        [Theory]
        [MemberData(nameof(Count3Data))]
        public void DialCounter_items_are_reset_on_rollover(string expected, DialCounter counter)
        {
            var dials = counter.Dials;
            var ok = counter.Roll(dials - 1);

            Assert.Equal(expected, counter.ToString());
        }

        public static IEnumerable<object[]> Count3Data =>
            new List<object[]>
            {
                new object[] { "8888", new DialCounter(new int[] { 9, 0, 0, 0 }) },
                new object[] { "955", new DialCounter(new int[] { 9, 6, 0 }) }
            };


    }
}
