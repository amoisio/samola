using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MathExtensions;

namespace MathExtensions
{
    public static class Divisors
    {
        public static long NumberOfDivisors(int number, IPrimeDecomposer decomposer)
        {
            var decomposition = decomposer.CalculateDecomposition(number);

            // if the decomposition only consists of the trivial prime 1 then return 1
            if (decomposition.Count == 1 && decomposition.First().Key == 1)
                return 1;

            var D = decomposition.Select(e => e.Value + 1)
                .Aggregate((x, y) => x * y);

            return D;
        }

        //public static HashSet<long> Divisors(int number, IPrimeDecomposer decomposer)
        //{
        //    var decomposition = decomposer.CalculateDecomposition(number);
        //}

        //public static HashSet<long> ProperDivisors(int number, IPrimeDecomposer decomposer)
        //{
        //    var divisors = NumberOfDivisors(number, decomposer);

        //    divisors.Where()
        //}
    }
}