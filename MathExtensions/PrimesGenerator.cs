using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions
{
    public enum PrimesGeneratorType
    {
        Primes,
        PrimesNew
    }

    public static class PrimesGenerator
    {
        public static IPrimes Create(long n, 
            PrimesGenerationRule rule = PrimesGenerationRule.GenerateNPrimes, 
            PrimesGeneratorType primesGeneratorType = PrimesGeneratorType.PrimesNew)
        {
            switch (primesGeneratorType)
            {
                case PrimesGeneratorType.Primes:
                    return new Primes(n);
                default:
                case PrimesGeneratorType.PrimesNew:
                    return PrimesNew.Create(n, rule);
            }
        }
    }
}
