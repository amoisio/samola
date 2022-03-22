using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Samola.Extensions;

namespace Samola.Algorithms.Utilities
{
    public class LargeIntegerContext
    {
        public const int MAX_DIGIT_PLACES = 9;
        private readonly int _digitPlaces;

        public LargeIntegerContext()
            : this(MAX_DIGIT_PLACES)
        {
        }

        public LargeIntegerContext(int digitPlaces)
        {
            if (digitPlaces > MAX_DIGIT_PLACES)
                throw new ArgumentException($"digitPlaces must be less or equal to {MAX_DIGIT_PLACES}.");

            if (digitPlaces < 1)
                throw new ArgumentException("digitPlaces must be at least 1.");

            _digitPlaces = digitPlaces;
        }

        public LargeInteger CreateInteger(int integer)
        {
            return CreateInteger(integer.ToString());
        }

        public LargeInteger CreateInteger(string integer)
        {
            return new LargeInteger(integer, _digitPlaces);
        }
    }

    /// <summary>
    /// Represents an arbitrarily large integer number. Internally, values are stored as an array of Int32s.
    /// </summary>
    public struct LargeInteger : IComparable<LargeInteger>
    {
        public bool Equals(LargeInteger other)
        {
            return this == other;
        }

        public override bool Equals(object obj)
        {
            return this.Equals((LargeInteger) obj);
        }

        public override int GetHashCode()
        {
            return _values.GetHashCode();
        }

        /// <summary>
        /// Each internal data storage item can store up a value up to DigitPlaces digits.
        /// </summary>
        public int DigitPlaces { get; }

        /// <summary>
        /// Internal storage of data items. Data is stored as numbers with fixed amount of digits. The least significant 
        /// digits are in the index 0.
        /// </summary>
        private List<int> _values;

        /// <summary>
        /// Gets the data items stored in the internal storage as an array.
        /// </summary>
        public int[] Values => _values.ToArray();

        /// <summary>
        /// Digits of the number stored in such a way that the least significant digit is in index 0, the next in index 1 and so forth.
        /// </summary>
        public IEnumerable<int> Digits
        {
            get
            {
                int count = _values.Count;
                for (int i = 0; i < count; i++)
                {
                    int number = _values[i];
                    for (int j = 0; j < this.DigitPlaces; j++)
                    {
                        if (i == count - 1 && number == 0)
                            yield break;

                        number = Math.DivRem(number, 10, out int remainder);
                        yield return remainder;
                    }
                }
            }
        }

        public LargeInteger(int integer, int digitPlaces = 9) : this(integer.ToString(), digitPlaces)
        {
        }

        public LargeInteger(string integerStr, int digitPlaces = 9)
        {
            if (String.IsNullOrWhiteSpace(integerStr))
                throw new ArgumentNullException(nameof(integerStr));

            if (!integerStr.IsNonNegativeInteger())
                throw new ArgumentException($"{nameof(integerStr)} must represent a non-negative integer.");

            if (digitPlaces > 9)
                throw new ArgumentException($"{nameof(digitPlaces)} must be 9 or less.");

            if (digitPlaces < 1)
                throw new ArgumentException($"{nameof(digitPlaces)} must be 1 or more.");

            _values = new List<int>(ParseIntegersForInternalStorage(integerStr, digitPlaces));
            this.DigitPlaces = digitPlaces;
        }

        private static int[] ParseIntegersForInternalStorage(string integerStr, int digitPlaces)
        {
            return integerStr
                .Substrings(digitPlaces, true) //true == Start with the remainder (ie. the most significant digits)
                .Reverse() // Least significant digits in index 0
                .Select(e => Int32.Parse(e))
                .ToArray();
        }

        public static LargeInteger operator +(LargeInteger left, int right)
        {
            var newValues = new List<int>(left._values);
            int digitPlaces = left.DigitPlaces;

            Add(newValues, right, 0, digitPlaces);

            var result = new LargeInteger(0, digitPlaces);
            result._values = newValues;
            return result;
        }

        public static LargeInteger operator +(int left, LargeInteger right)
        {
            return right + left;
        }

        public static LargeInteger operator +(LargeInteger left, LargeInteger right)
        {
            if (left.DigitPlaces != right.DigitPlaces)
            {
                // TODO: Change this as the internal storage type should not determine whether a basic operation works or not
                throw new ArgumentException("Operands must have the same number of digits.");
            }

            var newValues = new List<int>(left._values);
            int digitPlaces = left.DigitPlaces;

            int len = right._values.Count();
            for (int i = 0; i < len; i++)
            {
                Add(newValues, right._values[i], i, digitPlaces);
            }

            var result = new LargeInteger(0, digitPlaces);
            result._values = newValues;
            return result;

            //if (left._values.Count == 1)
            //    return right + left._values[0];

            //if (right._values.Count == 1)
            //    return left + right._values[0];

            //int maxSize = Math.Max(left._values.Count, right._values.Count);

            //List<int> lvalues = new List<int>(Enumerable.Repeat(0, maxSize));
            //for (int i = 0; i < left._values.Count; i++)
            //    lvalues[i] = left._values[i];

            //List<int> rvalues = new List<int>(Enumerable.Repeat(0, maxSize));
            //for (int i = 0; i < right._values.Count; i++)
            //    rvalues[i] = right._values[i];

            //List<int> result = new List<int>(Enumerable.Repeat(0, maxSize));

            //int count = maxSize;
            //int temp = 0;
            //for (int i = 0; i < count; i++)
            //{
            //    int number = lvalues[i] + rvalues[i] + temp;
            //    temp = Math.DivRem(number, (int)Math.Pow(10, 9/*DIGIT_PLACES*/), out int remainder);
            //    result[i] = remainder;
            //}

            //if (temp > 0)
            //    result.Add(temp);

            //return new LargeInteger(result.ToArray());
        }

