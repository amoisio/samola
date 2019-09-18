namespace MathExtensions
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
}