using System;

namespace Samola.DataStructures.Collections.CircularList
{
    internal class CyclicIndex
    {
        public int Value { get; }
        public int CycleSize { get; }

        public static CyclicIndex Create(int normalIndex, int cycleSize)
        {
            var index = CyclicIndexUtils.FromInteger(normalIndex, cycleSize);
            return new CyclicIndex(index, cycleSize);
        }

        private CyclicIndex(int index, int cycleSize)
        {
            Value = index;
            CycleSize = cycleSize;
        }

        public static CyclicIndex operator +(CyclicIndex index1, CyclicIndex index2)
        {
            if (index1.CycleSize != index2.CycleSize)
                throw new InvalidOperationException("Cannot mix cyclic indices from different sized cycles");
            int index = index1.Value + index2.Value;
            int cycleSize = index1.CycleSize;
            return CyclicIndex.Create(index, cycleSize);
        }

        public static CyclicIndex operator +(CyclicIndex index1, int index2)
        {
            var index = index1.Value + index2;
            int cycleSize = index1.CycleSize;
            return CyclicIndex.Create(index, cycleSize);
        }

        public static CyclicIndex operator +(int index1, CyclicIndex index2)
        {
            return index2 + index1;
        }

        public static CyclicIndex operator -(CyclicIndex index1, CyclicIndex index2)
        {
            if (index1.CycleSize != index2.CycleSize)
                throw new InvalidOperationException("Cannot mix cyclic indices from different sized cycles");
            int index = index1.Value - index2.Value;
            int cycleSize = index1.CycleSize;
            return CyclicIndex.Create(index, cycleSize);
        }

        public static CyclicIndex operator -(CyclicIndex index1, int index2)
        {
            var index = index1.Value - index2;
            int cycleSize = index1.CycleSize;
            return CyclicIndex.Create(index, cycleSize);
        }

        public static CyclicIndex operator -(int index1, CyclicIndex index2)
        {
            var index = index1 - index2.Value;
            int cycleSize = index2.CycleSize;
            return CyclicIndex.Create(index, cycleSize);
        }

        public static bool operator ==(CyclicIndex index1, CyclicIndex index2)
        {
            return (object)index1 == null && (object)index2 == null
                || ((object)index1 != null && (object)index2 != null
                    && index1.Value == index2.Value
                    && index1.CycleSize == index2.CycleSize);
        }

        public static bool operator !=(CyclicIndex index1, CyclicIndex index2)
        {
            return (object)index1 == null && (object)index2 != null
                || (object)index1 != null && (object)index2 == null
                || index1.Value != index2.Value
                || index1.CycleSize != index2.CycleSize;
        }

        public override bool Equals(object obj)
        {
            var index = obj as CyclicIndex;
            return index != null
                && Value == index.Value
                && CycleSize == index.CycleSize;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Value, CycleSize);
        }
    }
}