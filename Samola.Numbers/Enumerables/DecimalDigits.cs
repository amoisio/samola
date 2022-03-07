using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Samola.Numbers.Enumerables
{
    public class DecimalDigits : IEnumerable<int>
    {
        /// <summary>
        /// The divisor.
        /// </summary>
        private readonly int _denominator;
        /// <summary>
        /// The minimum number of decimals to yield in case of a recurrence.
        /// </summary>
        private readonly int _tailCount;
        /// <summary>
        /// Internal counter to keep track of tail digits still to be yielded.
        /// </summary>
        private int _tailTemp;
        /// <summary>
        /// Temporary variable holding the next dividend. Initialized at 10 because we only yield decimal digits.
        /// </summary>
        private int _nominator = 10;
        /// <summary>
        /// Internal state - contains the rules and logic for detecting recurrences in the decimal fraction
        /// </summary>
        private readonly RemainderState _state;
        /// <summary>
        /// List of yielded digits in the order in which they were yielded. Recurringly yielded digits not included
        /// </summary>
        private readonly List<int> _yieldHistory;

        public DecimalDigits(int denominator, int tailCount = 5)
        {
            _denominator = denominator;
            _tailCount = tailCount;
            _tailTemp = tailCount;
            _state = new RemainderState();
            _yieldHistory = new List<int>(10);
        }

        public bool HasRecurrence => _state.HasRecurrence;

        /// <summary>
        /// String representation of the recurring decimal fraction
        /// </summary>
        public string RecurringDecimalFraction
        {
            get
            {
                string decimalFraction = null;
                if (_state.HasRecurrence)
                {
                    var sb = new StringBuilder();
                    var startIndex = _state.RecurrenceStartIndex;
                    var len = _yieldHistory.Count;
                    for (int i = startIndex; i < len; i++)
                    {
                        sb.Append(_yieldHistory[i]);
                    }
                    decimalFraction = sb.ToString();
                }
                return decimalFraction;
            }
        }


        public string DecimalPart
        {
            get
            {
                StringBuilder sb = new StringBuilder();

                var startIndex = _state.HasRecurrence
                    ? _state.RecurrenceStartIndex
                    : _yieldHistory.Count;

                for (int i = 0; i < startIndex; i++)
                {
                    sb.Append(_yieldHistory[i]);
                }

                if (_state.HasRecurrence)
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
            int digit = Math.DivRem(_nominator, _denominator, out remainder);

            yield return digit;

            while (remainder > 0 && _tailTemp > 0)
            {
                _state.CheckForRecurrence(digit, remainder, _yieldHistory.Count);
                if (_state.HasRecurrence)
                {
                    if (_tailTemp == _tailCount)
                    {
                        // +1 to balance out the very first yield
                        // +1 to balance out the very last yield
                        _tailTemp -= _yieldHistory.Count + 2;
                    }
                    else
                    {
                        _tailTemp--;
                    }
                }
                else
                {
                    _yieldHistory.Add(digit);
                }

                _nominator = remainder * 10;
                digit = Math.DivRem(_nominator, _denominator, out remainder);

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


        private class RemainderState
        {
            /// <summary>
            /// Contains the remainder history (which lets us detemine the recurrence) and a link to the yielded digit.
            /// Key : remainder, Value : a list of (digit, yield history index) tuples which share the same remainder.
            /// e.g. in 1/6 (digit, remainder)_0 = (1, 4) and (digit, remainder)_N = (6, 4)
            /// </summary>
            private readonly Dictionary<int, Dictionary<int, int>> _remainders;
            /// <summary>
            /// The remainder value from which onwards the recurrence occurs
            /// </summary>
            private int _recurrenceRemainder;

            private int _recurrenceDigit;

            public RemainderState()
            {
                _remainders = new Dictionary<int, Dictionary<int, int>>(10);
                HasRecurrence = false;
            }

            public int RecurrenceStartIndex => _remainders[_recurrenceRemainder][_recurrenceDigit];

            public bool HasRecurrence { get; private set; }

            public void CheckForRecurrence(int digit, int remainder, int yieldIndex)
            {
                if (!HasRecurrence)
                {
                    if (_remainders.ContainsKey(remainder))
                    {
                        if (_remainders[remainder].ContainsKey(digit))
                        {
                            // Recurrence in the decimal fraction has been detected
                            HasRecurrence = true;
                            _recurrenceRemainder = remainder;
                            _recurrenceDigit = digit;
                        }
                        else
                        {
                            // No recurrence - remainder is know but digit is new
                            _remainders[remainder].Add(digit, yieldIndex);
                        }
                    }
                    else
                    {
                        // No recurrence - both remainder and digit are new
                        _remainders.Add(remainder, new Dictionary<int, int>() { { digit, yieldIndex } });
                    }
                }
            }
        }
    }
}
