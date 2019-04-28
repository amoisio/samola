using EchoServer.ScreenConsole.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Samola.EchoServer.ScreenConsole.RenderMaps
{
    public interface IRenderMap : IEnumerable<IRenderable>
    {
        void Add(IRenderable renderable);

        bool HasChanges();
    }
}
