using System.Collections.Generic;
using System.Linq;

namespace EchoServer.ScreenConsole.Components
{
    /// <summary>
    /// Represents a rendering map - a list of of items in the exact order in which they are supposted to be rendered
    /// </summary>
    public class ReversedRenderMap : RenderMap
    {
        public ReversedRenderMap(int maxSize) : base(maxSize)
        {
            
        }

        /// <summary>
        /// Basic mapping - put new item at the bottom of the map, drop old items out from the top
        /// </summary>
        public override void Add(IRenderable renderable)
        {
            if (this.Map.Count > this.MaxSize)
            {
                TruncateMapFromStartToMaxSize();
            }

            if (this.Map.Count == this.MaxSize)
            {
                //MoveItemsUpOnePlace(1);
            }

            Map.Add(renderable);
            renderable.SetDirty(); 
        }

        private void TruncateMapFromStartToMaxSize()
        {
            int truncateCount = this.Map.Count - this.MaxSize;
            this.Map.RemoveRange(0, truncateCount);
        }

        //protected void MoveItemsDownOnePlace(int startingIndex)
        //{
        //    if (startingIndex == 0)
        //    {
        //        MoveItemsUpOnePlace(1);
        //    }
        //    else
        //    {
        //        for (int i = startingIndex - 1; i < _maxSize - 1; i++)
        //        {
        //            _map[i] = _map[i + 1];
        //            _map[i].SetDirty();
        //        }
        //        _map.RemoveAt(_maxSize - 1);
        //    }
        //}

    }
}
