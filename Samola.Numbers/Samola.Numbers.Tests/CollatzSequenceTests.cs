using Samola.Numbers.Enumerables;
using System.Linq;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class CollatzSequenceTests
    {
        [Fact]
        public void CollatzSequence_returns_a_correct_sequence()
        {
            int startingNumber = 13;

            CollatzSequence sequence = new CollatzSequence(startingNumber);

            var numbers = sequence.ToArray();

            /// 13 → 40 → 20 → 10 → 5 → 16 → 8 → 4 → 2 → 1
            Assert.Equal(13, numbers[0]);
            Assert.Equal(40, numbers[1]);
            Assert.Equal(20, numbers[2]);
            Assert.Equal(10, numbers[3]);
            Assert.Equal(5, numbers[4]);
            Assert.Equal(16, numbers[5]);
            Assert.Equal(8, numbers[6]);
            Assert.Equal(4, numbers[7]);
            Assert.Equal(2, numbers[8]);
            Assert.Equal(1, numbers[9]);
        }

        [Fact]
        public void CollatzSequence_general_enumerator_test()
        {
            int startingNumber = 13;

            CollatzSequence sequence = new CollatzSequence(startingNumber);

            foreach(var number in sequence)
            {
                if (number == 16)
                    break;
            }
            Assert.True(true);
        }

        [Fact]
        public void CollatzSequence_general_enumerator_test2()
        {
            int startingNumber = 113383
;

            CollatzSequence sequence = new CollatzSequence(startingNumber);

            var c = sequence.Count();

            Assert.True(true);
        }

    }
}
