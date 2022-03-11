using System.Collections;
using System.Collections.Generic;

namespace Samola.Numbers.Primes
{
    public class PrimeDecomposition : IPrimeDecomposition
    {
        private readonly Dictionary<int, int> _decomposition;

        public PrimeDecomposition(Dictionary<int, int> decomposition)
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

        public int Count => _decomposition.Count;

        public override bool Equals(object obj)
        {
            if (!(obj is PrimeDecomposition o))
            {
                return false;
            }
            else
            {
                if (this.Count != o.Count)
                    return false;

                foreach (var k in this)
                {
                    if (!o._decomposition.ContainsKey(k.Key) || o._decomposition[k.Key] != k.Value)
                        return false;
                }

                return true;
            }
        }

        public override int GetHashCode()
        {
            // TODO: come back to this. how to write the get hash code method correctly?
            return _decomposition.GetHashCode();
        }

        public IEnumerator<KeyValuePair<int, int>> GetEnumerator()
        {
            return _decomposition.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
