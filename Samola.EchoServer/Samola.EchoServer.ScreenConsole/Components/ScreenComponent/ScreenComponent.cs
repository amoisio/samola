using EchoServer.ScreenConsole.Renderer;
using Samola.EchoServer.ScreenConsole.RenderMaps;

namespace Samola.EchoServer.ScreenConsole.Components
{
    /// <summary>
    /// Base class for a console GUI component
    /// PURPOSE:
    ///     To maintain a list of existing events and process new, incoming events
    /// </summary>
    public abstract class ScreenComponent : IScreenComponent
    {
        public ScreenComponent(IRenderer renderer)
        {
            this.Renderer = renderer;
        }

        protected IRenderer Renderer { get; }
        protected IRenderMap RenderMap { get; private set; }

        /// <summary>
        /// Draws the component onto the screen 
        /// </summary>
        public void Initialize()
        {
            this.RenderMap = new RenderMap2(GetRenderMapMaxSize());
            this.Renderer.ClearDrawArea();
            RenderComponent();
        }

        protected abstract int GetRenderMapMaxSize();

        /// <summary>
        /// Renders the components contents on to the screen
        /// </summary>
        protected abstract void RenderComponent();

        /// <summary>
        /// Refreshes the component contents to the rendering device
        /// </summary>
        public void Refresh()
        {
            RenderComponentChanges();
        }

        /// <summary>
        /// Renders the changed components onto the screen
        /// </summary>
        protected abstract void RenderComponentChanges();
    }
}
