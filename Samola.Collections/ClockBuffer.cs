using Samola.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Samola.Collections
{
    /// <summary>
    /// A clock buffer is a fixed size circular buffer. Items are added to end of the buffer.
    /// When the buffer is full and a new item is added, the oldest item, which resides in index 0, is discarded to make room for the incoming item.
    /// The new item is placed at the end of the list and the second-oldest item is made the new 0 index item.
    /// </summary>
    public class ClockBuffer<T> : IClockBuffer<T>
    {
        /// <summary>
        /// Internally, the circular buffer is just an array
        /// </summary>
        private readonly T[] _storage;

        /// <summary>
        /// The HEAD keeps track of the storage index which points to the most recently added item. Null when storage is empty.
        /// </summary>
        private CyclicIndex _head;

        /// <summary>
        /// The ROOT keeps track of the storage index which points to the oldest item in the storage. Null when storage is empty.
        /// </summary>
        private CyclicIndex _root;

        public ClockBuffer(int size) : this(size, new T[size]) { }

        public ClockBuffer(int size, IEnumerable<T> collection) : this(size, collection.ToArray())
        {
            _head = CyclicIndex.Create(collection.Count() - 1, Size);
            _root = CyclicIndex.Create(0, Size);
        }

        private ClockBuffer(int size, T[] items)
        {
            if (size < 1) throw new ArgumentException("Size must be greater than 0");
            Size = size;

            if (items.Length > size) throw new ArgumentException("Items collection has too many items");
            _storage = new T[Size];
            items.CopyTo(_storage, 0);
        }

        /// <summary>
        /// Determines the maximum amount of items that can be stored in the buffer
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Number of items currently in the storage
        /// </summary>
        public int Count
        {
            get
            {
                if (_root == null || _head == null)
                {
                    return 0;
                }
                else
                {
                    if (_root == _head)
                    {
                        return 1;
                    }
                    else
                    {
                        CyclicIndex diff = _head - _root;
                        return diff.Value + 1;
                    }
                }
            }
        }

        public void Add(T item)
        {
            CyclicIndex next;
            if (_head == null && _root == null)
            {
                _head = CyclicIndex.Create(0, Size);
                _root = CyclicIndex.Create(0, Size);
                next = _head;
            }
            else
            {
                next = _head + 1;
                if (next == _root)
                {
                    _root = _root + 1;
                }
                _head = next;
            }            

            _storage[next.Value] = item;
        }

        public void Remove()
        {
            if (_head == null && _root == null)
            {
            }
            else
            {
                if (_head == _root)
                {
                    _head = null;
                    _root = null;
                }
                else
                {
                    _root = _root + 1;
                }
            }
        }

        public void Clear()
        {
            _head = null;
            _root = null;
            for (int i = 0; i < _storage.Length; i++)
            {
                _storage[i] = default(T);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_root == null)
                yield break;

            for (int i = 0; i < Count; i++)
            {
                var index = _root + i;
                yield return _storage[index.Value];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
