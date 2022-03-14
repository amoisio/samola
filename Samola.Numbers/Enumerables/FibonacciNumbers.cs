﻿using Samola.Numbers.CustomTypes;
using System.Collections.Generic;
using System.Linq;
using Samola.Collections;

namespace Samola.Numbers.Enumerables
{
    public class FibonacciNumbers : CalculatedEnumerable<int>
    {
        public FibonacciNumbers(MaximumYieldedCountLimit<int> integerLimit)
            : base(integerLimit)
        {

        }
        
        protected override int CalculateNext(IReadOnlyList<int> previousItems)
        {
            if (previousItems == null)
            {
                return 1;
            }
            
            int len = previousItems.Count();
            if (len < 2)
            {
                return 1;
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
