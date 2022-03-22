using System.Linq;
using Samola.Algorithms.CalculatedEnumerable;
using Samola.Algorithms.Utilities;

namespace Samola.Algorithms.Sequences
{
    public class FibonacciNumbers : CalculatedEnumerable<int, FibonacciState<int>>
    {
        protected override int CalculateInitial(FibonacciState<int> state) => 1;

        protected override int CalculateNext(FibonacciState<int> state)
        {
            var penultimate = state.PenultimateYieldedValue;
            if (penultimate == 0)
            {
                return 1;
            }
            var last = state.PreviouslyYieldedItem;
            return last + penultimate;
        }
        
        public static int GetNthTerm(int n)
        {
            var numbers = new FibonacciNumbers();
            var nth = numbers.Skip(n - 1).Take(1).First();
            return nth;
        }
    }
}
