using Samola.Numbers.Enumerables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samola.Numbers.Comparers
{
    public class PrimeDecompositionEqualityComparer : IEqualityComparer<PrimeDecomposition>
    {
        public bool Equals(PrimeDecomposition x, PrimeDecomposition y)
        {
            if (x.Count != y.Count)
                return false;

            foreach (var item in x)
            {
                if (!y.Contains(item))
                    return false;
            }

            return true;
        }

        public int GetHashCode(PrimeDecomposition obj)
        {
            return obj.GetHashCode();
        }
    }
}
