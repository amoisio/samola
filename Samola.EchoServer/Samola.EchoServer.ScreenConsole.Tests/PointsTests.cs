using EchoServer.ScreenConsole.Components;
using Xunit;

namespace EchoServer.ScreenConsole.Tests
{
    public class PointsTests
    {
        [Fact]
        public void Incrementing_Y_coordinate_implicitly_creates_a_new_point()
        {
            Point point1 = new Point(0, 0);

            Point point2 = point1.IncrementY(1);

            Assert.True(point1.Y == 0);
            Assert.True(point2.Y == 1);
            Assert.NotEqual(point1, point2);
        }

        [Fact]
        public void Incrementing_X_coordinate_implicitly_creates_a_new_point()
        {
            Point point1 = new Point(0, 0);

            Point point2 = point1.IncrementX(2);

            Assert.True(point1.X == 0);
            Assert.True(point2.X == 2);
            Assert.NotEqual(point1, point2);
        }
    }
}
