using System.Linq;
using Samola.Algorithms.CalculatedEnumerable;
using Samola.Algorithms.CalculatedEnumerable.State;

namespace Samola.Algorithms.Sequences
{
    public class AmicableNumbers : CalculatedEnumerable<(int, int), DefaultEnumerationState<(int, int)>>
    {
        private readonly DivisorCalculator _divisorCalculator;
        private readonly int _initialValue;

        public AmicableNumbers(DivisorCalculator divisorCalculator, int initialValue)
        {
            _divisorCalculator = divisorCalculator;
            _initialValue = initialValue;
        }

        protected override (int, int) CalculateInitial(DefaultEnumerationState<(int, int)> state)
        {
            return CalculateAmicableFriend(_initialValue);
        }

        protected override (int, int) CalculateNext(DefaultEnumerationState<(int, int)> state)
        {
            int next = state.PreviouslyYieldedItem.Item1 + 1;
            return CalculateAmicableFriend(next);
        }

        private (int, int) CalculateAmicableFriend(int startFrom)
        {
            int item = startFrom;
            int friend, friendOfFriend = 0;
            do
            {
                var nextDivisors = _divisorCalculator.GetProperDivisors(item);
                friend = nextDivisors.Sum(); // a = number, d(a) = b = sumNumber
                if (friend <= item)
                {
                    friendOfFriend = 0;
                    continue;
                }
                var friendDivisors = _divisorCalculator.GetProperDivisors(friend);
                friendOfFriend = friendDivisors.Sum(); // d(b) = sumSum
            } while (item++ != friendOfFriend);
            return (item - 1, friend);
        }
    }
}
