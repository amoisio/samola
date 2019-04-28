using EchoServer.ScreenConsole.Components;
using EchoServer.ScreenConsole.Renderer;
using System.Collections.Generic;
using System.Linq;

namespace Samola.EchoServer.ScreenConsole.Components.EventsComponent
{
    /// <summary>
    /// Displays a list of the latest HTTP events
    /// PURPOSE:
    ///     To maintain a list of existing events and process new, incoming events
    /// </summary>
    public class EventsComponent : ScreenComponent
    {
        private int _eventCount = 0;
        private readonly string _title;

        public EventsComponent(string title, IRenderer renderer) : base(renderer)
        {
            _title = title;
        }

        /// <summary>
        /// Register a new event and push it to the render map
        /// </summary>
        /// <param name="event"></param>
        public void Register(Event @event)
        {
            int nextId = _eventCount++;
            var next = new RenderableEvent(@event, this.Renderer, nextId);
            this.RenderMap.Add(next);
        }

        /// <summary>
        /// Initial rendering calls for rendering the title, the header and all registered events
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="drawArea"></param>
        protected override void RenderComponent()
        {
            RenderTitle();
            RenderHeader();
            RenderItems();
        }

        private void RenderTitle()
        {
            IRenderable title = GetRenderableTitle();
            title.Render(0); // Title renders on the first row
        }

        private IRenderable GetRenderableTitle()
        {
            return new RenderableEventTitle(_title, this.Renderer);
        }

        private void RenderHeader()
        {
            IRenderable header = GetRenderableHeader();
            header.Render(1); // Header renders on the second row
        }

        private IRenderable GetRenderableHeader()
        {
            return new RenderableEventHeader(this.Renderer);
        }

        private void RenderItems()
        {
            var items = GetAllRenderables();
            RenderItems(items);
        }

        private void RenderItems(IEnumerable<IRenderable> items)
        {
            foreach (var renderable in items)
            {
                var rowToRenderOn = this.RenderMap.GetRenderMapIndex(renderable);
                renderable.Render(rowToRenderOn);
            }
        }

        private IEnumerable<IRenderable> GetAllRenderables()
        {
            return this.RenderMap
                .Renderables
                .AsEnumerable();
        }

        private IEnumerable<IRenderable> GetDirtyRenderables()
        {
            return GetAllRenderables()
                .Where(renderable => renderable.IsDirty());
        }

        protected override void RenderComponentChanges()
        {
            var items = GetDirtyRenderables();
            RenderItems(items);
        }

        protected override int GetRenderMapMaxSize()
        {
            return this.Renderer.GetMaxSize() - 2; // title and header are not included
        }
    }
}
