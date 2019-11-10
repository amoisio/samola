using System;
using Xunit;

namespace Samola.Utilities.Tests
{
    public class StringExtensionsTests
    {
        [Theory]
        [InlineData("This is a string that I would like to split", 4, new string[] { "This", " is a string that I would like to split" })]
        [InlineData("This is a string that I would like to split", 15, new string[] { "This is a strin", "g that I would like to split" })]
        [InlineData("This is a string that I would like to split", 42, new string[] { "This is a string that I would like to spli", "t" })]
        public void Split_splits_the_line_into_two(string line, int index, string[] expected)
        {
            var result = line.Split(index);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("This is a test string", 0)]
        [InlineData("This is a test string", 21)]
        public void Split_returns_the_full_string_if_index_is_0_or_len(string line, int index)
        {
            var result = line.Split(index);

            Assert.Single(result);
            Assert.Equal(line, result[0]);
        }

        [Theory]
        [InlineData("This is a test string", -1)]
        [InlineData("This is a test string", 22)]
        public void Split_throw_index_out_of_range_exception_if_index_is_negative_or_over_len(string line, int index)
        {
            try
            {
                var result = line.Split(index);
                Assert.False(true);
            }
            catch (IndexOutOfRangeException)
            {
                Assert.True(true);
            }
            catch(Exception)
            {
                Assert.False(true);
            }
        }

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

        [Theory]
        [InlineData("This is a test string", 0)]
        [InlineData("This is a test string", -1)]
        public void SplitToMaxLengthLines_throws_an_exception_for_nonpositive_lengths(string line, int index)
        {
            try
            {
                var result = line.SplitToMaxLengthLines(index);
                Assert.False(true);
            }
            catch (ArgumentException ae)
            {
                Assert.Equal("maxLength", ae.ParamName);
            }
        }

        [Theory]
        [InlineData("Averyverylongword and afterthatevenalongerword", 10, new string[] {"Averyverylongword", "and", "afterthatevenalongerword" })]
        public void SplitToMaxLengthLines_returns_the_full_word_if_it_cannot_fit_into_maxlength(string line, int maxLength, string[] expected)
        {
            var result = line.SplitToMaxLengthLines(maxLength);

            Assert.Equal(expected, result);
        }
    }
}