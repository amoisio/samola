using System.Collections.Generic;
using Samola.Collections.CalculatedEnumerable;

namespace Samola.Numbers.Primes
{
    /// <summary>
    /// Sieve of Eratosthenes builds primes up to a given natural number
    /// </summary>
    public class SieveOfEratosthenes : StatefulCalculatedEnumerable<int, EratostheneState>, IPrimeNumerable<int>
    {
        private readonly int _upToInt;
        public SieveOfEratosthenes(int upToInt) :
            base(new MaximumYieldedValueLimit<int>(upToInt))
        {
            _upToInt = upToInt;
        }

        protected override EratostheneState InitializeState() => new(_upToInt, 1 /* Trivial prime */);

        protected override int CalculateNext(EratostheneState state)
        {
            var nonPrimes = state.NonPrimes;
            var maxValue = state.MaxValue;
            var next = state.PreviousValue + 1;
            while (nonPrimes.Contains(next))
            {
                next++;
            }

            for (int i = next + 1; i <= maxValue; i++)
            {
                if (i % next == 0)
                {
                    nonPrimes.Add(i);
                }
            }

            return next;
        }

        public bool IsPrime(int number)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class EratostheneState : IEnumerationState<int>
    {
        public EratostheneState(int maxValue, int initValue = default)
        {
            MaxValue = maxValue;
            PreviousValue = initValue;
            NonPrimes = new HashSet<int>();
        }

        public HashSet<int> NonPrimes { get; }
        public int MaxValue { get; }
        public int PreviousValue { get; private set; }
        public void RegisterYieldedItem(int item)
        {
            PreviousValue = item;
        }
    }
}