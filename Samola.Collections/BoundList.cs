using System;
using System.Collections;
using System.Collections.Generic;

namespace BoundList
{
    // List that is restricted in the amount of items it can hold 
    // this could be generalized to a set.... maybe not, as a set is supposed to hold only unique values. I do not want to restrict my list that way for now...

    // List that runs a piece of code whenever an index of an item changes
    // -> make it a property => put it in an Interface

    /// <summary>
    /// Signature for method
    /// </summary>
    public interface ICustomChange<T>
    {
        Func<T> OnIndexChange { get; }
    }

    public class CustomIndexChangeList<T> : List<T>, ICustomChange<T>
    {
        public CustomIndexChangeList(Func<T> onIndexChange) : base()
        {
            this.OnIndexChange = onIndexChange;
        }

        public CustomIndexChangeList(int capacity, Func<T> onIndexChange) : base(capacity)
        {
            this.OnIndexChange = onIndexChange;
        }

        public CustomIndexChangeList(IEnumerable<T> collection, Func<T> onIndexChange) : base(collection)
        {
            this.OnIndexChange = onIndexChange;
        }

        public Func<T> OnIndexChange { get; }
    }


    // What I would really need here is multiple inheritance..

    public class BoundList<T> : List<T>
    {
        private readonly int _maxSize;
        public BoundList(int maxSize, int capacity) : base(capacity)
        {
            _maxSize = maxSize;
        }

        public BoundList(int maxSize, IEnumerable<T> collection) : base(collection)
        {
            _maxSize = maxSize;
        }


    }
}
