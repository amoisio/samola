﻿using Samola.Numbers.Utilities;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class CombinatoricsTests
    {
        [Theory]
        [InlineData(6, 3, 20)]
        [InlineData(52, 5, 2598960)]
        public void Combinations_return_correct_amount_of_combinations(int n, int k, long expected)
        {
            var combinations = Combinatorics.Combinations(n, k);

            Assert.Equal(expected, combinations);
        }
    }
}