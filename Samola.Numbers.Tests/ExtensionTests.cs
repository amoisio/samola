using System;
using System.Collections.Generic;
using System.Text;
using Samola.Numbers.Utilities;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class ExtensionTests
    {
        [Theory]
        [InlineData("12394712", true)]
        [InlineData("+2312412414", false)]
        [InlineData("-1241412", false)]
        [InlineData("12414124124a1441095718904124124", false)]
        [InlineData("one", false)]
        [InlineData("hello world!", false)]
        public void IsNonNegativeInteger_correctly_identifies_strings_with_only_numbers(string str, bool expected)
        {
            Assert.Equal(expected, str.IsNonNegativeInteger());
        }

        [Fact]
        public void Negative_startIndex_throws_an_error()
        {
            try
            {
                "hello".Substrings(2);
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
                "hello".Substrings(2);
                Assert.False(true);
            }
            catch (Exception)
            {
                Assert.True(true);
            }
        }

        [Theory]
        [InlineData("0123456789", 3, new string[] { "012", "345", "678", "9" })]
        [InlineData("012345678", 3, new string[] { "012", "345", "678" })]
        [InlineData("012 456 8", 3, new string[] { "012", " 45", "6 8" })]
        public void Substrings_splits_a_string_into_several_substrings(string testString, int len, string[] expected)
        {
            var substrings = testString.Substrings(len);

            Assert.Equal(expected, substrings);
        }

        [Theory]
        [InlineData("0123456789", 3, new string[] { "0", "123", "456", "789"})]
        [InlineData("012345678", 3, new string[] { "012", "345", "678" })]
        [InlineData("012 456 8", 3, new string[] { "012", " 45", "6 8" })]
        public void Substrings_splits_a_string_into_several_substrings_starting_from_the_end(string testString, int len, string[] expected)
        {
            var substrings = testString.Substrings(len, true);

            Assert.Equal(expected, substrings);
        }
    }
}
