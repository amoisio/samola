using EchoServer.ScreenConsole.Renderer;
using System;

namespace EchoServer.ScreenConsole.Components
{
    public interface IRenderable
    {
        void Render(int rowToRenderOn);

        bool IsDirty();

        void SetDirty();
    }
}
