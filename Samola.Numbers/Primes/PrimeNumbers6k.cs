using System;
using System.Collections.Generic;
using Samola.Collections.CalculatedEnumerable;

namespace Samola.Numbers.Primes
{
    public class PrimeNumbers6k : StatefulCalculatedEnumerable<int, PrimeState>, IPrimeNumerable<int>
    {
        public PrimeNumbers6k(ICalculationLimit<int> limit = null)
            : base(limit ?? MaximumYieldedCountLimit<int>.Default)
        {

        }

        protected override PrimeState InitializeState() => new(100, 1 /* trivial prime */);
        
        protected override int CalculateNext(PrimeState state)
        {
            var lastPrime = state.PreviousValue;
            if (lastPrime < 2)
            {
                return 2;
            }

            if (lastPrime < 3)
            {
                return 3;
            }

            var (k, a) = DetermineCoefficients(lastPrime);
            foreach (var candidate in GetPrimeCandidates(k, a))
            {
                foreach (var prime in state.PreviousItems)
                {
                    if (prime * prime > candidate)
                    {
                        return candidate;
                    }

                    if (candidate % prime == 0)
                    {
                        break;
                    }
                }
            }

            throw new InvalidOperationException("Prime generation failed!");
        }

        /// <summary>
        /// Determine k and a in [prime] = 6 * k + a
        /// </summary>
        private static (int k, int a) DetermineCoefficients(int prime)
        {
            if (prime < 5)
            {
                return (0, 0);
            }

            int k = Math.DivRem(prime + 1, 6, out int remainder);
            if (remainder == 0)
            {
                return (k, -1);
            }

            k = Math.DivRem(prime - 1, 6, out remainder);
            if (remainder == 0)
            {
                return (k, 1);
            }

            throw new InvalidOperationException("6k + a decomposition failed.");
        }

        private static IEnumerable<int> GetPrimeCandidates(int k, int a)
        {
            if (a == -1)
            {
                yield return 6 * k + 1;
            }

            while (true)
            {
                k++;
                yield return 6 * k - 1;
                yield return 6 * k + 1;
            }
        }
        
        public bool IsPrime(int number)
        {
            if (number < 1)
            {
                return false;
            }

            if (number <= 3)
            {
                return true;
            }

            if (number % 2 == 0 || number % 3 == 0)
            {
                return false;
            }

            for (int k = 1; (6 * k - 1) * (6 * k - 1) <= number; k++)
            {
                int lvalue = 6 * k - 1;
                if (number % lvalue == 0)
                    return false;

                int rvalue = 6 * k + 1;
                if (number % rvalue == 0)
                    return false;
            }

            return true;
        }
    }

    public class PrimeState : IEnumerationState<int>
    {
        private readonly List<int> _previousItems;
        private readonly int _initValue;
        
        public PrimeState(int capacity, int initValue = default)
        {
            _previousItems = new List<int>(capacity);
            _initValue = initValue;
        }
        
        public void RegisterYieldedItem(int item)
        {
            _previousItems.Add(item);
        }
        
        public IReadOnlyList<int> PreviousItems => _previousItems;
        public int PreviousValue
        {
            get
            {
                var count = _previousItems.Count;
                return count == 0 ? _initValue : _previousItems[count - 1];
            }
        }
    }
}
