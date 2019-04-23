using EchoServer.ScreenConsole.Renderer;

namespace EchoServer.ScreenConsole.Components
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
        protected RenderMap RenderMap { get; private set; }

        /// <summary>
        /// Draws the component onto the screen 
        /// </summary>
        public void Initialize()
        {
            this.RenderMap = new RenderMap(GetRenderMapMaxSize());
            ClearRenderArea();
            RenderInitialComponent();
        }

        protected abstract int GetRenderMapMaxSize();

        private void ClearRenderArea()
        {
            this.Renderer.ClearDrawArea();
        }

        /// <summary>
        /// Renders the components contents on to the screen
        /// </summary>
        protected abstract void RenderInitialComponent();

        /// <summary>
        /// Refreshes the component contents to the rendering device
        /// </summary>
        public void Refresh()
        {
            if (IsComponentDirty())
            {
                RenderChangedComponent();
            }
        }

        private bool IsComponentDirty()
        {
            return this.RenderMap.HasChanges();
        }

        /// <summary>
        /// Renders the changed components onto the screen
        /// </summary>
        protected abstract void RenderChangedComponent();
    }
}
