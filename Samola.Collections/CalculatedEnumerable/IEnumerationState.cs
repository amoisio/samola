namespace Samola.Collections.CalculatedEnumerable
{
    public interface IEnumerationState<TItem>
    {
        void RegisterYieldedItem(TItem item);
    }
}