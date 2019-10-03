namespace MathExtensions.Enumerables
{
    public class CountLimit : IntegerLimit
    {
        public CountLimit(int limit) : base(limit) { }

        public static CountLimit Default => new CountLimit(2000000);

        public override bool LimitOK(EnumerationState<int> state)
        {
            // Yielded count must be less than the limit
            return state.YieldedCount < this.Limit;
        }
    }

    public class MaxValueLimit : IntegerLimit
    {
        public MaxValueLimit(int limit) : base(limit) { }

        public override bool LimitOK(EnumerationState<int> state)
        {
            // The item that is to be yielded must be leq the limit.
            return state.Item <= this.Limit;
        }
    }

    public abstract class IntegerLimit : EnumerableLimit<int>
    {
        public IntegerLimit(int limit) : base(limit) { }
    }

    public abstract class EnumerableLimit<T>
    {
        public EnumerableLimit(T limit)
        {
            this.Limit = limit;
        }

        protected T Limit { get; }

        public abstract bool LimitOK(EnumerationState<T> state);
    }
}