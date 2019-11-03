using Samola.Numbers.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samola.Numbers.CustomTypes
{
    public struct ArrayedNumber
    {
        public readonly int[] digits;
        
        public ArrayedNumber(int number)
            : this(number.ToDigits())
        {

        }

        public ArrayedNumber(int[] digits)
        {
            this.digits = digits;
        }

        public static ArrayedNumber operator+(ArrayedNumber left, int right)
        {
            return left + new ArrayedNumber(right.ToDigits());
        }

        public static ArrayedNumber operator +(int left, ArrayedNumber right)
        {
            return right + left;
        }

        public static ArrayedNumber operator+ (ArrayedNumber left, ArrayedNumber right)
        {
            int digitsLeft = left.digits.Length;
            int digitsRight = right.digits.Length;
            int maxDigits = Math.Max(digitsLeft, digitsRight);

            int digitsResult = maxDigits + 1;
            List<int> digits = new List<int>(digitsResult);

            int carry = 0;

            for (int i = 0; i < digitsResult; i++)
            {
                int leftop = i < digitsLeft ? left.digits[i] : 0;
                int rightop = i < digitsRight ? right.digits[i] : 0;
                carry = Math.DivRem(leftop + rightop + carry, 10, out int result);

                if (i < digitsResult - 1 || result != 0)
                    digits.Add(result); 
            }

            return new ArrayedNumber(digits.ToArray());
        }

        public static ArrayedNumber operator *(ArrayedNumber left, int right)
        {
            int digitsLeft = left.digits.Length;
            int digitsRight = (int)Math.Floor(Math.Log10(right)) + 1;

            int digitsResult = digitsLeft + digitsRight + 1;
            List<int> digits = new List<int>(digitsResult);

            int carry = 0;

            int rightop = right;
            for (int i = 0; i < digitsLeft; i++)
            {
                int leftop = left.digits[i];
                carry = Math.DivRem(leftop * rightop + carry, 10, out int result);
                digits.Add(result);
            }

            while(carry != 0)
            {
                carry = Math.DivRem(carry, 10, out int result);
                digits.Add(result);
            }

            return new ArrayedNumber(digits.ToArray());
        }
    }
}