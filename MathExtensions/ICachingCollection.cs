using System.Collections.Generic;

namespace MathExtensions
{
    public interface ICachingCollection<T> : IEnumerable<T> {

        T LastYielded { get; }
        int YieldedCount { get; }
    }
}
