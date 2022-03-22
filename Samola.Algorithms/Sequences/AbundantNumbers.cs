using Samola.Algorithms.CalculatedEnumerable;
using Samola.Algorithms.CalculatedEnumerable.State;
using Samola.Algorithms.Utilities;

namespace Samola.Algorithms.Sequences
{
    public class AbundantNumbers : CalculatedEnumerable<int, DefaultEnumerationState<int>>
    {
        private readonly NumberClassifier _classifier;
        private readonly int _initialValue;

        public AbundantNumbers(NumberClassifier classifier, int initialValue)
        {
            _classifier = classifier;
            _initialValue = initialValue;
        }

        protected override int CalculateInitial(DefaultEnumerationState<int> state)
        {
            return CalculateNextAbundantNumber(_initialValue);  
        } 

        protected override int CalculateNext(DefaultEnumerationState<int> state)
        {
            return CalculateNextAbundantNumber(state.PreviouslyYieldedItem + 1);
        }

        private int CalculateNextAbundantNumber(int startFrom)
        {
            var item = startFrom;
            var classification = _classifier.Classify(item);
            while (classification != NumberClassification.Abundant)
            {
                item++;
                classification = _classifier.Classify(item);
            }
            return item;
        }
    }
}
