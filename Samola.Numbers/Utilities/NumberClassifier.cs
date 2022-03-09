using System.Collections.Generic;
using System.Linq;

namespace Samola.Numbers.Utilities
{
    // TODO: Comments
    public class NumberClassifier
    {
        private readonly DivisorCalculator _divisorCalculator;
        private readonly Dictionary<int, NumberClassification> _classifications;

        public NumberClassifier() : this(new DivisorCalculator())
        {

        }

        public NumberClassifier(DivisorCalculator divisorCalculator)
        {
            _divisorCalculator = divisorCalculator;
            _classifications = new Dictionary<int, NumberClassification>();
        }

        public NumberClassification Classify(int number)
        {
            if (_classifications.ContainsKey(number))
            {
                return _classifications[number];
            }
            else
            {
                var divisors = _divisorCalculator.GetProperDivisors(number);
                int properSum = divisors.Sum();

                if (properSum < number)
                    return NumberClassification.Deficient;
                else if (properSum > number)
                    return NumberClassification.Abundant;
                else
                    return NumberClassification.Perfect;
            }
        }
    }
}
