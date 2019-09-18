using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions
{
    public interface IPrimeDecomposer
    {
        Dictionary<int, int> CalculateDecomposition(int number);
    }
}
