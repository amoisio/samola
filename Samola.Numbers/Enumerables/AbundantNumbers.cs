using Samola.Numbers.Utilities;
using Samola.Collections.CalculatedEnumerable;

namespace Samola.Numbers.Enumerables
{
    public class AbundantNumbers : StatefulCalculatedEnumerable<int, PreviousValueState<int>>
    {
        private readonly NumberClassifier _classifier;

        public AbundantNumbers(NumberClassifier classifier, ICalculationLimit<int> calculationLimit)
            : base(calculationLimit)
        {
            _classifier = classifier;
        }

        protected override PreviousValueState<int> InitializeState() => new();

        protected override int CalculateNext(PreviousValueState<int> state)
        {
            int next = state.PreviousValue + 1;
            var classification = _classifier.Classify(next);
            while (classification != NumberClassification.Abundant)
            {
                next++;
                classification = _classifier.Classify(next);
            }
            return next;
        }
    }
}
