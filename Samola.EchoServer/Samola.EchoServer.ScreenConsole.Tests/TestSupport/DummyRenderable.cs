using EchoServer.ScreenConsole.Components;
using EchoServer.ScreenConsole.Renderer;
using System;
using System.Collections.Generic;
using System.Text;

namespace EchoServer.ScreenConsole.Tests.TestSupport
{
    internal class DummyRenderable : Renderable<string>
    {
        string _item;
        public DummyRenderable(string item, IRenderer renderer) : base(item, renderer)
        {
            _item = item;
        }

        protected override string GetRenderableString(int maxWidth)
        {
            return _item.Substring(0, Math.Min(_item.Length, maxWidth));
        }
    }
}
