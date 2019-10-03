using MathExtensions.Cache;

namespace MathExtensions.Enumerables
{
    public class PrimeNumbersBuilder
    {
        public const string cachePrefix = "primes";
        public const int capacity = 100000;
        public IntegerLimit Limit { get; set; }
        public bool UseCache { get; set; }

        public PrimeNumbersBuilder()
        {
            UseCache = false;
        }

        public virtual PrimeNumbers Build()
        {
            EnumerableListCacheProvider<int> provider = null;

            if (UseCache)
            {
                provider = new EnumerableListCacheProvider<int>(cachePrefix, capacity);
            }

            return new PrimeNumbers(Limit, provider);
        }
    }
}
