using Samola.Collections;
using Samola.Utilities;
using Xunit;

namespace Samole.Collections.Tests
{
    public class CyclicBufferIndexTests
    {
        [Theory]
        [InlineData(0, 3, 0, 0)]
        [InlineData(1, 3, 0, 1)]
        [InlineData(2, 3, 0, 2)]
        [InlineData(0, 3, 1, 1)]
        [InlineData(1, 3, 1, 2)]
        [InlineData(2, 3, 1, 0)]
        [InlineData(0, 3, 2, 2)]
        [InlineData(1, 3, 2, 0)]
        [InlineData(2, 3, 2, 1)]
        public void Maps_buffer_index_onto_storage_index(int bufferIndex, int storageSize, int storageRootIndex, int expected)
        {
            var storageIndex = CyclicBufferIndex.ToStorageIndex(bufferIndex, storageSize, storageRootIndex);
            Assert.Equal(expected, storageIndex);
        }

        [Theory]
        [InlineData(0, 3, 0, 0)]
        [InlineData(0, 3, 1, 2)]
        [InlineData(0, 3, 2, 1)]
        [InlineData(1, 3, 0, 1)]
        [InlineData(1, 3, 1, 0)]
        [InlineData(1, 3, 2, 2)]
        [InlineData(2, 3, 0, 2)]
        [InlineData(2, 3, 1, 1)]
        [InlineData(2, 3, 2, 0)]
        public void Maps_storage_index_onto_buffer_index(int storageIndex, int storageSize, int storageRootIndex, int expected)
        {
            var bufferIndex = CyclicBufferIndex.FromStorageIndex(storageIndex, storageSize, storageRootIndex);
            Assert.Equal(expected, bufferIndex);
        }
    }
}
