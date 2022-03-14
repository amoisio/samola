using Samola.Numbers.Primes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Samola.Numbers.Primes
{
    public class PrimeDecompositionEqualityComparer : IEqualityComparer<IPrimeDecomposition>
    {
        public bool Equals(IPrimeDecomposition x, IPrimeDecomposition y)
        {
            if (x == null || y == null)
            {
                return false;
            }

            if (x.Count() != y.Count())
            {
                return false;
            }

            foreach (var item in x)
            {
                if (!y.Contains(item))
                {
                    return false;
                }
            }

            return true;
        }

        public int GetHashCode(IPrimeDecomposition obj)
        {
            return obj.GetHashCode();
        }
    }
}