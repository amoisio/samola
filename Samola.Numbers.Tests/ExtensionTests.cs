using System;
using System.Collections.Generic;
using System.Text;
using Samola.Numbers.Utilities;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class ExtensionTests
    {
        [Fact]
        public void Negative_startIndex_throws_an_error()
        {
            try
            {
                "hello".Substrings(-1, 2);
                Assert.False(true);
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Fact]
        public void OutOfBounds_startIndex_throws_an_error()
        {
            try
            {
                "hello".Substrings(5, 2);
                Assert.False(true);
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Theory]
        [InlineData("0123456789", 0, 3, new string[] { "012", "345", "678", "9" })]
        [InlineData("012345678", 0, 3, new string[] { "012", "345", "678" })]
        [InlineData("012 456 8", 0, 3, new string[] { "012", " 45", "6 8" })]
        public void Substrings_splits_a_string_into_several_substrings(string testString, int startIndex, int len, string[] expected)
        {
            var substrings = testString.Substrings(startIndex, len);

            Assert.Equal(expected, substrings);
        }
    }
}
