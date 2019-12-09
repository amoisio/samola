using Samola.Numbers.Cache;
using Samola.Numbers.Utilities;

namespace Samola.Numbers.Enumerables
{
    public class AmicableNumbersBuilder
    {
        public const string cachePrefix = "amicable-numbers";
        public const int capacity = 1000;
        public IntegerLimit Limit { get; set; }
        public DivisorCalculator Divisor { get; set; }
        public bool UseCache { get; set; }

        public AmicableNumbersBuilder()
        {
            UseCache = true;
        }

        public virtual AmicableNumbers Build()
        {
            EnumerableListCacheProvider<int> provider = null;

            if (UseCache)
            {
                provider = new EnumerableListCacheProvider<int>(cachePrefix, capacity);
            }

            return new AmicableNumbers(Divisor, Limit, provider);
        }
    }
}
