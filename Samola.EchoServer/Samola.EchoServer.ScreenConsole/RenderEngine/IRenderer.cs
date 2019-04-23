using EchoServer.ScreenConsole.Components;

namespace EchoServer.ScreenConsole.Renderer
{
    public interface IRenderer
    {
        int GetMaxWidth();

        int GetMaxSize();

        void Render(string stringToRender, int rowToRenderOn);

        void ClearDrawArea();
    }
}
