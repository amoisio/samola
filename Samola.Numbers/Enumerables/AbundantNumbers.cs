using Samola.Numbers.Utilities;
using System.Collections.Generic;
using System.Linq;
using Samola.Collections;

namespace Samola.Numbers.Enumerables
{
    public class AbundantNumbers : CalculatedEnumerable<int>
    {
        private readonly NumberClassifier _classifier;

        public AbundantNumbers(NumberClassifier classifier, ICalculationLimit<int> calculationLimit)
            : base(calculationLimit)
        {
            _classifier = classifier;
        }

        protected override int CalculateNext(IReadOnlyList<int> previousItems)
        {
            int next = GetInitialItemToEvaluate(previousItems);
            var classification = _classifier.Classify(next);
            while (classification != NumberClassification.Abundant)
            {
                next++;
                classification = _classifier.Classify(next);
            }
            return next;
        }
        
        private int GetInitialItemToEvaluate(IReadOnlyList<int> previousItems)
        {
            int next = 1;
            if (previousItems != null && previousItems.Any())
            {
                next = previousItems.Last() + 1;
            }
            return next;
        }
    }
}
