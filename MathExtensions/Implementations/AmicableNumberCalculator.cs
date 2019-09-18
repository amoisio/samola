using System.Collections.Generic;
using System.Linq;

namespace MathExtensions
{
    public class AmicableNumberCalculator
    {
        private readonly HashSet<long> _nonAmicableNumbers;
        private readonly Dictionary<long, long> _amicableNumbers;
        private readonly DivisorCalculator _divisorCalculator;

        public AmicableNumberCalculator(IPrimesCreator primesCreator)
        {
            _divisorCalculator = new DivisorCalculator(primesCreator);
            _nonAmicableNumbers = new HashSet<long>();
            _amicableNumbers = new Dictionary<long, long>();
        }

        public long? FindAmicableNumber(long a)
        {
            if (_amicableNumbers.ContainsKey(a))
                return _amicableNumbers[a];

            if (_nonAmicableNumbers.Contains(a))
                return null;

            var divisors = _divisorCalculator.GetProperDivisors(a);
            var b = divisors.Sum(); // a = number, d(a) = b = sumNumber

            if (b > 0)
            {
                var sumDivisors = _divisorCalculator.GetProperDivisors(b);
                long bSum = sumDivisors.Sum(); // d(b) = sumSum
                if (a != b && a == bSum)
                {

                    _amicableNumbers.Add(a, b);
                    _amicableNumbers.Add(b, a);
                    return b;
                }
            }

            if (!_nonAmicableNumbers.Contains(a))
                _nonAmicableNumbers.Add(a);

            if (!_nonAmicableNumbers.Contains(b))
                _nonAmicableNumbers.Add(b);

            return null;
        }
    }
}
