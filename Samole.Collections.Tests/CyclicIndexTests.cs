using Samola.Collections;
using System;
using Xunit;
using Samola.Utilities;

namespace Samole.Collections.Tests
{
    public class CyclicIndexTests
    {
        [Theory]
        [InlineData(-8, 3, 1)]
        [InlineData(-7, 3, 2)]
        [InlineData(-6, 3, 0)]
        [InlineData(-5, 3, 1)]
        [InlineData(-4, 3, 2)]
        [InlineData(-3, 3, 0)]
        [InlineData(-2, 3, 1)]
        [InlineData(-1, 3, 2)]
        [InlineData(0, 3, 0)]
        [InlineData(1, 3, 1)]
        [InlineData(2, 3, 2)]
        [InlineData(3, 3, 0)]
        [InlineData(4, 3, 1)]
        [InlineData(5, 3, 2)]
        [InlineData(6, 3, 0)]
        [InlineData(7, 3, 1)]
        [InlineData(8, 3, 2)]
        public void Creates_a_cyclic_index_from_an_integer(int number, int cycleSize, int expected)
        {
            var ci = CyclicIndex.Create(number, cycleSize);

            Assert.Equal(expected, ci.Value);
            Assert.Equal(cycleSize, ci.CycleSize);
        }

        [Fact]
        public void Adds_normally_with_other_cyclic_indices()
        {
            CyclicIndex c1 = CyclicIndex.Create(1, 4);
            CyclicIndex c2 = CyclicIndex.Create(2, 4);

            CyclicIndex c3 = c1 + c2;
            CyclicIndex c4 = c2 + c1;

            Assert.Equal(3, c3.Value);
            Assert.Equal(3, c4.Value);
        }

        [Fact]
        public void Adds_normally_with_integers()
        {
            CyclicIndex c1 = CyclicIndex.Create(1, 4);
            int i1 = 2;

            CyclicIndex c3 = c1 + i1;
            CyclicIndex c4 = i1 + c1;

            Assert.Equal(3, c3.Value);
            Assert.Equal(3, c4.Value);
        }

        [Fact]
        public void Differences_normally_with_other_cyclic_indices()
        {
            CyclicIndex c1 = CyclicIndex.Create(1, 4);
            CyclicIndex c2 = CyclicIndex.Create(2, 4);

            CyclicIndex c3 = c1 - c2;
            CyclicIndex c4 = c2 - c1;

            Assert.Equal(3, c3.Value);
            Assert.Equal(1, c4.Value);
        }

        [Fact]
        public void Differences_normally_with_integers()
        {
            CyclicIndex c1 = CyclicIndex.Create(1, 4);
            int i1 = 2;

            CyclicIndex c3 = c1 - i1;
            CyclicIndex c4 = i1 - c1;

            Assert.Equal(3, c3.Value);
            Assert.Equal(1, c4.Value);
        }

        [Fact]
        public void Throws_when_operands_have_different_cycle_sizes()
        {
            CyclicIndex c1 = CyclicIndex.Create(1, 4);
            CyclicIndex c2 = CyclicIndex.Create(2, 5);

            Assert.Throws<InvalidOperationException>(() => c1 + c2);
        }

        [Fact]
        public void Increments_as_integers_when_in_bounds()
        {
            CyclicIndex c1 = CyclicIndex.Create(1, 4);

            CyclicIndex c2 = c1 + 1;

            Assert.Equal(2, c2.Value);
        }

        [Fact]
        public void Increments_by_looping_back_when_goes_out_of_bounds()
        {
            CyclicIndex c1 = CyclicIndex.Create(3, 4);

            CyclicIndex c2 = c1 + 1;

            Assert.Equal(0, c2.Value);
        }

        [Fact]
        public void Decrements_as_integers_when_in_bounds()
        {
            CyclicIndex c1 = CyclicIndex.Create(1, 4);

            CyclicIndex c2 = c1 - 1;

            Assert.Equal(0, c2.Value);
        }

        [Fact]
        public void Decrements_by_looping_back_when_goes_out_of_bounds()
        {
            CyclicIndex c1 = CyclicIndex.Create(0, 4);

            CyclicIndex c2 = c1 - 1;

            Assert.Equal(3, c2.Value);
        }

        [Fact]
        public void Is_equal_to_another_cyclic_index_when_index_and_cyclesize_are_the_same()
        {
            CyclicIndex c1 = CyclicIndex.Create(1, 4);
            CyclicIndex c2 = CyclicIndex.Create(1, 4);
            CyclicIndex c3 = CyclicIndex.Create(2, 4);
            CyclicIndex c4 = CyclicIndex.Create(1, 5);

            Assert.True(c1 == c2);
            Assert.True(c1 != c3);
            Assert.True(c1 != c4);
        }
    }

    public class CyclicIndexUtilsTests
    {
        [Theory]
        [InlineData(-8, 3, 1)]
        [InlineData(-7, 3, 2)]
        [InlineData(-6, 3, 0)]
        [InlineData(-5, 3, 1)]
        [InlineData(-4, 3, 2)]
        [InlineData(-3, 3, 0)]
        [InlineData(-2, 3, 1)]
        [InlineData(-1, 3, 2)]
        [InlineData(0, 3, 0)]
        [InlineData(1, 3, 1)]
        [InlineData(2, 3, 2)]
        [InlineData(3, 3, 0)]
        [InlineData(4, 3, 1)]
        [InlineData(5, 3, 2)]
        [InlineData(6, 3, 0)]
        [InlineData(7, 3, 1)]
        [InlineData(8, 3, 2)]
        public void Projects_integers_onto_cyclic_space(int number, int cycleSize, int expected)
        {
            var index = CyclicIndexUtils.FromInteger(number, cycleSize);
            Assert.Equal(expected, index);
        }

        [Theory]
        [InlineData(0, 0, 3, 0)]
        [InlineData(1, 0, 3, 1)]
        [InlineData(2, 0, 3, 2)]
        [InlineData(0, 1, 3, 3)]
        [InlineData(1, 1, 3, 4)]
        [InlineData(2, 1, 3, 5)]
        [InlineData(0, 2, 3, 6)]
        [InlineData(1, 2, 3, 7)]
        [InlineData(2, 2, 3, 8)]
        public void Projects_cyclic_indices_onto_integer_space(int cycleIndex, int cycleNumber, int cycleSize,  int expected)
        {
            var number = CyclicIndexUtils.ToInteger(cycleIndex, cycleNumber, cycleSize);
            Assert.Equal(expected, number);
        }

        [Theory]
        [InlineData(-1)]
        public void Negative_cyclic_index_throws_on_project(int index)
        {
            Assert.Throws<IndexOutOfRangeException>(() => CyclicIndexUtils.ToInteger(index));
            Assert.Throws<IndexOutOfRangeException>(() => CyclicIndexUtils.ToInteger(index, 1, 4));
        }
    }
}
