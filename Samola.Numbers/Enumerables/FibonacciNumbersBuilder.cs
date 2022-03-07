using Samola.Numbers.Cache;
using Samola.Numbers.CustomTypes;

namespace Samola.Numbers.Enumerables
{
    public class FibonacciNumbersBuilder
    {
        public const string cachePrefix = "fibonacciNumbers";
        public const int capacity = 5000;
        public LargeIntegerCountLimit Limit { get; set; }
        public bool UseCache { get; set; }

        public FibonacciNumbersBuilder()
        {
            UseCache = true;
        }

        public virtual FibonacciNumbers Build()
        {
            EnumerableListCacheProvider<LargeInteger> provider = null;

            if (UseCache)
            {
                provider = new EnumerableListCacheProvider<LargeInteger>(cachePrefix, capacity);
            }

            return new FibonacciNumbers(Limit, provider);
        }
    }
}
