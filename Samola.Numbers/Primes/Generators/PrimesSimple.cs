using System;
using System.Collections.Generic;
using System.Linq;

namespace Samola.Numbers.Primes.Generators
{
    public class PrimesSimple : Primes
    {
        public PrimesSimple(IEnumerable<int> pregeneratedPrimes = null)
            : base(pregeneratedPrimes)
        {

        }

        protected override int GenerateNext(IReadOnlyList<int> previousPrimes)
        {
            var number = previousPrimes.LastOrDefault() + 1;
            if (number == 1)
            {
                // 1 is a trivial prime, let's not return it
                number++;
            }
            while (!IsPrime(number)) 
            { 
                number++;
            }
            return number;
        }

        public override bool IsPrime(int number)
        {
            if (number < 1)
            {
                throw new ArgumentException("Number must be non-negative.");
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
