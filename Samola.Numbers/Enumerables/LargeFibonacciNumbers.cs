using Samola.Numbers.CustomTypes;
using System.Collections.Generic;
using System.Linq;
using Samola.Collections;

namespace Samola.Numbers.Enumerables
{
    public class LargeFibonacciNumbers : CalculatedEnumerable<LargeInteger>
    {
        public LargeFibonacciNumbers(MaximumYieldedCountLimit<LargeInteger> integerLimit)
            : base(integerLimit)
        {

        }
        
        protected override LargeInteger CalculateNext(IReadOnlyList<LargeInteger> previousItems)
        {
            if (previousItems == null)
            {
                return new LargeInteger(1);
            }
            
            int len = previousItems.Count();
            if (len < 2)
            {
                return new LargeInteger(1);
            }
            else
            {
                var last = previousItems[len - 1];
                var penultimate = previousItems[len - 2];
                return last + penultimate;
            }
        }
    }
}