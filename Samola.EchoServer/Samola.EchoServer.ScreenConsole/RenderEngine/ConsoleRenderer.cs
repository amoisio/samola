using System;
using EchoServer.ScreenConsole.Components;

namespace EchoServer.ScreenConsole.Renderer
{
    public class ConsoleRenderer : BaseRenderer
    {
        public ConsoleRenderer(DrawArea drawArea) : base(drawArea) { }

        protected override void SetCursorPosition(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }

        protected override void WriteToDevice(string stringToRender)
        {
            Console.Write(stringToRender);
        }
    }
}