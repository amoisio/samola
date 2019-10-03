using MathExtensions.Primes;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions.CustomTypes
{
    /// <summary>
    /// Integer wrapper creates a nicer development experience when using MathExtension.
    /// All classes
    /// </summary>
    public class IntegerWrapper
    {
        private readonly long _number;
        private IntegerWrapper(long number)
        {
            _number = number;
        }

        public class IntegerWrapperBuilder
        {
            private readonly long _number;
            public IntegerWrapperBuilder(long number)
            {
                _number = number;
            }

            public IPrimes PrimeGenerator { get; set; }

            public IntegerWrapper Build()
            {
                return new IntegerWrapper(_number);
            }
        }

    }
}
