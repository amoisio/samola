using Samola.DataStructures.Miscellaneous;
using Xunit;

namespace Samola.Algorithms.Tests
{
    public class NumberPyramidBuilderTests
    {
        [Fact]
        public void PyramidTreeBuilder_builds_a_valid_tree()
        {
            var builder = new NumberPyramidBuilder();
            builder.Data = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var tree = builder.Build();

            var root = tree.Root;
            Assert.Equal(1, root.Value);
            Assert.Equal(2, root.Left.Value);
            Assert.Equal(3, root.Right.Value);
            Assert.Equal(4, root.Left.Left.Value);
            Assert.Equal(5, root.Left.Right.Value);
            Assert.Equal(5, root.Right.Left.Value);
            Assert.Equal(6, root.Right.Right.Value);
            Assert.Equal(7, root.Left.Left.Left.Value);
            Assert.Equal(8, root.Left.Left.Right.Value);
            Assert.Equal(8, root.Left.Right.Left.Value);
            Assert.Equal(8, root.Right.Left.Left.Value);
            Assert.Equal(9, root.Left.Right.Right.Value);
            Assert.Equal(9, root.Right.Left.Right.Value);
            Assert.Equal(9, root.Right.Right.Left.Value);
            Assert.Equal(10, root.Right.Right.Right.Value);
        }
    }
}
