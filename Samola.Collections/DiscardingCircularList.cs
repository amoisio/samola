using Samola.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Samola.Collections
{
    /* Discarding Circular List
     *  A fixed size circular list in which the oldest item is discarded to make room for new, incoming items (ie. when the list is full).
     *  
     * PARAMETERS
     *  size        - determines the maximum number of items that the list can hold 
     *  collection  - items to be used to prefill the list on creation
     *  addToEnd    - controls whether new items are added to the end of the list (default) or to the beginning of the list
     *  
     *  Author: Aleksi Moisio
     *  Date: 2019-04-23
     * 
     **/
    public class DiscardingCircularList<T> : ICircularList<T>
    {
        /// <summary>
        /// Internally the circular list is just an array
        /// </summary>
        private readonly List<T> _storage;

        /// <summary>
        /// Determines if items are added to the end or to the beginning of the list
        /// </summary>
        private readonly bool _addToEnd;

        /// <summary>
        /// The HEAD points to the most recently added item. Null when list is empty.
        /// </summary>
        private CyclicIndex _head;

        /// <summary>
        /// The ROOT points to the oldest item. Null when list is empty.
        /// </summary>
        private CyclicIndex _root;

        #region ctors
        public DiscardingCircularList(int size) : this(size, true) { }

        public DiscardingCircularList(int size, IEnumerable<T> collection) : this(size, collection, true) { }

        public DiscardingCircularList(int size, bool addToEnd) : this(size, null, addToEnd) { }

        public DiscardingCircularList(int size, IEnumerable<T> items, bool addToEnd)
        {
            if (size < 1) throw new ArgumentException("Size must be greater than 0");
            Size = size;

            if (items?.Count() > size) throw new ArgumentException("Items collection has too many items");
            _storage = new List<T>(Size);
            _addToEnd = addToEnd;

            if (items != null && items.Count() > 0)
            {
                _storage.AddRange(items);
                _head = CyclicIndex.Create(items.Count() - 1, Size);
                _root = CyclicIndex.Create(0, Size);
            }
        }
        #endregion

        /// <summary>
        /// The maximum amount of items that can be stored in the list
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// The number of items currently in the list
        /// </summary>
        public int Count
        {
            get
            {
                if (IsEmpty())
                {
                    return 0;
                }
                else
                {
                    return IndexDifference() + 1;
                }
            }
        }

        private bool IsEmpty()
        {
            return _root == null && _head == null;
        }

        private int IndexDifference()
        {
            CyclicIndex diff = _head - _root;
            return diff.Value;
        }

        public Action<T> OnIndexChange { get; set; }

        /// <summary>
        /// Adds a new item to the end/beginning of the list
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            var next = _head + 1;

            if (Count < Size)
                _storage.Insert(next.Value, item);
            else
                _storage[next.Value] = item;

            IncrementHead();

            if (OnIndexChange != null)
            {
                if (_addToEnd)
                {
                    OnIndexChange(item);
                }
                else
                {
                    foreach (var existingItem in _storage)
                    {
                        OnIndexChange(existingItem);
                    }
                }
            }

            if (_root == null || _head == _root)
            {
                IncrementRoot();

                if (OnIndexChange != null)
                {
                    if (_addToEnd)
                    {
                        foreach (var existingItem in _storage.SkipWhile(e => Object.Equals(e, item)))
                        {
                            OnIndexChange(existingItem);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Removes an item from the beginning/end of the list
        /// </summary>
        public void Remove()
        {
            if (!IsEmpty())
            {
                if (_head == _root)
                {
                    _head = null;
                    _root = null;
                }
                else
                {
                    IncrementRoot();
                }
            }
        }

        private void IncrementRoot()
        {
            if (_root == null)
                _root = CyclicIndex.Create(0, Size);
            else
                _root = _root + 1;
        }

        private void IncrementHead()
        {
            if (_head == null)
                _head = CyclicIndex.Create(0, Size);
            else
                _head = _head + 1;
        }

        /// <summary>
        /// Clears the list
        /// </summary>
        public void Clear()
        {
            _head = null;
            _root = null;
            for (int i = 0; i < _storage.Count; i++)
            {
                _storage[i] = default(T);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            if (_addToEnd)
                return GetAscendingEnumerator();
            else
                return GetDescendingEnumerator();

        }

        private IEnumerator<T> GetAscendingEnumerator()
        {
            if (_root == null)
                yield break;

            for (int i = 0; i < Count; i++)
            {
                var index = _root + i;
                yield return _storage[index.Value];
            }
        }

        private IEnumerator<T> GetDescendingEnumerator()
        {
            if (_head == null)
                yield break;

            for (int i = 0; i < Count; i++)
            {
                var index = _head - i;
                yield return _storage[index.Value];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
