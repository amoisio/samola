using Samola.Numbers.Cache;
using Samola.Numbers.Utilities;
using System.Collections.Generic;
using System.Linq;

namespace Samola.Numbers.Enumerables
{
    public class AmicableNumbers : CalculatedEnumerable<int>
    {
        private readonly HashSet<int> _amicableNumbers;
        private readonly DivisorCalculator _divisorCalculator;

        internal AmicableNumbers(DivisorCalculator divisorCalculator, IntegerLimit integerLimit, IEnumerableCacheProvider<int> cacheProvider)
            : base(integerLimit, cacheProvider)
        {
            _divisorCalculator = divisorCalculator;
            _amicableNumbers = new HashSet<int>();
        }

        protected override IEnumerable<int> GetItems(int[] previousItems)
        {
            int next = 1;
            if (previousItems != null && previousItems.Length > 0)
            {
                next = previousItems[previousItems.Length - 1] + 1;
            }

            while (true)
            {
                int a = next;
                if (_amicableNumbers.Contains(a))
                    yield return a;

                var divisors = _divisorCalculator.GetProperDivisors(a);
                int b = divisors.Sum(); // a = number, d(a) = b = sumNumber

                if (b > 0)
                {
                    var sumDivisors = _divisorCalculator.GetProperDivisors(b);
                    int bSum = sumDivisors.Sum(); // d(b) = sumSum
                    if (a < b && a == bSum)
                    {
                        _amicableNumbers.Add(a);
                        _amicableNumbers.Add(b);
                        yield return a;
                    }
                }

                next++;
            }
        }
    }
}
