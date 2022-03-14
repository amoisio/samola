using System.Collections.Generic;

namespace Samola.Collections
{
    public interface ICalculationLimit<in TItem>
    {
        bool CanYield(TItem item, IEnumerable<TItem> previousItems);
    }
}