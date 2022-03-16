﻿using Samola.Numbers.Utilities;
using System.Linq;
using Samola.Collections.CalculatedEnumerable;

namespace Samola.Numbers.Enumerables
{
    public class AmicableNumbers : StatefulCalculatedEnumerable<int, PreviousValueState<int>>
    {
        private readonly DivisorCalculator _divisorCalculator;

        public AmicableNumbers(DivisorCalculator divisorCalculator, ICalculationLimit<int> calculationLimit)
            : base(calculationLimit)
        {
            _divisorCalculator = divisorCalculator;
        }

        protected override PreviousValueState<int> InitializeState() => new();
        
        protected override int CalculateNext(PreviousValueState<int> state)
        {
            int next = state.PreviousValue + 1;
            int checkSum = CalculateAmicableCheckSumFor(next);
            while (next != checkSum)
            {
                next++;
                checkSum = CalculateAmicableCheckSumFor(next);
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
