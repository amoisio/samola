using Samola.Collections;

namespace Samola.Numbers.Primes
{
    public class PrimeNumbersSimple : StatefulCalculatedEnumerable<int, PreviousValueState<int>>, IPrimeNumerable<int>
    {
        public PrimeNumbersSimple(ICalculationLimit<int> limit = null)
            : base(limit ?? MaximumYieldedCountLimit<int>.Default)
        {

        }

        protected override PreviousValueState<int> InitializeState() => new(1 /* trivial prime */);
        
        protected override int CalculateNext(PreviousValueState<int> state)
        {
            var next = state.PreviousValue + 1;
            while (!IsPrime(next)) 
            { 
                next++;
            }
            return next;
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

            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
