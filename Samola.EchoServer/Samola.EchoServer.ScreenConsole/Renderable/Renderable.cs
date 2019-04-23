using EchoServer.ScreenConsole.Renderer;

namespace EchoServer.ScreenConsole.Components
{
    public abstract class Renderable<T> : IRenderable
    {
        private readonly IRenderer _renderer;
        private bool _isDirty;

        protected virtual T Item { get; }

        public Renderable(T item, IRenderer renderer)
        {
            this.Item = item;
            _renderer = renderer;
            _isDirty = true;
        }

        public virtual void Render(int rowToRenderOn)
        {
            string stringToRender = GetRenderableString(_renderer.GetMaxWidth());
            _renderer.Render(stringToRender, rowToRenderOn);
            _isDirty = false;
        }

        public virtual bool IsDirty()
        {
            return _isDirty;
        }

        public virtual void SetDirty()
        {
            _isDirty = true;
        }

        protected abstract string GetRenderableString(int maxWidth);
    }
}