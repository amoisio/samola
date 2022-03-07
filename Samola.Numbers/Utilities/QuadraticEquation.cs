using System;
using System.Collections.Generic;
using System.Text;

namespace Samola.Numbers.Utilities
{
    public class QuadraticEquation
    {
        private readonly int _a;
        private readonly int _b;
        private readonly int _c;

        public QuadraticEquation(int a, int b, int c)
        {
            _a = a;
            _b = b;
            _c = c;
        }

        public int NumberOfRoots
        {
            get
            {
                return GetNumberOfRoots(_a, _b, _c);
            }
        }

        public static int GetNumberOfRoots(int a, int b, int c)
        {
            int determinant = b * b - 4 * a * c;
            if (determinant == 0)
                return 1;
            else if (determinant > 0)
                return 2;
            else
                return 0;
        }

        public double[] Roots
        {
            get
            {
                return GetRoots(_a, _b, _c);
            }
        }

        public static double[] GetRoots(int a, int b, int c)
        {
            int nroots = GetNumberOfRoots(a, b, c);
            if (nroots == 0)
            {
                throw new InvalidOperationException("No real roots exist");
            }

            double[] roots = new double[nroots];
            if (nroots == 1)
            {
                roots[0] = -b / (2D * a);
            }
            else
            {
                roots[0] = (-b - Math.Sqrt(b * b - 4D * a * c)) / (2D * a);
                roots[1] = (-b + Math.Sqrt(b * b - 4D * a * c)) / (2D * a);
            }
            return roots;
        }

        public int Evaluate(int n)
        {
            return GetValue(_a, _b, _c, n);
        }

        public static int GetValue(int a, int b, int c, int n)
        {
            return a * n * n + b * n + c;
        }
    }
}
