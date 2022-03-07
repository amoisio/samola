using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Samola.Numbers.Utilities
{
    public static class StringExtensions
    {
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
                FullsizedSplits(substrings, str, remainderLength, substringLength);
            }
            else
            {
                FullsizedSplits(substrings, str, 0, substringLength);
                int startIndex = numberOfFullSplits * substringLength;
                RemainderSplit(substrings, str, startIndex, remainderLength);
            }
            return substrings.ToArray();
        }

        private static void FullsizedSplits(List<string> substrings, string str, int startIndex, int substringLength)
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
