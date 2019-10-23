using System;
using System.Text;

namespace Samola.Numbers.Counters
{
    /// <summary>
    /// DialCounter 
    /// - Has N dials with numbers from 0 to 9
    /// - Count-down dial - dials are initially at 9 and count down to 0
    /// - TODO: Add count-up functionality
    /// - Dials have different weights.
    /// - The left most dial is most significant and the right most is least significant
    /// - Dials rolls according to the following rules
    /// - 1. Dials _never_ roll upwards
    /// - 2. The exception to this is that dials can roll "roll over" from 0 back to 9
    /// - 3. When a dial rolls normally, all dials to the left of it are unaffected
    /// - 4. When a dial rolls normally, all dials to the right of it are reset to the same value 
    /// -    i.e. 9743, 7 rolls to 6, new value is 9666
    /// - 5. When a dial rolls over, the dial immediately to the left is rolled once
    /// -    ie. 9703, 0 rolls to 9, intermediate value: 9693, new value is 9666
    /// -    ie. 9000, last zero rolls to 9, changes: 9009, 9099, 9999, 8999, 8888
    /// </summary>
    public class DialCounter
    {
        private readonly int[] _dials;

        public DialCounter(int n)
        {
            _dials = new int[n];
            Dials = n;
            InitializeDials();
        }

        public DialCounter(int[] dials)
        {
            _dials = dials;
            Dials = dials.Length;
        }

        private void InitializeDials()
        {
            for (int i = 0; i < Dials; i++)
            {
                _dials[i] = 9;
            }
        }

        public int Dials { get; }

        /// <summary>
        /// Roll dial once
        /// </summary>
        /// <param name="dialIndex">0 based index of the dial. 0 being the most significant dial.</param>
        public bool Roll(int dialIndex)
        {
            if (dialIndex < 0 || dialIndex >= Dials)
                throw new IndexOutOfRangeException("dialIndex is out of range");

            int oldValue = _dials[dialIndex];
            bool isRollOver = (oldValue == 0);

            int affectedIndex = -1;
            if (isRollOver)
            {
                for (int i = dialIndex - 1; i >= 0; i--)
                {
                    if (_dials[i] > 0)
                    {
                        affectedIndex = i;
                        break;
                    }
                }
            }
            else
            {
                affectedIndex = dialIndex;
            }

            if (affectedIndex == -1)
            {
                // The counter has been spent.
                return false;
            }
            else
            {
                int newValue = _dials[affectedIndex] - 1;
                for (int i = affectedIndex; i < Dials; i++)
                {
                    _dials[i] = newValue;
                }
                return true;
            }
        }

        public void Reset()
        {
            InitializeDials();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Dials; i++)
            {
                sb.Append(_dials[i]);
            }
            return sb.ToString();
        }
    }
}
