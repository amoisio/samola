namespace Samola.Collections
{
    public interface IEnumerationState<TItem>
    {
        void RegisterYieldedItem(TItem item);
    }
}