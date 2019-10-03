using MathExtensions.Primes;

namespace MathExtensions.Construction
{
    public class PrimesSimpleFactory : IPrimesCreator
    {
        /// <summary>
        /// Back-stop count for the general purpose collection. This prevents the collection from counting 
        /// to infinity if used unwisely.
        /// </summary>
        private readonly int _maxCount;

        public PrimesSimpleFactory(int maxCount )
        {
            _maxCount = maxCount;
        }

        public IPrimes Create()
        {
            return new PrimesSimple(_maxCount);
        }
    }

    public class PrimesNewFactory : IPrimesCreator
    {
        /// <summary>
        /// Back-stop count for the general purpose collection. This prevents the collection from counting 
        /// to infinity if used unwisely.
        /// </summary>
        private readonly int _maxCount;
        private readonly PrimesGenerationRule _rule;

        public PrimesNewFactory(int maxCount, PrimesGenerationRule rule)
        {
            _maxCount = maxCount;
            _rule = rule;
        }

        public IPrimes Create()
        {
            return PrimesNew.Create(_maxCount, _rule);
        }
    }

    public class Primes6kFactory : IPrimesCreator
    {
        private readonly int _maxCount;
        private readonly bool _useCache;


        public Primes6kFactory(int maxCount, bool useCache)
        {
            _maxCount = maxCount;
            _useCache = useCache;
        }

        public IPrimes Create()
        {
            return Primes6k.Create(_maxCount, _useCache);
        }
    }
}
