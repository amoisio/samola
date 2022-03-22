using Samola.Algorithms.CalculatedEnumerable;
using Samola.Algorithms.CalculatedEnumerable.State;
using Samola.Algorithms.Utilities;

namespace Samola.Algorithms.Sequences
{
    public class PrimeNumbersSimple : CalculatedEnumerable<int, DefaultEnumerationState<int>>, IPrimeNumerable<int>
    {
        protected override int CalculateInitial(DefaultEnumerationState<int> state) => 2; 

        protected override int CalculateNext(DefaultEnumerationState<int> state)
        {
            var next = state.PreviouslyYieldedItem + 1;
            while (!IsPrime(next))
            {
                next++;
            }
            return next;
        }

        public bool IsPrime(int number)
        {
            if (number < 1)
            {
                return false;
            }

            if (number <= 3)
            {
                return true;
            }

            if (number % 2 == 0 || number % 3 == 0)
            {
                return false;
            }

            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
