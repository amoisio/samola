using Samola.Numbers.CustomTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samola.Numbers.Utilities
{
    public static class Fibonacci
    {
        public static LargeInteger GetNth(long n)
        {
            if (n < 1)
                throw new ArgumentException("n must be greater than 0");

            LargeInteger temp = new LargeInteger(0);
            if (n == 1 || n == 2)
                temp = new LargeInteger(1);
            else
            {
                LargeInteger n0 = new LargeInteger(1);
                LargeInteger n1 = new LargeInteger(1);
                for (long i = 3; i <= n; i++)
                {
                    temp = n0 + n1;
                    n0 = n1;
                    n1 = temp;
                }
            }

            return temp;
        }

        public static long GetNthTerm(long n)
        {
            if (n < 1)
                throw new ArgumentException("n must be greater than 0");

            long temp = 0;
            if (n == 1 || n == 2)
                temp = 1;
            else
            {
                long n0 = 1;
                long n1 = 1;
                for (long i = 3; i <= n; i++)
                {
                    temp = n0 + n1;
                    n0 = n1;
                    n1 = temp;
                }
            }

            return temp;
        }
    }
}
