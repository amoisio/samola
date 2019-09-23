namespace MathExtensions.Implementations
{
    public abstract class IntegerCachingCollection : BaseCachingCollection<int>
    {
        //
        // Constants
        //
        public const int MAX_COUNT = 2000000;

        protected IntegerCachingCollection()
            : this(MAX_COUNT, true)
        {

        }

        protected IntegerCachingCollection(int maxCount, bool useCache)
            : base(new CountLimit(maxCount), useCache)
        {

        }

        protected IntegerCachingCollection(EnumerateLimit<int> limit, bool useCache)
            : base(limit, useCache)
        {

        }
    }
}