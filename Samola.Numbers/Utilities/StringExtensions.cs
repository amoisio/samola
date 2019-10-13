using System;
using System.Collections.Generic;

namespace Samola.Numbers.Utilities
{
    public static class StringExtensions
    {
        public static string[] Substrings(this string s, int startIndex, int length)
        {
            if (startIndex < 0)
                throw new ArgumentException("startIndex must be non-negative.");

            if (startIndex >= s.Length)
                throw new IndexOutOfRangeException("startIndex must be within the string.");

            int strLength = s.Length - startIndex;

            var substrings = new List<string>(strLength / length + 1);

            string t = s;
            int tempStartIndex = startIndex;
            bool isFirst = true;
            while (t.Length > 0)
            {
                int len = Math.Min(length, t.Length - tempStartIndex);
                var str = t.Substring(tempStartIndex, len);
                t = t.Substring(tempStartIndex + len);

                substrings.Add(str);
                if (isFirst)
                {
                    tempStartIndex = 0;
                    isFirst = false;
                }
            }

            return substrings.ToArray();
        }
    }
}
