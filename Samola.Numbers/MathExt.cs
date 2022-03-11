using System;
using System.Collections.Generic;
using System.Threading;

namespace Samola.Numbers
{
    public static partial class MathExt
    {
        public static bool IsPrime(int number)
        {
            return IsPrimeSimple6k(number);
        }

        /// <summary>
        /// Tests if a number is prime
        /// </summary>
        /// <param name="number">Number to test</param>
        public static bool IsPrimeBase(int number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be >= 1.");

            if (number <= 2)
                return true;

            if (number % 2 == 0)
                return false;

            double max = Math.Sqrt(number);
            for (int i = 3; i <= max; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsPrimeSimple(int number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be non-negative.");

            if (number <= 3)
                return true;

            if (number % 2 == 0 || number % 3 == 0)
                return false;

            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }

            return true;
        }



        private static readonly ReaderWriterLockSlim _cachedLock = new ReaderWriterLockSlim();
        private static HashSet<int> _cached = new HashSet<int>() { 2 };

        public static bool IsPrimeCached(int number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be >= 1.");

            if (number == 2 || number == 1)
                return true;

            if (number % 2 == 0)
                return false;

            // The value up to which we to check to determine if the number is prime
            double max = Math.Sqrt(number);

            int tempPrime = 2;

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

            for (int i = tempPrime; i <= max; i++)
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

        private static HashSet<int> _cachedNoLocks = new HashSet<int>() { 2 };

        public static bool IsPrimeCachedNoLocks(int number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be >= 1.");

            if (number == 2 || number == 1)
                return true;

            if (number % 2 == 0)
                return false;

            // The value up to which we to check to determine if the number is prime
            double max = Math.Sqrt(number);

            int tempPrime = 2;

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

            for (int i = tempPrime; i <= max; i++)
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
        public static bool IsPrimeSimple6k(int number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be non-negative.");

            if (number <= 3)
                return true;

            if (number % 2 == 0 || number % 3 == 0)
                return false;

            for (int k = 1; (6 * k - 1) * (6 * k - 1) <= number; k++)
            {
                int lvalue = 6 * k - 1;
                if (number % lvalue == 0)
                    return false;

                int rvalue = 6 * k + 1;
                if (number % rvalue == 0)
                    return false;
            }

            return true;
        }

        private static readonly ReaderWriterLockSlim _cached6kLock = new ReaderWriterLockSlim();
        private static HashSet<int> _cached6k = new HashSet<int>() { 2, 3 };

        /// <summary>
        /// Tests if a number is prime (employs the realization that all primes >= 5 can be represented as 6*k +- 1 where k > 0).
        /// </summary>
        /// <param name="number">Number to test</param>
        public static bool IsPrimeSimple6kCached(int number)
        {
            if (number < 1)
                throw new ArgumentException("Number must be non-negative.");

            if (number <= 3)
                return true;

            if (number % 2 == 0 || number % 3 == 0)
                return false;

            int tempPrime = 2;

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
            for (int k = a; (6 * k - 1) * (6 * k - 1) <= number; k++)
            {
                int lvalue = 6 * k - 1;
                if (number % lvalue == 0)
                    return false;

                int rvalue = 6 * k + 1;
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



