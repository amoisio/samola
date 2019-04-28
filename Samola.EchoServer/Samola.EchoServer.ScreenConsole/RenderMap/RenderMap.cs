using EchoServer.ScreenConsole.Components;
using System.Collections.Generic;
using System.Linq;

namespace Samola.EchoServer.ScreenConsole.RenderMaps
{
    /// <summary>
    /// Represents a rendering map - a list of of items in the exact order in which they are supposted to be rendered
    /// </summary>
    public class RenderMap
    {
        protected List<IRenderable> Map { get; }
        protected int MaxSize { get; }

        public RenderMap(int maxSize)
        {
            this.MaxSize = maxSize;
            Map = new List<IRenderable>(maxSize);
        }

        public IRenderable[] Renderables { get { return this.Map.Take(this.MaxSize).ToArray(); } }

        /// <summary>
        /// Basic mapping - put new item at the bottom of the map, drop old items out from the top
        /// </summary>
        public virtual void Add(IRenderable renderable)
        {
            if (this.Map.Count > this.MaxSize)
            {
                TruncateMapFromEndToMaxSize();
            }

            if (this.Map.Count == this.MaxSize)
            {
                MoveItemsUpOnePlace(1);
            }

            this.Map.Add(renderable);
            renderable.SetDirty();
        }

        private void TruncateMapFromEndToMaxSize()
        {
            int truncateCount = this.Map.Count - this.MaxSize;
            this.Map.RemoveRange(this.MaxSize, truncateCount);
        }

        private void MoveItemsUpOnePlace(int startingIndex)
        {
            if (startingIndex == 0)
            {
                MoveItemsUpOnePlace(1);
            }
            else
            {
                for (int i = startingIndex - 1; i < this.MaxSize - 1; i++)
                {
                    this.Map[i] = this.Map[i + 1];
                    this.Map[i].SetDirty();
                }
                this.Map.RemoveAt(this.MaxSize - 1);
            }
        }

        /// <summary>
        /// Gets the rendering index of the given renderable within the map
        /// </summary>
        public int GetRenderMapIndex(IRenderable renderable)
        {
            int index = this.Map.IndexOf(renderable);

            if (index == -1)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                return index;
            }
        }

        /// <summary>
        /// Determines if there are any changes in the map that have not yet been rendered
        /// </summary>
        public bool HasChanges()
        {
            return this.Map.Any(renderable => renderable.IsDirty());
        }
    }
}
