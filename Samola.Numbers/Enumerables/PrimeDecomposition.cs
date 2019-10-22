using System.Collections;
using System.Collections.Generic;

namespace Samola.Numbers.Enumerables
{
    public class PrimeDecomposition : IEnumerable<KeyValuePair<int, int>>
    {
        private readonly Dictionary<int, int> _decomposition;

        private PrimeDecomposition(Dictionary<int, int> decomposition)
        {
            _decomposition = decomposition;
        }

        public static PrimeDecomposition Create(int number)
        {
            PrimeDecomposition decomposition;
            var d = CalculateDecomposition(number);
            decomposition = new PrimeDecomposition(d);
            return decomposition;
        }

        private static Dictionary<int, int> CalculateDecomposition(int number)
        {
            var builder = new PrimeNumbersBuilder();
            builder.UseCache = true;
            builder.Limit = new MaxValueLimit(number);
            var primes = builder.Build();

            // 99% of the values from 2 up to 200k can be represented with just 25 first primes
            var decomposition = new Dictionary<int, int>(25);
            if (number == 1 || MathExt.IsPrime(number))
            {
                decomposition.Add(number, 1);
            }
            else
            {
                var temp = number;
                foreach (var prime in primes)
                {
                    if (temp == 1)
                        break;

                    while (temp % prime == 0)
                    {
                        if (decomposition.ContainsKey(prime))
                            decomposition[prime]++;
                        else
                            decomposition.Add(prime, 1);

                        temp = temp / prime;
                    }
                }
            }
            return decomposition;
        }

        public PrimeDecomposition Pow(int k)
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
