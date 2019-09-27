using MathExtensions.Cache;
using MathExtensions.Enumerables;
using System;
using System.Collections.Generic;
using System.Text;

namespace MathExtensions.Construction
{
    public interface ICalculatedEnumerableFactory<T>
    {
        CalculatedEnumerable<T> Create();
    }

}
