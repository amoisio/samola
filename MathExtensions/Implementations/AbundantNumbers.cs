﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions.Implementations
{
    public class AbundantNumbers : IntegerCachingCollection
    {
        private readonly NumberClassifier _classifier;

        public AbundantNumbers(IPrimesCreator primesCreator) : base()
        {
            _classifier = new NumberClassifier(primesCreator);
        }

        public AbundantNumbers(IPrimesCreator primesCreator, int maxCount, bool useCache) : base(maxCount, useCache)
        {
            _classifier = new NumberClassifier(primesCreator);
        }

        public AbundantNumbers(IPrimesCreator primesCreator, EnumerateLimit<int> limit, bool useCache) 
            : base(limit, useCache)
        {
            _classifier = new NumberClassifier(primesCreator);
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
