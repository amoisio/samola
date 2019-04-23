using EchoServer.ScreenConsole.Renderer;
using System;
using System.Text;

namespace EchoServer.ScreenConsole.Components.EventsComponent
{
    public class RenderableEvent : Renderable<Event>
    {
        public int Id { get; }

        public RenderableEvent(Event @event, IRenderer renderer, int id)
            : base(@event, renderer)
        {
            this.Id = id;
        }

        protected override string GetRenderableString(int maxWidth)
        {
            string idString = this.Id.ToString();
            string dtString = this.Item.DateTime.ToString("yy-MM-dd HH:mm:ss");
            string opString = this.Item.Operation;
            string daString = this.Item.Data;

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

        #region Constants

        /// <summary>
        /// Number of characters allocated for the ID column
        /// </summary>
        public const int ID_SPACE = 3;

        /// <summary>
        /// The header of the ID column
        /// </summary>
        public const string ID_HEADER = "ID";

        /// <summary>
        /// Format to be used for formatting the datetime column values
        /// </summary>
        public const string DATETIME_FORMAT = "yy-MM-dd HH:mm:ss";

        /// <summary>
        /// Number of characters allocated for the Datetime column
        /// </summary>
        public const int DATETIME_SPACE = 17;

        /// <summary>
        /// The header of the datetime column
        /// </summary>
        public const string DATETIME_HEADER = "DATETIME";

        /// <summary>
        /// Number of characters allocated for the Operation column
        /// </summary>
        public const int OP_SPACE = 4; // GET, POST, PUT, DEL

        /// <summary>
        /// The header of the operation column
        /// </summary>
        public const string OP_HEADER = "OPER";

        /// <summary>
        /// The header of the data column
        /// </summary>
        public const string DATA_HEADER = "DATA";

        /// <summary>
        /// The minimum available space required for the data column to print.
        /// </summary>
        public const int DATA_MIN_SPACE = 4;

        /// <summary>
        /// Number of empty space characters between columns
        /// </summary>
        public const int PADDING = 1;

        /// <summary>
        /// The padding character
        /// </summary>
        public const char PADDING_CHARACTER = ' ';

        #endregion
    }
}
