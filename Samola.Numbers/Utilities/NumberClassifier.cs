using System.Linq;

namespace Samola.Numbers.Utilities
{
    public class NumberClassifier
    {
        private readonly DivisorCalculator _divisorCalculator;

        public NumberClassifier(DivisorCalculator divisorCalculator)
        {
            _divisorCalculator = divisorCalculator;
        }

        public NumberClassification Classify(int number)
        {
            var divisors = _divisorCalculator.GetProperDivisors(number);
            var properSum = divisors.Sum();

            if (properSum < number) 
            {
                return NumberClassification.Deficient;
            }
            
            if (properSum > number) 
            {
                return NumberClassification.Abundant;
            }
            
            return NumberClassification.Perfect;
        }
    }
}
