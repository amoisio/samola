using MathExtensions.Cache;
using MathExtensions.Construction;
using MathExtensions.Enumerables;

namespace MathExtensions.Construction
{
    public class PrimeNumbersFactory : ICalculatedEnumerableFactory<int>
    {
        protected readonly IEnumerableCacheProvider<int> _cacheProvider;
        protected readonly IntegerLimit _limit;

        public PrimeNumbersFactory(IntegerLimit limit, IEnumerableCacheProvider<int> cacheProvider)
        {
            _cacheProvider = cacheProvider;
            _limit = limit;
        }

        public virtual CalculatedEnumerable<int> Create()
        {
            return new PrimeNumbers(_limit, _cacheProvider);
        }
    }
}
