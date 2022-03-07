using Samola.Numbers.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Samola.Numbers.Tests
{
    public class QuadraticEquationTests
    {
        [Theory]
        [InlineData(1, 1, 1, 0)]
        [InlineData(1, 2, 1, 1)]
        [InlineData(1, 0, -1, 2)]
        public void NumberOfRoots_are_correctly_calculated(int a, int b, int c, int expected)
        {
            QuadraticEquation qe = new QuadraticEquation(a, b, c);

            Assert.Equal(expected, qe.NumberOfRoots);
        }

        [Theory]
        [InlineData(1, 2, 1, new double[] {-1})]
        [InlineData(1, -3, 2, new double[] { 1, 2 })]
        public void Roots_are_correctly_calculated(int a, int b, int c, double[] expected)
        {
            QuadraticEquation qe = new QuadraticEquation(a, b, c);

            Assert.Equal(expected, qe.Roots);
        }

    }
}
