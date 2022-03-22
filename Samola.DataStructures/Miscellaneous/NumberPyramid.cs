using System;

namespace Samola.DataStructures.Miscellaneous
{
    public class NumberPyramid
    {
        private readonly PyramidNode[] _nodes;

        public NumberPyramid(PyramidNode[] nodes)
        {
            _nodes = nodes;
            this.Root = _nodes[0];
            this.Current = _nodes[0];
        }

        public PyramidNode Root { get; }
        public PyramidNode Current { get; private set; }


        public int CalculateMaxPathValue()
        {
            int count = _nodes.Length;
            for (int i = count - 1; i >= 0; i--)
            {
                var node = _nodes[i];
                node.Value = node.Value + Math.Max(node.Left?.Value ?? 0, node.Right?.Value ?? 0);
            }
            return this.Root.Value;
        }

        public void Reset()
        {
            foreach (var node in _nodes)
            {
                node.Reset();
            }
            this.Current = this.Root;
        }
    }

    public class PyramidNode
    {
        private readonly int _value;
        private int? _temp;

        public PyramidNode(int value)
        {
            _value = value;
        }
        public int Value
        {
            get
            {
                return _temp.HasValue ? _temp.Value : _value;
            }
            set
            {
                _temp = value;
            }
        }

        public void Reset()
        {
            _temp = null;
        }

        public PyramidNode Left;
        public PyramidNode Right;
    }
}
