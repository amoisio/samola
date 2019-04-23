using EchoServer.ScreenConsole.Renderer;
using System;
using System.Text;

namespace EchoServer.ScreenConsole.Components.EventsComponent
{
    public class RenderableEventHeader : Renderable<string>
    {
        public RenderableEventHeader(IRenderer renderer)
            : base(null, renderer)
        { }

        protected override string GetRenderableString(int maxWidth)
        {
            string idString = RenderableEvent.ID_HEADER;
            string dtString = RenderableEvent.DATETIME_HEADER;
            string opString = RenderableEvent.OP_HEADER;
            string daString = RenderableEvent.DATA_HEADER;

            return GetRenderableStringImpl(idString, dtString, opString, daString, maxWidth);
        }

        private static string GetRenderableStringImpl(string id, string datetime, string operation, string data, int maxWidth)
        {
            string padding = $"{RenderableEvent.PADDING_CHARACTER,RenderableEvent.PADDING}";
            string idString = $"{id,-RenderableEvent.ID_SPACE}";
            string dtString = $"{datetime,-RenderableEvent.DATETIME_SPACE}";
            string opString = $"{operation,-RenderableEvent.OP_SPACE}";

            StringBuilder sb = new StringBuilder();
            sb.Append(idString);
            sb.Append(padding);
            sb.Append(dtString);
            sb.Append(padding);
            sb.Append(opString);

            if (sb.Length > maxWidth)
            {
                return sb.ToString().Substring(0, maxWidth);
            }
            else
            {
                if (sb.Length + RenderableEvent.PADDING + RenderableEvent.DATA_MIN_SPACE < maxWidth)
                {
                    sb.Append(padding);

                    int dataSpace = maxWidth - sb.Length;
                    string dataString = String.Format($"{{0, -{dataSpace}}}", data);
                    sb.Append(dataString);
                }
                else
                {
                    int emptySpace = maxWidth - sb.Length;
                    string emptyString = String.Format($"{{0, -{emptySpace}}}", " ");
                    sb.Append(emptyString);
                }

                return sb.ToString();
            }
        }
    }
}
