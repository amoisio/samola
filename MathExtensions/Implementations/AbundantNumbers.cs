using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions.Implementations
{
    public class AbundantNumbers : BaseCachingCollection<int>
    {
        public AbundantNumbers(int maxCount, bool useCache) : base(maxCount, useCache)
        {

        }

        protected override IEnumerable<int> GetItems(int[] previousItems)
        {
            throw new NotImplementedException();
        }
    }
}
