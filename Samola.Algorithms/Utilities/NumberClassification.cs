namespace Samola.Algorithms.Sequences
{
    public enum NumberClassification
    {
        /// <summary>
        /// The number has not yet been classified
        /// </summary>
        Undetermined = 0,
        /// <summary>
        /// The sum of proper divisors is less than the number
        /// </summary>
        Deficient = 1,
        /// <summary>
        /// The sum of proper divisors is equal to the number
        /// </summary>
        Perfect = 2,
        /// <summary>
        /// The sum of proper divisors is greater than the number
        /// </summary>
        Abundant = 3
    }
}