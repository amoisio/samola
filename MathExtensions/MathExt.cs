using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace MathExtensions
{
    public enum PrimeAlgorithm
    {
        Simple = 0,
        Shared = 1
    }

    public static class MathExt
    {
        // Problem with IsPrime checks
        // - Can use IsPrimeSimple which is simple and does not store state (ie. is pure computation) but is also a slow method
        // - Or can use the IsPrimeCached which attempts to optimize over the simple algorithm by storing calculated primes in process memory


        /// <summary>
        /// The problem with these algorithm switches is that what happens if one yoinks the switch in the middle of a run
        /// </summary>
        //public static PrimeAlgorithm PrimeAlgorithm = PrimeAlgorithm.Simple;

        //public static bool IsPrime(long number)
        //{
        //    switch (PrimeAlgorithm)
        //    {
        //        case PrimeAlgorithm.Shared:
        //            return IsPrimeCached(number);
        //        case PrimeAlgorithm.Simple:
        //        default:
        //            return IsPrimeSimple(number);
        //    }
        //}

        /// <summary>
        /// Tests if a number is prime
        /// </summary>
        /// <param name="number">Number to test</param>
        public static bool IsPrimeBase(long number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be >= 1.");

            if (number <= 2)
                return true;

            if (number % 2 == 0 )
                return false;

            double max = Math.Sqrt(number);
            for (long i = 3; i <= max; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsPrimeSimple(long number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be non-negative.");

            if (number <= 3)
                return true;

            if (number % 2 == 0 || number % 3 == 0)
                return false;

            for (long i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }

       

        private static readonly ReaderWriterLockSlim _cachedLock = new ReaderWriterLockSlim();
        private static HashSet<long> _cached = new HashSet<long>() { 2 };

        public static bool IsPrimeCached(long number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be >= 1.");

            if (number == 2 || number == 1)
                return true;

            if (number % 2 == 0 )
                return false;

            // The value up to which we to check to determine if the number is prime
            double max = Math.Sqrt(number);

            long tempPrime = 2;

            _cachedLock.EnterReadLock();
            try
            {
                // First, iterate over existing primes
                foreach (var prime in _cached)
                {
                    if (prime == number) 
                    {
                        return true; // The number is a prime and is already included
                    }

                    if (number % prime == 0)
                    {
                        return false; // The number is divisible, by another number except 1. Hence it is not a prime
                    }

                    if (prime > max)
                    {
                        break; // The current prime is larger than max, we can stop checking
                    }

                    tempPrime = prime; // Set tempPrime to hold the value of the last iteration
                }
            }
            finally
            {
                _cachedLock.ExitReadLock();
            }

            for (long i = tempPrime; i <= max; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            _cachedLock.EnterWriteLock();
            try
            {
                if (!_cached.Contains(number))
                {
                    _cached.Add(number);
                }
            }
            finally
            {
                _cachedLock.ExitWriteLock();
            }
            return true;
        }

        private static HashSet<long> _cachedNoLocks = new HashSet<long>() { 2 };

        public static bool IsPrimeCachedNoLocks(long number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be >= 1.");

            if (number == 2 || number == 1)
                return true;

            if (number % 2 == 0)
                return false;

            // The value up to which we to check to determine if the number is prime
            double max = Math.Sqrt(number);

            long tempPrime = 2;

            // First, iterate over existing primes
            foreach (var prime in _cachedNoLocks)
            {
                if (prime == number)
                {
                    return true; // The number is a prime and is already included
                }

                if (number % prime == 0)
                {
                    return false; // The number is divisible, by another number except 1. Hence it is not a prime
                }

                if (prime > max)
                {
                    break; // The current prime is larger than max, we can stop checking
                }

                tempPrime = prime; // Set tempPrime to hold the value of the last iteration
            }

            for (long i = tempPrime; i <= max; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            _cachedNoLocks.Add(number);
            return true;
        }

        /// <summary>
        /// Tests if a number is prime
        /// </summary>
        /// <param name="number">Number to test</param>
        public static bool IsPrimeSimple6k(long number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be non-negative.");

            if (number <= 3)
                return true;

            if (number % 2 == 0 || number % 3 == 0)
                return false;

            for (long i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;

            // init. check all i less than sqrt(n) -> if divisible then not a prime
            // all primes above 6 are of form 6k +- 1

            // for checking primeness it is enough to check divisibility with primes
            // and in this case we check divisibility with primes
            // (6k +- 1) less than sqrt(n)
            for (long i = 5; i * i < number; i++)
            {

            }

            double max = (Math.Sqrt(number) + 1)/6D;
            
            for (long i = 1; i <= max; i++)
            {
                long kp = 6 * i + 1;
                long km = 6 * i - 1;
                if (number % kp == 0 || number % km == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}



