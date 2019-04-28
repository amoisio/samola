using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EchoServer.ScreenConsole.Components;
using Samola.Collections;

namespace Samola.EchoServer.ScreenConsole.RenderMaps
{
    /// <summary>
    /// Represents a rendering map - a list of of items in the exact order in which they are supposted to be rendered
    /// </summary>
    public class RenderMap2 : IRenderMap
    {
        private ClockBuffer<IRenderable> _buffer;
        private List<IRenderable> _cache;

        public RenderMap2(int maxSize)
        {
            _buffer = new ClockBuffer<IRenderable>(maxSize);
            _cache = new List<IRenderable>();
        }

        public void Add(IRenderable renderable)
        {
            _buffer.Add(renderable);
        }

        /// <summary>
        /// Determines if there are any changes in the map that have not yet been rendered
        /// </summary>
        public bool HasChanges()
        {
            return true;
        }

        public IEnumerator<IRenderable> GetEnumerator()
        {
            return _buffer.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
