using Samola.DataStructures.Miscellaneous;
using Xunit;

namespace Samola.Algorithms.Tests
{
    public class NumberPyramidTests
    {
        [Fact]
        public void PyramidTree_calculates_max_path_correctly()
        {
            var builder = new NumberPyramidBuilder();
            builder.Data = new int[] { 3, 4, 1, 2, 5, 8, 10, 9, 7, 6 };

            var tree = builder.Build();

            Assert.Equal(21, tree.CalculateMaxPathValue());
        }
    }
}
