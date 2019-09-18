using System.Collections.Generic;
using System.Linq;

namespace MathExtensions
{
    public class AmicableNumberCalculator
    {
        private readonly HashSet<int> _nonAmicableNumbers;
        private readonly Dictionary<int, int> _amicableNumbers;
        private readonly DivisorCalculator _divisorCalculator;

        public AmicableNumberCalculator(IPrimesCreator primesCreator)
        {
            _divisorCalculator = new DivisorCalculator(primesCreator);
            _nonAmicableNumbers = new HashSet<int>();
            _amicableNumbers = new Dictionary<int, int>();
        }

        public int? FindAmicableNumber(int a)
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
                int bSum = sumDivisors.Sum(); // d(b) = sumSum
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
