using Samola.Numbers.Cache;
using Samola.Numbers.CustomTypes;
using System.Collections.Generic;
using System.Linq;

namespace Samola.Numbers.Enumerables
{
    public class NaturalNumbers : CalculatedEnumerable<NaturalNumber>
    {
        internal NaturalNumbers(NaturalNumberLimit limit, IEnumerableCacheProvider<NaturalNumber> cacheProvider)
            : base(limit, cacheProvider)
        {

        }

        protected override IEnumerable<NaturalNumber> GetItems(NaturalNumber[] previousItems)
        {
            NaturalNumber next;
            if (previousItems != null && previousItems.Length > 0)
            {
                next = previousItems.Last() + 1;
            }
            else
            {
                next = new NaturalNumber(1);
            }

            while (true)
            {
                yield return next++;
            }
        }
    }
}
