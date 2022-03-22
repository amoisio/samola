using System.Linq;

namespace Samola.Extensions
{
    public static class ArrayExtensions
    {
        public static bool ContainsSameItems(this int[] array, int[] other)
        {
            if (array == null || other == null)
                return false;

            if (array.Length != other.Length)
                return false;

            var orderedArray = array.OrderBy(e => e).ToArray();
            var orderedOther = other.OrderBy(e => e).ToArray();

            int len = other.Length;
            for (int i = 0; i < len; i++)
            {
                if (orderedArray[i] != orderedOther[i])
                    return false;
            }
            return true;
        }
    }
}
