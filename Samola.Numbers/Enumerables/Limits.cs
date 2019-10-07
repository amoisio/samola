using MathExtensions.CustomTypes;

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

    public class MaxNaturalCountLimit : NaturalNumberLimit
    {
        public MaxNaturalCountLimit(int limit) : base(new NaturalNumber(limit)) { }

        public override bool LimitOK(EnumerationState<NaturalNumber> state)
        {
            return state.YieldedCount < this.Limit.Value;
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

    public class MaxNaturalValueLimit : NaturalNumberLimit
    {
        public MaxNaturalValueLimit(int limit) : base(new NaturalNumber(limit)) { }

        public override bool LimitOK(EnumerationState<NaturalNumber> state)
        {

            return state.Item == null || state.Item.Value <= this.Limit.Value;
        }
    }

    public abstract class NaturalNumberLimit : EnumerableLimit<NaturalNumber>
    {
        public NaturalNumberLimit(NaturalNumber limit) : base(limit) { }

        public NaturalNumberLimit(int limit) : this(new NaturalNumber(limit)) { }
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