using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Samola.Numbers.Utilities;

namespace Samola.Numbers.Tests
{
    public class ArrayExtensionsTests
    {
        [Theory]
        [InlineData(null, new int[] { 1 }, false)]
        [InlineData(new int[] { 1 }, null, false)]
        [InlineData(null, null, false)]
        [InlineData(new int[] { 1, 2 }, new int[] { 1 }, false)]
        [InlineData(new int[] { 1 }, new int[] { 1, 2 }, false)]
        [InlineData(new int[] { 1, 3 }, new int[] { 1, 2 }, false)]
        [InlineData(new int[] { 1, 2 }, new int[] { 1, 2 }, true)]
        public void IntArrayEquals_check_if_the_arrays_contain_the_same_integers(int[] array1, int[] array2, bool expected)
        {
            var ok = array1.ContainsSameItems(array2);
            Assert.Equal(expected, ok);
        }
    }
}
