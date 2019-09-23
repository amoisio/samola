using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions
{
    public class CountLimit : EnumerateLimit<int>
    {
        public CountLimit(int limit) : base(limit)
        {

        }

        public override bool CanStillYield(int countYielded, int yieldedValue)
        {
            return countYielded < Limit;
        }
    }

    public class MaxValueLimit : EnumerateLimit<int>
    {
        public MaxValueLimit(int limit) : base(limit)
        {

        }

        public override bool CanStillYield(int countYielded, int yieldedValue)
        {
            return yieldedValue < Limit;
        }
    }

    public abstract class EnumerateLimit<T>
    {
        public EnumerateLimit(T limit)
        {
            this.Limit = limit;
        }

        public T Limit { get; }

        public abstract bool CanStillYield(int countYielded, T yieldedValue);
    }
}
