using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Samola.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Extracts the placeholder value from a string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="pattern">String format pattern with the placeholder substring {0}</param>
        public static string Extract(this string s, string pattern)
        {
            if (String.IsNullOrEmpty(pattern))
                throw new ArgumentNullException(nameof(pattern));

            if (!pattern.Contains("{0}"))
                throw new ArgumentException("No placeholder string found!", nameof(pattern));

            int patternIndex = pattern.IndexOf("{0}", StringComparison.InvariantCulture);
            string startOfString = pattern.Substring(0, patternIndex);
            string endOfString = pattern.Substring(patternIndex + 3);

            int sourceStartIndex = s.IndexOf(startOfString, StringComparison.InvariantCulture);
            int valueStartIndex = sourceStartIndex + startOfString.Length;
            int sourceEndIndex = s.IndexOf(endOfString, valueStartIndex, StringComparison.InvariantCulture);
            int valueEndIndex = sourceEndIndex - 1;

            if (sourceStartIndex >= 0 && sourceEndIndex > valueStartIndex)
            {
                return s.Substring(valueStartIndex, valueEndIndex - valueStartIndex + 1);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Splits the string into two so that the first string runs from 0 to index - 1, and the second string runs from index to end-of-string
        /// </summary>
        /// <param name="line">Line to split</param>
        /// <param name="splitFromIndex">Staring index of the second string</param>
        public static string[] Split(this string line, int splitFromIndex)
        {
            if (splitFromIndex < 0 || splitFromIndex > line.Length)
                throw new IndexOutOfRangeException();

            List<string> lines = new List<string>(2);

            if (splitFromIndex == 0)
            {
                lines.Add(line);
            }
            else if (splitFromIndex == line.Length)
            {
                lines.Add(line);
            }
            else
            {
                lines.Add(line.Substring(0, splitFromIndex));
                lines.Add(line.Substring(splitFromIndex));
            }

            return lines.ToArray();
        }

        /// <summary>
        /// Splits the string between words into strings that have length less than the given maximum length
        /// </summary>
        /// <param name="line">Line to split</param>
        /// <param name="maxLength">Maximum length of each string</param>
        public static string[] SplitToMaxLengthLines(this string line, int maxLength)
        {
            if (maxLength <= 0)
                throw new ArgumentException("must be greater than 0.", nameof(maxLength));

            List<string> lines = new List<string>();

            string tempLine = line.Trim();
            while (tempLine.Length > 0)
            {
                var tempLength = GetSplitLength(tempLine, maxLength);
                var tempLines = tempLine.Split(tempLength);

                lines.Add(tempLines[0].TrimEnd());
                tempLine = tempLines.Length == 1 ? String.Empty : tempLines[1].TrimStart();
            }

            return lines.ToArray();
        }

        private static int GetSplitLength(string trimmedLine, int maxLength)
        {
            var len = trimmedLine.Length;
            if (len <= maxLength)
            {
                return len;
            }
            else
            {
                var tempLength = maxLength;

                // Track back to the beginning of the current word so that split action happens 
                // between word breaks
                while (tempLength > 0 && !SplitLengthFound(trimmedLine, tempLength--))
                {
                }

                if (tempLength == 0)
                {
                    tempLength = maxLength;

                    // Track back produced a string of 0-length => the current word must then be longer
                    // than the provided maxLength. Run fallback code to return the full word.
                    while (tempLength < len && !SplitLengthFound(trimmedLine, tempLength++))
                    {
                    }

                    return tempLength;
                }
                else
                {
                    return tempLength + 1;
                }
            }
        }

        private static bool SplitLengthFound(string line, int index)
        {
            return Char.IsWhiteSpace(line[index]) && !Char.IsWhiteSpace(line[index - 1]);
        }

        public static bool IsNonNegativeInteger(this string s)
        {
            var regex = new Regex("[^0-9]");
            return !regex.IsMatch(s);
        }

        public static string[] Substrings(this string str, int substringLength, bool startWithRemainder = false)
        {
            int len = str.Length;
            int numberOfFullSplits = Math.DivRem(len, substringLength, out int remainderLength);
            int capacity = remainderLength > 0
                ? numberOfFullSplits + 1
                : numberOfFullSplits;

            var substrings = new List<string>(capacity);

            if (startWithRemainder)
            {
                RemainderSplit(substrings, str, 0, remainderLength);
                FullSizedSplits(substrings, str, remainderLength, substringLength);
            }
            else
            {
                FullSizedSplits(substrings, str, 0, substringLength);
                int startIndex = numberOfFullSplits * substringLength;
                RemainderSplit(substrings, str, startIndex, remainderLength);
            }

            return substrings.ToArray();
        }

        private static void FullSizedSplits(List<string> substrings, string str, int startIndex, int substringLength)
        {
            int stringLength = str.Length;
            int endIndex = stringLength - 1;
            int currentIndex = startIndex;

            while (currentIndex < endIndex)
            {
                var s = str.Substring(currentIndex, substringLength);
                substrings.Add(s);
                currentIndex += substringLength;
            }
        }

        private static void RemainderSplit(List<string> substrings, string str, int startIndex, int substringLength)
        {
            if (substringLength > 0)
            {
                var s = str.Substring(startIndex, substringLength);
                substrings.Add(s);
            }
        }
    }
}