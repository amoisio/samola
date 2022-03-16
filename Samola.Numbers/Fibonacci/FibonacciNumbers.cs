using System.Linq;
using Samola.Collections.CalculatedEnumerable;

namespace Samola.Numbers.Fibonacci
{
    public class FibonacciNumbers : StatefulCalculatedEnumerable<int, FibonacciState<int>>
    {
        public FibonacciNumbers(ICalculationLimit<int> calculationLimit = null)
            : base(calculationLimit)
        {

        }
        
        protected override FibonacciState<int> InitializeState() => new();
        
        protected override int CalculateNext(FibonacciState<int> state)
        {
            var last = state.LastValue;
            if (!last.HasValue)
            {
                return 1;
            }

            var penultimate = state.PenultimateValue;
            if (!penultimate.HasValue)
            {
                return 1;
            }

            return last.Value + penultimate.Value;
        }
        
        public static int GetNthTerm(int n)
        {
            var numbers = new FibonacciNumbers();
            var nth = numbers.Skip(n - 1).Take(1).First();
            return nth;
        }
    }
}
