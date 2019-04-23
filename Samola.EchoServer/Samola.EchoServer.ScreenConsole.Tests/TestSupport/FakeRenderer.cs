using EchoServer.ScreenConsole.Components;
using EchoServer.ScreenConsole.Renderer;
using System;
using System.Collections.Generic;

namespace EchoServer.ScreenConsole.Tests.TestSupport
{
    public class FakeRenderer : BaseRenderer
    {
        public FakeRenderer(DrawArea drawArea) : base(drawArea)
        {
            this.RenderHistory = new List<Tuple<int, int, string>>();
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public string StringToRender { get; private set; }
        public List<Tuple<int, int, string>> RenderHistory { get; set; }

        protected override void SetCursorPosition(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        protected override void WriteToDevice(string stringToRender)
        {
            this.StringToRender = stringToRender;
            this.RenderHistory.Add(Tuple.Create(this.X, this.Y, this.StringToRender));
        }
    }
}
