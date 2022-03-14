using Samola.Numbers.Utilities;
using System.Collections.Generic;
using System.Linq;
using Samola.Collections;

namespace Samola.Numbers.Enumerables
{
    public class AmicableNumbers : CalculatedEnumerable<int>
    {
        private readonly DivisorCalculator _divisorCalculator;

        public AmicableNumbers(DivisorCalculator divisorCalculator, ICalculationLimit<int> calculationLimit)
            : base(calculationLimit)
        {
            _divisorCalculator = divisorCalculator;
        }

        protected override int CalculateNext(IReadOnlyList<int> previousItems)
        {
            int next = GetInitialItemToEvaluate(previousItems);
            int checkSum = CalculateAmicableCheckSumFor(next);
            while (next != checkSum)
            {
                next++;
                checkSum = CalculateAmicableCheckSumFor(next);
            }
            return next;
        }

        private int GetInitialItemToEvaluate(IReadOnlyList<int> previousItems)
        {
            int next = 1;
            if (previousItems != null && previousItems.Any())
            {
                next = previousItems.Last() + 1;
            }
            return next;
        }

        /// <summary>
        /// Amicable checksum is the sum of proper divisors of the sum of
        /// proper divisors of the given item.
        /// </summary>
        private int CalculateAmicableCheckSumFor(int item)
        {
            var nextDivisors = _divisorCalculator.GetProperDivisors(item);
            var friend = nextDivisors.Sum(); // a = number, d(a) = b = sumNumber
            // Zero the checksum for items who are amicable with themselves or with no one 
            if (friend == 0 || friend == item)
            {
                return 0;
            }
            var friendDivisors = _divisorCalculator.GetProperDivisors(friend);
            return friendDivisors.Sum(); // d(b) = sumSum
        }
    }
}
