using System;
using Samola.Algorithms.CalculatedEnumerable;
using Samola.Algorithms.CalculatedEnumerable.State;

namespace Samola.Algorithms.Sequences
{
    public class DayOfTheWeekSequence : CalculatedEnumerable<DateTime, DefaultEnumerationState<DateTime>>
    {
        private readonly DateTime _from;

        public DayOfTheWeekSequence(DateTime from)
        {
            _from = from;
        }

        protected override DateTime CalculateInitial(DefaultEnumerationState<DateTime> state) => _from;

        protected override DateTime CalculateNext(DefaultEnumerationState<DateTime> state)
        {
            return state.PreviouslyYieldedItem.AddDays(7);
        }
    }
}
