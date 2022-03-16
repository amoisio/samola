using Samola.Numbers.CustomTypes;
using System.Linq;
using Samola.Collections;

namespace Samola.Numbers.Fibonacci
{
    public class LargeFibonacciNumbers : StatefulCalculatedEnumerable<LargeInteger, FibonacciState<LargeInteger>>
    {
        public LargeFibonacciNumbers(ICalculationLimit<LargeInteger> calculationLimit = null)
            : base(calculationLimit)
        {

        }
        
        protected override FibonacciState<LargeInteger> InitializeState() => new();
        
        protected override LargeInteger CalculateNext(FibonacciState<LargeInteger> state)
        {
            var last = state.LastValue;
            if (!last.HasValue)
            {
                return new LargeInteger(1);;
            }

            var penultimate = state.PenultimateValue;
            if (!penultimate.HasValue)
            {
                return new LargeInteger(1);
            }

            return last.Value + penultimate.Value;
        }
        
        public static LargeInteger GetNthTerm(int n)
        {
            var numbers = new LargeFibonacciNumbers();
            var nth = numbers.Skip(n - 1).Take(1).First();
            return nth;
        }
    }
}
