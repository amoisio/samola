using Samola.Numbers.Cache;
using Samola.Numbers.CustomTypes;

namespace Samola.Numbers.Enumerables
{
    public class NaturalNumbersBuilder
    {
        public const string cachePrefix = "natural-numbers";
        public const int capacity = 1000;
        public NaturalNumberLimit Limit { get; set; }
        public bool UseCache { get; set; }

        public NaturalNumbersBuilder()
        {
            UseCache = true;
        }

        public NaturalNumbers Build()
        {
            EnumerableListCacheProvider<NaturalNumber> provider = null;

            if (UseCache)
            {
                provider = new EnumerableListCacheProvider<NaturalNumber>(cachePrefix, capacity);
            }

            return new NaturalNumbers(Limit, provider);
        }
    }
}
