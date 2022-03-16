namespace Samola.Collections
{
    public class PreviousValueState<TItem> : IEnumerationState<TItem> 
    {
        public PreviousValueState(TItem initValue = default)
        {
            PreviousValue = initValue;
        }
        
        public void RegisterYieldedItem(TItem item)
        {
            PreviousValue = item;
        }

        public TItem PreviousValue { get; private set; }
    }
}