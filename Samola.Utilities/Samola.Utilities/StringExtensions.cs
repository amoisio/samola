using System;
using System.Collections.Generic;

namespace Samola.Utilities
{
    public static class StringExtensions 
    {
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
            while(tempLine.Length > 0)
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
                    ;

                
                if (tempLength == 0)
                {
                    tempLength = maxLength;

                    // Track back produced a string of 0-length => the current word must then be longer
                    // than the provided maxLength. Run fallback code to return the full word.
                    while (tempLength < len && !SplitLengthFound(trimmedLine, tempLength++))
                        ;

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
    }
}