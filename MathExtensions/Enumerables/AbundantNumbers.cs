using MathExtensions.Cache;
using MathExtensions.Implementations;
using System;
using System.Collections.Generic;

namespace MathExtensions.Enumerables
{
    public class AbundantNumbers : CalculatedEnumerableBase<int>
    {
        private readonly NumberClassifier _classifier;
        private readonly IntegerLimit _integerLimit;

        public AbundantNumbers(IPrimesCreator primesCreator, IntegerLimit integerLimit, IEnumerableCacheProvider<int> cacheProvider)
            : base(cacheProvider)
        {
            _integerLimit = integerLimit;
            _classifier = new NumberClassifier(primesCreator);
        }

        protected override bool CanYield(EnumerationState<int> state)
        {
            return _integerLimit.LimitOK(state);
        }

        protected override IEnumerable<int> GetItems(int[] previousItems)
        {
            int next = 1;
            if (previousItems != null && previousItems.Length > 0)
            {
                next = previousItems[previousItems.Length - 1];
            }

            while (true)
            {
                var @class = _classifier.Classify(next);

                if (@class == NumberClassification.Abundant)
                    yield return next;

                next++;
            }
        }
    }
}



//public AbundantNumbers(IPrimesCreator primesCreator) 
//    : base()
//{
//    _classifier = new NumberClassifier(primesCreator);
//}

//public AbundantNumbers(IPrimesCreator primesCreator, int maxCount, bool useCache) : base(maxCount, useCache)
//{
//    _classifier = new NumberClassifier(primesCreator);
//}

//public AbundantNumbers(IPrimesCreator primesCreator, EnumerateLimit<int> limit, bool useCache) 
//    : base(limit, useCache)
//{
//    _classifier = new NumberClassifier(primesCreator);
//}