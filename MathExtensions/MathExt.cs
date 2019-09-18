using System;
using System.Collections.Generic;
using System.Threading;

namespace MathExtensions
{
    public static partial class MathExt
    {
        public static int Pow(int x, int y)
        {
            if (y < 0)
                throw new ArgumentException("Exponent must be non-negative.");

            if (y == 0)
                return 1;

            if (y == 1)
                return x;

            if (x % 2 == 0)
            {
                return x << y - 1;
            }
            else
            {
                int result = 1;
                while (y-- > 0)
                {
                    result *= x;
                }
                return result;
            }
        }

        public static bool IsPrime(long number)
        {
            return IsPrimeSimple6k(number);
        }

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
        /// Tests if a number is prime (employs the realization that all primes >= 5 can be represented as 6*k +- 1 where k > 0).
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

            for (long k = 1; (6*k - 1) * (6*k - 1) <= number; k++)
            {
                long lvalue = 6 * k - 1;
                if (number % lvalue == 0)
                    return false;

                long rvalue = 6 * k + 1;
                if (number % rvalue == 0)
                    return false;
            }

            return true;
        }

        private static readonly ReaderWriterLockSlim _cached6kLock = new ReaderWriterLockSlim();
        private static HashSet<long> _cached6k = new HashSet<long>() { 2, 3 };

        /// <summary>
        /// Tests if a number is prime (employs the realization that all primes >= 5 can be represented as 6*k +- 1 where k > 0).
        /// </summary>
        /// <param name="number">Number to test</param>
        public static bool IsPrimeSimple6kCached(long number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be non-negative.");

            if (number <= 3)
                return true;

            if (number % 2 == 0 || number % 3 == 0)
                return false;

            long tempPrime = 2;

            _cached6kLock.EnterReadLock();
            try
            {
                // First, iterate over existing primes
                foreach (var prime in _cached6k)
                {
                    if (prime == number)
                    {
                        return true; // The number is a prime and is already included
                    }

                    if (number % prime == 0)
                    {
                        return false; // The number is divisible, by another number except 1. Hence it is not a prime
                    }

                    if (prime * prime > number)
                    {
                        break; // The current prime is larger than max, we can stop checking
                    }

                    tempPrime = prime; // Set tempPrime to hold the value of the last iteration
                }
            }
            finally
            {
                _cached6kLock.ExitReadLock();
            }

            var a = Math.Max((tempPrime + 1) / 6, 1);
            for (long k = a; (6 * k - 1) * (6 * k - 1) <= number; k++)
            {
                long lvalue = 6 * k - 1;
                if (number % lvalue == 0)
                    return false;

                long rvalue = 6 * k + 1;
                if (number % rvalue == 0)
                    return false;
            }

            _cached6kLock.EnterWriteLock();
            try
            {
                if (!_cached6k.Contains(number))
                {
                    _cached6k.Add(number);
                }
            }
            finally
            {
                _cached6kLock.ExitWriteLock();
            }

            return true;
        }
    }
}



