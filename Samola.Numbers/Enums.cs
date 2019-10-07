namespace Samola.Numbers
{
    /// <summary>
    /// Determines the prime generator method.
    /// </summary>
    public enum PrimesGeneratorType
    {
        /// <summary>
        /// A simple generator based on the "less than sqrt(N)" check
        /// </summary>
        PrimesSimple,
        /// <summary>
        /// A faster (than simple) generator based on the "6k +- 1" form of prime numbers
        /// </summary>
        PrimesNew,
        /// <summary>
        /// Like PrimesNew but utilizes an in-memory cache to speed up subsequent queries
        /// </summary>
        PrimesCached
    }

    /// <summary>
    /// Determines a rule which when fulfilled ends the prime generation
    /// </summary>
    public enum PrimesGenerationRule
    {
        /// <summary>
        /// Generate a set amount (N) of primes
        /// </summary>
        GenerateNPrimes = 0,
        /// <summary>
        /// Generate primes up to a given number (N)
        /// </summary>
        GenaratePrimesUpToN = 1
    }

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