using System.Collections.Generic;

namespace Samola.Collections
{
    public class MaximumYieldedValueLimit : ICalculationLimit<int>
    {
        private readonly int _limit;

        public MaximumYieldedValueLimit(int limit)
        {
            _limit = limit;
        }

        public bool CanYield(int item, IEnumerable<int> previousItems)
        {
            return item <= _limit;
        }
    }
}
