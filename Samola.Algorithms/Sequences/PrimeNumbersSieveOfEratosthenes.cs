using System.Collections.Generic;
using Samola.Algorithms.CalculatedEnumerable;
using Samola.Algorithms.CalculatedEnumerable.State;
using Samola.Algorithms.Utilities;

namespace Samola.Algorithms.Sequences
{
    /// <summary>
    /// Sieve of Eratosthenes builds primes up to a given natural number
    /// </summary>
    public class PrimeNumbersSieveOfEratosthenes : CalculatedEnumerable<int, EratostheneState>, IPrimeNumerable<int>
    {
        private readonly int _upToInt;
        public PrimeNumbersSieveOfEratosthenes(int upToInt) 
        {
            _upToInt = upToInt;
        }

        protected override int CalculateInitial(EratostheneState state)
        {
            return CalculateNextPrime(2, state.NonPrimes);
        }

        protected override int CalculateNext(EratostheneState state)
        {
            return CalculateNextPrime(state.PreviouslyYieldedItem + 1, state.NonPrimes);
        }

        private int CalculateNextPrime(int startFrom, HashSet<int> nonPrimes)
        {
            var maxValue = _upToInt;
            var item = startFrom;
            while (nonPrimes.Contains(item))
            {
                item++;
            }
            for (int i = item + 1; i <= maxValue; i++)
            {
                if (i % item == 0)
                {
                    nonPrimes.Add(i);
                }
            }
            return item;
        }

        public bool IsPrime(int number)
        {
            throw new System.NotImplementedException();
        }
    }
    
    public class EratostheneState : DefaultEnumerationState<int>
    {
        public HashSet<int> NonPrimes { get; } = new HashSet<int>();
    }
}