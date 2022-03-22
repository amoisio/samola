using System.Linq;
using Samola.Algorithms.CalculatedEnumerable;
using Samola.Algorithms.Utilities;

namespace Samola.Algorithms.Sequences
{
    public class LargeFibonacciNumbers : CalculatedEnumerable<LargeInteger, FibonacciState<LargeInteger>>
    {
        protected override LargeInteger CalculateInitial(FibonacciState<LargeInteger> state) => new(1);

        protected override LargeInteger CalculateNext(FibonacciState<LargeInteger> state)
        {
            var penultimate = state.PenultimateYieldedValue;
            if (penultimate == new LargeInteger(0))
            {
                return new LargeInteger(1);
            }
            var last = state.PreviouslyYieldedItem;
            return last + penultimate;
        }
        
        public static LargeInteger GetNthTerm(int n)
        {
            var numbers = new LargeFibonacciNumbers();
            var nth = numbers.Skip(n - 1).Take(1).First();
            return nth;
        }
    }
}
