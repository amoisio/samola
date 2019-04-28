using EchoServer.ScreenConsole.Components;
using EchoServer.ScreenConsole.Renderer;
using System;
using System.Text;

namespace Samola.EchoServer.ScreenConsole.Components.EventsComponent
{
    public class RenderableEventTitle : Renderable<string>
    {
        public RenderableEventTitle(string title, IRenderer renderer)
            : base(title, renderer)
        { }

        protected override string GetRenderableString(int maxWidth)
        {
            int width = maxWidth;

            StringBuilder sb = new StringBuilder();
            sb.Append("=[");
            sb.Append(this.Item.Substring(0, Math.Min(width - 4, this.Item.Length)));
            sb.Append("]=");

            while (sb.Length < width)
            {
                sb.Append("=");
            }

            return sb.ToString();
        }
    }
}