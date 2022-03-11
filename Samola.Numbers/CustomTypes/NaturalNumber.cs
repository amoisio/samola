using Samola.Numbers.Utilities;

namespace Samola.Numbers.CustomTypes
{
    /// <summary>
    /// Integer wrapper creates a nicer development experience when using MathExtension.
    /// </summary>
    public class NaturalNumber
    {
        public NaturalNumber(int number)
        {
            Value = number;
        }
        public int Value { get; }

        public static NaturalNumber operator +(NaturalNumber a, int b)
        {
            return new NaturalNumber(a.Value + b);
        }

        public static NaturalNumber operator +(int a, NaturalNumber b)
        {
            return b + a;
        }

        public static NaturalNumber operator ++(NaturalNumber a)
        {
            return a + 1;
        }
    }
}
