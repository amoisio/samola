using Samola.Numbers.Enumerables;
using Samola.Numbers.Primes;
using Samola.Numbers.Utilities;

namespace Samola.Numbers.CustomTypes
{
    /// <summary>
    /// Integer wrapper creates a nicer development experience when using MathExtension.
    /// </summary>
    public class NaturalNumber
    {
        public int Value { get; }
        public NaturalNumber(int number)
        {
            Value = number;
        }

        public static NaturalNumber operator+(NaturalNumber a, int b)
        {
            return new NaturalNumber(a.Value + b);
        }

        public static NaturalNumber operator +(int a, NaturalNumber b)
        {
            return b + a;
        }

        public static NaturalNumber operator++(NaturalNumber a)
        {
            return a + 1;
        }

        private bool? _isPrime;
        public bool IsPrime
        {
            get
            {
                if (!_isPrime.HasValue)
                {
                    _isPrime = MathExt.IsPrime(Value);
                }

                return _isPrime.Value;
            }
        }

        private NumberClassification? _classification;
        public NumberClassification Classification
        {
            get
            {
                if (!_classification.HasValue)
                {
                    var decomposer = new PrimeDecomposer();
                    var divisor = new DivisorCalculator(decomposer);
                    var classifier = new NumberClassifier(divisor);
                    _classification = classifier.Classify(Value);
                }

                return _classification.Value;
            }
        }
    }
}
