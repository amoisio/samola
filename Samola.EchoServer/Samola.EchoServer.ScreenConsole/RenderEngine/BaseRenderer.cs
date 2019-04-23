using System;
using EchoServer.ScreenConsole.Components;

namespace EchoServer.ScreenConsole.Renderer
{
    public abstract class BaseRenderer : IRenderer
    {
        private readonly DrawArea _drawArea;
        private readonly object _renderLock = new object();

        public BaseRenderer(DrawArea drawArea)
        {
            _drawArea = drawArea;
        }

        public int GetMaxWidth()
        {
            return _drawArea.Width;
        }

        public int GetMaxSize()
        {
            return _drawArea.Height;
        }

        public void Render(string stringToRender, int rowToRenderOn)
        {
            if (rowToRenderOn >= _drawArea.Height)
            {
                throw new InvalidOperationException("Row to render is out of bounds.");
            }

            int maxWidth = _drawArea.Width;
            if (stringToRender.Length > maxWidth)
            {
                stringToRender = stringToRender.Substring(0, maxWidth);
            }

            lock (_renderLock)
            {
                SetCursorPosition(_drawArea.TopLeft.X, _drawArea.TopLeft.Y + rowToRenderOn);
                WriteToDevice(stringToRender);
            }
        }

        public void ClearDrawArea()
        {
            string stringToRender = String.Format($"{{0,{_drawArea.Width}}}", " ");
            for (int i = 0; i < _drawArea.Height; i++)
            {
                Render(stringToRender, i);
            }
        }

        protected abstract void SetCursorPosition(int x, int y);

        protected abstract void WriteToDevice(string stringToRender);
    }
}