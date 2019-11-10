using System;
using Xunit;

namespace Samola.Utilities.Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("This is a string that I would like to split", 10, new string[] {"This is a", "string", "that I", "would like", "to split"})]
        [InlineData("This is a short line", 20, new string[] { "This is a short line" })]
        [InlineData("                   This is a short line                       ", 20, new string[] { "This is a short line" })]
        [InlineData("                   This is a short  line                      ", 20, new string[] { "This is a short", "line" })]
        public void SplitToMaxLengthLines_splits_lines_to_max_size_and_between_words(string line, int maxLength, string[] expected)
        {
            var result = line.SplitToMaxLengthLines(maxLength);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void SplitToMaxLengthLines_returns_an_empty_array_for_empty_strings()
        {
            string line = String.Empty;

            var result = line.SplitToMaxLengthLines(10);

            Assert.Empty(result);
        }

        [Fact]
        public void SplitToMaxLengthLines_throws_an_exception_for_nonpositive_lengths()
        {
            string line = "This is a test string";

            try
            {
                var result = line.SplitToMaxLengthLines(0);
                Assert.False(true);
            }
            catch (ArgumentException ae)
            {
                Assert.Equal("maxLength", ae.ParamName);
            }

            try
            {
                var result = line.SplitToMaxLengthLines(-1);
                Assert.False(true);
            }
            catch (ArgumentException ae)
            {
                Assert.Equal("maxLength", ae.ParamName);
            }
        }
    }
}
