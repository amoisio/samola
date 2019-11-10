using System;
using System.Collections.Generic;

namespace Samola.Utilities
{
    public static class StringExtensions 
    {
        public static string[] SplitToMaxLengthLines(this string line, int maxLength)
        {
            if (maxLength <= 0)
                throw new ArgumentException("must be greater than 0.", nameof(maxLength));

            List<string> lines = new List<string>();

            string tempLine = line.Trim();
            int tempIndex = 0;
            while(tempLine.Length > 0)
            {
                var tempLength = GetSplitLength(tempLine, maxLength);
                var newLine = tempLine.Substring(tempIndex, tempLength).TrimEnd();
                tempLine = tempLine.Substring(tempLength).TrimStart();
                lines.Add(newLine);
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

                while (!SplitLengthFound(trimmedLine, tempLength--))
                    ;

                return tempLength + 1;
            }
        }

        private static bool SplitLengthFound(string line, int index)
        {
            return Char.IsWhiteSpace(line[index]) && !Char.IsWhiteSpace(line[index - 1]);
        }
    }
}
