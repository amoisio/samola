namespace EchoServer.ScreenConsole.Components
{
    /// <summary>
    /// A box-shaped area of the screen
    /// </summary>
    public class DrawArea
    {
        /// <summary>
        /// Upper left point of the drawing area
        /// </summary>
        public Point TopLeft { get; }
        public byte Width { get; }
        public byte Height { get; }

        public DrawArea(byte x, byte y, byte width, byte height)
            : this(new Point(x, y), width, height) { }

        public DrawArea(Point topLeft, byte width, byte height)
        {
            this.TopLeft = topLeft;
            this.Width = width;
            this.Height = height;
        }
    }
}
