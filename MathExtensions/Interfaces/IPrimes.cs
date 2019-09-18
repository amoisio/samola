using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions
{
    /// <summary>
    /// Represents a collection of prime number
    /// </summary>
    public interface IPrimes : IEnumerable<int> {

        int LastYieldedPrime { get; }
    }
}
