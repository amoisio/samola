using MathExtensions.Cache;
using MathExtensions.Utilities;

namespace MathExtensions.Enumerables
{
    public class AbundantNumbersBuilder
    {
        public const string cachePrefix = "abundants";
        public const int capacity = 1000;
        public IntegerLimit Limit { get; set; }
        public NumberClassifier Classifier { get; set; }
        public bool UseCache { get; set; }

        public AbundantNumbersBuilder()
        {
            UseCache = true;
        }

        public virtual AbundantNumbers Build()
        {
            EnumerableListCacheProvider<int> provider = null;

            if (UseCache)
            {
                provider = new EnumerableListCacheProvider<int>(cachePrefix, capacity);
            }

            return new AbundantNumbers(Classifier, Limit, provider);
        }
    }
}
