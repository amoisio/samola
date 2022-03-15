using System.Collections.Generic;
using System.Linq;
using Samola.Collections;

namespace Samola.Numbers.Primes
{
    public class PrimeNumbersSimple : PrimeNumbers
    {
        public PrimeNumbersSimple(ICalculationLimit<int> limit = null)
            : base(limit)
        {

        }

        protected override int CalculateNext(IReadOnlyList<int> previousItems)
        {
            var next = GetInitialItemToEvaluate(previousItems);
            while (!IsPrime(next)) 
            { 
                next++;
            }
            return next;
        }
        
        private int GetInitialItemToEvaluate(IReadOnlyList<int> previousItems)
        {
            // 1 is a trivial prime, let's not return it
            int next = 2;
            if (previousItems != null && previousItems.Any())
            {
                next = previousItems.Last() + 1;
            }
            return next;
        }
        
        public override bool IsPrime(int number)
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
