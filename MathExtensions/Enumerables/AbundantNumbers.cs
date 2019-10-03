﻿using MathExtensions.Cache;
using MathExtensions.Utilities;
using System.Collections.Generic;

namespace MathExtensions.Enumerables
{
    // TODO: Comments...
    public class AbundantNumbers : CalculatedEnumerable<int>
    {
        private readonly NumberClassifier _classifier;

        public AbundantNumbers(NumberClassifier classifier, IntegerLimit integerLimit, IEnumerableCacheProvider<int> cacheProvider)
            : base(integerLimit, cacheProvider)
        {
            _classifier = classifier;
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