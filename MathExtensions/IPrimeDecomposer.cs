using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions
{
    public interface IPrimeDecomposer
    {
        Dictionary<long, long> CalculateDecomposition(long number);
    }
}
