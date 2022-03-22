using Samola.DataStructures.Counters;
using System.Collections.Generic;
using Xunit;

namespace Samola.DataStructures.Tests
{
    public class DialCounterTests
    {
        [Fact]
        public void DialCounter_can_be_created_with_n_dials()
        {
            DialCounter counter2 = new DialCounter(2);
            DialCounter counter4 = new DialCounter(4);

            Assert.Equal(2, counter2.NumberOfDials);
            Assert.Equal(4, counter4.NumberOfDials);
        }

        [Fact]
        public void DialCounter_can_be_created_with_a_dial_state()
        {
            DialCounter counter = new DialCounter(new int[] { 2, 2, 1, 1, 0 });

            Assert.Equal("22110", counter.ToString());
            Assert.Equal(5, counter.NumberOfDials);
        }

        [Fact]
        public void DialCounter_throw_if_created_with_faulty_state()
        {
            try
            {
                DialCounter counter = new DialCounter(new int[] { 2, 2, 1, 1, 5 });
                Assert.True(false);
            }
            catch (System.Exception)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void DialCounter_default_value_mapper_maps_dial_value_as_value()
        {
            DialCounter counter = new DialCounter(3);

            Assert.Equal(9, counter.DialValue(0));
            Assert.Equal(9, counter.DialValue(1));
            Assert.Equal(9, counter.DialValue(2));
        }

        [Fact]
        public void DialCounter_can_be_given_a_custom_value_mapper()
        {
            DialCounter counter = new DialCounter(4, (d) => 2 * d);

            counter.Roll(1);
            counter.Roll(2);
            counter.Roll(3);

            Assert.Equal(18, counter.DialValue(0));
            Assert.Equal(16, counter.DialValue(1));
            Assert.Equal(14, counter.DialValue(2));
            Assert.Equal(12, counter.DialValue(3));
        }

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
                new object[] { "9764", new DialCounter(new int[] { 9, 7, 6, 4 }) }
            };

        [Theory]
        [MemberData(nameof(Count1Data))]
        public void DialCounter_counts_down_normally(string expected, DialCounter counter)
        {
            int dials = counter.NumberOfDials;

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

            Assert.Equal(3, counter.NumberOfDials);
            Assert.Equal("999", counter.ToString());
        }

        [Fact]
        public void DialCounter_can_we_initialized_with_dial_values()
        {
            DialCounter counter = new DialCounter(new int[] { 5, 4, 3, 2, 1 });

            Assert.Equal(5, counter.NumberOfDials);
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
            var dials = counter.NumberOfDials;
            var ok = counter.Roll(dials - 1);

            Assert.Equal(expected, counter.ToString());
        }

        public static IEnumerable<object[]> Count3Data =>
            new List<object[]>
            {
                new object[] { "8888", new DialCounter(new int[] { 9, 0, 0, 0 }) },
                new object[] { "955", new DialCounter(new int[] { 9, 6, 0 }) }
            };

        [Fact]
        public void Reset_sets_dials_back_to_starting_position()
        {
            DialCounter counter = new DialCounter(3);

            counter.Roll(0);
            counter.Roll(1);
            counter.Roll(2);

            Assert.Equal(8, counter.DialValue(0));
            Assert.Equal(7, counter.DialValue(1));
            Assert.Equal(6, counter.DialValue(2));

            counter.Reset();

            Assert.Equal(9, counter.DialValue(0));
            Assert.Equal(9, counter.DialValue(1));
            Assert.Equal(9, counter.DialValue(2));
        }

    }
}
