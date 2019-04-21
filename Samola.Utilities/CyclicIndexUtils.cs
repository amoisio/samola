using System;

namespace Samola.Utilities
{
    public static class CyclicIndexUtils
    {
        /// <summary>
        /// Projects integer numbers onto a space A_N = [0, N-1], where N is the cycle size.
        /// </summary>
        /// <param name="number">Integer number to project onto A_N</param>
        /// <param name="cycleSize">Size (the N) of space A_N</param>
        /// <returns>Projection of the integer number on A_N</returns>
        public static int FromInteger(int number, int cycleSize)
        {
            if (number == 0)
                return 0;
            else if (number > 0)
                return ProjectPositive(number, cycleSize);
            else
                return ProjectNegative(number, cycleSize);
        }

        /// <summary>
        /// Projects positive integer numbers onto A_N
        /// </summary>
        /// <param name="number">A positive integer number to project onto A_N</param>
        /// <param name="cycleSize">Size (the N) of space A_N</param>
        /// <returns>Projection of the integer number on A_N</returns>
        private static int ProjectPositive(int number, int cycleSize)
        {
            return Math.Abs(number) % cycleSize;
        }

        /// <summary>
        /// Projects negative integer numbers onto A_N
        /// </summary>
        /// <param name="number">A negative integer number to project onto A_N</param>
        /// <param name="cycleSize">Size (the N) of space A_N</param>
        /// <returns>Projection of the integer number on A_N</returns>
        private static int ProjectNegative(int index, int bufferSize)
        {
            var t = ProjectPositive(index, bufferSize);
            return ProjectPositive(bufferSize - t, bufferSize);
        }

        /// <summary>
        /// Projects a cyclic index from [0, N-1] onto the space of integer numbers (Z).
        /// </summary>
        /// <param name="cycleIndex">Index to project onto Z</param>
        /// <returns>Projection of the index on Z</returns>
        public static int ToInteger(int cycleIndex)
        {
            return ToInteger(cycleIndex, 0, 0);
        }

        /// <summary>
        /// Projects a cyclic index from [0, N-1] onto the space of integer numbers (Z).
        /// </summary>
        /// <param name="cycleIndex">Index to project onto Z</param>
        /// <param name="cycleNumber">The ordinal number of the cycle</param>
        /// <param name="cycleSize">Size (the N) of space A_N</param>
        /// <returns>Projection of the index on Z</returns>
        public static int ToInteger(int cycleIndex, int cycleNumber, int cycleSize)
        {
            if (cycleIndex < 0)
                throw new IndexOutOfRangeException("Cycle index must not be negative.");
            return cycleIndex + cycleNumber * cycleSize;
        }
    }
}