        /// <summary>
        /// Adds operand 'right' with given 'offset' to the number represented by the internal storage 'leftStorage'. 
        /// Modifies the array in-place.
        /// </summary>
        /// <param name="leftStorage"></param>
        /// <param name="right"></param>
        /// <param name="offset"></param>
        /// <param name="digitPlaces"></param>
        private static void Add(List<int> leftStorage, int right, int offset, int digitPlaces)
        {
            List<int> values = leftStorage;
            int count = values.Count;
            int temp = right;

            // Recalculate the internal storage values starting from the offset index
            for (int i = offset; i < count; i++)
            {
                int number = values[i] + temp;
                temp = Math.DivRem(number, (int) Math.Pow(10, digitPlaces), out int remainder);
                values[i] = remainder;

                // Break if the division yields 0 (as the remaining values in the array will remain the same)
                if (temp == 0) break;
            }

            // Calculate "overflowing" numbers in the case the result cannot fit in the original array
            while (temp > 0)
            {
                temp = Math.DivRem(temp, (int) Math.Pow(10, digitPlaces), out int remainder);
                values.Add(remainder);
            }
        }

        private static void Multiply(List<int> leftStorage, int right, int offset, int digitPlaces)
        {
            List<int> values = leftStorage;
            int count = values.Count;
            int temp = right;
            int carry = 0;

            for (int i = 0; i < count; i++)
            {
                // In multiplication, the temporary storage must be a long as our max result (digitPlaces digits) is
                // (10^digitPlaces - 1) * Int32.MaxValue + Int32.MaxValue = 10^digitPlaces * Int32.MaxValue
                long number = checked((long) values[i] * temp + carry);

                // cast is safe as carry will always remain <= Int32.MaxValue
                carry = (int) Math.DivRem(number, (long) Math.Pow(10, digitPlaces), out long remainder);

                // cast is safe as remainder will always remain <= 10^digitPlaces
                values[i] = (int) remainder;
            }

            // Calculate "overflowing" numbers in the case the result cannot fit in the original array
            while (carry > 0)
            {
                carry = (int) Math.DivRem(carry, (long) Math.Pow(10, digitPlaces), out long remainder);
                values.Add((int) remainder);
            }
        }

        public static LargeInteger operator *(LargeInteger left, int right)
        {
            var newValues = new List<int>(left._values);
            int digitPlaces = left.DigitPlaces;

            Multiply(newValues, right, 0, digitPlaces);

            var result = new LargeInteger(0, digitPlaces);
            result._values = newValues;
            return result;
        }

        public static LargeInteger operator *(int left, LargeInteger right)
        {
            return right * left;
        }

        public static bool operator ==(LargeInteger left, LargeInteger right)
        {
            var lcount = left._values.Count;
            var rcount = right._values.Count;
            if (lcount != rcount)
            {
                return false;
            }

            for (var i = 0; i < lcount; i++)
            {
                if (left._values[i] != right._values[i])
                {
                    return false;
                }
            }

            return true;
        }

        public static bool operator !=(LargeInteger left, LargeInteger right)
        {
            return !(left == right);
        }

        //public static LargeInteger operator *(LargeInteger left, LargeInteger right)
        //{
        //    if (left._values.Count == 1)
        //        return right * left.Values.First();

        //    if (right._values.Count == 1)
        //        return left * right.Values.First();

        //    int maxSize = Math.Max(left._values.Count, right._values.Count);

        //    List<int> lvalues = new List<int>(Enumerable.Repeat(0, maxSize));
        //    for (int i = 0; i < left._values.Count; i++)
        //        lvalues[i] = left._values[i];

        //    List<int> rvalues = new List<int>(Enumerable.Repeat(0, maxSize));
        //    for (int i = 0; i < right._values.Count; i++)
        //        rvalues[i] = right._values[i];

        //    List<int> result = new List<int>(Enumerable.Repeat(0, maxSize));

        //    20 31
        //    11 11

        //    11*31 = 341 = 341 => 3 41

        //    20*11 = 220 (100) => 220 + 341 + 3 = 564 = 5 64
        //    11*31 = 341 (100)

        //    20*11 = 220 (100 00) => 2 25 

        //    2 256 441


        //    int count = maxSize;
        //    int temp = 0;
        //    for (int i = 0; i < count; i++)
        //    {
        //        int number = lvalues[i] * rvalues[i] + temp;
        //        temp = Math.DivRem(number, (int)Math.Pow(10, DIGIT_PLACES), out int remainder);
        //        result[i] = remainder;
        //    }

        //    if (temp > 0)
        //        result.Add(temp);

        //    return new LargeInteger(result.ToArray());
        //}

        public int CompareTo(LargeInteger other)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            int len = _values.Count;

            sb.Append(_values[len - 1].ToString());
            for (int i = len - 2; i >= 0; i--)
            {
                sb.Append(_values[i].ToString($"D{this.DigitPlaces}"));
            }

            return sb.ToString();
        }
    }
}