using System;
using Samola.Extensions;

namespace Samola.Algorithms.Utilities
{
    public static class MiscellaneousUtilities
    {
        public static Tuple<int, int> ComputeMultiplicandRangeForPandigitalIdentity(int multiplier, int digits = 9)
        {
            int multiplierDigits = multiplier.ToDigits().Length;

            // NUMBER OF DIGITS IN MULTIPLICATION
            // Let <a> and <b> be s.t. d(<a>) = n and d(<b>) = m
            //  , where d is a function which extracts the number of digits of the provided value
            // Then d(<a>*<b>) will be in [m+n-1, m+n] (e.g. 10 * 10 = 100 and 99 * 99 = 9811)

            // COROLLARY: FIXED AMOUNT OF DIGITS
            // Given a the value <a> and total amount of digits <d> in pandigital identity <a> * <b> = <r>, the
            // number of digits reserved for <b> is given by
            // d(<b>) = (<d> + 1) / 2 - d(<a>)
            int multiplicandDigits = (digits + 1 - 2 * multiplierDigits) / 2;

            // Multiplicand minimum must have the required about of digits
            int multiplicandMin = (int)Math.Pow(10, multiplicandDigits - 1);

            // Multiplicand maximum is such that the result still has resultDigits number of digits
            int resultDigits = digits - multiplierDigits - multiplicandDigits;
            double resultMax = Math.Pow(10, resultDigits) - 1;
            double temp = resultMax / multiplier;
            int multiplicandMax = (int)Math.Floor(temp);

            return Tuple.Create(multiplicandMin, multiplicandMax);
        }
    }
}