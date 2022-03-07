using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Samola.Numbers.Enumerables
{
    public class CollatzSequence : IEnumerable<long>
    {
        private readonly long _startingNumber;
        public CollatzSequence(long startingNumber)
        {
            _startingNumber = startingNumber;
        }

        public IEnumerator<long> GetEnumerator()
        {
            long temp = _startingNumber;

            while (temp != 1)
            {

                yield return temp;

                //if (temp % 2 == 0) // even
                if ((temp & 1) == 0) // even 
                {
                    temp = temp / 2;
                }
                else // odd 
                {
                    temp = 3 * temp + 1;
                }
            }

            yield return temp;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class CollatzCache
    {
        private readonly Dictionary<int, int[]> _sequences;
        public CollatzCache(int capacity = 5000)
        {
            _sequences = new Dictionary<int, int[]>(5000);
        }

        public int[] this[int index]
        {
            get { return _sequences[index]; }
            private set
            {
                if (_sequences.ContainsKey(index))
                    _sequences[index] = value;
                else
                    _sequences.Add(index, value);
            }
        }

        public bool ContainsSequenceFor(int number) => _sequences.ContainsKey(number);

        public void Add(int[] sequence)
        {
            var subsequences = sequence.Select((number, index) =>
            {
                return new { key = number, sequence = sequence.Skip(index).ToArray() };
            });

            foreach (var subsequence in subsequences)
            {
                if (!_sequences.ContainsKey(subsequence.key))
                    _sequences.Add(subsequence.key, subsequence.sequence.ToArray());
            }
        }

    }
}
