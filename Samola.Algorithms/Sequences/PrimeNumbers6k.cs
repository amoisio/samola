using System;
using System.Collections.Generic;
using Samola.Algorithms.CalculatedEnumerable;
using Samola.Algorithms.CalculatedEnumerable.State;
using Samola.Algorithms.Sequences.Primes;

namespace Samola.Algorithms.Sequences
{
    public class PrimeNumbers6k : CalculatedEnumerable<int, PrimeState>, IPrimeNumerable<int>
    {
        protected override int CalculateInitial(PrimeState state) => 2;

        protected override int CalculateNext(PrimeState state)
        {
            var lastPrime = state.PreviouslyYieldedItem;
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

    public class PrimeState : DefaultEnumerationState<int>
    {
        private readonly List<int> _previousItems = new();
        public IReadOnlyList<int> PreviousItems => _previousItems;
        public override void Yield(int item)
        {
            base.Yield(item);
            _previousItems.Add(PreviouslyYieldedItem);
        }
    }
}
