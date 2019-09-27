using MathExtensions.Cache;
using MathExtensions.Construction;
using MathExtensions.Implementations;
using System;
using System.Collections.Generic;

namespace MathExtensions.Enumerables
{
    public class AbundantNumbers : CalculatedEnumerable<int>
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