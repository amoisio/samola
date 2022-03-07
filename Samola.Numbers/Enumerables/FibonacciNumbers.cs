using Samola.Numbers.Cache;
using Samola.Numbers.CustomTypes;
using System;
using System.Collections.Generic;

namespace Samola.Numbers.Enumerables
{
    // TODO: comments...
    public class FibonacciNumbers : CalculatedEnumerable<LargeInteger>
    {
        internal FibonacciNumbers(LargeIntegerCountLimit integerLimit, IEnumerableCacheProvider<LargeInteger> cacheProvider)
            : base(integerLimit, cacheProvider)
        {
            
        }

        protected override IEnumerable<LargeInteger> GetItems(LargeInteger[] previousItems)
        {
            LargeInteger first, second, temp;

            if (previousItems != null)
            {
                var len = previousItems.Length;
                if (len >= 2)
                {
                    first = previousItems[len - 2];
                    second = previousItems[len - 1];
                }
                else if (len == 1)
                {
                    first = previousItems[len - 1];
                    second = new LargeInteger(1);
                    yield return second;
                }
                else
                {
                    first = new LargeInteger(1);
                    second = new LargeInteger(1);
                    yield return first;
                    yield return second;
                }
            }
            else
            {
                first = new LargeInteger(1);
                second = new LargeInteger(1);
                yield return first;
                yield return second;
            }

            while(true)
            {
                temp = first + second;
                first = second;
                second = temp;
                yield return temp;
            }
        }
    }
}