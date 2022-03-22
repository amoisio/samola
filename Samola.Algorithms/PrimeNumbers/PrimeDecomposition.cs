using System;
using System.Collections;
using System.Collections.Generic;

namespace Samola.Algorithms.Sequences.Primes
{
    internal class PrimeDecomposition : IPrimeDecomposition, IEquatable<PrimeDecomposition>
    {
        private readonly Dictionary<int, int> _decomposition;

        internal PrimeDecomposition(Dictionary<int, int> decomposition)
        {
            _decomposition = decomposition;
        }

        public IPrimeDecomposition Pow(int k)
        {
            var decomposition = new Dictionary<int, int>(_decomposition.Count);

            foreach (var key in _decomposition.Keys)
            {
                decomposition.Add(key, _decomposition[key] * k);
            }

            return new PrimeDecomposition(decomposition);
        }

        public int Factors => _decomposition.Count;

        public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
        {
            return _decomposition.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PrimeDecomposition) obj);
        }
        
        public bool Equals(PrimeDecomposition other)
        {
            if (other == null || Factors != other.Factors)
            {
                return false;
            }

            var d = other._decomposition;
            foreach (var k in this)
            {
                if (!d.ContainsKey(k.Key) || d[k.Key] != k.Value)
                {
                    return false;
                }
            }

            return true;
        }

        public override int GetHashCode()
        {
            return _decomposition != null ? _decomposition.GetHashCode() : 0;
        }
    }
}