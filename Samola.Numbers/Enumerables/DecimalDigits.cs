using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Samola.Numbers.Enumerables
{
    // TODO: Move to Samola.Numbers, document the algorithm and see if it can be cleaned up - the remainders part / yieldhistory part is messy
    public class DecimalDigits : IEnumerable<int>
    {
        /// <summary>
        /// The divisor
        /// </summary>
        private readonly int _denominator;
        /// <summary>
        /// The minimum number of decimals to yield in case of a recurrence
        /// </summary>
        private int _tailCount;
        /// <summary>
        /// Temporary variable holding the next dividend. Initialized at 10 because we only yield decimal digits
        /// </summary>
        private int _temp = 10;
        /// <summary>
        /// Contains the remainder history (which lets us detemine the recurrence) and a link to the yielded digit.
        /// Key : remainder, Value : a list of (digit, yield history index) tuples which share the same remainder.
        /// e.g. in 1/6 (digit, remainder)_0 = (1, 4) and (digit, remainder)_N = (6, 4)
        /// </summary>
        private Dictionary<int, Dictionary<int, int>> _remainders;
        /// <summary>
        /// List of yielded digits in the order in which they were yielded. Recurringly yielded digits not included
        /// </summary>
        private List<int> _yieldHistory;

        public DecimalDigits(int denominator, int tailCount = 5)
        {
            _denominator = denominator;
            _tailCount = tailCount;
            _remainders = new Dictionary<int, Dictionary<int, int>>(10);
            _yieldHistory = new List<int>(10);
        }

        /// <summary>
        /// The remainder value from which onwards the recurrence occurs
        /// </summary>
        private int _recurrenceRemainder;
        private int _recurrenceDigit;

        public bool RecurringDecimalFractionFound { get; private set; }

        private string _recurringDecimalFraction;
        /// <summary>
        /// String representation of the recurring decimal fraction
        /// </summary>
        public string RecurringDecimalFraction
        {
            get
            {
                if (RecurringDecimalFractionFound && String.IsNullOrEmpty(_recurringDecimalFraction))
                {
                    var sb = new StringBuilder();

                    /// TODO: fix, problem here or in how yieldhistory is indexed

                    var startIndex = _remainders[_recurrenceRemainder][_recurrenceDigit];
                    var len = _yieldHistory.Count;
                    for (int i = startIndex; i < len; i++)
                    {
                        sb.Append(_yieldHistory[i]);
                    }
                    _recurringDecimalFraction = sb.ToString();
                }
                return _recurringDecimalFraction;
            }
        }

        public string DecimalPart
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                var startIndex = RecurringDecimalFractionFound
                    ? _remainders[_recurrenceRemainder][_recurrenceDigit]
                    : _yieldHistory.Count;

                for (int i = 0; i < startIndex; i++)
                {
                    sb.Append(_yieldHistory[i]);
                }

                if (RecurringDecimalFractionFound)
                {
                    sb.Append("(");
                    sb.Append(RecurringDecimalFraction);
                    sb.Append(")");
                }

                return sb.ToString();
            }
        }

        public IEnumerator<int> GetEnumerator()
        {
            int remainder;
            int digit = Math.DivRem(_temp, _denominator, out remainder);

            yield return digit;

            while (remainder > 0 && _tailCount > 0)
            {
                if (_remainders.ContainsKey(remainder))
                {
                    if (_remainders[remainder].ContainsKey(digit))
                    {
                        if (!RecurringDecimalFractionFound)
                        {
                            RecurringDecimalFractionFound = true;
                            _recurrenceRemainder = remainder;
                            _recurrenceDigit = digit;

                            // +1 to balance out the very first yield
                            // +1 to balance out the very last yield
                            _tailCount -= _yieldHistory.Count + 2;
                        }
                        else
                        {
                            _tailCount--;
                        }
                    }
                    else
                    {
                        _remainders[remainder].Add(digit, _yieldHistory.Count);
                        _yieldHistory.Add(digit);
                    }
                }
                else
                {
                    _remainders.Add(remainder, new Dictionary<int, int>() { { digit, _yieldHistory.Count } });
                    _yieldHistory.Add(digit);
                }

                _temp = remainder * 10;
                digit = Math.DivRem(_temp, _denominator, out remainder);

                yield return digit;
            }

            if (remainder == 0)
            {
                _yieldHistory.Add(digit);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
