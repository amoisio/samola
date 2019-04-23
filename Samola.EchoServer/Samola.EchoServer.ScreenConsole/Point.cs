namespace EchoServer.ScreenConsole.Components
{
    /// <summary>
    /// A 2D point
    /// </summary>
    public struct Point
    {
        public byte X;
        public byte Y;

        public Point(byte x, byte y)
        {
            this.X = x;
            this.Y = y;
        }

        public Point IncrementX(int dx)
        {
            return new Point((byte)(this.X + dx), this.Y);
        }

        public Point IncrementY(int dy)
        {
            return new Point(this.X, (byte)(this.Y + dy));
        }
    }
}
