using Samola.Numbers.Cache;
using Samola.Numbers.Utilities;
using System.Collections.Generic;

namespace Samola.Numbers.Enumerables
{
    // TODO: Comments...
    public class AbundantNumbers : CalculatedEnumerable<int>
    {
        private readonly NumberClassifier _classifier;

        internal AbundantNumbers(NumberClassifier classifier, IntegerLimit integerLimit, IEnumerableCacheProvider<int> cacheProvider)
            : base(integerLimit, cacheProvider)
        {
            _classifier = classifier;
        }

        protected override IEnumerable<int> GetItems(int[] previousItems)
        {
            int next = 1;
            if (previousItems != null && previousItems.Length > 0)
            {
                next = previousItems[previousItems.Length - 1] + 1;
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