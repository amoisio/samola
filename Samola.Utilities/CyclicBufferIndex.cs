using System;

namespace Samola.Utilities
{
    public static class CyclicBufferIndex
    {
        /// <summary>
        /// Converts a logical buffer index to the physical storage array index
        /// </summary>
        /// <param name="bufferIndex"></param>
        /// <param name="storageSize"></param>
        /// <param name="storageRootIndex"></param>
        /// <returns></returns>
        public static int ToStorageIndex(int bufferIndex, int storageSize, int storageRootIndex)
        {
            return CyclicIndexUtils.FromInteger(storageRootIndex + bufferIndex, storageSize);
        }

        /// <summary>
        /// Converts a physical array index to corresponding logical buffer index
        /// </summary>
        /// <param name="storageIndex"></param>
        /// <param name="storageSize"></param>
        /// <param name="storageRootIndex"></param>
        /// <returns></returns>
        public static int FromStorageIndex(int storageIndex, int storageSize, int storageRootIndex)
        {
            return CyclicIndexUtils.FromInteger(storageIndex - storageRootIndex, storageSize);
        }
    }


}
